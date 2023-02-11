using NW.UnivariateForecasting;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>A factory for <see cref="UnivariateForecastingSettings"/>.</summary>
    public interface IUnivariateForecastingSettingsFactory
    {

        /// <summary>Creates an instance of <see cref="UnivariateForecastingSettings"/>.</summary>
        UnivariateForecastingSettings Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/