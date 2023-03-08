using System;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.UnitTests.Utilities;
using NUnit.Framework;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecastingClient.UnitTests.ApplicationSession
{
    [TestFixture]
    public class SessionManagerComponentsTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SessionManagerComponents_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SessionManagerComponents actual = new SessionManagerComponents();

            // Assert
            Assert.IsInstanceOf<SessionManagerComponents>(actual);
            Assert.IsInstanceOf<IOptionValidator>(actual.RoundingDigitsValidator);

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
    Last Update: 08.03.2023
*/