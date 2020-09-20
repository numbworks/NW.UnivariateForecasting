﻿using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class IntervalTests
    {

        // Fields
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData( 
                ObjectMother.SlidingWindow1_Interval,
                ObjectMother.SlidingWindow1_Interval_ToString,
                ObjectMother.SlidingWindow1_Interval_ToStringOnlyDates
                ),
            new TestCaseData(
                ObjectMother.SlidingWindow1_SubInterval1,
                ObjectMother.SlidingWindow1_SubInterval1_ToString,
                ObjectMother.SlidingWindow1_SubInterval1_ToStringOnlyDates
                ),
            new TestCaseData(
                ObjectMother.NewInterval,
                ObjectMother.NewInterval_ToString,
                ObjectMother.NewInterval_ToStringOnlyDates
                )

        };

        // SetUp
        // Tests
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

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 09.09.2020

*/
