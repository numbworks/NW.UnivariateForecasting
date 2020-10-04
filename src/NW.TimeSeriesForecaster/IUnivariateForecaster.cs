using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
    /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
    /// which consists of observations on only a single characteristic or attribute.".</para>     
    /// </summary>
    public interface IUnivariateForecaster
    {

        Observation Forecast(SlidingWindow objSlidingWindow);
        SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow);
        SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow, uint steps);
        List<double> ExtractXActualValues(SlidingWindow slidingWindow);
        List<DateTime> ExtractStartDates(SlidingWindow slidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
