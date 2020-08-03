using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowCreator : ISlidingWindowCreator
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        
        // Properties
        // Constructors
        public SlidingWindowCreator(UnivariateForecastingSettings settings)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _settings = settings;

        }

        // Methods (public)
        public SlidingWindow CreateSlidingWindow
            (string id, DateTime startDate, List<double> values, IntervalUnits intervalUnit, string observationName)
        {

            if (string.IsNullOrWhiteSpace(id))
                throw new Exception(MessageCollection.StringCantBeEmptyOrNull.Invoke(nameof(id)));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Count == 0)
                throw new ArgumentNullException(nameof(values));
            if (string.IsNullOrWhiteSpace(observationName))
                throw new Exception(MessageCollection.StringCantBeEmptyOrNull.Invoke(nameof(observationName)));

            SlidingWindow slidingWindow = new SlidingWindow();

            slidingWindow.Id = id;
            slidingWindow.StartDate = startDate;
            slidingWindow.EndDate = CalculateNext(startDate, intervalUnit, values.Count);
            slidingWindow.TargetDate = CalculateNext(slidingWindow.EndDate, intervalUnit);
            slidingWindow.Interval = values.Count;
            slidingWindow.IntervalUnit = intervalUnit;
            slidingWindow.Items = CreateItems(startDate, Round(values), intervalUnit);
            slidingWindow.ObservationName = observationName;

            return slidingWindow;

        }
        public SlidingWindow CreateSlidingWindow
            (DateTime startDate, List<double> values, IntervalUnits intervalUnit, string observationName)
                => CreateSlidingWindow(
                        _settings.IdCreationFunction.Invoke(),
                        startDate,
                        values,
                        intervalUnit,
                        observationName);
        public DateTime CalculateNext(DateTime date, IntervalUnits intervalUnit, int steps)
        {

            if (steps < 1)
                throw new Exception(MessageCollection.StepsCantBeLessThanOne);

            if (intervalUnit == IntervalUnits.Months)
                return AddMonths(date, steps);

            throw new Exception(MessageCollection.NoStrategyToCalculateNextDateUnit.Invoke(intervalUnit.ToString()));

        }
        public DateTime CalculateNext(DateTime date, IntervalUnits intervalUnit)
            => CalculateNext(date, intervalUnit, 1);

        // Methods (private)
        private SlidingWindowItem CreateItem(
            int id, DateTime startDate, IntervalUnits intervalUnit, double firstValue, double? nextValue)
        {

            SlidingWindowItem item = new SlidingWindowItem();

            item.StartDate = startDate;
            item.EndDate = CalculateNext(startDate, intervalUnit);
            item.TargetDate = CalculateNext(item.EndDate, intervalUnit);
            item.X_Actual = firstValue;
            item.Y_Forecasted = nextValue;

            return item;

        }
        private List<SlidingWindowItem> CreateItems(DateTime startDate, List<double> values, IntervalUnits intervalUnit)
        {

            if (intervalUnit == IntervalUnits.Months)
                return CreateItemsIfMonths(startDate, values);

            throw new Exception(MessageCollection.NoStrategyToCreateItemsUnit.Invoke(intervalUnit.ToString()));

        }
        private List<SlidingWindowItem> CreateItemsIfMonths(DateTime startDate, List<double> values)
        {

            /*
                
                startDate = '2019-01-31'
                values = [58.50, 615.26, 659.84, 635.69, 612.27, 632.94]

                List<SlidingWindowItem>

	                - SlidingWindowItem
		                - Id		    1
		                - StartDate	    2019-01-31	
		                - EndDate	    2019-02-28
		                - TargetDate	2019-03-31
		                - X_Actual	    58.50
		                - Y_Forecasted	615.26

	                - SlidingWindowItem
		                - Id		    2
		                - StartDate	    2019-02-28	
		                - EndDate	    2019-03-31
		                - TargetDate	2019-04-30
		                - X_Actual	    615.26
		                - Y_Forecasted	659.84

	                - SlidingWindowItem
		                - Id		    3
		                - StartDate	    2019-02-28	
		                - EndDate	    2019-03-31
		                - TargetDate	2019-04-30
		                - X_Actual	    659.84
		                - Y_Forecasted	635.69

	                - SlidingWindowItem
		                - Id		    4
		                - StartDate	    2019-03-31	
		                - EndDate	    2019-04-30
		                - TargetDate	2019-05-31
		                - X_Actual	    635.69
		                - Y_Forecasted	612.27

	                - SlidingWindowItem
		                - Id		    5
		                - StartDate	    2019-04-30	
		                - EndDate	    2019-05-31
		                - TargetDate	2019-06-30
		                - X_Actual	    612.27
		                - Y_Forecasted	632.94

	                - SlidingWindowItem
		                - Id		    6
		                - StartDate	    2019-05-31	
		                - EndDate	    2019-06-30
		                - TargetDate	2019-07-31
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
                if (i == (values.Count-1))
                    item = CreateItem((i+1), current, intervalUnit, values[i], null);
                else
                    item = CreateItem((i+1), current, intervalUnit, values[i], values[i+1]);

                items.Add(item);
                current = CalculateNext(current, intervalUnit);

            }

            return items;

        }
        private List<double> Round(List<double> values)
        {

            /*
                934.5322222 => 934.53
                978.5600101 => 978.56
                ...
             */

            return values.Select(item => _settings.RoundingFunction.Invoke(item)).ToList();

        }
        private bool IsEndOfTheMonth(DateTime dt)
            => dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month);
        private DateTime MoveToEndOfTheMonth(DateTime date)
            => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        private DateTime AddMonths(DateTime date, int months)
        {

            /*
                2019-01-31
                2019-02-28
                2019-03-28 < Error, this should be 2019-03-31
             */

            DateTime nextDate = date.AddMonths(months);

            if (!IsEndOfTheMonth(date))
                return nextDate;

            return MoveToEndOfTheMonth(nextDate);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
