using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>A factory for <see cref="ComponentBag"/>.</summary>
    public interface IComponentBagFactory
    {

        /// <summary>Creates an instance of <see cref="ComponentBag"/>.</summary>
        ComponentBag Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/