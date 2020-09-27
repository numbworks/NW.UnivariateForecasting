using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
    /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
    /// which consists of observations on only a single characteristic or attribute.".</para>     
    /// </summary>
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
            if (observationManager == null)
                throw new ArgumentNullException(nameof(observationManager));
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
        public Observation Forecast(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return _observationManager.Create(slidingWindow);

        }
        public List<double> ExtractXActualValues(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            _settings.LoggingAction.Invoke(MessageCollection.ExtractingValuesOutOfProvidedSlidingWindow.Invoke(slidingWindow));

            List<double> values = slidingWindow.Items.Select(item => item.X_Actual).ToList();
            _settings.LoggingAction.Invoke(MessageCollection.ValuesHaveBeenSuccessfullyExtracted.Invoke(values));

            return values;

        }
        public List<DateTime> ExtractStartDates(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            _settings.LoggingAction.Invoke(MessageCollection.ExtractingStartDatesOutOfProvidedSlidingWindow.Invoke(slidingWindow));

            List<DateTime> startDates = slidingWindow.Items.Select(item => item.Interval.StartDate).ToList();
            _settings.LoggingAction.Invoke(MessageCollection.StartDatesHaveBeenSuccessfullyExtracted.Invoke(startDates));

            return startDates;

        }
        public SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation)
        {

            /*

                SlidingWindow:

                    [ Id: 'SW20200803063734', ObservationName: 'Some_Identifier', Interval: '6:Months:20190131:20190731:20190831:1:6', Items: '6' ]          

                newSlidingWindow:

                    [ Id: 'SW20200803063734', ObservationName: 'Some_Identifier', Interval: '7:Months:20190131:20190831:20190930:1:7', Items: '7' ]

             */

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (!_observationManager.IsValid(observation))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(Observation)));

            _settings.LoggingAction.Invoke(MessageCollection.CombiningProvidedSlidingWindowWithObservation);
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedSlidingWindowIs.Invoke(slidingWindow));
            _settings.LoggingAction.Invoke(MessageCollection.ProvidedObservationIs.Invoke(observation));

            uint steps = (uint)(slidingWindow.Interval.Size / slidingWindow.Items.Count);
            SlidingWindow newSlidingWindow = new SlidingWindow()
            {

                Id = _settings.IdCreationFunction.Invoke(),
                ObservationName = slidingWindow.ObservationName,
                Interval = _intervalManager.Create(
                                                    slidingWindow.Interval.Size + 1,
                                                    slidingWindow.Interval.Unit,
                                                    slidingWindow.Interval.StartDate,
                                                    steps),
                Items = CombineItems(slidingWindow.Items, slidingWindow.Interval.Unit, steps, observation)


            };

            _settings.LoggingAction.Invoke(MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(newSlidingWindow));

            return newSlidingWindow;

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow, uint steps)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (steps < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));

            _settings.LoggingAction.Invoke(MessageCollection.RunningForecastAndCombineForSteps.Invoke(steps));

            SlidingWindow newSlidingWindow = slidingWindow;
            for (uint i = 1; i <= steps; i++)
            {

                _settings.LoggingAction.Invoke(MessageCollection.ForecastingAndCombineForStepNr.Invoke(steps));

                Observation observation = Forecast(slidingWindow);
                newSlidingWindow = Combine(slidingWindow, observation);

            };

            _settings.LoggingAction.Invoke(MessageCollection.ForecastAndCombineSuccessfullyRunForSteps.Invoke(steps));
            _settings.LoggingAction.Invoke(MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(newSlidingWindow));

            return newSlidingWindow;

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow)
            => ForecastAndCombine(slidingWindow, 1);

        // Methods (private)
        private List<SlidingWindowItem> CombineItems(
            List<SlidingWindowItem> slidingWindowItems, IntervalUnits intervalUnits, uint steps, Observation observation)
        {

            /*
             
                Items:

                    [ Id: '1', Interval: '2019-01-31:2019-02-28:2019-03-31', X_Actual: '58,5', Y_Forecasted: '615,26' ]
                    [ Id: '2', Interval: '2019-02-28:2019-03-31:2019-04-30', X_Actual: '615,26', Y_Forecasted: '659,84' ]
                    ...                  
                    [ Id: '6', Interval: '2019-06-30:2019-07-31:2019-08-31', X_Actual: '632,94', Y_Forecasted: '' ]
 
                Observation:

                    [ ..., Y_Forecasted: '519,23', ... ]

                newItems:

                    [ Id: '1', Interval: '2019-01-31:2019-02-28:2019-03-31', X_Actual: '58,5', Y_Forecasted: '615,26' ]
                    [ Id: '2', Interval: '2019-02-28:2019-03-31:2019-04-30', X_Actual: '615,26', Y_Forecasted: '659,84' ]
                    ...
                    => [ Id: '6', Interval: '2019-06-30:2019-07-31:2019-08-31', X_Actual: '632,94', Y_Forecasted: '519,23' ]
                    => [ Id: '7', Interval: '2019-07-31:2019-08-31:2019-09-30', X_Actual: '519,23', Y_Forecasted: '' ]

             */

            List<SlidingWindowItem> newItems = 
                slidingWindowItems.ConvertAll(item => new SlidingWindowItem()
                                                            {
                                                                Id = item.Id,
                                                                Interval = item.Interval,
                                                                X_Actual = item.X_Actual,
                                                                Y_Forecasted = item.Y_Forecasted
                                                            });

            SlidingWindowItem oldLastItem = newItems.OrderBy(item => item.Id).Last();
            newItems.Remove(oldLastItem);
            oldLastItem.Y_Forecasted = observation.Y_Forecasted;
            newItems.Add(oldLastItem);

            SlidingWindowItem newLastItem = 
                _slidingWindowItemManager.CreateItem(
                                            oldLastItem.Id + 1,
                                            _intervalManager.CalculateNext(oldLastItem.Interval.StartDate, intervalUnits, steps),
                                            intervalUnits,
                                            observation.Y_Forecasted,
                                            null);
            newItems.Add(newLastItem);

            return newItems;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 27.09.2020

*/
