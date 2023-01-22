using System;
using NW.UnivariateForecastingClient.ApplicationSession;
using NUnit.Framework;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecastingClient.UnitTests
{
    [TestFixture]
    public class SessionManagerComponentsTests
    {

        #region Fields

        private static TestCaseData[] sessionManagerComponentsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SessionManagerComponents(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("doubleManager").Message
            ).SetArgDisplayNames($"{nameof(sessionManagerComponentsExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(sessionManagerComponentsExceptionTestCases))]
        public void SessionManagerComponents_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SessionManagerComponents_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SessionManagerComponents actual1 = new SessionManagerComponents();
            SessionManagerComponents actual2 = new SessionManagerComponents(new DoubleManager());

            // Assert
            Assert.IsInstanceOf<SessionManagerComponents>(actual1);
            Assert.IsInstanceOf<SessionManagerComponents>(actual2);

            Assert.IsInstanceOf<IDoubleManager>(actual1.DoubleManager);
            Assert.IsInstanceOf<IOptionValidator>(actual1.MinimumAccuracyValidator);

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
    Last Update: 22.01.2023
*/