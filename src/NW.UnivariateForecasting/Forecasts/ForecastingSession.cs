using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;
using NW.Shared.Validation;

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
        public string Version { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingSession"/> instance.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public ForecastingSession(ForecastingInit init, List<Observation> observations, string version) 
        {

            Validator.ValidateObject(init, nameof(init));
            Validator.ValidateList(observations, nameof(observations));
            Validator.ValidateStringNullOrWhiteSpace(version, nameof(version));

            Init = init;
            Observations = observations;
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
    Last Update: 01.03.2023
*/