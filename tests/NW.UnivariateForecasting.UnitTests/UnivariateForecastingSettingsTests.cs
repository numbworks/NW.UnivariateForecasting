using System;
using NW.UnivariateForecasting.Forecasts;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingSettingsTests
    {

        #region Fields

        private static TestCaseData[] univariateForecastingSettingsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecastingSettings(forecastingDenominator: 0)
                ),
                typeof(ArgumentException),
                MessageCollection.DenominatorCantBeLessThan(
                                    "forecastingDenominator", 
                                    UnivariateForecastingSettings.DefaultForecastingDenominator)
                ).SetArgDisplayNames($"{nameof(univariateForecastingSettingsExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(univariateForecastingSettingsExceptionTestCases))]
        public void UnivariateForecastingSettings_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void UnivariateForecastingSettings_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            UnivariateForecastingSettings actual = new UnivariateForecastingSettings();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingSettings>(actual);
            Assert.IsInstanceOf<double>(actual.ForecastingDenominator);

            Assert.IsInstanceOf<double>(UnivariateForecastingSettings.DefaultForecastingDenominator);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.03.2023
*/