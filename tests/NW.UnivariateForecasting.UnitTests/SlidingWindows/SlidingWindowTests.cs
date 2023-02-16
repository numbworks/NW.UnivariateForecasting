using NUnit.Framework;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowTests
    {

        #region Fields

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

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (SlidingWindow slidingWindow, string expected1, string expected2)
        {

            // Arrange
            // Act
            string actual1 = slidingWindow.ToString(false);
            string actual2 = slidingWindow.ToString(); // This tests both ToString(true) and ToString()

            // Assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);

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
