using NW.UnivariateForecasting;
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
        public void UnivariateForecastingSettingsFactory_ShouldCreateAnObjectOfTypeUnivariateForecastingSettingsFactory_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecastingSettingsFactory actual = new UnivariateForecastingSettingsFactory();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingSettingsFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeUnivariateForecastingSettings_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecastingSettings actual
                = new UnivariateForecastingSettingsFactory().Create();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingSettings>(actual);

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