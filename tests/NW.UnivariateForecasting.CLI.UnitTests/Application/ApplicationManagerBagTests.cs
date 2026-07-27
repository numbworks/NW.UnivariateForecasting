using System;
using NW.UnivariateForecasting.CLI.Application;
using NW.UnivariateForecasting.CLI.ApplicationAbout;
using NW.UnivariateForecasting.CLI.ApplicationSession;
using NW.UnivariateForecasting.CLI.Shared;
using NW.UnivariateForecasting.CLI.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecasting.CLI.UnitTests.Application
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
            Assert.That(actual, Is.InstanceOf<ApplicationManagerBag>());
            Assert.That(actual.AboutManager, Is.InstanceOf<IAboutManager>());
            Assert.That(actual.SessionManager, Is.InstanceOf<ISessionManager>());

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
    Last Update: 09.02.2024
*/