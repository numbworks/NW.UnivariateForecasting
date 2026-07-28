using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
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

        #region Methods (public)

        public UnivariateForecaster Create(SettingBag settingBag, ComponentBag componentBag)
            => new UnivariateForecaster(settingBag, componentBag);

        #endregion

        #region Methods (private)
        #endregion

    }
}