using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <inheritdoc cref="ISlidingWindowItemManager"/>
    public class SlidingWindowItemManager : ISlidingWindowItemManager
    {

        #region Fields

        private IIntervalManager _intervalManager;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="SlidingWindowItemManager"/>.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public SlidingWindowItemManager(IIntervalManager intervalManager)
        {

            Validator.ValidateObject(intervalManager, nameof(intervalManager));

            _intervalManager = intervalManager;

        }

        /// <summary>Initializes an instance of <see cref="SlidingWindowItemManager"/> using default values.</summary>
        public SlidingWindowItemManager()
            : this(new IntervalManager()) { }

        #endregion

        #region Methods_public

        public SlidingWindowItem CreateItem(uint id, Interval interval, double X_Actual, double? Y_Forecasted)
        {

            if (!_intervalManager.IsValid(interval))
                throw new ArgumentException(Intervals.MessageCollection.IntervalNullOrInvalid);

            return new SlidingWindowItem()
            {
                Id = id,
                Interval = interval,
                X_Actual = X_Actual,
                Y_Forecasted = Y_Forecasted

            };

        }
        public SlidingWindowItem CreateItem(uint id, DateTime startDate, IntervalUnits intervalUnit, double X_Actual, double? Y_Forecasted)
        {

            if (intervalUnit != IntervalUnits.Months)
                throw new ArgumentException(Validation.MessageCollection.ProvidedIntervalUnitNotSupported(intervalUnit.ToString()));

            Interval interval = new Interval()
            {

                Size = 1,
                Unit = intervalUnit,
                StartDate = startDate,
                EndDate = _intervalManager.CalculateNext(startDate, intervalUnit, 1),
                TargetDate = _intervalManager.CalculateNext(startDate, intervalUnit, 2),
                Steps = 1,
                SubIntervals = 1

            };

            return CreateItem(id, interval, X_Actual, Y_Forecasted);

        }
        public List<SlidingWindowItem> CreateItems(DateTime startDate, List<double> values, IntervalUnits intervalUnit)
        {

            Validator.ValidateList(values, nameof(values));
            Validator.ValidateIntervalUnit(intervalUnit);

            return CreateItemsIfMonths(startDate, values);

        }

        public bool IsValid(SlidingWindowItem slidingWindowItem)
        {

            if (slidingWindowItem == null)
                return false;
            if (!_intervalManager.IsValid(slidingWindowItem.Interval))
                return false;

            return true;

        }

        #endregion

        #region Methods_private

        private List<SlidingWindowItem> CreateItemsIfMonths(DateTime startDate, List<double> values)
        {

            /*
                
                startDate = '2019-01-31'
                values = [58.50, 615.26, 659.84, 635.69, 612.27, 632.94]

                List<SlidingWindowItem>

	                - SlidingWindowItem
		                - Id		    1
                        - Interval      2019-01-31:2019-02-28:2019-03-31
		                - X_Actual	    58.50
		                - Y_Forecasted	615.26

	                - SlidingWindowItem
		                - Id		    2
                        - Interval      2019-02-28:2019-03-31:2019-04-30
		                - X_Actual	    615.26
		                - Y_Forecasted	659.84

	                - SlidingWindowItem
		                - Id		    3
                        - Interval      2019-02-28:2019-03-31:2019-04-30
		                - X_Actual	    659.84
		                - Y_Forecasted	635.69

	                - SlidingWindowItem
		                - Id		    4
                        - Interval      2019-03-31:2019-04-30:2019-05-31
		                - X_Actual	    635.69
		                - Y_Forecasted	612.27

	                - SlidingWindowItem
		                - Id		    5
                        - Interval      2019-04-30:2019-05-31:2019-06-30
		                - X_Actual	    612.27
		                - Y_Forecasted	632.94

	                - SlidingWindowItem
		                - Id		    6
                        - Interval      2019-05-31:2019-06-30:2019-07-31
		                - X_Actual	    632.94
		                - Y_Forecasted	null

                The item.Id should start from '1' and not from '0'.

             */

            IntervalUnits intervalUnit = IntervalUnits.Months;
            DateTime current = startDate;
            List<SlidingWindowItem> items = new List<SlidingWindowItem>();

            for (int i = 0; i < values.Count; i++)
            {

                SlidingWindowItem item = null;
                if (i == (values.Count - 1))
                    item = CreateItem((uint)(i + 1), current, intervalUnit, values[i], null);
                else
                    item = CreateItem((uint)(i + 1), current, intervalUnit, values[i], values[i + 1]);

                items.Add(item);
                current = _intervalManager.CalculateNext(current, intervalUnit, 1);

            }

            return items;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/
