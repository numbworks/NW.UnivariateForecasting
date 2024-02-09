using System;
using System.Collections.Generic;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Serializations;
using NW.UnivariateForecastingClient.Shared;
using NW.UnivariateForecastingClient.UnitTests.Utilities;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.UnitTests.Shared
{
    [TestFixture]
    public class LibraryBrokerTests
    {

        #region Fields

        private static TestCaseData[] libraryBrokerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentBagFactory: null,
                                settingBagFactory: new SettingBagFactory(),
                                univariateForecasterFactory: new UnivariateForecasterFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("componentBagFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentBagFactory: new ComponentBagFactory(),
                                settingBagFactory: null,
                                univariateForecasterFactory: new UnivariateForecasterFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("settingBagFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentBagFactory: new ComponentBagFactory(),
                                settingBagFactory: new SettingBagFactory(),
                                univariateForecasterFactory: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("univariateForecasterFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(libraryBrokerExceptionTestCases))]
        public void LibraryBroker_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LibraryBroker_ShouldCreateAnObjectOfTypeLibraryBroker_WhenInvoked()
        {

            // Arrange
            // Act
            LibraryBroker actual = new LibraryBroker();

            // Assert
            Assert.That(actual, Is.InstanceOf<LibraryBroker>());
            Assert.That(LibraryBroker.Success, Is.InstanceOf<int>());
            Assert.That(LibraryBroker.Failure, Is.InstanceOf<int>());
            Assert.That(LibraryBroker.SeparatorLine, Is.InstanceOf<string>());
            Assert.That(LibraryBroker.ErrorMessageFormatter, Is.InstanceOf<Func<string, string>>());

        }

        [Test]
        public void RunSessionForecast_ShouldReturnFailureAndLogException_WhenForecastDataIsNull()
        {

            // Arrange
            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple();

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
                        univariateForecasterFactory: new UnivariateForecasterFactory()
                    );

            // Act
            int actual = libraryBroker.RunSessionForecast(null);

            // Assert
            Assert.That(actual, Is.EqualTo(LibraryBroker.Failure));
            Assert.That(
                    messages[0],
                    Is.EqualTo(LibraryBroker.ErrorMessageFormatter(new ArgumentNullException("forecastData").Message))
                    );
            Assert.That(
                    messagesAsciiBanner[0],
                    Is.EqualTo(LibraryBroker.SeparatorLine)
                    );

        }

        [Test]
        public void RunSessionForecast_ShouldReturnSuccess_WhenForecastDataIsNotNull()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>()
            {

                (fileName: "Init.json", content: UnivariateForecasting.UnitTests.Forecasts.ObjectMother.ForecastingInitBareMinimumAsJson_Content)

            };

            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
                        univariateForecasterFactory: new UnivariateForecasterFactory()
                    );

            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        saveSession: true,
                        folderPath: @"C:\unifor\",
                        roundingDigits: 2,
                        forecastingDenominator: 0.001
                    );

            // Act

            int actual = libraryBroker.RunSessionForecast(forecastData);

            // Assert
            Assert.That(actual, Is.EqualTo(LibraryBroker.Success));

            Assert.That(
                    messages[0],
                    Is.EqualTo("Attempting to load a 'ForecastingInit' object from: C:\\unifor\\Init.json.")
                    );
            Assert.That(
                    messagesAsciiBanner[0],
                    Is.EqualTo(LibraryBroker.SeparatorLine)
                    );
            Assert.That(
                    messagesAsciiBanner[1],
                    Is.EqualTo(new UnivariateForecaster().AsciiBanner)
                    );
            Assert.That(
                    messagesAsciiBanner[2],
                    Is.EqualTo(LibraryBroker.SeparatorLine)
                    );

        }

        [Test]
        public void RunSessionForecast_ShouldThrowExceptionAndReturnFailure_WhenInitFailsToLoad()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>()
            {

                (fileName: "Init.json", content: @"{ ""SomeField"": ""SomeValue"" }")

            };

            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
                        univariateForecasterFactory: new UnivariateForecasterFactory()
                    );

            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        saveSession: true,
                        folderPath: @"C:\unifor\",
                        roundingDigits: 2,
                        forecastingDenominator: 0.001
                    );

            Exception e
                = new Exception(UnivariateForecastingClient.Shared.MessageCollection.LoadingFileNameReturnedDefault("Init.json"));
            string expected = LibraryBroker.ErrorMessageFormatter(e.Message);

            // Act

            int actual = libraryBroker.RunSessionForecast(forecastData);

            // Assert
            Assert.That(actual, Is.EqualTo(LibraryBroker.Failure));
            Assert.That(messages[2], Is.EqualTo(expected));

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        private (List<string>, List<string>, ComponentBag) CreateTuple
            (List<(string fileName, string content)> readBehaviours = null)
        {

            List<string> messages = new List<string>();
            Action<string> fakeLoggingAction = (message) => messages.Add(message);

            List<string> messagesAsciiBanner = new List<string>();
            Action<string> fakeLoggingActionAsciiBanner = (message) => messagesAsciiBanner.Add(message);

            ComponentBag componentBag = new ComponentBag(

                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                        fileManager: new FakeFileManagerWithDynamicRead(readBehaviours), // When we pass null, it means the test won't use it.

                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: ComponentBag.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
            );

            return (messages, messagesAsciiBanner, componentBag);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/