using NW.UnivariateForecasting;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NW.UnivariateForecastingClient.Shared;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingSettingsFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void UnivariateForecastingSettingsFactory_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecastingSettingsFactory actual = new UnivariateForecastingSettingsFactory();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingSettingsFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeUnivariateForecastingSettings_WhenDefault()
        {

            // Arrange
            // Act
            UnivariateForecastingSettings actual
                = new UnivariateForecastingSettingsFactory().Create();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingSettings>(actual);

        }

        [Test]
        public void Create_ShouldCreateExpectedUnivariateForecastingSettings_WhenForecastDataWithNullValues()
        {

            // Arrange
            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        saveSession: true,
                        folderPath: null,
                        roundingDigits: null,
                        forecastingDenominator: null
                    );
            UnivariateForecastingSettings expected = new UnivariateForecastingSettings(
                    folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                    roundingDigits: UnivariateForecastingSettings.DefaultRoundingDigits,
                    forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator
                );

            // Act
            UnivariateForecastingSettings actual
                = new UnivariateForecastingSettingsFactory().Create(forecastData: forecastData);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));

        }

        [Test]
        public void Create_ShouldCreateExpectedUnivariateForecastingSettings_WhenForecastDataWithNotNullValues()
        {

            // Arrange
            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        saveSession: true,
                        folderPath: @"C:\unifor\",
                        roundingDigits: 2,
                        forecastingDenominator: 0.001
                    );
            UnivariateForecastingSettings expected = new UnivariateForecastingSettings(
                    folderPath: @"C:\unifor\",
                    roundingDigits: 2,
                    forecastingDenominator: 0.001
                );

            // Act
            UnivariateForecastingSettings actual
                = new UnivariateForecastingSettingsFactory().Create(forecastData: forecastData);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));

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
    Last Update: 08.03.2023
*/