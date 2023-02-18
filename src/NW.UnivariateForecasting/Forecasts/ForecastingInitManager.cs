using System.Collections.Generic;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.Forecasts
{
    /// <inheritdoc cref="IForecastingInitManager"/>
    public class ForecastingInitManager : IForecastingInitManager
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingInitManager"/> instance using default parameters.</summary>	
        public ForecastingInitManager() { }

        #endregion

        #region Methods_public

        public ForecastingInit ExpandValues(ForecastingInit forecastingInit, double nextValue)
        {

            Validator.ValidateObject(forecastingInit, nameof(forecastingInit));

            List<double> expandedValues = new List<double>();
            expandedValues.AddRange(forecastingInit.Values);
            expandedValues.Add(nextValue);

            ForecastingInit expandedInit = new ForecastingInit(
                    observationName: forecastingInit.ObservationName,
                    values: expandedValues,
                    coefficient: forecastingInit.Coefficient,
                    error: forecastingInit.Error
                );

            return expandedInit;

        }

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2023
*/