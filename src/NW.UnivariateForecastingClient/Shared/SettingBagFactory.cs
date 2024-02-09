using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="ISettingBagFactory"/>
    public class SettingBagFactory : ISettingBagFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SettingBagFactory"/> instance.</summary>
        public SettingBagFactory() { }

        #endregion

        #region Methods_public

        public SettingBag Create()
            => new SettingBag();

        public SettingBag Create(ForecastData forecastData)
        {

            SettingBag settingBag = new SettingBag(
                    forecastingDenominator: forecastData.ForecastingDenominator ?? SettingBag.DefaultForecastingDenominator,
                    folderPath: forecastData.FolderPath ?? SettingBag.DefaultFolderPath,
                    roundingDigits: forecastData.RoundingDigits ?? SettingBag.DefaultRoundingDigits
                );

            return settingBag;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/