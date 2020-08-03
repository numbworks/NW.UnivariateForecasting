using System;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private ISlidingWindowValidator _slidingWindowValidator;
        private IObservationForecaster _observationForecaster;

        // Properties
        // Constructors
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            ISlidingWindowValidator slidingWindowValidator,
            IObservationForecaster observationForecaster)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (slidingWindowValidator == null)
                throw new ArgumentNullException(nameof(slidingWindowValidator));
            if (observationForecaster == null)
                throw new ArgumentNullException(nameof(observationForecaster));

            _settings = settings;
            _slidingWindowValidator = slidingWindowValidator;
            _observationForecaster = observationForecaster;

        }
        public UnivariateForecaster(UnivariateForecastingSettings settings)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _settings = settings;
            _slidingWindowValidator = new SlidingWindowValidator();
            _observationForecaster = new ObservationForecaster(settings);

        }

        // Methods (public)

        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>     
        /// </summary>
        public Observation Do(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowValidator.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedSlidingWindowNotValid);

            return _observationForecaster.Create(slidingWindow);

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
