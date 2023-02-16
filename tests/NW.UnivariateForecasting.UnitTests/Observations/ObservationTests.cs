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

        [Test]
        public void Observation_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            Observation actual = new Observation(coefficient: 0.82, error: 0.22, nextValue: 519.23);

            // Assert
            Assert.IsInstanceOf<Observation>(actual);
            Assert.IsInstanceOf<double>(actual.Coefficient);
            Assert.IsInstanceOf<double>(actual.Error);
            Assert.IsInstanceOf<double>(actual.NextValue);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/