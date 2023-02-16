using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    [TestFixture]
    public class ObservationManagerTests
    {

        #region Fields

        private static TestCaseData[] observationManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            settings: null, 
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            roundingFunction: null,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_03")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager().Create(slidingWindow: null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindow").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                null,
                null,
                ObjectMother.Observation01,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(ObjectMother.Observation01)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                ObjectMother.Observation01_WithCustomCE.Coefficient,
                ObjectMother.Observation01_WithCustomCE.Error,
                ObjectMother.Observation01_WithCustomCE,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(ObjectMother.Observation01_WithCustomCE)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(observationManagerExceptionTestCases))]
        public void ObservationManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, double? coefficient, double? error, Observation expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            ObservationManager observationManager 
                = new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: (message) => fakeLogger.Log(message)
                        );

            // Act
            Observation actual = observationManager.Create(slidingWindow, coefficient, error);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void ObservationManager_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            ObservationManager actual = new ObservationManager();

            // Assert
            Assert.IsInstanceOf<ObservationManager>(actual);

            Assert.IsInstanceOf<Func<double, double>>(ObservationManager.DefaultRoundingFunction);
            Assert.IsInstanceOf<Action<string>>(ObservationManager.DefaultLoggingAction);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/