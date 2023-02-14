using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    /// <summary>Represents the library's entry-point.</summary>
    public interface IUnivariateForecaster
    {

        /// <summary>
        /// Logs the library's ascii banner.
        /// </summary>
        void LogAsciiBanner();

        /// <summary>Forecasts the next value for the provided list of values.</summary>
        /// <exception cref="ArgumentNullException"/> 
        /// <exception cref="ArgumentException"/>         
        double ForecastNextValue(List<double> values, double? C = null, double? E = null);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
