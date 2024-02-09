using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>A factory for <see cref="SettingBag"/>.</summary>
    public interface ISettingBagFactory
    {

        /// <summary>Creates an instance of <see cref="SettingBag"/>.</summary>
        SettingBag Create();

        /// <summary>Creates an instance of <see cref="SettingBag"/> out of <paramref name="forecastData"/>.</summary>
        SettingBag Create(ForecastData forecastData);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/