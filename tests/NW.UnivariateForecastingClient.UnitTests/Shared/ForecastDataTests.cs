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
                        init: "init.json",
                        folderPath: @"C:\unifor\",
                        saveSession: true,
                        roundingDigits: 2,
                        forecastingDenominator: 0.001
                    );

            // Assert
            Assert.IsInstanceOf<ForecastData>(actual);

            Assert.IsInstanceOf<string>(actual.Init);
            Assert.IsInstanceOf<string>(actual.FolderPath);
            Assert.IsInstanceOf<bool>(actual.SaveSession);
            Assert.IsInstanceOf<uint?>(actual.RoundingDigits);
            Assert.IsInstanceOf<double?>(actual.ForecastingDenominator);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/
