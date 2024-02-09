using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Bags;

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

        public UnivariateForecaster Create(SettingBag settingBag, ComponentBag componentBag)
            => new UnivariateForecaster(settingBag, componentBag);

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/