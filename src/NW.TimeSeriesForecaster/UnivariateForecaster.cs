using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private IObservationManager _observationManager;
        private IStategyProvider _strategyProvider;
        private ISlidingWindowManager _slidingWindowManager;

        // Properties
        // Constructors
        public UnivariateForecaster(
            ISlidingWindowManager slidingWindowManager,
            IObservationManager observationManager,
            IStategyProvider strategyProvider)
        {

            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));
            if (observationManager == null)
                throw new ArgumentNullException(nameof(observationManager));
            if (strategyProvider == null)
                throw new ArgumentNullException(nameof(strategyProvider));

            _slidingWindowManager = slidingWindowManager;
            _observationManager = observationManager;
            _strategyProvider = strategyProvider;

        }
        public UnivariateForecaster() 
            : this(
                  new SlidingWindowManager(),
                  new ObservationManager(new SlidingWindowManager()), 
                  new StategyProvider()
                  ) { }
 
        // Methods (public)
        public List<Observation> Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedSlidingWindowNotValid);

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
             List<SlidingWindowItem> items)
        {

            Observation observation = new Observation();
            observation.Name = observationName;
            observation.SlidingWindowId = slidingWindowId;

            _observationManager.CalculateValues
                (items, ref observation, _strategyProvider.TwoDecimalDigitsRounding);

            return observation;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
