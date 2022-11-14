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
                Intervals.ObjectMother.Interval_SixMonths,
                Intervals.ObjectMother.Interval_SixMonths_AsString,
                Intervals.ObjectMother.Interval_SixMonths_AsStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                Intervals.ObjectMother.Interval_SixMonths_SubInterval01,
                Intervals.ObjectMother.Interval_SixMonths_SubInterval01_AsString,
                Intervals.ObjectMother.Interval_SixMonths_SubInterval01_AsStringOnlyDates
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
    Last Update: 14.11.2022
*/