using System;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Intervals;
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
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                                    slidingWindowItemManager: null,
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                new ArgumentNullException("slidingWindowItemManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: null,
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: null,
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                new ArgumentNullException("intervalManager").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: null,
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: null,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                new ArgumentNullException("idCreationFunction").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_08"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_09"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_10"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_11"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_12"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_13"),

            new TestCaseData(
                new TestDelegate(
                        () => new UnivariateForecastingComponents(
                                    slidingWindowManager: new SlidingWindowManager(),
                                    slidingWindowItemManager: new SlidingWindowItemManager(),
                                    observationManager: new ObservationManager(),
                                    intervalManager: new IntervalManager(),
                                    fileManager: new FileManager(),
                                    idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                                    roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
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
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_14")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(univariateForecastingComponentsExceptionsTestCases))]
        public void UnivariateForecastingComponents_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/