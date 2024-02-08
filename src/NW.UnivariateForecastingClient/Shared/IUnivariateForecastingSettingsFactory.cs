using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>A factory for <see cref="UnivariateForecastingSettings"/>.</summary>
    public interface IUnivariateForecastingSettingsFactory
    {

        /// <summary>Creates an instance of <see cref="UnivariateForecastingSettings"/>.</summary>
        UnivariateForecastingSettings Create();

        /// <summary>Creates an instance of <see cref="UnivariateForecastingSettings"/> out of <paramref name="forecastData"/>.</summary>
        UnivariateForecastingSettings Create(ForecastData forecastData);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/