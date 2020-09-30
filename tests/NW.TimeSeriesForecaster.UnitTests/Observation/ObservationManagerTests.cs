using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class ObservationManagerTests
    {

        // Fields
        private static TestCaseData[] constructorExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            null, 
                            new IntervalManager(),
                            new SlidingWindowManager(
                                new UnivariateForecastingSettings()))),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            new UnivariateForecastingSettings(),
                            null,
                            new SlidingWindowManager(
                                new UnivariateForecastingSettings()))),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            new UnivariateForecastingSettings(),
                            new IntervalManager(),
                            null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowManager").Message
                ),

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.ObservationManager_Default.Create(null) // Whatever invalid SlidingWindow
                    ),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetDescription(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)))

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(null, false),
            new TestCaseData(ObjectMother.Observation_InvalidDueOfNullName, false),
            new TestCaseData(ObjectMother.Observation_InvalidDueOfNullInterval, false),
            new TestCaseData(ObjectMother.Observation_InvalidDueOfNullSlidingWindow, false),
            new TestCaseData(ObjectMother.Observation1, true)

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                null,
                null,
                ObjectMother.Observation1,
                new List<string>() {
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1)
                    }
                ),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.Observation1withCustomCE_C,
                ObjectMother.Observation1withCustomCE_E,
                ObjectMother.Observation1withCustomCE,
                new List<string>() {
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1withCustomCE)
                    }
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(constructorExceptionTestCases))]
        public void Constructor_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

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
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            ObservationManager observationManager = new ObservationManager(settings);

            // Act
            Observation actual = observationManager.Create(slidingWindow, C, E);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 17.09.2020

*/
