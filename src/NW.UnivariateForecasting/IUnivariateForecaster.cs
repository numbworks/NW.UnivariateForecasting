using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;

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

        /// <summary>
        /// Loads a <see cref="ForecastingInit"/> object from the provided <paramref name="jsonFile"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <c>default(ForecastingInit)</c> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>     
        ForecastingInit LoadInitOrDefault(IFileInfoAdapter jsonFile);

        /// <summary>
        /// Saves the provided <see cref="ForecastingSession"/> object as JSON into <paramref name="folderPath"/>. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        void SaveSession(ForecastingSession session, string folderPath);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.02.2023
*/
