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
                ObjectMother.Observation01_WithoutInitCE,
                ObjectMother.Observation01_WithoutInitCE_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked(Observation observation, string expected)
        {

            // Arrange
            // Act
            string actual = observation.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void Observation_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            Observation actual = new Observation(coefficient: 0.82, error: 0.22, nextValue: 519.23);

            // Assert
            Assert.That(actual, Is.InstanceOf<Observation>());
            Assert.That(actual.Coefficient, Is.InstanceOf<double>());
            Assert.That(actual.Error, Is.InstanceOf<double>());
            Assert.That(actual.NextValue, Is.InstanceOf<double>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/