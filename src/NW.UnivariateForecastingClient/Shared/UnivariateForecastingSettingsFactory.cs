using NW.UnivariateForecasting;

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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/