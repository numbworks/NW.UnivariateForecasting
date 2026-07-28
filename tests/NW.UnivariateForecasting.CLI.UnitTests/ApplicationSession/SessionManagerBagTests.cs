using NW.UnivariateForecasting.CLI.ApplicationSession;
using NW.UnivariateForecasting.CLI.ArgumentParsing;
using NUnit.Framework;

namespace NW.UnivariateForecasting.CLI.UnitTests.ApplicationSession
{
    [TestFixture]
    public class SessionManagerBagTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SessionManagerBag_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SessionManagerBag actual = new SessionManagerBag();

            // Assert
            Assert.That(actual, Is.InstanceOf<SessionManagerBag>());
            Assert.That(actual.RoundingDigitsValidator, Is.InstanceOf<RoundingDigitsValidator>());
            Assert.That(actual.ForecastingDenominatorValidator, Is.InstanceOf<ForecastingDenominatorValidator>());

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}