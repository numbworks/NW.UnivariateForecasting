namespace NW.UnivariateForecasting
{
    public interface IUnivariateForecaster
    {

        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>     
        /// </summary>
        Observation Forecast(SlidingWindow objSlidingWindow);

        SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
