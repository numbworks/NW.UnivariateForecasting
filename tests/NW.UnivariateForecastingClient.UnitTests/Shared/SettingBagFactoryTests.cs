using NW.UnivariateForecasting.UnitTests.Utilities;
using NW.UnivariateForecastingClient.Shared;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.UnitTests
{
    [TestFixture]
    public class SettingBagFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SettingBagFactory_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SettingBagFactory actual = new SettingBagFactory();

            // Assert
            Assert.IsInstanceOf<SettingBagFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeSettingBag_WhenDefault()
        {

            // Arrange
            // Act
            SettingBag actual
                = new SettingBagFactory().Create();

            // Assert
            Assert.IsInstanceOf<SettingBag>(actual);

        }

        [Test]
        public void Create_ShouldCreateExpectedSettingBag_WhenForecastDataWithNullValues()
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
            SettingBag expected = new SettingBag(
                    folderPath: SettingBag.DefaultFolderPath,
                    roundingDigits: SettingBag.DefaultRoundingDigits,
                    forecastingDenominator: SettingBag.DefaultForecastingDenominator
                );

            // Act
            SettingBag actual
                = new SettingBagFactory().Create(forecastData: forecastData);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));

        }

        [Test]
        public void Create_ShouldCreateExpectedSettingBag_WhenForecastDataWithNotNullValues()
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
            SettingBag expected = new SettingBag(
                    folderPath: @"C:\unifor\",
                    roundingDigits: 2,
                    forecastingDenominator: 0.001
                );

            // Act
            SettingBag actual
                = new SettingBagFactory().Create(forecastData: forecastData);

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
    Last Update: 08.02.2024
*/