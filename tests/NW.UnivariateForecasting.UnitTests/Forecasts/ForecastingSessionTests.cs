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
                                steps: ObjectMother.ForecastingSession_Single_Steps,
                                version: ObjectMother.ForecastingSession_Version
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("init").Message
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingSession(
                                init: ObjectMother.ForecastingInit_WithInitCE,
                                observations: null,
                                steps: ObjectMother.ForecastingSession_Single_Steps,
                                version: ObjectMother.ForecastingSession_Version
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("observations").Message
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingSession(
                                init: ObjectMother.ForecastingInit_WithInitCE,
                                observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                                steps: 0,
                                version: ObjectMother.ForecastingSession_Version
                        )
                ),
                typeof(ArgumentException),
                "'steps' can't be less than one."
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingSession(
                                init: ObjectMother.ForecastingInit_WithInitCE,
                                observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                                steps: ObjectMother.ForecastingSession_Single_Steps,
                                version: null
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
            ).SetArgDisplayNames($"{nameof(forecastingSessionExceptionTestCases)}_04")

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
                        init: ObjectMother.ForecastingInit_WithInitCE,
                        observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                        steps: ObjectMother.ForecastingSession_Single_Steps,
                        version: ObjectMother.ForecastingSession_Version
                    );

            // Assert
            Assert.IsInstanceOf<ForecastingSession>(actual);

            Assert.IsInstanceOf<ForecastingInit>(actual.Init);
            Assert.IsInstanceOf<List<Observation>>(actual.Observations);
            Assert.IsInstanceOf<uint>(actual.Steps);
            Assert.IsInstanceOf<string>(actual.Version);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2023
*/
