using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
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
                    () => ObjectMother.ObservationManager_Default.Create(null) // Whatever invalid SlidingWindow
                    ),
                typeof(ArgumentException),
                Observations.MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
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
                ObjectMother.Shared_Observation1, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_05")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                ObjectMother.Shared_SlidingWindow1,
                null,
                null,
                ObjectMother.Shared_Observation1,
                new List<string>() {
                    Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.Shared_SlidingWindow1),
                    Observations.MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Shared_Observation1)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01"),

            new TestCaseData(
                ObjectMother.Shared_SlidingWindow1,
                ObjectMother.Shared_Observation1WithCustomCE_C,
                ObjectMother.Shared_Observation1WithCustomCE_E,
                ObjectMother.Shared_Observation1WithCustomCE,
                new List<string>() {
                    Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.Shared_SlidingWindow1),
                    Observations.MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Shared_Observation1WithCustomCE)
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
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (Observation observation, bool expected)
        {

            // Arrange
            // Act
            bool actual = ObjectMother.ObservationManager_Default.IsValid(observation);

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
    Last Update: 12.11.2022
*/