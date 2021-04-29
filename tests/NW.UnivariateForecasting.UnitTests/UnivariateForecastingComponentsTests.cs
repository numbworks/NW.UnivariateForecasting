using System;
using NUnit.Framework;

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
                ),

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
                ),

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
                ),

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
                ),

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
                ),

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
                ),

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
                ),

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
                )

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
