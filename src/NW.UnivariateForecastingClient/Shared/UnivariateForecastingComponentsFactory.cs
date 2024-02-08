using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="IUnivariateForecastingComponentsFactory"/>
    public class UnivariateForecastingComponentsFactory : IUnivariateForecastingComponentsFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="UnivariateForecastingComponentsFactory"/> instance.</summary>
        public UnivariateForecastingComponentsFactory() { }

        #endregion

        #region Methods_public

        public UnivariateForecastingComponents Create()
            => new UnivariateForecastingComponents();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/