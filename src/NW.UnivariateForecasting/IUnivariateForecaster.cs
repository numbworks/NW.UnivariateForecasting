using System;
using System.Collections.Generic;
using System.IO;
using NW.UnivariateForecasting.Files;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Represents the library's entry-point.
    /// </summary>
    public interface IUnivariateForecaster
    {

        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/>.
        /// </summary>
        Observation Forecast(SlidingWindow objSlidingWindow, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, out List<Observation> observations, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value for the provided list of values.
        /// </summary>
        double ForecastNextValue(List<double> values, double? C = null, double? E = null);

        /// <summary>
        /// Add the provided <see cref="Observation"/> to the provided <see cref="SlidingWindow"/> object.
        /// </summary>
        SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation);

        List<double> ExtractXActualValues(SlidingWindow slidingWindow);
        List<DateTime> ExtractStartDates(SlidingWindow slidingWindow);

        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, IFileInfoAdapter fileInfoAdapter);
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, FileInfo fileInfo);
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, string filePath);
        void SaveObservationAsJson(Observation observation, IFileInfoAdapter fileInfoAdapter);
        void SaveObservationAsJson(Observation observation, FileInfo fileInfo);
        void SaveObservationAsJson(Observation observation, string filePath);

        SlidingWindow LoadSlidingWindowFromJson(IFileInfoAdapter fileInfoAdapter);
        SlidingWindow LoadSlidingWindowFromJson(FileInfo fileInfo);
        SlidingWindow LoadSlidingWindowFromJson(string filePath);
        Observation LoadObservationFromJson(IFileInfoAdapter fileInfoAdapter);
        Observation LoadObservationFromJson(FileInfo fileInfo);
        Observation LoadObservationFromJson(string filePath);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021

*/
