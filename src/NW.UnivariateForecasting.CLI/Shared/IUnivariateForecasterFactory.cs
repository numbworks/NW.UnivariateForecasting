using System;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.Shared
{
    /// <summary>A factory for <see cref="TextClassifier"/>.</summary>
    public interface IUnivariateForecasterFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifier"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        UnivariateForecaster Create(SettingBag settingBag, ComponentBag componentBag);

    }
}