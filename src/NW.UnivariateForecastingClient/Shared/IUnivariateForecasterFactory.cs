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
        UnivariateForecaster Create(UnivariateForecastingSettings settings, UnivariateForecastingComponents components);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/