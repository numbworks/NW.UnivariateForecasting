using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IObservationManager _observationManager;
        private ISlidingWindowManager _slidingWindowManager;

        // Properties
        // Constructors
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            ISlidingWindowManager slidingWindowManager,
            IObservationManager observationManager)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));

            _settings = settings;
            _observationManager = observationManager;
            _slidingWindowManager = slidingWindowManager;

        }
        public UnivariateForecaster(UnivariateForecastingSettings settings)
            : this (settings, new SlidingWindowManager(settings), new ObservationManager(settings)) { }

        // Methods (public)
        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>     
        /// </summary>
        public Observation Forecast(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return _observationManager.Create(slidingWindow);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow)
        {

            Observation observation = Forecast(slidingWindow);

            return _slidingWindowManager.Combine(slidingWindow, observation);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow, uint steps)
        {

            if (steps < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));

            SlidingWindow newSlidingWindow = slidingWindow;
            for (int i = 1; i <= steps; i++)
                newSlidingWindow = ForecastAndCombine(newSlidingWindow);

            return newSlidingWindow;

        }
        public SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation)
            => _slidingWindowManager.Combine(slidingWindow, observation);
        public List<double> ExtractValues(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return slidingWindow.Items.Select(item => item.X_Actual).ToList();

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.08.2020

*/
