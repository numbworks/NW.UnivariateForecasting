using System;
using NUnit.Framework;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecastingComponentsTests
    {

        // Fields
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
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
                                    loggingAction: null
                            )
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingComponentsExceptionsTestCases)}_08")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(univariateForecastingComponentsExceptionsTestCases))]
        public void UnivariateForecastingComponents_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 29.04.2021

*/
