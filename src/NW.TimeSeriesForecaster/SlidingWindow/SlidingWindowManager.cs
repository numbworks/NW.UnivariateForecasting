﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowManager : ISlidingWindowManager
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IIntervalManager _intervalManager;
        private ISlidingWindowItemManager _slidingWindowItemManager;

        // Properties
        // Constructors
        public SlidingWindowManager(
            UnivariateForecastingSettings settings,
            IIntervalManager intervalManager,
            ISlidingWindowItemManager slidingWindowItemManager)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (intervalManager == null)
                throw new ArgumentNullException(nameof(intervalManager));
            if (slidingWindowItemManager == null)
                throw new ArgumentNullException(nameof(slidingWindowItemManager));

            _settings = settings;
            _intervalManager = intervalManager;
            _slidingWindowItemManager = slidingWindowItemManager;

        }
        public SlidingWindowManager(UnivariateForecastingSettings settings)
            : this(settings, new IntervalManager(), new SlidingWindowItemManager()) { }

        // Methods (public)
        public SlidingWindow Create(
            string id, 
            string observationName, 
            Interval interval, 
            List<SlidingWindowItem> items)
        {

            if (string.IsNullOrWhiteSpace(id))
                throw new Exception(MessageCollection.VariableCantBeEmptyOrNull.Invoke(nameof(id)));
            if (string.IsNullOrWhiteSpace(observationName))
                throw new Exception(MessageCollection.VariableCantBeEmptyOrNull.Invoke(nameof(observationName)));
            if (!_intervalManager.IsValid(interval))
                throw new Exception(MessageCollection.IntervalNullOrInvalid);
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items.Count == 0)
                throw new Exception(MessageCollection.VariableContainsZeroItems.Invoke(nameof(items)));
            if (items.Count != interval.SubIntervals)
                throw new Exception(MessageCollection.ItemsDontMatchSubintervals.Invoke(items.Count, interval));

            _settings.LoggingAction.Invoke(MessageCollection.CreatingSlidingWindowOutOfFollowingArguments);
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedIdIs.Invoke(id));
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedObservationNameIs.Invoke(observationName));
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedIntervalIs.Invoke(interval));
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedItemsCountIs.Invoke(items));

            SlidingWindow slidingWindow = new SlidingWindow()
            {

                Id = id,
                ObservationName = observationName,
                Interval = interval,
                Items = items

            };

            _settings.LoggingAction.Invoke(MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(slidingWindow));

            return slidingWindow;

        }
        public SlidingWindow Create
            (string id,
            string observationName,
            List<double> values,
            uint steps,
            IntervalUnits intervalUnit,
            DateTime startDate)
        {

            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Count == 0)
                throw new Exception(MessageCollection.VariableContainsZeroItems.Invoke(nameof(values)));

            _settings.LoggingAction.Invoke(MessageCollection.CreatingIntervalOutOfFollowingArguments);
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedValuesAre.Invoke(values));
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedStepsAre.Invoke(steps));
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedIntervalUnitsIs.Invoke(intervalUnit));

            Interval interval = _intervalManager.Create((uint)values.Count, intervalUnit, startDate, steps);
            List<SlidingWindowItem> items = CreateItems(interval, Round(values));

            return Create(id, observationName, interval, items);

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

        // Methods (private)
        private List<double> Round(List<double> values)
        {

            /*
                934.5322222 => 934.53
                978.5600101 => 978.56
                ...
             */

            return values.Select(item => _settings.RoundingFunction.Invoke(item)).ToList();

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

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.09.2020

*/
