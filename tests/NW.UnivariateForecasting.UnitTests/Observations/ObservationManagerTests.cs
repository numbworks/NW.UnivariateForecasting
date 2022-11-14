using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
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
                            intervalManager: new IntervalManager(),
                            slidingWindowManager: new SlidingWindowManager(),
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
                            intervalManager: null,
                            slidingWindowManager: new SlidingWindowManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowManager: null,
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowManager").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowManager: new SlidingWindowManager(),
                            roundingFunction: null,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowManager: new SlidingWindowManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_05")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.ObservationManager.Create(null) // Whatever invalid SlidingWindow
                    ),
                typeof(ArgumentException),
                UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01")

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(
                null, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData(
                ObjectMother.Observation_InvalidDueOfNullName, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(
                ObjectMother.Observation_InvalidDueOfNullInterval, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData(
                ObjectMother.Observation_InvalidDueOfNullSlidingWindow, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData(
                ObjectMother.Observation01, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_05")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                null,
                null,
                ObjectMother.Observation01,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation01)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                ObjectMother.Observation01_WithCustomCE_C,
                ObjectMother.Observation01_WithCustomCE_E,
                ObjectMother.Observation01_WithCustomCE,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation01_WithCustomCE)
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

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (Observation observation, bool expected)
        {

            // Arrange
            // Act
            bool actual = ObjectMother.ObservationManager.IsValid(observation);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, double? C, double? E, Observation expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            ObservationManager observationManager 
                = new ObservationManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowManager: new SlidingWindowManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: (message) => fakeLogger.Log(message)
                        );

            // Act
            Observation actual = observationManager.Create(slidingWindow, C, E);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/