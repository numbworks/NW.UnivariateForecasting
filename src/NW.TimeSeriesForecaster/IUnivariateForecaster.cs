using System;
using System.Collections.Generic;
using System.IO;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
    /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
    /// which consists of observations on only a single characteristic or attribute.".</para>     
    /// </summary>
    public interface IUnivariateForecaster
    {

        Observation Forecast(SlidingWindow objSlidingWindow, double? C = null, double? E = null);
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, out List<Observation> observations, double? C = null, double? E = null);
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, double? C = null, double? E = null);
        SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, double? C = null, double? E = null);
        double ForecastNextValue(List<double> values, double? C = null, double? E = null);
        SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation);
        List<double> ExtractXActualValues(SlidingWindow slidingWindow);
        List<DateTime> ExtractStartDates(SlidingWindow slidingWindow);

        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, FileInfoAdapter fileInfoAdapter);
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, FileInfo fileInfo);
        void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, string filePath);
        void SaveObservationAsJson(Observation observation, FileInfoAdapter fileInfoAdapter);
        void SaveObservationAsJson(Observation observation, FileInfo fileInfo);
        void SaveObservationAsJson(Observation observation, string filePath);

        SlidingWindow GetSlidingWindowFromJson(FileInfoAdapter fileInfoAdapter);
        SlidingWindow GetSlidingWindowFromJson(FileInfo fileInfo);
        SlidingWindow GetSlidingWindowtFromJson(string filePath);
        Observation GetObservationFromJson(FileInfoAdapter fileInfoAdapter);
        Observation GetObservationFromJson(FileInfo fileInfo);
        Observation GetObservationFromJson(string filePath);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.12.2020

*/
