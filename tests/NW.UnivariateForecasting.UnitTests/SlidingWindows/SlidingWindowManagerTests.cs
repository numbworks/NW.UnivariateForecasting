using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NW.Shared.Validation;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowManagerTests
    {

        #region Fields

        private static TestCaseData[] slidingWindowManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            roundingFunction: null,
                            loggingAction: SlidingWindowManager.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            roundingFunction: SlidingWindowManager.DefaultRoundingFunction,
                            loggingAction: null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_02")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager().Create(
                                values: null,
                                roundingDigits: SlidingWindowManager.DefaultRoundingDigits
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager().Create(
                                values: ObjectMother.SlidingWindow01_Values,
                                roundingDigits: 16
                        )),
                typeof(ArgumentException),
                Shared.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("roundingDigits", "DefaultRoundingDigits")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow01_Values,
                ObjectMother.SlidingWindow01_RoundingDigits,
                ObjectMother.SlidingWindow01,
                new List<string>() {
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedValuesAre(ObjectMother.SlidingWindow01_Values),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(ObjectMother.SlidingWindow01)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(slidingWindowManagerExceptionTestCases))]
        public void SlidingWindowManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedSlidingWindowAndLogExpectedMessages_WhenProperArguments
            (List<double> values, uint roundingDigits, SlidingWindow expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            SlidingWindowManager slidingWindowManager
                = new SlidingWindowManager(
                        roundingFunction: SlidingWindowManager.DefaultRoundingFunction,
                        loggingAction: (message) => fakeLogger.Log(message)
                    );

            // Act
            SlidingWindow actual = slidingWindowManager.Create(values, roundingDigits);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True);
            Assert.That(fakeLogger.Messages, Is.EqualTo(expectedMessages));

        }

        [Test]
        public void SlidingWindowManager_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SlidingWindowManager actual = new SlidingWindowManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<SlidingWindowManager>());

            Assert.That(SlidingWindowManager.DefaultRoundingFunction, Is.InstanceOf<Func<double, uint, double>>());
            Assert.That(SlidingWindowManager.DefaultLoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(SlidingWindowManager.DefaultRoundingDigits, Is.InstanceOf<uint>());

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
