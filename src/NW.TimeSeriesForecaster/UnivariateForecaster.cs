using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private IObservationManager _forecastingStrategies;
        private IRoundingStategies _roundingStrategies;
        private ISlidingWindowManager _slidingWindowManager;

        // Properties
        // Constructors
        public UnivariateForecaster(
            IObservationManager valuesCalculator,
            IRoundingStategies roundingStrategies,
            ISlidingWindowManager slidingWindowManager)
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
                  new ObservationManager(), 
                  new RoundingStategies(),
                  new SlidingWindowManager()) { }
 
        // Methods (public)
        public List<Observation> Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception("The provided SlidingWindow object is not valid.");

            List<string> observationNames = new HashSet<string>(
                slidingWindow.Items.Select(Item => Item.ObservationName))
                .ToList();

            List<Observation> forecastedObservations = new List<Observation>();
            for (int i = 0; i < observationNames.Count; i++)
            {

                List<SlidingWindowItem> timeSeriesList = new List<SlidingWindowItem>();
                timeSeriesList.AddRange(slidingWindow.Items);
                timeSeriesList.RemoveAll(Item => Item.ObservationName != observationNames[i]);

                // The TagCollection is the same for a List<*TimeSeries> belonging to the same observation
                string tagCollection =
                    (timeSeriesList
                    .Where(Item => Item.ObservationName == observationNames[i])
                    .First()).TagCollection;

                Observation forecastedObservation 
                    = Do(
                        observationNames[i],
                        slidingWindow.Id,
                        timeSeriesList,
                        tagCollection);

                forecastedObservations.Add(forecastedObservation);

            };

            return forecastedObservations;

        }

        // Methods (private)
        private Observation Do
            (string observationName,
             string slidingWindowId,
             List<SlidingWindowItem> timeSeries,
             string tagCollection)
        {

            Observation forecastedObservation = new Observation();
            forecastedObservation.Name = observationName;
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
    Last Update: 28.04.2020

*/
