using System;
using System.Collections.Generic;
using System.Reflection;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting
{
    /// <inheritdoc cref="IUnivariateForecaster"/>
    public class UnivariateForecaster : IUnivariateForecaster
    {

        #region Fields

        private UnivariateForecastingSettings _settings;
        private UnivariateForecastingComponents _components;

        #endregion

        #region Properties

        public string Version { get; }
        public string AsciiBanner { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="UnivariateForecaster"/>.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            UnivariateForecastingComponents components)
        {

            Validator.ValidateObject(settings, nameof(settings));
            Validator.ValidateObject(components, nameof(components));

            _settings = settings;
            _components = components;

            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AsciiBanner = _components.AsciiBannerManager.Create(Version);

        }

        /// <summary>Initializes an instance of <see cref="UnivariateForecaster"/> using default values.</summary>
        public UnivariateForecaster()
            : this (
                  new UnivariateForecastingSettings(), 
                  new UnivariateForecastingComponents()) { }

        #endregion

        #region Methods_public

        public void LogAsciiBanner()
            => _components.LoggingActionAsciiBanner(AsciiBanner);

        public double ForecastNextValue(List<double> values, double? C = null, double? E = null)
        {

            Validator.ValidateList(values, nameof(values));

            _components.LoggingAction(Forecasts.MessageCollection.ForecastNextValueRunningForProvidedValues(values));

            SlidingWindow slidingWindow = _components.SlidingWindowManager.Create(values);
            double nextValue = _components.ObservationManager.Create(slidingWindow, C, E).Y_Forecasted;

            _components.LoggingAction(Forecasts.MessageCollection.ForecastNextValueSuccessfullyRun(nextValue));

            return nextValue;

        }

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
