using NW.UnivariateForecasting.Observations;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class ObservationTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                Observations.ObjectMother.Observation01,
                Observations.ObjectMother.Observation01_AsString,
                Observations.ObjectMother.Observation01_AsStringOnlyDates
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                Observations.ObjectMother.Observation_Empty,
                Observations.ObjectMother.Observation_Empty_AsString,
                Observations.ObjectMother.Observation_Empty_AsStringOnlyDates
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
    Last Update: 14.11.2022
*/