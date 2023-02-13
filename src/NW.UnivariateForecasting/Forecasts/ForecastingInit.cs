﻿using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.Forecasts
{
    /// <summary>Collects all the data required to initialize a forecasting session.</summary>
    public class ForecastingInit
    {

        #region Fields

        #endregion

        #region Properties

        public string ObservationName { get; }
        public List<decimal> Values { get; }
        public decimal? Coefficient { get; }
        public decimal? Error { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingInit"/> instance using default parameters.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public ForecastingInit(string observationName, List<decimal> values, decimal? coefficient, decimal? error) 
        {

            Validator.ValidateList(values, nameof(values));

            ObservationName = observationName;
            Values = values;
            Coefficient = coefficient;
            Error = error;

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
    Last Update: 13.02.2023
*/