using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowItemTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindowItem_Empty,
                ObjectMother.SlidingWindowItem_Empty_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindow01_Item01,
                ObjectMother.SlidingWindow01_Item01_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (SlidingWindowItem slidingWindowItem, string expected)
        {

            // Arrange
            // Act
            string actual = slidingWindowItem.ToString();

            // Assert
            Assert.AreEqual(expected, actual);

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