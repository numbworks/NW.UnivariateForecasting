using System;
using System.Collections.Generic;
using System.IO;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting
{
    /// <summary>Represents the library's entry-point.</summary>
    public interface IUnivariateForecaster
    {

        /// <summary>Forecasts the next value for the provided <see cref="SlidingWindow"/>.</summary>
        /// <exception cref="ArgumentException"/> 
        Observation Forecast(SlidingWindow objSlidingWindow, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        /// <exception cref="ArgumentException"/>         
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, out List<Observation> observations, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        /// <exception cref="ArgumentException"/>         
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, double? C = null, double? E = null);

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        /// <exception cref="ArgumentException"/>         
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, double? C = null, double? E = null);

        /// <summary>Forecasts the next value for the provided list of values.</summary>
        /// <exception cref="ArgumentNullException"/> 
        /// <exception cref="ArgumentException"/>         
        double ForecastNextValue(List<double> values, double? C = null, double? E = null);

        /// <summary>Add the provided <see cref="Observation"/> to the provided <see cref="SlidingWindow"/> object.</summary>
        /// <exception cref="ArgumentException"/>        
        SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation);

        /// <summary>Extracts all the <see cref="SlidingWindowItem.X_Actual"/> values.</summary>
        /// <exception cref="ArgumentException"/>   
        List<double> ExtractXActualValues(SlidingWindow slidingWindow);

        /// <summary>Extracts all the <see cref="Interval.StartDate"/> dates.</summary>
        /// <exception cref="ArgumentException"/> 
        List<DateTime> ExtractStartDates(SlidingWindow slidingWindow);

        /// <summary>Convert <paramref name="slidingWindow"/> to JSON and save it on disk.</summary>
        /// <exception cref="ArgumentNullException"/>
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, IFileInfoAdapter fileInfoAdapter);

        /// <inheritdoc cref="SaveSlidingWindowAsJson(SlidingWindow, IFileInfoAdapter)"/>
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, FileInfo fileInfo);

        /// <inheritdoc cref="SaveSlidingWindowAsJson(SlidingWindow, IFileInfoAdapter)"/>
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, string filePath);

        /// <summary>Convert <paramref name="observation"/> to JSON and save it on disk.</summary>
        /// <exception cref="ArgumentNullException"/>
        void SaveObservationAsJson(Observation observation, IFileInfoAdapter fileInfoAdapter);

        /// <inheritdoc cref="SaveObservationAsJson(Observation, IFileInfoAdapter)"/>
        void SaveObservationAsJson(Observation observation, FileInfo fileInfo);

        /// <inheritdoc cref="SaveObservationAsJson(Observation, IFileInfoAdapter)"/>
        void SaveObservationAsJson(Observation observation, string filePath);

        /// <summary>Read the provided JSON file and deserialize it to a <see cref="SlidingWindow"/> object.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        SlidingWindow LoadSlidingWindowFromJson(IFileInfoAdapter fileInfoAdapter);

        /// <inheritdoc cref="LoadSlidingWindowFromJson(IFileInfoAdapter)"/>
        SlidingWindow LoadSlidingWindowFromJson(FileInfo fileInfo);

        /// <inheritdoc cref="LoadSlidingWindowFromJson(IFileInfoAdapter)"/>
        SlidingWindow LoadSlidingWindowFromJson(string filePath);

        /// <summary>Read the provided JSON file and deserialize it to a <see cref="Observation"/> object.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        Observation LoadObservationFromJson(IFileInfoAdapter fileInfoAdapter);

        /// <inheritdoc cref="LoadObservationFromJson(IFileInfoAdapter)"/>
        Observation LoadObservationFromJson(FileInfo fileInfo);

        /// <inheritdoc cref="LoadObservationFromJson(IFileInfoAdapter)"/>
        Observation LoadObservationFromJson(string filePath);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/
