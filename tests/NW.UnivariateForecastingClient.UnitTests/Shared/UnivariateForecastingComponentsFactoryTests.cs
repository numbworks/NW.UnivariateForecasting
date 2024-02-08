using NW.UnivariateForecastingClient.Shared;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingComponentsFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void UnivariateForecastingComponentsFactory_ShouldCreateAnObjectOfTypeUnivariateForecastingComponentsFactory_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecastingComponentsFactory actual = new UnivariateForecastingComponentsFactory();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingComponentsFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeUnivariateForecastingComponents_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecastingComponents actual
                = new UnivariateForecastingComponentsFactory().Create();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingComponents>(actual);

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
    Last Update: 22.01.2023
*/