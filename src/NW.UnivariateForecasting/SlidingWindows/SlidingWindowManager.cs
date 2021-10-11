using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Messages;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <inheritdoc cref="ISlidingWindowManager"/>
    public class SlidingWindowManager : ISlidingWindowManager
    {

        #region Fields

        private UnivariateForecastingSettings _settings;
        private IIntervalManager _intervalManager;
        private ISlidingWindowItemManager _slidingWindowItemManager;
        private Func<double, double> _roundingFunction;
        private Action<string> _loggingAction;

        #endregion

        #region Properties

        public static Func<double, double> DefaultRoundingFunction { get; }
            = UnivariateForecastingComponents.DefaultRoundingFunction;
        public static Action<string> DefaultLoggingAction { get; }
            = UnivariateForecastingComponents.DefaultLoggingAction;

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="SlidingWindowManager"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SlidingWindowManager(
            UnivariateForecastingSettings settings,
            IIntervalManager intervalManager,
            ISlidingWindowItemManager slidingWindowItemManager,
            Func<double, double> roundingFunction,
            Action<string> loggingAction)
        {

            Validator.ValidateObject(settings, nameof(settings));
            Validator.ValidateObject(intervalManager, nameof(intervalManager));
            Validator.ValidateObject(slidingWindowItemManager, nameof(slidingWindowItemManager));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));

            _settings = settings;
            _intervalManager = intervalManager;
            _slidingWindowItemManager = slidingWindowItemManager;
            _roundingFunction = roundingFunction;
            _loggingAction = loggingAction;

        }

        /// <summary>Initializes an instance of <see cref="SlidingWindowManager"/> using default values.</summary>
        public SlidingWindowManager()
            : this(
                  new UnivariateForecastingSettings(),
                  new IntervalManager(),
                  new SlidingWindowItemManager(),
                  DefaultRoundingFunction,
                  DefaultLoggingAction
                  ) { }

        #endregion

        #region Methods_public

        public SlidingWindow Create(string id, string observationName, Interval interval, List<SlidingWindowItem> items)
        {

            Validator.ValidateStringNullOrWhiteSpace(id, nameof(id));
            Validator.ValidateStringNullOrWhiteSpace(observationName, nameof(observationName));
            if (!_intervalManager.IsValid(interval))
                throw new ArgumentException(MessageCollection.IntervalManager_IntervalNullOrInvalid);
            Validator.ValidateList(items, nameof(items));
            if (items.Count != interval.SubIntervals)
                throw new ArgumentException(MessageCollection.IntervalManager_ItemsDontMatchSubintervals.Invoke(items.Count, interval));

            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_CreatingSlidingWindowOutOfFollowingArguments);
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedIdIs.Invoke(id));
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedObservationNameIs.Invoke(observationName));
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedIntervalIs.Invoke(interval));
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedItemsCountIs.Invoke(items));

            SlidingWindow slidingWindow = new SlidingWindow()
            {

                Id = id,
                ObservationName = observationName,
                Interval = interval,
                Items = items

            };

            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(slidingWindow));

            return slidingWindow;

        }
        public SlidingWindow Create(string id, string observationName, List<double> values, uint steps, IntervalUnits intervalUnit, DateTime startDate)
        {

            Validator.ValidateList(values, nameof(values));

            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_CreatingIntervalOutOfFollowingArguments);
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedValuesAre.Invoke(values));
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedStepsAre.Invoke(steps));
            _loggingAction.Invoke(MessageCollection.SlidingWindowManager_ProvidedIntervalUnitsIs.Invoke(intervalUnit));

            Interval interval = _intervalManager.Create((uint)values.Count, intervalUnit, startDate, steps);
            List<SlidingWindowItem> items = CreateItems(interval, Round(values));

            return Create(id, observationName, interval, items);

        }
        public SlidingWindow Create(List<double> values)
        {

            return Create(
                    _settings.DummyId,
                    _settings.DummyObservationName,
                    values,
                    _settings.DummySteps,
                    _settings.DummyIntervalUnit,
                    _settings.DummyStartDate
                );

        }

        public bool IsValid(SlidingWindow slidingWindow)
        {

            if (slidingWindow == null)
                return false;
            if (string.IsNullOrWhiteSpace(slidingWindow.Id))
                return false;
            if (string.IsNullOrWhiteSpace(slidingWindow.ObservationName))
                return false;
            if (_intervalManager.IsValid(slidingWindow.Interval) == false)
                return false;
            if (slidingWindow.Items == null)
                return false;
            if (slidingWindow.Items.Count < 1)
                return false;
            if (slidingWindow.Items.Count != slidingWindow.Interval.SubIntervals)
                return false;

            return true;

        }

        #endregion

        #region Methods_private

        private List<double> Round(List<double> values)
        {

            /*
                934.5322222 => 934.53
                978.5600101 => 978.56
                ...
             */

            return values.Select(item => _roundingFunction.Invoke(item)).ToList();

        }
        private List<SlidingWindowItem> CreateItems(Interval interval, List<double> values)
        {

            List<Interval> subIntervals = _intervalManager.CalculateSubIntervals(interval);

            List<SlidingWindowItem> slidingWindowItems = new List<SlidingWindowItem>();
            for (int i = 0; i < subIntervals.Count; i++)
            {

                uint id = (uint)i + 1; // id starts from "1"
                Interval subInterval = subIntervals[i];
                double X_Actual = values[i];

                double? Y_Forecasted = null;
                if (i != subIntervals.Count - 1)
                    Y_Forecasted = values[i + 1]; // only the "before last" is null

                SlidingWindowItem slidingWindowItem
                    = _slidingWindowItemManager.CreateItem(id, subInterval, X_Actual, Y_Forecasted);

                slidingWindowItems.Add(slidingWindowItem);

            };

            return slidingWindowItems;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/
