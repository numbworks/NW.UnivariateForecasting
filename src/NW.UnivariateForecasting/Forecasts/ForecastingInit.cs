using System;
using System.Collections.Generic;
using NW.Shared.Validation;

namespace NW.UnivariateForecasting.Forecasts
{
    /// <summary>Collects all the data required to initialize a forecasting session.</summary>
    public class ForecastingInit
    {

        #region Fields

        #endregion

        #region Properties

        public string ObservationName { get; }
        public List<double> Values { get; }
        public double? Coefficient { get; }
        public double? Error { get; }
        public uint Steps { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingInit"/> instance.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ForecastingInit(
            string observationName, 
            List<double> values, 
            double? coefficient, 
            double? error,
            uint steps) 
        {

            Validator.ValidateList(values, nameof(values));
            Validator.ThrowIfLessThanOne(steps, nameof(steps));

            ObservationName = observationName;
            Values = values;
            Coefficient = coefficient;
            Error = error;
            Steps = steps;

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