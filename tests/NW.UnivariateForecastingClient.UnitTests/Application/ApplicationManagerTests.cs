﻿using System;
using NW.UnivariateForecastingClient.Application;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.Shared;
using NW.UnivariateForecastingClient.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests.Application
{
    [TestFixture]
    public class ApplicationManagerTests
    {

        #region Fields

        private static TestCaseData[] applicationManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManager(
                                libraryBroker: null,
                                applicationManagerBagFactory: new ApplicationManagerBagFactory(),
                                sessionManagerBag: new SessionManagerBag())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("libraryBroker").Message
            ).SetArgDisplayNames($"{nameof(applicationManagerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManager(
                                libraryBroker: new LibraryBroker(),
                                applicationManagerBagFactory: null,
                                sessionManagerBag: new SessionManagerBag())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("applicationManagerBagFactory").Message
            ).SetArgDisplayNames($"{nameof(applicationManagerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManager(
                                libraryBroker: new LibraryBroker(),
                                applicationManagerBagFactory: new ApplicationManagerBagFactory(),
                                sessionManagerBag: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("sessionManagerBag").Message
            ).SetArgDisplayNames($"{nameof(applicationManagerExceptionTestCases)}_03")

        };
        private static TestCaseData[] executeTestCases =
        {

            new TestCaseData(
                new string[] { "about" },
                (int)ExitCodes.Success
            ).SetArgDisplayNames($"{nameof(applicationManagerExceptionTestCases)}_01"),

            new TestCaseData(
                new string[] { "session" },
                (int)ExitCodes.Success
            ).SetArgDisplayNames($"{nameof(applicationManagerExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(applicationManagerExceptionTestCases))]
        public void ApplicationManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ApplicationManager_ShouldCreateAnObjectOfTypeApplicationManager_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationManager actual = new ApplicationManager();

            // Assert
            Assert.IsInstanceOf<ApplicationManager>(actual);

        }

        [TestCaseSource(nameof(executeTestCases))]
        public void Execute_ShouldReturnExpectedExitCode_WhenInvoked(string[] args, int expected)
        {

            // Arrange
            // Act
            int actual = new ApplicationManager().Execute(args);

            // Assert
            Assert.AreEqual(expected, actual);

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