using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class IntervalTests
    {

        // Fields
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData( 
                MemberRepository.SlidingWindow1_Interval,
                MemberRepository.SlidingWindow1_Interval_ToString,
                MemberRepository.SlidingWindow1_Interval_ToStringOnlyDates
                ),
            new TestCaseData(
                MemberRepository.SlidingWindow1_SubInterval1,
                MemberRepository.SlidingWindow1_SubInterval1_ToString,
                MemberRepository.SlidingWindow1_SubInterval1_ToStringOnlyDates
                ),
            new TestCaseData(
                MemberRepository.NewInterval,
                MemberRepository.NewInterval_ToString,
                MemberRepository.NewInterval_ToStringOnlyDates
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
