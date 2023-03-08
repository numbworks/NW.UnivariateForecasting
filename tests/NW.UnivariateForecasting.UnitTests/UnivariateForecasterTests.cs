using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.Serializations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecasterTests
    {

        #region Fields

        private static TestCaseData[] univariateForecasterExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            settings: null,
                            components: new UnivariateForecastingComponents())),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ).SetArgDisplayNames($"{nameof(univariateForecasterExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            settings: new UnivariateForecastingSettings(),
                            components: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("components").Message
                ).SetArgDisplayNames($"{nameof(univariateForecasterExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster().Forecast(init: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("init").Message
                ).SetArgDisplayNames($"{nameof(forecastExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster().Forecast(
                            init: new ForecastingInit(
                                    observationName: null, 
                                    values: new List<double>() { 58.5 },
                                    coefficient: null,
                                    error: null,
                                    steps: 1
                                ))),
                typeof(ArgumentException),
                "'Values' can't be less than '2'."
                ).SetArgDisplayNames($"{nameof(forecastExceptionTestCases)}_02")

        };
        private static TestCaseData[] loadInitOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecaster().LoadInitOrDefault(jsonFile: (IFileInfoAdapter)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("jsonFile").Message
            ).SetArgDisplayNames($"{nameof(loadInitOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecaster().LoadInitOrDefault(jsonFile: Files.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.ProvidedPathDoesntExist(Files.ObjectMother.FileInfoAdapterDoesntExist)
            ).SetArgDisplayNames($"{nameof(loadInitOrDefaultExceptionTestCases)}_02")

        };
        private static TestCaseData[] convertExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecaster().Convert(filePath: null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("filePath").Message
            ).SetArgDisplayNames($"{nameof(convertExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void UnivariateForecaster_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            UnivariateForecaster actual = new UnivariateForecaster();

            // Assert
            Assert.IsInstanceOf<UnivariateForecaster>(actual);

            Assert.IsInstanceOf<string>(actual.AsciiBanner);
            Assert.IsInstanceOf<string>(actual.Version);

        }

        [TestCaseSource(nameof(univariateForecasterExceptionTestCases))]
        public void UnivariateForecaster_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastExceptionTestCases))]
        public void Forecast_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LogAsciiBanner_ShouldLogAsExpected_WhenInvoked()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingActionAsciiBanner = (message) => actualLogMessages.Add(message);

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            List<string> expectedMessages = new List<string>()
            {

                new AsciiBannerManager().Create(univariateForecaster.Version)

            };

            // Act            
            univariateForecaster.LogAsciiBanner();

            // Assert
            Assert.AreEqual(expectedMessages, actualLogMessages);

        }


        [TestCaseSource(nameof(loadInitOrDefaultExceptionTestCases))]
        public void LoadInitOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadInitOrDefault_ShouldReturnExpectedForecastingInit_WhenProperJsonFileContent()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FakeFileManager(Forecasts.ObjectMother.ForecastingInitBareMinimumAsJson_Content),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\ForecastingInit.json");
            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToLoadObjectFrom(typeof(ForecastingInit), fakeJsonFile),
                UnivariateForecasting.Forecasts.MessageCollection.ObjectSuccessfullyLoaded(typeof(ForecastingInit))

            };

            // Act
            ForecastingInit actual = univariateForecaster.LoadInitOrDefault(fakeJsonFile);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(
                        obj1: Forecasts.ObjectMother.ForecastingInit_BareMinimum, 
                        obj2: actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void LoadInitOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FakeFileManager("Unproper Json content"),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\ForecastingInit.json");
            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToLoadObjectFrom(typeof(ForecastingInit), fakeJsonFile),
                UnivariateForecasting.Forecasts.MessageCollection.ObjectFailedToLoad(typeof(ForecastingInit))

            };

            // Act
            ForecastingInit actual = univariateForecaster.LoadInitOrDefault(fakeJsonFile);

            // Assert
            Assert.AreEqual(default(ForecastingInit), actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void SaveSession_ShouldLogExpectedMessage_WhenWriteAllTextThrowsException()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: Forecasts.ObjectMother.ForecastingSessionSingleWithCEAsJson_Content,
                                                writeExceptionMessage: "A random write-to-disk issue."),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: FakeNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"unifor_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToSaveObjectAs(typeof(ForecastingSession), fakeJsonFile),
                UnivariateForecasting.Forecasts.MessageCollection.ObjectFailedToSave(typeof(ForecastingSession))

            };

            // Act
            univariateForecaster.SaveSession(
                                    session: Forecasts.ObjectMother.ForecastingSession_SingleWithCE,
                                    folderPath: folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void SaveSession_ShouldSaveSession_WhenInvoked()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FakeFileManager(
                                                content: Forecasts.ObjectMother.ForecastingSessionSingleWithCEAsJson_Content),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: FakeNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"unifor_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToSaveObjectAs(typeof(ForecastingSession), fakeJsonFile),
                UnivariateForecasting.Forecasts.MessageCollection.ObjectSuccessfullySaved(typeof(ForecastingSession))

            };

            // Act
            univariateForecaster.SaveSession(
                                    session: Forecasts.ObjectMother.ForecastingSession_SingleWithCE,
                                    folderPath: folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [Test]
        public void Create_ShouldThrowExpectedException_WhenProvidedTypeIsNotSupported()
        {

            // Arrange
            UnivariateForecaster univariateForecaster = new UnivariateForecaster();

            try
            {

                // Act
                IFileInfoAdapter actual
                    = ObjectMother.CallPrivateGenericMethod<UnivariateForecaster, IFileInfoAdapter>(
                            obj: univariateForecaster,
                            methodName: "Create",
                            args: new object[] { @"C:\", DateTime.Now },
                            methodType: typeof(Observation) // "Observation" is a not supported type.
                        );

            }
            catch (TargetInvocationException e)
            {

                // Assert
                Assert.IsInstanceOf<Exception>(e.InnerException);
                Assert.AreEqual(
                    UnivariateForecasting.Forecasts.MessageCollection.ThereIsNoStrategyOutOfType(typeof(Observation)),
                    e.InnerException.Message);

            }

        }

        [TestCaseSource(nameof(convertExceptionTestCases))]
        public void Convert_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);


        [Test]
        public void Forecast_ShouldReturnExpectedSession_WhenBareMinimum()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(

                        slidingWindowManager: new SlidingWindowManager(
                                roundingFunction: SlidingWindowManager.DefaultRoundingFunction,
                                loggingAction: fakeLoggingAction
                            ),
                        observationManager: new ObservationManager(
                                roundingFunction: ObservationManager.DefaultRoundingFunction,
                                loggingAction: fakeLoggingAction
                            ),
                        loggingAction: fakeLoggingAction,
                        nowFunction: FakeNowFunction,

                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()

                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            ForecastingInit init = Forecasts.ObjectMother.ForecastingInit_BareMinimum;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_BareMinimum;

            List<SlidingWindowItem> slidingWindowItems = new List<SlidingWindowItem>()
            {

                new SlidingWindowItem(id: 1, X_Actual: 58.5, Y_Forecasted: 615.26),
                new SlidingWindowItem(id: 2, X_Actual: 615.26, Y_Forecasted: null)

            };
            SlidingWindow slidingWindow = new SlidingWindow(items: slidingWindowItems);

            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToForecast,
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedFolderPathIs(settings.FolderPath),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedForecastingDenominatorIs(settings.ForecastingDenominator),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedRoundingDigitsAre(settings.RoundingDigits),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationNameIs(init.ObservationName),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedValuesAre(init.Values.Count),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedCoefficientIs(init.Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedErrorIs(init.Error),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedStepsAre(init.Steps),
                UnivariateForecasting.Forecasts.MessageCollection.ProcessingStepNr(1),

                UnivariateForecasting.SlidingWindows.MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedValuesAre(init.Values),
                UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(slidingWindow),

                UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(slidingWindow),
                UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(expected.Observations[0]),

                UnivariateForecasting.Forecasts.MessageCollection.ObservationCoefficientIs(expected.Observations[0].Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationErrorIs(expected.Observations[0].Error),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationNextValueIs(expected.Observations[0].NextValue),

                UnivariateForecasting.Forecasts.MessageCollection.ForecastSuccessfullyCompleted

            };

            // Act
            ForecastingSession actual = univariateForecaster.Forecast(init: init);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(obj1: expected, obj2: actual));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void Forecast_ShouldReturnExpectedSession_WhenSingleWithCE()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(

                        slidingWindowManager: new SlidingWindowManager(
                                roundingFunction: SlidingWindowManager.DefaultRoundingFunction,
                                loggingAction: SlidingWindowManager.DefaultLoggingAction // We skip these messages.
                            ),
                        observationManager: new ObservationManager(
                                roundingFunction: ObservationManager.DefaultRoundingFunction,
                                loggingAction: SlidingWindowManager.DefaultLoggingAction // We skip these messages.
                            ),
                        loggingAction: fakeLoggingAction,
                        nowFunction: FakeNowFunction,

                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            ForecastingInit init = Forecasts.ObjectMother.ForecastingInit_SingleWithCE;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_SingleWithCE;

            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToForecast,
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedFolderPathIs(settings.FolderPath),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedForecastingDenominatorIs(settings.ForecastingDenominator),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedRoundingDigitsAre(settings.RoundingDigits),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationNameIs(init.ObservationName),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedValuesAre(init.Values.Count),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedCoefficientIs(init.Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedErrorIs(init.Error),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedStepsAre(init.Steps),
                UnivariateForecasting.Forecasts.MessageCollection.ProcessingStepNr(1),

                UnivariateForecasting.Forecasts.MessageCollection.ObservationCoefficientIs(expected.Observations[0].Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationErrorIs(expected.Observations[0].Error),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationNextValueIs(expected.Observations[0].NextValue),

                UnivariateForecasting.Forecasts.MessageCollection.ForecastSuccessfullyCompleted

            };

            // Act
            ForecastingSession actual = univariateForecaster.Forecast(init: init);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(obj1: expected, obj2: actual));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void Forecast_ShouldReturnExpectedSession_WhenSingleWithoutCE()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(

                        slidingWindowManager: new SlidingWindowManager(
                                roundingFunction: SlidingWindowManager.DefaultRoundingFunction,
                                loggingAction: SlidingWindowManager.DefaultLoggingAction // We skip these messages.
                            ),
                        observationManager: new ObservationManager(
                                roundingFunction: ObservationManager.DefaultRoundingFunction,
                                loggingAction: SlidingWindowManager.DefaultLoggingAction // We skip these messages.
                            ),
                        loggingAction: fakeLoggingAction,
                        nowFunction: FakeNowFunction,

                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            ForecastingInit init = Forecasts.ObjectMother.ForecastingInit_SingleWithoutCE;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_SingleWithoutCE;

            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToForecast,
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedFolderPathIs(settings.FolderPath),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedForecastingDenominatorIs(settings.ForecastingDenominator),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedRoundingDigitsAre(settings.RoundingDigits),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationNameIs(init.ObservationName),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedValuesAre(init.Values.Count),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedCoefficientIs(init.Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedErrorIs(init.Error),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedStepsAre(init.Steps),
                UnivariateForecasting.Forecasts.MessageCollection.ProcessingStepNr(1),

                UnivariateForecasting.Forecasts.MessageCollection.ObservationCoefficientIs(expected.Observations[0].Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationErrorIs(expected.Observations[0].Error),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationNextValueIs(expected.Observations[0].NextValue),

                UnivariateForecasting.Forecasts.MessageCollection.ForecastSuccessfullyCompleted

            };

            // Act
            ForecastingSession actual = univariateForecaster.Forecast(init: init);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(obj1: expected, obj2: actual));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void Forecast_ShouldReturnExpectedSession_WhenDoubleWithCE()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(
                            forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                            folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                            roundingDigits: 2
                        );

            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(

                        slidingWindowManager: new SlidingWindowManager(
                                roundingFunction: SlidingWindowManager.DefaultRoundingFunction,
                                loggingAction: SlidingWindowManager.DefaultLoggingAction // We skip these messages.
                            ),
                        observationManager: new ObservationManager(
                                roundingFunction: ObservationManager.DefaultRoundingFunction,
                                loggingAction: SlidingWindowManager.DefaultLoggingAction // We skip these messages.
                            ),
                        loggingAction: fakeLoggingAction,
                        nowFunction: FakeNowFunction,

                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );

            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings, components);

            ForecastingInit init = Forecasts.ObjectMother.ForecastingInit_DoubleWithCE;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_DoubleWithCE;

            List<string> expectedLogMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.AttemptingToForecast,
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedFolderPathIs(settings.FolderPath),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedForecastingDenominatorIs(settings.ForecastingDenominator),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedRoundingDigitsAre(settings.RoundingDigits),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationNameIs(init.ObservationName),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedValuesAre(init.Values.Count),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedCoefficientIs(init.Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedErrorIs(init.Error),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedStepsAre(init.Steps),
                UnivariateForecasting.Forecasts.MessageCollection.ProcessingStepNr(1),

                UnivariateForecasting.Forecasts.MessageCollection.ObservationCoefficientIs(expected.Observations[0].Coefficient),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationErrorIs(expected.Observations[0].Error),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationNextValueIs(expected.Observations[0].NextValue),

                UnivariateForecasting.Forecasts.MessageCollection.ProcessingStepNr(2),
                UnivariateForecasting.Forecasts.MessageCollection.ObservationNextValueIs(expected.Observations[1].NextValue),

                UnivariateForecasting.Forecasts.MessageCollection.ForecastSuccessfullyCompleted

            };

            // Act
            ForecastingSession actual = univariateForecaster.Forecast(init : init);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(obj1: expected, obj2: actual));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/