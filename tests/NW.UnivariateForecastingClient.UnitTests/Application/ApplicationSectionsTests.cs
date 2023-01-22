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
    public class ApplicationSectionsTests
    {

        #region Fields

        private static TestCaseData[] applicationSectionsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationSections(
                                aboutManager: null,
                                sessionManager: new SessionManager(new LibraryBroker(), new SessionManagerComponents()))
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("aboutManager").Message
            ).SetArgDisplayNames($"{nameof(applicationSectionsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationSections(
                                aboutManager: new AboutManager(new LibraryBroker()),
                                sessionManager: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("sessionManager").Message
            ).SetArgDisplayNames($"{nameof(applicationSectionsExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(applicationSectionsExceptionTestCases))]
        public void ApplicationSections_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ApplicationSections_ShouldCreateAnObjectOfTypeApplicationSections_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationSections actual
                = new ApplicationSections(
                        aboutManager: new AboutManager(new LibraryBroker()),
                        sessionManager: new SessionManager(new LibraryBroker(), new SessionManagerComponents()));

            // Assert
            Assert.IsInstanceOf<ApplicationSections>(actual);
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
    Last Update: 22.01.2023
*/