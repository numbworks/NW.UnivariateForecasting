﻿using System;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.Serializations;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingComponentsTests
    {

        #region Fields

        private static TestCaseData[] univariateForecastingComponentsExceptionsTestCases =
        {

            new TestCaseData(
                new TestDelegate( 
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: null,
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: null,
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("observationManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: null,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: null,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: null,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: null,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingActionAsciiBanner").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: null,
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("asciiBannerManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: null,
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("filenameFactory").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_08"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: null,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("nowFunction").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_09"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: null,
                                    serializerFactory: new SerializerFactory()
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("forecastingInitManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_10"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    observationManager: new ObservationManager(),
                                    fileManager: new FileManager(),
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits,
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction,
                                    loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                                    asciiBannerManager: new AsciiBannerManager(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                                    forecastingInitManager: new ForecastingInitManager(),
                                    serializerFactory: null
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("serializerFactory").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_11")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(univariateForecastingComponentsExceptionsTestCases))]
        public void UnivariateForecastingComponents_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void UnivariateForecastingComponents_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            UnivariateForecastingComponents actual = new UnivariateForecastingComponents();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingComponents>(actual);

            Assert.IsInstanceOf<IObservationManager>(actual.ObservationManager);
            Assert.IsInstanceOf<ISlidingWindowManager>(actual.SlidingWindowManager);
            Assert.IsInstanceOf<IFileManager>(actual.FileManager);
            Assert.IsInstanceOf<Func<double, double>>(actual.RoundingFunction);
            Assert.IsInstanceOf<Action<string>>(actual.LoggingAction);
            Assert.IsInstanceOf<Action<string>>(actual.LoggingActionAsciiBanner);
            Assert.IsInstanceOf<IAsciiBannerManager>(actual.AsciiBannerManager);
            Assert.IsInstanceOf<IFilenameFactory>(actual.FilenameFactory);
            Assert.IsInstanceOf<Func<DateTime>>(actual.NowFunction);
            Assert.IsInstanceOf<IForecastingInitManager>(actual.ForecastingInitManager);
            Assert.IsInstanceOf<ISerializerFactory>(actual.SerializerFactory);

            Assert.IsInstanceOf<Func<double, uint, double>>(UnivariateForecastingComponents.DefaultRoundingFunctionXDigits);
            Assert.IsInstanceOf<Func<double, double>>(UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits);
            Assert.IsInstanceOf<Func<double, double>>(UnivariateForecastingComponents.DefaultRoundingFunctionDoNothing);

            Assert.IsInstanceOf<Action<string>>(UnivariateForecastingComponents.DefaultLoggingAction);
            Assert.IsInstanceOf<Action<string>>(UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner);
            Assert.IsInstanceOf<Func<DateTime>>(UnivariateForecastingComponents.DefaultNowFunction);

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
            double actual = UnivariateForecastingComponents.DefaultRoundingFunctionXDigits(x, digits);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void DefaultRoundingFunctionTwoDigits_ShouldReturnExpectedValue_WhenInvoked()
        {

            // Arrange
            double x = 0.577777777777788D;
            double expected = 0.58;

            // Act
            double actual = UnivariateForecastingComponents.DefaultRoundingFunctionTwoDigits(x);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void DefaultRoundingFunctionDoNothing_ShouldReturnExpectedValue_WhenInvoked()
        {

            // Arrange
            double x = 0.577777777777788D;
            double expected = 0.577777777777788;

            // Act
            double actual = UnivariateForecastingComponents.DefaultRoundingFunctionDoNothing(x);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.03.2023
*/