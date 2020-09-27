using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IObservationManager _observationManager;
        private ISlidingWindowManager _slidingWindowManager;
        private ISlidingWindowItemManager _slidingWindowItemManager;
        private IIntervalManager _intervalManager;

        // Properties
        // Constructors
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            ISlidingWindowManager slidingWindowManager,
            ISlidingWindowItemManager slidingWindowItemManager,
            IObservationManager observationManager,
            IIntervalManager intervalManager)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));
            if (slidingWindowItemManager == null)
                throw new ArgumentNullException(nameof(slidingWindowItemManager));
            if (intervalManager == null)
                throw new ArgumentNullException(nameof(intervalManager));

            _settings = settings;
            _observationManager = observationManager;
            _slidingWindowManager = slidingWindowManager;
            _slidingWindowItemManager = slidingWindowItemManager;
            _intervalManager = intervalManager;

        }
        public UnivariateForecaster(UnivariateForecastingSettings settings)
            : this (
                  settings, 
                  new SlidingWindowManager(settings),
                  new SlidingWindowItemManager(),
                  new ObservationManager(settings), 
                  new IntervalManager()) { }

        // Methods (public)
        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>     
        /// </summary>
        public Observation Forecast(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return _observationManager.Create(slidingWindow);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow)
        {

            Observation observation = Forecast(slidingWindow);

            return Combine(slidingWindow, observation);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow, uint steps)
        {

            if (steps < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));

            SlidingWindow newSlidingWindow = slidingWindow;
            for (int i = 1; i <= steps; i++)
                newSlidingWindow = ForecastAndCombine(newSlidingWindow);

            return newSlidingWindow;

        }
        public SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (!_observationManager.IsValid(observation))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(Observation)));

            SlidingWindow newSlidingWindow = new SlidingWindow();
            newSlidingWindow.Id = _settings.IdCreationFunction.Invoke();
            newSlidingWindow.ObservationName = slidingWindow.ObservationName;

            uint steps = (uint)(slidingWindow.Interval.Size / slidingWindow.Items.Count);
            Interval interval = new Interval()
            {

                StartDate = slidingWindow.Interval.StartDate,
                EndDate = _intervalManager.CalculateNext(slidingWindow.Interval.EndDate, slidingWindow.Interval.Unit, steps),
                TargetDate = _intervalManager.CalculateNext(slidingWindow.Interval.TargetDate, slidingWindow.Interval.Unit, steps),
                Size = slidingWindow.Interval.Size + 1,
                Unit = slidingWindow.Interval.Unit,
                SubIntervals = slidingWindow.Interval.SubIntervals + 1

            };

            newSlidingWindow.Items = Combine(slidingWindow.Items, slidingWindow.Interval.Unit, steps, observation);

            return newSlidingWindow;

        }
        public List<double> ExtractXActualValues(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return slidingWindow.Items.Select(item => item.X_Actual).ToList();

        }
        public List<DateTime> ExtractStartDates(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return slidingWindow.Items.Select(item => item.Interval.StartDate).ToList();

        }

        // Methods (private)
        private SlidingWindowItem CreateItem(
            uint id, DateTime startDate, IntervalUnits intervalUnit, double firstValue, double? nextValue)
        {

            SlidingWindowItem item = new SlidingWindowItem();
            item.Id = id;

            Interval interval = new Interval()
            {

                StartDate = startDate,
                EndDate = _intervalManager.CalculateNext(startDate, intervalUnit, 1),
                TargetDate = _intervalManager.CalculateNext(startDate, intervalUnit, 2)

            };

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

            uint steps = (uint)(slidingWindow.Size / slidingWindow.Items.Count);
            newSlidingWindow.EndDate = CalculateNext(slidingWindow.EndDate, slidingWindow.Unit, steps);
            newSlidingWindow.TargetDate = CalculateNext(slidingWindow.TargetDate, slidingWindow.Unit, steps);
            newSlidingWindow.Size = slidingWindow.Size + 1;
            newSlidingWindow.Unit = slidingWindow.Unit;
            newSlidingWindow.Items = Combine(slidingWindow.Items, slidingWindow.Unit, steps, observation);
            newSlidingWindow.ObservationName = slidingWindow.ObservationName;

            return newSlidingWindow;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 27.09.2020

*/
