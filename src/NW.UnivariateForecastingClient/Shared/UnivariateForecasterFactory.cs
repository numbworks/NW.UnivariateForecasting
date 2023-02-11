using NW.UnivariateForecasting;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="IUnivariateForecasterFactory"/>
    public class UnivariateForecasterFactory : IUnivariateForecasterFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="UnivariateForecasterFactory"/> instance.</summary>
        public UnivariateForecasterFactory() { }

        #endregion

        #region Methods_public

        public UnivariateForecaster Create(UnivariateForecastingSettings settings, UnivariateForecastingComponents components)
            => new UnivariateForecaster(settings, components);

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/