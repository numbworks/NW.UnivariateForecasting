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
                ObjectMother.Observation1,
                ObjectMother.Observation1_ToString,
                ObjectMother.Observation1_ToStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                ObjectMother.NewObservation,
                ObjectMother.NewObservation_ToString,
                ObjectMother.NewObservation_ToStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02")

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