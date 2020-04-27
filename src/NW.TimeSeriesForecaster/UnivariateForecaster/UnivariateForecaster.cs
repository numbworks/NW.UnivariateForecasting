using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.TimeSeriesForecaster
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private IUnivariateValuesCalculator _forecastingStrategies;
        private IRoundingStategies _roundingStrategies;
        private ISlidingWindowManager _slidingWindowManager;

        // Properties
        // Constructors
        public UnivariateForecaster(
            IUnivariateValuesCalculator valuesCalculator,
            IRoundingStategies roundingStrategies,
            ISlidingWindowManager slidingWindowManager
            )
        {

            if (valuesCalculator == null)
                throw new ArgumentNullException(nameof(valuesCalculator));
            if (roundingStrategies == null)
                throw new ArgumentNullException(nameof(roundingStrategies));
            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));

            _forecastingStrategies = valuesCalculator;
            _roundingStrategies = roundingStrategies;
            _slidingWindowManager = slidingWindowManager;

        }
        public UnivariateForecaster() 
            : this(
                  new UnivariateValuesCalculator(), 
                  new RoundingStategies(),
                  new SlidingWindowManager()) { }
 
        // Methods (public)
        public List<UnivariateForecastedObservation> Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception("The provided SlidingWindow object is not valid.");

            List<string> observationNames = new HashSet<string>(
                slidingWindow.TimeSeriesCollection.Select(Item => Item.ObservationName))
                .ToList();

            List<UnivariateForecastedObservation> forecastedObservations = new List<UnivariateForecastedObservation>();
            for (int i = 0; i < observationNames.Count; i++)
            {

                List<SlidingWindowTimeSeries> timeSeriesList = new List<SlidingWindowTimeSeries>();
                timeSeriesList.AddRange(slidingWindow.TimeSeriesCollection);
                timeSeriesList.RemoveAll(Item => Item.ObservationName != observationNames[i]);

                // The TagCollection is the same for a List<*TimeSeries> belonging to the same observation
                string tagCollection =
                    (timeSeriesList
                    .Where(Item => Item.ObservationName == observationNames[i])
                    .First()).TagCollection;

                UnivariateForecastedObservation forecastedObservation 
                    = Do(
                        observationNames[i],
                        slidingWindow.SlidingWindowId,
                        timeSeriesList,
                        tagCollection);

                forecastedObservations.Add(forecastedObservation);

            };

            return forecastedObservations;

        }

        // Methods (private)
        private UnivariateForecastedObservation Do
            (string observationName,
             string slidingWindowId,
             List<SlidingWindowTimeSeries> timeSeries,
             string tagCollection)
        {

            UnivariateForecastedObservation forecastedObservation = new UnivariateForecastedObservation();
            forecastedObservation.ObservationName = observationName;
            forecastedObservation.SlidingWindowId = slidingWindowId;
            forecastedObservation.TagCollection = tagCollection;

            _forecastingStrategies.CalculateValues
                (timeSeries, ref forecastedObservation, _roundingStrategies.GetTwoDecimalDigitStrategy());

            return forecastedObservation;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/
