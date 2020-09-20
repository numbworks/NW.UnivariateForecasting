using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowTests
    {

        // Fields
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                MemberRepository.NewSlidingWindow,
                MemberRepository.NewSlidingWindow_ToString,
                MemberRepository.NewSlidingWindow_ToStringRolloutItems
                ),
            new TestCaseData(
                MemberRepository.SlidingWindow1,
                MemberRepository.SlidingWindow1_ToString,
                MemberRepository.SlidingWindow1_ToStringRolloutItems
                )

        };

        // SetUp
        // Tests
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

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.09.2020

*/
