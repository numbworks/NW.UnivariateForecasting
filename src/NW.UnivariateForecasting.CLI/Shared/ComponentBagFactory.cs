using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.Shared
{
    /// <inheritdoc cref="IComponentBagFactory"/>
    public class ComponentBagFactory : IComponentBagFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ComponentBagFactory"/> instance.</summary>
        public ComponentBagFactory() { }

        #endregion

        #region Methods_public

        public ComponentBag Create()
            => new ComponentBag();

        #endregion

    }
}