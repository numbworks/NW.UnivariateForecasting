using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    [TestFixture]
    public class ForecastingSessionTests
    {

        #region Fields

        private static TestCaseData[] forecastingSessionExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingSession(
                                init: null,
                                observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                                version: ObjectMother.ForecastingSession_Version
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("init").Message
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingSession(
                                init: ObjectMother.ForecastingInit_SingleWithCE,
                                observations: null,
                                version: ObjectMother.ForecastingSession_Version
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("observations").Message
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingSession(
                                init: ObjectMother.ForecastingInit_SingleWithCE,
                                observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                                version: null
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(forecastingSessionExceptionTestCases))]
        public void ForecastingSession_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ForecastingSession_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            ForecastingSession actual
                = new ForecastingSession(
                        init: ObjectMother.ForecastingInit_SingleWithCE,
                        observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                        version: ObjectMother.ForecastingSession_Version
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<ForecastingSession>());

            Assert.That(actual.Init, Is.InstanceOf<ForecastingInit>());
            Assert.That(actual.Observations, Is.InstanceOf<List<Observation>>());
            Assert.That(actual.Version, Is.InstanceOf<string>());

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
