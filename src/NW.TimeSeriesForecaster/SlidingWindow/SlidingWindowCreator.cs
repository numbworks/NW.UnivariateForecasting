using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowCreator : ISlidingWindowCreator
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IValidator _validator;

        // Properties
        // Constructors
        public SlidingWindowCreator(
            UnivariateForecastingSettings settings,
            IValidator validator)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            _settings = settings;
            _validator = validator;

        }
        public SlidingWindowCreator(UnivariateForecastingSettings settings)
            : this(settings, new Validator()) { }

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
            slidingWindow.EndDate = CalculateNext(startDate, intervalUnit, (uint)values.Count);
            slidingWindow.TargetDate = CalculateNext(slidingWindow.EndDate, intervalUnit);
            slidingWindow.Interval = (uint)values.Count;
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
        public DateTime CalculateNext(DateTime date, IntervalUnits intervalUnit, uint steps)
        {

            if (steps < 1)
                throw new Exception(MessageCollection.StepsCantBeLessThanOne);

            if (intervalUnit == IntervalUnits.Months)
                return AddMonths(date, steps);

            throw new Exception(MessageCollection.NoStrategyToCalculateNextDateUnit.Invoke(intervalUnit.ToString()));

        }
        public DateTime CalculateNext(DateTime date, IntervalUnits intervalUnit)
            => CalculateNext(date, intervalUnit, 1);
        public SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation)
        {

            /*

                SlidingWindow:

                    [ Id: 'SW20200803063734', StartDate: '2019-01-31', EndDate: '2019-07-31', TargetDate: '2019-08-31', Interval: '6', IntervalUnit: 'Months', Items: '6', ObservationName: 'Some_Identifier' ]
                    
                newSlidingWindow:

                    [ Id: 'SW20200805011010', StartDate: '2019-01-31', EndDate: '2019-08-31', TargetDate: '2019-09-30', Interval: '7', IntervalUnit: 'Months', Items: '7', ObservationName: 'Some_Identifier' ]

             */

            if (!_validator.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (!_validator.IsValid(observation))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(Observation)));

            SlidingWindow newSlidingWindow = new SlidingWindow();

            newSlidingWindow.Id = _settings.IdCreationFunction.Invoke();
            newSlidingWindow.StartDate = slidingWindow.StartDate;

            uint steps = (uint)(slidingWindow.Interval / slidingWindow.Items.Count);
            newSlidingWindow.EndDate = CalculateNext(slidingWindow.EndDate, slidingWindow.IntervalUnit, steps);
            newSlidingWindow.TargetDate = CalculateNext(slidingWindow.TargetDate, slidingWindow.IntervalUnit, steps);
            newSlidingWindow.Interval = slidingWindow.Interval + 1;
            newSlidingWindow.IntervalUnit = slidingWindow.IntervalUnit;
            newSlidingWindow.Items = Combine(slidingWindow.Items, slidingWindow.IntervalUnit, steps, observation);
            newSlidingWindow.ObservationName = slidingWindow.ObservationName;

            return newSlidingWindow;

        }

        // Methods (private)
        private SlidingWindowItem CreateItem(
            int id, DateTime startDate, IntervalUnits intervalUnit, double firstValue, double? nextValue)
        {

            SlidingWindowItem item = new SlidingWindowItem();

            item.Id = id;
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
        private DateTime AddMonths(DateTime date, uint months)
        {

            /*
                2019-01-31
                2019-02-28
                2019-03-28 < Error, this should be 2019-03-31
             */

            DateTime nextDate = date.AddMonths((int)months);

            if (!IsEndOfTheMonth(date))
                return nextDate;

            return MoveToEndOfTheMonth(nextDate);

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
