using System;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private ISlidingWindowValidator _slidingWindowValidator;
        private IObservationForecaster _observationForecaster;

        // Properties
        // Constructors
        public UnivariateForecaster(
            ISlidingWindowValidator slidingWindowValidator,
            IObservationForecaster observationForecaster)
        {

            if (slidingWindowValidator == null)
                throw new ArgumentNullException(nameof(slidingWindowValidator));
            if (observationForecaster == null)
                throw new ArgumentNullException(nameof(observationForecaster));

            _slidingWindowValidator = slidingWindowValidator;
            _observationForecaster = observationForecaster;

        }
        public UnivariateForecaster(IStategyProvider strategyProvider)
        {

            if (strategyProvider == null)
                throw new ArgumentNullException(nameof(strategyProvider));

            _slidingWindowValidator = new SlidingWindowValidator();
            _observationForecaster = new ObservationForecaster(_slidingWindowValidator, strategyProvider);

        }
        public UnivariateForecaster(Func<double, double> roundingStrategy = null)
        {

            _slidingWindowValidator = new SlidingWindowValidator();
            _observationForecaster = new ObservationForecaster(_slidingWindowValidator, roundingStrategy);

        }

        // Methods (public)
        public Observation Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowValidator.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedSlidingWindowNotValid);

            return _observationForecaster.Create(slidingWindow);

        }
        public Observation Do(SlidingWindow slidingWindow, double denominator)
        {

            if (!_slidingWindowValidator.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedSlidingWindowNotValid);

            return _observationForecaster.Create(slidingWindow, denominator);

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
