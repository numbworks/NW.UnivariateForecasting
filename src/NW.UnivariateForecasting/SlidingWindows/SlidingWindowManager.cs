using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <inheritdoc cref="ISlidingWindowManager"/>
    public class SlidingWindowManager : ISlidingWindowManager
    {

        #region Fields

        private Func<double, uint, double> _roundingFunction;
        private Action<string> _loggingAction;
        private uint _roundingDigits;

        #endregion

        #region Properties

        public static Func<double, uint, double> DefaultRoundingFunction { get; }
            = UnivariateForecastingComponents.DefaultRoundingFunction;
        public static Action<string> DefaultLoggingAction { get; }
            = UnivariateForecastingComponents.DefaultLoggingAction;
        public static uint DefaultRoundingDigits { get; }
            = UnivariateForecastingSettings.DefaultRoundingDigits;

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="SlidingWindowManager"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public SlidingWindowManager(Func<double, uint, double> roundingFunction, Action<string> loggingAction, uint roundingDigits)
        {

            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));
            Validator.ThrowIfFirstIsGreater((int)roundingDigits, nameof(roundingDigits), (int)DefaultRoundingDigits, nameof(DefaultRoundingDigits));

            _roundingFunction = roundingFunction;
            _loggingAction = loggingAction;
            _roundingDigits = roundingDigits;

        }

        /// <summary>Initializes an instance of <see cref="SlidingWindowManager"/> using default values.</summary>
        public SlidingWindowManager()
            : this(
                  DefaultRoundingFunction,
                  DefaultLoggingAction,
                  DefaultRoundingDigits
                  ) { }

        #endregion

        #region Methods_public

        public SlidingWindow Create(List<double> values)
        {

            Validator.ValidateList(values, nameof(values));

            _loggingAction(MessageCollection.CreatingSlidingWindowOutOfFollowingArguments);
            _loggingAction(MessageCollection.ProvidedValuesAre(values));

            List<SlidingWindowItem> items = CreateItems(Round(values));
            SlidingWindow slidingWindow = new SlidingWindow(items);

            _loggingAction(MessageCollection.FollowingSlidingWindowHasBeenCreated(slidingWindow));

            return slidingWindow;

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

            return values.Select(item => _roundingFunction(item, _roundingDigits)).ToList();

        }
        private List<SlidingWindowItem> CreateItems(List<double> values)
        {

            /*

                values = [58.50, 615.26, 659.84, 635.69, 612.27, 632.94]

                List<SlidingWindowItem>

                    - SlidingWindowItem
                        - Id		    1
                        - X_Actual	    58.50
                        - Y_Forecasted	615.26

                    - SlidingWindowItem
                        - Id		    2
                        - X_Actual	    615.26
                        - Y_Forecasted	659.84

                    - SlidingWindowItem
                        - Id		    3
                        - X_Actual	    659.84
                        - Y_Forecasted	635.69

                    - SlidingWindowItem
                        - Id		    4
                        - X_Actual	    635.69
                        - Y_Forecasted	612.27

                    - SlidingWindowItem
                        - Id		    5
                        - X_Actual	    612.27
                        - Y_Forecasted	632.94

                    - SlidingWindowItem
                        - Id		    6
                        - X_Actual	    632.94
                        - Y_Forecasted	null

                The item.Id should start from '1' and not from '0'.

            */

            List<SlidingWindowItem> items = new List<SlidingWindowItem>();
            for (int i = 0; i < values.Count; i++)
            {

                SlidingWindowItem item = null;
                if (i == (values.Count - 1))
                    item = new SlidingWindowItem(id: (uint)(i + 1), X_Actual: values[i], Y_Forecasted: null);
                else
                    item = new SlidingWindowItem(id: (uint)(i + 1), X_Actual: values[i], Y_Forecasted: values[i + 1]);

                items.Add(item);

            }

            return items;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/