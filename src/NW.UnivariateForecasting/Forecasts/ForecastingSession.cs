using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.Forecasts
{
    /// <summary>Collects the result of a forecasting session.</summary>
    public class ForecastingSession
    {

        #region Fields

        #endregion

        #region Properties

        public ForecastingInit Init { get; }
        public List<Observation> Observations { get; }
        public uint Steps { get; }
        public string Version { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingSession"/> instance using default parameters.</summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public ForecastingSession(ForecastingInit init, List<Observation> observations, uint steps, string version) 
        {

            Validator.ValidateObject(init, nameof(init));
            Validator.ValidateList(observations, nameof(observations));
            Validator.ThrowIfLessThanOne(steps, nameof(steps));
            Validator.ValidateStringNullOrWhiteSpace(version, nameof(version));

            Init = init;
            Observations = observations;
            Steps = steps;
            Version = version;

        }

        #endregion

        #region Methods_public

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2023
*/