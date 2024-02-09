using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowTests
    {

        #region Fields

        private static TestCaseData[] slidingWindowExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindow(items: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("items").Message
                ).SetArgDisplayNames($"{nameof(slidingWindowExceptionTestCases)}_01")

        };
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow01,
                ObjectMother.SlidingWindow01_AsString,
                ObjectMother.SlidingWindow01_AsStringRolloutItems
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(slidingWindowExceptionTestCases))]
        public void SlidingWindow_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (SlidingWindow slidingWindow, string expected1, string expected2)
        {

            // Arrange
            // Act
            string actual1 = slidingWindow.ToString(false);
            string actual2 = slidingWindow.ToString(); // This tests both ToString(true) and ToString()

            // Assert
            Assert.That(actual1, Is.EqualTo(expected1));
            Assert.That(actual2, Is.EqualTo(expected2));

        }

        [Test]
        public void SlidingWindow_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SlidingWindow actual = new SlidingWindow(items: ObjectMother.SlidingWindow01_Items);

            // Assert
            Assert.That(actual, Is.InstanceOf<SlidingWindow>());
            Assert.That(actual.Items, Is.InstanceOf<List<SlidingWindowItem>>());

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
