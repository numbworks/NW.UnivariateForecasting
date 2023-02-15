using NW.UnivariateForecasting.Observations;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    [TestFixture]
    public class ObservationTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                ObjectMother.Observation01,
                ObjectMother.Observation01_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked(Observation observation, string expected1)
        {

            // Arrange
            // Act
            string actual = observation.ToString();

            // Assert
            Assert.AreEqual(expected1, actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/