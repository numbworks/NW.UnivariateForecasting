using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="IUnivariateForecastingSettingsFactory"/>
    public class UnivariateForecastingSettingsFactory : IUnivariateForecastingSettingsFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="UnivariateForecastingSettingsFactory"/> instance.</summary>
        public UnivariateForecastingSettingsFactory() { }

        #endregion

        #region Methods_public

        public UnivariateForecastingSettings Create()
            => new UnivariateForecastingSettings();

        public UnivariateForecastingSettings Create(ForecastData forecastData)
        {

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                    forecastingDenominator: forecastData.ForecastingDenominator ?? UnivariateForecastingSettings.DefaultForecastingDenominator,
                    folderPath: forecastData.FolderPath ?? UnivariateForecastingSettings.DefaultFolderPath,
                    roundingDigits: forecastData.RoundingDigits ?? UnivariateForecastingSettings.DefaultRoundingDigits
                );

            return settings;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/