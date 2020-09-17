using System;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class ObservationManagerTests
    {

        // Fields
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(null, false),
            new TestCaseData(MemberRepository.Observation_InvalidDueOfNullName, false),
            new TestCaseData(MemberRepository.Observation_InvalidDueOfNullInterval, false),
            new TestCaseData(MemberRepository.Observation_InvalidDueOfNullSlidingWindow, false),
            new TestCaseData(MemberRepository.Observation1, true)

        };
        private static TestCaseData[] createTestCases =
        {



        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (Observation observation, bool expected)
        {

            // Arrange
            // Act
            bool actual = MemberRepository.ObservationManager_Default.IsValid(observation);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, bool expected)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            ObservationManager observationManager = new ObservationManager(settings);

            // Act
            Observation actual = observationManager.Create(slidingWindow);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 17.09.2020

*/
