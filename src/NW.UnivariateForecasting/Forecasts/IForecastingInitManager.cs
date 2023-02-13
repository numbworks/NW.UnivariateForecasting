using System;

namespace NW.UnivariateForecasting.Forecasts
{
    /// <summary>Collects all the methods useful to manipulate a <see cref="ForecastingInit"/>.</summary>
    public interface IForecastingInitManager
    {

        /// <summary>
        /// Add <paramref name="nextValue"/> to <see cref="ForecastingInit.Values"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        ForecastingInit ExpandValues(ForecastingInit forecastingInit, decimal nextValue);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2023
*/