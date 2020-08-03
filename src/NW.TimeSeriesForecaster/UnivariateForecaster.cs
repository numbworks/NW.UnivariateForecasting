using System;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private ISlidingWindowManager _slidingWindowManager;
        private IObservationManager _observationManager;
        private IStategyProvider _strategyProvider;

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
        public Observation Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedSlidingWindowNotValid);

            return _observationManager.Create(slidingWindow);

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
