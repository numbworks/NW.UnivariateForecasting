using NUnit.Framework;
using NW.UnivariateForecastingClient.Shared;

namespace NW.NGramTextClassificationClient.UnitTests.Shared
{
    [TestFixture]
    public class ForecastDataTests
    {

        #region Fields

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [Test]
        public void ForecastData_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            ForecastData actual
                = new ForecastData(
                        init: "Init.json",
                        folderPath: @"C:\unifor\",
                        saveSession: true,
                        roundingDigits: 2,
                        forecastingDenominator: 0.001
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<ForecastData>());

            Assert.That(actual.Init, Is.InstanceOf<string>());
            Assert.That(actual.FolderPath, Is.InstanceOf<string>());
            Assert.That(actual.SaveSession, Is.InstanceOf<bool>());
            Assert.That(actual.RoundingDigits, Is.InstanceOf<uint?>());
            Assert.That(actual.ForecastingDenominator, Is.InstanceOf<double?>());

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/
