using System;
using NUnit.Framework;
using NW.UnivariateForecasting.Forecasts;

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
                ).SetArgDisplayNames($"{nameof(univariateForecastingSettingsExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(univariateForecastingSettingsExceptionTestCases))]
        public void UnivariateForecastingSettings_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/