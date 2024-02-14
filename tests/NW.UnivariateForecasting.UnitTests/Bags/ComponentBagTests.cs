using System;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Filenames;
using NW.Shared.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.Shared.Serialization;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Bags
{
    [TestFixture]
    public class ComponentBagTests
    {

        #region Fields

        private static TestCaseData[] componentBagExceptionsTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: null,
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: null,
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("observationManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: null,
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: null,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: null,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: null,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingActionAsciiBanner").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: null,
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("asciiBannerManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: null,
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("filenameFactory").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_08"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: null,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("nowFunction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_09"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: null,
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("forecastingInitManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_10"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: null
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("serializerFactory").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionsTestCases)}_11")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentBagExceptionsTestCases))]
        public void ComponentBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
            Assert.That(actual.AsciiBannerManager, Is.InstanceOf<IAsciiBannerManager>());
            Assert.That(actual.FilenameFactory, Is.InstanceOf<IFilenameFactory>());
            Assert.That(actual.NowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(actual.ForecastingInitManager, Is.InstanceOf<IForecastingInitManager>());
            Assert.That(actual.SerializerFactory, Is.InstanceOf<ISerializerFactory>());

            Assert.That(ComponentBag.DefaultRoundingFunction, Is.InstanceOf<Func<double, uint, double>>());
            Assert.That(ComponentBag.DefaultLoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultLoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(ComponentBag.DefaultLoggingActionDateFormat, Is.InstanceOf<string>());

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

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/