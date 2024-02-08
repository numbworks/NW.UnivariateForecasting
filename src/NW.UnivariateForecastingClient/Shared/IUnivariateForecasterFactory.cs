using System;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>A factory for <see cref="TextClassifier"/>.</summary>
    public interface IUnivariateForecasterFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifier"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        UnivariateForecaster Create(UnivariateForecastingSettings settings, ComponentBag componentBag);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/