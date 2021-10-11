using NUnit.Framework;
using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class ObservationTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                ObjectMother.Shared_Observation1,
                ObjectMother.Shared_Observation1_String,
                ObjectMother.Shared_Observation1_StringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                ObjectMother.Observation_Empty_Object,
                ObjectMother.Observation_Empty_String,
                ObjectMother.Observation_Empty_StringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

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

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/