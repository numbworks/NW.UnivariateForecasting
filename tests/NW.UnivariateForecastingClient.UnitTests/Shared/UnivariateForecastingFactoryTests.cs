﻿using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecastingClient.Shared;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void UnivariateForecasterFactory_ShouldCreateAnObjectOfTypeUnivariateForecasterFactory_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecasterFactory actual = new UnivariateForecasterFactory();

            // Assert
            Assert.IsInstanceOf<UnivariateForecasterFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeUnivariateForecaster_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecaster actual
                = new UnivariateForecasterFactory().Create(
                        componentBag: new ComponentBag(),
                        settings: new UnivariateForecastingSettings()
                        );

            // Assert
            Assert.IsInstanceOf<UnivariateForecaster>(actual);

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/