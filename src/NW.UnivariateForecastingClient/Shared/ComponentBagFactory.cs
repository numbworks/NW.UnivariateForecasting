﻿using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
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

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/