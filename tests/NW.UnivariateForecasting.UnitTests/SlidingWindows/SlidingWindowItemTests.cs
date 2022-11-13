using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowItemTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindowItem_Empty,
                SlidingWindows.ObjectMother.SlidingWindowItem_Empty_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                Utilities.ObjectMother.Shared_SlidingWindow1_Item1,
                Utilities.ObjectMother.Shared_SlidingWindow1_Item1_String
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
    Last Update: 13.11.2022
*/