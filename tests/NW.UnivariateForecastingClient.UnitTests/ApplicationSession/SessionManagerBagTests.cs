using NW.UnivariateForecastingClient.ApplicationSession;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests.ApplicationSession
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
            Assert.IsInstanceOf<SessionManagerBag>(actual);
            Assert.IsInstanceOf<RoundingDigitsValidator>(actual.RoundingDigitsValidator);
            Assert.IsInstanceOf<ForecastingDenominatorValidator>(actual.ForecastingDenominatorValidator);

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/