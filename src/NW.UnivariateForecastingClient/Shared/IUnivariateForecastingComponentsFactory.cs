using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>A factory for <see cref="UnivariateForecastingComponents"/>.</summary>
    public interface IUnivariateForecastingComponentsFactory
    {

        /// <summary>Creates an instance of <see cref="UnivariateForecastingComponents"/>.</summary>
        UnivariateForecastingComponents Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/