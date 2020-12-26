using System;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingSettingsTests
    {

        // Fields
        private static TestCaseData[] univariateForecastingSettingsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecastingSettings(
                                    forecastingDenominator: 0,
                                    dummyId: UnivariateForecastingSettings.DefaultDummyId,
                                    dummyObservationName: UnivariateForecastingSettings.DefaultDummyObservationName,
                                    dummyStartDate: UnivariateForecastingSettings.DefaultDummyStartDate,
                                    dummySteps: UnivariateForecastingSettings.DefaultDummySteps,
                                    dummyIntervalUnit: UnivariateForecastingSettings.DefaultDummyIntervalUnit
                                    )),
                typeof(ArgumentException),
                MessageCollection.DenominatorCantBeLessThan(
                                    "forecastingDenominator", 
                                    UnivariateForecastingSettings.DefaultForecastingDenominator)
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(univariateForecastingSettingsExceptionTestCases))]
        public void UnivariateForecastingSettings_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/
