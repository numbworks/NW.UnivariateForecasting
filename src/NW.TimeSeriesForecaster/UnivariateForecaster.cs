using System;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private ISlidingWindowManager _slidingWindowManager;
        private IObservationManager _observationManager;
        private Func<double, double> _roundingStrategy;

        // Properties
        // Constructors
        public UnivariateForecaster(
            Func<double, double> roundingStrategy,
            ISlidingWindowManager slidingWindowManager,
            IObservationManager observationManager)
        {

            if (roundingStrategy == null)
                throw new ArgumentNullException(nameof(roundingStrategy));
            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));
            if (observationManager == null)
                throw new ArgumentNullException(nameof(observationManager));

            _roundingStrategy = roundingStrategy;
            _slidingWindowManager = slidingWindowManager;
            _observationManager = observationManager;

        }
        public UnivariateForecaster(
            Func<double, double> roundingStrategy)
            : this(
                  roundingStrategy,
                  new SlidingWindowManager(roundingStrategy),
                  new ObservationManager(
                      new SlidingWindowManager(roundingStrategy), 
                      roundingStrategy)) { }
        public UnivariateForecaster() 
            : this(
                  null,
                  new SlidingWindowManager(null),
                  new ObservationManager(
                      new SlidingWindowManager(null), 
                      null)) { }
 
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
