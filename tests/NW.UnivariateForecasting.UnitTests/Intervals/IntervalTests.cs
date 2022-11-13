using NW.UnivariateForecasting.Intervals;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class IntervalTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
                Utilities.ObjectMother.Shared_SlidingWindow1_Interval_String,
                Utilities.ObjectMother.Shared_SlidingWindow1_Interval_StringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                Utilities.ObjectMother.Shared_SlidingWindow1_SubInterval1,
                Utilities.ObjectMother.Shared_SlidingWindow1_SubInterval1_String,
                Utilities.ObjectMother.Shared_SlidingWindow1_SubInterval1_StringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02"),

            new TestCaseData(
                Intervals.ObjectMother.Interval_Empty,
                Intervals.ObjectMother.Interval_Empty_AsString,
                Intervals.ObjectMother.Interval_Empty_AsStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (Interval interval, string expected1, string expected2)
        {

            // Arrange
            // Act
            string actual1 = interval.ToString(false);
            string actual2 = interval.ToString(true);

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
    Last Update: 13.11.2022
*/