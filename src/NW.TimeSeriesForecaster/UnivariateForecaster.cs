using System;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IValidator _validator;
        private IObservationForecaster _observationForecaster;
        private ISlidingWindowCreator _slidingWindowCreator;

        // Properties
        // Constructors
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            IValidator validator,
            IObservationForecaster observationForecaster,
            ISlidingWindowCreator slidingWindowCreator)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));
            if (observationForecaster == null)
                throw new ArgumentNullException(nameof(observationForecaster));
            if (slidingWindowCreator == null)
                throw new ArgumentNullException(nameof(slidingWindowCreator));

            _settings = settings;
            _validator = validator;
            _observationForecaster = observationForecaster;
            _slidingWindowCreator = slidingWindowCreator;

        }
        public UnivariateForecaster(UnivariateForecastingSettings settings)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _settings = settings;
            _validator = new Validator();
            _observationForecaster = new ObservationForecaster(settings);
            _slidingWindowCreator = new SlidingWindowCreator(settings);

        }

        // Methods (public)

        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>     
        /// </summary>
        public Observation Forecast(SlidingWindow slidingWindow)
        {

            if (!_validator.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return _observationForecaster.Create(slidingWindow);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow)
        {

            Observation observation = Forecast(slidingWindow);

            return _slidingWindowCreator.Combine(slidingWindow, observation);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow, uint steps)
        {

            if (steps < 1)
                throw new Exception(MessageCollection.StepsCantBeLessThanOne);

            SlidingWindow newSlidingWindow = slidingWindow;
            for (int i = 1; i <= steps; i++)
                newSlidingWindow = ForecastAndCombine(newSlidingWindow);

            return newSlidingWindow;

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
