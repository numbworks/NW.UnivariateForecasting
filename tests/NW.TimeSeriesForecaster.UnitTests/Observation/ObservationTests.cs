using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class ObservationTests
    {

        // Fields
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                MemberRepository.Observation1,
                MemberRepository.Observation1_ToString,
                MemberRepository.Observation1_ToStringOnlyDates
                ),
            new TestCaseData(
                MemberRepository.NewObservation,
                MemberRepository.NewObservation_ToString,
                MemberRepository.NewObservation_ToStringOnlyDates
                ),

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (Observation observation, string expected1, string expected2)
        {

            // Arrange
            // Act
            string actual1 = observation.ToString(false);
            string actual2 = observation.ToString(); // This tests both ToString(true) and ToString()

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