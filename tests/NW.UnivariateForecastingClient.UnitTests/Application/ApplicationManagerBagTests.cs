using System;
using NW.UnivariateForecastingClient.Application;
using NW.UnivariateForecastingClient.ApplicationAbout;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.Shared;
using NW.UnivariateForecastingClient.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests.Application
{
    [TestFixture]
    public class ApplicationManagerBagTests
    {

        #region Fields

        private static TestCaseData[] applicationManagerBagExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManagerBag(
                                aboutManager: null,
                                sessionManager: new SessionManager(new LibraryBroker(), new SessionManagerBag()))
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("aboutManager").Message
            ).SetArgDisplayNames($"{nameof(applicationManagerBagExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManagerBag(
                                aboutManager: new AboutManager(new LibraryBroker()),
                                sessionManager: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("sessionManager").Message
            ).SetArgDisplayNames($"{nameof(applicationManagerBagExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(applicationManagerBagExceptionTestCases))]
        public void ApplicationManagerBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ApplicationManagerBag_ShouldCreateAnObjectOfTypeApplicationSections_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationManagerBag actual
                = new ApplicationManagerBag(
                        aboutManager: new AboutManager(new LibraryBroker()),
                        sessionManager: new SessionManager(new LibraryBroker(), new SessionManagerBag()));

            // Assert
            Assert.IsInstanceOf<ApplicationManagerBag>(actual);
            Assert.IsInstanceOf<IAboutManager>(actual.AboutManager);
            Assert.IsInstanceOf<ISessionManager>(actual.SessionManager);

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