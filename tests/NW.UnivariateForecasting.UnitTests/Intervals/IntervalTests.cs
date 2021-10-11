﻿using NUnit.Framework;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class IntervalTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData( 
                ObjectMother.SlidingWindow1_Interval,
                ObjectMother.SlidingWindow1_Interval_ToString,
                ObjectMother.SlidingWindow1_Interval_ToStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindow1_SubInterval1,
                ObjectMother.SlidingWindow1_SubInterval1_ToString,
                ObjectMother.SlidingWindow1_SubInterval1_ToStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02"),

            new TestCaseData(
                ObjectMother.NewInterval,
                ObjectMother.NewInterval_ToString,
                ObjectMother.NewInterval_ToStringOnlyDates
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
    Last Update: 11.10.2021
*/