using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.TimeSeriesForecaster
{
    public class ForecasterUnivariate : IForecaster
    {

        // Fields
        private IForecastingStrategiesUnivariate _forecastingStrategies;
        private IRoundingStategies _roundingStrategies;
        private ISlidingWindowManager _slidingWindowManager;

        // Properties
        // Constructors
        public ForecasterUnivariate(
            IForecastingStrategiesUnivariate forecastingStrategies,
            IRoundingStategies roundingStrategies,
            ISlidingWindowManager slidingWindowManager
            )
        {

            if (forecastingStrategies == null)
                throw new ArgumentNullException(nameof(forecastingStrategies));
            if (roundingStrategies == null)
                throw new ArgumentNullException(nameof(roundingStrategies));
            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));

            _forecastingStrategies = forecastingStrategies;
            _roundingStrategies = roundingStrategies;
            _slidingWindowManager = slidingWindowManager;

        }
        public ForecasterUnivariate() 
            : this(
                  new ForecastingStrategiesUnivariate(), 
                  new RoundingStategies(),
                  new SlidingWindowManager()) { }
 
        // Methods (public)
        public List<ForecastedObservationUnivariate> Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception("The provided SlidingWindow object is not valid.");

            List<string> observationNames = new HashSet<string>(
                slidingWindow.TimeSeriesCollection.Select(Item => Item.ObservationName))
                .ToList();

            List<ForecastedObservationUnivariate> forecastedObservations = new List<ForecastedObservationUnivariate>();
            for (int i = 0; i < observationNames.Count; i++)
            {

                List<SlidingWindowTimeSeries> timeSeries = new List<SlidingWindowTimeSeries>();
                timeSeries.AddRange(slidingWindow.TimeSeriesCollection);
                timeSeries.RemoveAll(Item => Item.ObservationName != observationNames[i]);

                // The TagCollection is the same for a List<*TimeSeries> belonging to the same observation
                string strTagCollection =
                    (timeSeries
                    .Where(Item => Item.ObservationName == observationNames[i])
                    .First()).TagCollection;

                ForecastedObservationUnivariate forecastedObservation 
                    = Do(
                        observationNames[i],
                        slidingWindow.SlidingWindowId,
                        timeSeries,
                        strTagCollection);

                forecastedObservations.Add(forecastedObservation);

            };

            return forecastedObservations;

        }

        // Methods (private)
        private ForecastedObservationUnivariate Do
            (string observationName,
             string slidingWindowId,
             List<SlidingWindowTimeSeries> timeSeries,
             string tagCollection)
        {

            ForecastedObservationUnivariate forecastedObservation = new ForecastedObservationUnivariate();
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
