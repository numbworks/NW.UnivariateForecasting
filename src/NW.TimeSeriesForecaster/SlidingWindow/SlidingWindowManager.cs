using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowManager
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

            return new SlidingWindow()
            {

                Id = id,
                ObservationName = observationName,
                Interval = interval,
                Items = items

            };

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

            Interval interval = _intervalManager.Create((uint)values.Count, intervalUnit, startDate, steps);
            List<SlidingWindowItem> items = CreateItems(startDate, Round(values), intervalUnit);

            return Create(id, observationName, interval, items);

        }








        // Methods (private)
        private SlidingWindowItem CreateItem(
            int id, DateTime startDate, IntervalUnits intervalUnit, double firstValue, double? nextValue)
        {

            SlidingWindowItem item = new SlidingWindowItem();

            item.Id = id;
            item.StartDate = startDate;
            item.EndDate = _intervalManager.CalculateNext(startDate, intervalUnit);
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
                if (i == (values.Count - 1))
                    item = CreateItem((i + 1), current, intervalUnit, values[i], null);
                else
                    item = CreateItem((i + 1), current, intervalUnit, values[i], values[i + 1]);

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
        private List<SlidingWindowItem> Combine
            (List<SlidingWindowItem> slidingWindowItems,
            IntervalUnits intervalUnits,
            uint steps,
            Observation observation)
        {

            /*
             
                Items:

                    [ Id: '1', StartDate: '2019-01-31', EndDate: '2019-02-28', TargetDate: '2019-03-31', X_Actual: '58,5', Y_Forecasted: '615,26' ]
                    [ Id: '2', StartDate: '2019-02-28', EndDate: '2019-03-31', TargetDate: '2019-04-30', X_Actual: '615,26', Y_Forecasted: '659,84' ]
                    ...
                    [ Id: '6', StartDate: '2019-06-30', EndDate: '2019-07-31', TargetDate: '2019-08-31', X_Actual: '632,94', Y_Forecasted: '' ]
 
                Observation:

                    [ ..., Y_Forecasted: '519,23', ... ]

                newItems:

                    [ Id: '1', StartDate: '2019-01-31', EndDate: '2019-02-28', TargetDate: '2019-03-31', X_Actual: '58,5', Y_Forecasted: '615,26' ]
                    [ Id: '2', StartDate: '2019-02-28', EndDate: '2019-03-31', TargetDate: '2019-04-30', X_Actual: '615,26', Y_Forecasted: '659,84' ]
                    ...
                    => [ Id: '6', StartDate: '2019-06-30', EndDate: '2019-07-31', TargetDate: '2019-08-31', X_Actual: '632,94', Y_Forecasted: '519,23' ]
                    => [ Id: '7', StartDate: '2019-07-31', EndDate: '2019-08-31', TargetDate: '2019-09-30', X_Actual: '519,23', Y_Forecasted: '' ]

             */

            List<SlidingWindowItem> newItems = new List<SlidingWindowItem>();
            newItems.AddRange(slidingWindowItems);

            SlidingWindowItem oldLastItem = newItems.OrderBy(item => item.Id).Last();
            newItems.Remove(oldLastItem);
            oldLastItem.Y_Forecasted = observation.Y_Forecasted;
            newItems.Add(oldLastItem);

            SlidingWindowItem newLastItem = new SlidingWindowItem();
            newLastItem.Id = oldLastItem.Id + 1;
            newLastItem.StartDate = CalculateNext(oldLastItem.StartDate, intervalUnits, steps);
            newLastItem.EndDate = CalculateNext(oldLastItem.EndDate, intervalUnits, steps);
            newLastItem.TargetDate = CalculateNext(oldLastItem.TargetDate, intervalUnits, steps);
            newLastItem.X_Actual = observation.Y_Forecasted;
            newLastItem.Y_Forecasted = null;
            newItems.Add(newLastItem);

            return newItems;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
