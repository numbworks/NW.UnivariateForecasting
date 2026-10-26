using System;
using NW.Shared.Files;
using NW.Shared.Serialization;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Bags
{
    [TestFixture]
    public class ComponentBagTests
    {

        #region Fields

        private static TestCaseData[] componentBagDefaultValueTestCases =
        {
            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: null,
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.SlidingWindowManager, Is.InstanceOf<SlidingWindowManager>()))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_01"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: null,
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.ObservationManager, Is.InstanceOf<ObservationManager>()))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_02"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: null,
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.FileManager, Is.InstanceOf<FileManager>()))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_03"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: null,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.RoundingFunction, Is.EqualTo(ComponentBag.DefaultRoundingFunction)))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_04"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: null,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.LoggingAction, Is.EqualTo(ComponentBag.DefaultLoggingAction)))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_05"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: null,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.LoggingActionAsciiBanner, Is.EqualTo(ComponentBag.DefaultLoggingActionAsciiBanner)))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_06"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: null,
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.FilenameFactory, Is.InstanceOf<FilenameFactory>()))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_07"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: null,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.NowFunction, Is.EqualTo(ComponentBag.DefaultNowFunction)))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_08"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: null,
                                serializerFactory: new SerializerFactory(),
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.ForecastingInitManager, Is.InstanceOf<ForecastingInitManager>()))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_09"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: null,
                                versionFunction: ComponentBag.DefaultVersionFunction
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.SerializerFactory, Is.InstanceOf<SerializerFactory>()))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_10"),

            new TestCaseData(
                new Func<ComponentBag>(
                    () => new ComponentBag(
                                slidingWindowManager: new SlidingWindowManager(),
                                observationManager: new ObservationManager(),
                                fileManager: new FileManager(),
                                roundingFunction: ComponentBag.DefaultRoundingFunction,
                                loggingAction: ComponentBag.DefaultLoggingAction,
                                loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                filenameFactory: new FilenameFactory(),
                                nowFunction: ComponentBag.DefaultNowFunction,
                                forecastingInitManager: new ForecastingInitManager(),
                                serializerFactory: new SerializerFactory(),
                                versionFunction: null
                            )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.VersionFunction, Is.EqualTo(ComponentBag.DefaultVersionFunction)))
            ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_11")
        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentBagDefaultValueTestCases))]
        public void ComponentBag_ShouldUseDefaultValue_WhenParameterIsNull(Func<ComponentBag> initFunction, Action<ComponentBag> assertAction)
        {

            // Arrange
            // Act
            ComponentBag componentBag = initFunction();

            // Assert
            assertAction(componentBag);
        
        }

        [Test]
        public void ComponentBag_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            ComponentBag actual = new ComponentBag();

            // Assert
            Assert.That(actual, Is.InstanceOf<ComponentBag>());

            Assert.That(actual.ObservationManager, Is.InstanceOf<IObservationManager>());
            Assert.That(actual.SlidingWindowManager, Is.InstanceOf<ISlidingWindowManager>());
            Assert.That(actual.FileManager, Is.InstanceOf<IFileManager>());
            Assert.That(actual.RoundingFunction, Is.InstanceOf<Func<double, uint, double>>());
            Assert.That(actual.LoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(actual.LoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(actual.FilenameFactory, Is.InstanceOf<IFilenameFactory>());
            Assert.That(actual.NowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(actual.ForecastingInitManager, Is.InstanceOf<IForecastingInitManager>());
            Assert.That(actual.SerializerFactory, Is.InstanceOf<ISerializerFactory>());
            Assert.That(actual.VersionFunction, Is.InstanceOf<Func<string>>());

            Assert.That(ComponentBag.DefaultRoundingFunction, Is.InstanceOf<Func<double, uint, double>>());
            Assert.That(ComponentBag.DefaultLoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultLoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(ComponentBag.DefaultUTCNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(ComponentBag.DefaultLoggingActionDateFormat, Is.InstanceOf<string>());
            Assert.That(ComponentBag.DefaultVersionFunction, Is.InstanceOf<Func<string>>());

        }

        [TestCase(0.577777777777788D, (uint)0, 1)]
        [TestCase(0.577777777777788D, (uint)1, 0.6)]
        [TestCase(0.577777777777788D, (uint)2, 0.58)]
        [TestCase(0.577777777777788D, (uint)3, 0.578)]
        [TestCase(0.577777777777788D, (uint)4, 0.5778)]
        [TestCase(0.577777777777788D, (uint)5, 0.57778)]
        [TestCase(0.577777777777788D, (uint)6, 0.577778)]
        [TestCase(0.577777777777788D, (uint)7, 0.5777778)]
        [TestCase(0.577777777777788D, (uint)8, 0.57777778)]
        [TestCase(0.577777777777788D, (uint)9, 0.577777778)]
        [TestCase(0.577777777777788D, (uint)10, 0.5777777778)]
        [TestCase(0.577777777777788D, (uint)11, 0.57777777778)]
        [TestCase(0.577777777777788D, (uint)12, 0.577777777778)]
        [TestCase(0.577777777777788D, (uint)13, 0.5777777777778)]
        [TestCase(0.577777777777788D, (uint)14, 0.57777777777779)]
        [TestCase(0.577777777777788D, (uint)15, 0.577777777777788)]
        public void DefaultRoundingFunctionXDigits_ShouldReturnExpectedValue_WhenInvoked(double x, uint digits, double expected)
        {

            // Arrange
            // Act
            double actual = ComponentBag.DefaultRoundingFunction(x, digits);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        #endregion

        #region TearDown
        #endregion

    }
}