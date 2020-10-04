﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecasterTests
    {

        // Fields
        private static TestCaseData[] constructorExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            null,
                            new SlidingWindowManager(new UnivariateForecastingSettings()),
                            new SlidingWindowItemManager(),
                            new ObservationManager(new UnivariateForecastingSettings()),
                            new IntervalManager()
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            new UnivariateForecastingSettings(),
                            null,
                            new SlidingWindowItemManager(),
                            new ObservationManager(new UnivariateForecastingSettings()),
                            new IntervalManager()
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            new UnivariateForecastingSettings(),
                            new SlidingWindowManager(new UnivariateForecastingSettings()),
                            null,
                            new ObservationManager(new UnivariateForecastingSettings()),
                            new IntervalManager()
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowItemManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            new UnivariateForecastingSettings(),
                            new SlidingWindowManager(new UnivariateForecastingSettings()),
                            new SlidingWindowItemManager(),
                            null,
                            new IntervalManager()
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("observationManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            new UnivariateForecastingSettings(),
                            new SlidingWindowManager(new UnivariateForecastingSettings()),
                            new SlidingWindowItemManager(),
                            new ObservationManager(new UnivariateForecastingSettings()),
                            null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                )

        };
        private static TestCaseData[] forecastExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.Forecast(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                )

        };
        private static TestCaseData[] extractXActualValuesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ExtractXActualValues(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                )

        };
        private static TestCaseData[] extractStartDatesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ExtractStartDates(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                )

        };
        private static TestCaseData[] combineExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.Combine(
                            ObjectMother.SlidingWindow1,
                            ObjectMother.Observation_InvalidDueOfNullName
                        )),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(Observation))
                ),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.Combine(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval,
                            ObjectMother.Observation1
                        )),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                )

        };
        private static TestCaseData[] forecastAndCombineExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastAndCombine(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval,
                            1
                        )),
                typeof(Exception),
                MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastAndCombine(
                            ObjectMother.SlidingWindow1,
                            0
                        )),
                typeof(Exception),
                MessageCollection.VariableCantBeLessThanOne.Invoke("steps")
                )

        };
        private static TestCaseData[] forecastNextValueExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastNextValue(null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastNextValue(new List<double>() { })),
                typeof(Exception),
                MessageCollection.VariableContainsZeroItems.Invoke("values")
                )

        };
        private static TestCaseData[] forecastTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                null,
                null,
                ObjectMother.Observation1,
                new List<string>() {
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1)
                    }
                ),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.Observation1.C,
                ObjectMother.Observation1.E,
                ObjectMother.Observation1,
                new List<string>() {
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1)
                    }
                ),

        };
        private static TestCaseData[] extractXActualValuesTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.SlidingWindow1_Values,
                new List<string>() {
                    MessageCollection.ExtractingValuesOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ValuesHaveBeenSuccessfullyExtracted.Invoke(ObjectMother.SlidingWindow1_Values)
                    }
                )

        };
        private static TestCaseData[] extractStartDatesTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.SlidingWindow1_StartDates,
                new List<string>() {
                    MessageCollection.ExtractingStartDatesOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.StartDatesHaveBeenSuccessfullyExtracted.Invoke(ObjectMother.SlidingWindow1_StartDates)
                    }
                )

        };
        private static TestCaseData[] combineTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.Observation1,
                ObjectMother.FaCSteps1_Final,
                new List<string>() {
                    MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final)
                    }
                )

        };
        private static TestCaseData[] forecastAndCombineTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                (uint)1,
                null,
                null,
                ObjectMother.FaCSteps1_Final,
                new List<Observation>()
                {
                    ObjectMother.Observation1
                },
                new List<string>() {
                    MessageCollection.RunningForecastAndCombineForSteps.Invoke(1),
                    MessageCollection.ForecastingAndCombineForStepNr.Invoke(1),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1),
                    MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final),
                    MessageCollection.ForecastAndCombineSuccessfullyRunForSteps.Invoke(1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final)
                    }
                ),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                (uint)1,
                ObjectMother.Observation1.C,
                ObjectMother.Observation1.E,
                ObjectMother.FaCSteps1_Final,
                new List<Observation>()
                {
                    ObjectMother.Observation1
                },
                new List<string>() {
                    MessageCollection.RunningForecastAndCombineForSteps.Invoke(1),
                    MessageCollection.ForecastingAndCombineForStepNr.Invoke(1),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1),
                    MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final),
                    MessageCollection.ForecastAndCombineSuccessfullyRunForSteps.Invoke(1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final)
                    }
                ),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                (uint)3,
                null,
                null,
                ObjectMother.FaCSteps3_Final,
                new List<Observation>()
                {
                    ObjectMother.Observation1,
                    ObjectMother.FaCSteps3_MidwayObservation_1,
                    ObjectMother.FaCSteps3_MidwayObservation_2
                },
                new List<string>() {
                    MessageCollection.RunningForecastAndCombineForSteps.Invoke(3),
                    // i = 1
                    MessageCollection.ForecastingAndCombineForStepNr.Invoke(1),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1),
                    MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_1),
                    // i = 2
                    MessageCollection.ForecastingAndCombineForStepNr.Invoke(2),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwayObservation_1),
                    MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.ProvidedSlidingWindowIs.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_1),
                    MessageCollection.ProvidedObservationIs.Invoke(ObjectMother.FaCSteps3_MidwayObservation_1),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_2),
                    // i = 3
                    MessageCollection.ForecastingAndCombineForStepNr.Invoke(3),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_2),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwayObservation_2),
                    MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.ProvidedSlidingWindowIs.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_2),
                    MessageCollection.ProvidedObservationIs.Invoke(ObjectMother.FaCSteps3_MidwayObservation_2),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_Final),
                    // final
                    MessageCollection.ForecastAndCombineSuccessfullyRunForSteps.Invoke(3),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_Final)
                    }
                )

        };
        private static TestCaseData[] forecastNextValueTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1_Values,
                null,
                null,
                ObjectMother.Observation1_WithDummyFields.Y_Forecasted,
                new List<string>() {
                    MessageCollection.ForecastNextValueRunningForProvidedValues.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    MessageCollection.ProvidedValuesAre.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.ProvidedStepsAre.Invoke(new UnivariateForecastingSettings().DummySteps),
                    MessageCollection.ProvidedIntervalUnitsIs.Invoke(new UnivariateForecastingSettings().DummyIntervalUnit),
                    MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    MessageCollection.ProvidedIdIs.Invoke(new UnivariateForecastingSettings().DummyId),
                    MessageCollection.ProvidedObservationNameIs.Invoke(new UnivariateForecastingSettings().DummyObservationName),
                    MessageCollection.ProvidedIntervalIs.Invoke(ObjectMother.SlidingWindow1_DummyInterval),
                    MessageCollection.ProvidedItemsCountIs.Invoke(ObjectMother.SlidingWindow1_DummyItems),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.SlidingWindow1_WithDummyFields),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1_WithDummyFields),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1_WithDummyFields),
                    MessageCollection.ForecastNextValueSuccessfullyRun.Invoke(ObjectMother.Observation1.Y_Forecasted)
                    }
                ),

            new TestCaseData(
                ObjectMother.SlidingWindow1_Values,
                ObjectMother.Observation1_WithDummyFields.C,
                ObjectMother.Observation1_WithDummyFields.E,
                ObjectMother.Observation1_WithDummyFields.Y_Forecasted,
                new List<string>() {
                    MessageCollection.ForecastNextValueRunningForProvidedValues.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    MessageCollection.ProvidedValuesAre.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.ProvidedStepsAre.Invoke(new UnivariateForecastingSettings().DummySteps),
                    MessageCollection.ProvidedIntervalUnitsIs.Invoke(new UnivariateForecastingSettings().DummyIntervalUnit),
                    MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    MessageCollection.ProvidedIdIs.Invoke(new UnivariateForecastingSettings().DummyId),
                    MessageCollection.ProvidedObservationNameIs.Invoke(new UnivariateForecastingSettings().DummyObservationName),
                    MessageCollection.ProvidedIntervalIs.Invoke(ObjectMother.SlidingWindow1_DummyInterval),
                    MessageCollection.ProvidedItemsCountIs.Invoke(ObjectMother.SlidingWindow1_DummyItems),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.SlidingWindow1_WithDummyFields),
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1_WithDummyFields),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1_WithDummyFields),
                    MessageCollection.ForecastNextValueSuccessfullyRun.Invoke(ObjectMother.Observation1.Y_Forecasted)
                    }
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(constructorExceptionTestCases))]
        public void Constructor_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(forecastExceptionTestCases))]
        public void Forecast_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(extractXActualValuesExceptionTestCases))]
        public void ExtractXActualValues_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(extractStartDatesExceptionTestCases))]
        public void ExtractStartDates_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(combineExceptionTestCases))]
        public void Combine_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(forecastAndCombineExceptionTestCases))]
        public void ForecastAndCombine_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(forecastNextValueExceptionTestCases))]
        public void ForecastNextValue_ShouldThrowAnException_WhenValues
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(forecastTestCases))]
        public void Forecast_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, double? C, double? E, Observation expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            Observation actual = univariateForecaster.Forecast(slidingWindow, C, E);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(extractXActualValuesTestCases))]
        public void ExtractXActualValues_ShouldReturnExpectedValuesAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, List<double> expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            List<double> actual = univariateForecaster.ExtractXActualValues(slidingWindow);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(extractStartDatesTestCases))]
        public void ExtractStartDates_ShouldReturnExpectedDatesAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, List<DateTime> expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            List<DateTime> actual = univariateForecaster.ExtractStartDates(slidingWindow);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(combineTestCases))]
        public void Combine_ShouldReturnExpectedSlidingWindowAndLogExpectedMessages_WhenProperArguments
            (SlidingWindow slidingWindow, Observation observation, SlidingWindow expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings 
                = new UnivariateForecastingSettings(
                    loggingAction: (message) => fakeLogger.Log(message),
                    idCreationFunction: ObjectMother.FaC_IdCreationFunction
                    );
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            SlidingWindow actual = univariateForecaster.Combine(slidingWindow, observation);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(forecastAndCombineTestCases))]
        public void ForecastAndCombine_ShouldReturnExpectedObjectsAndLogExpectedMessages_WhenProperArguments
            (SlidingWindow slidingWindow, 
            uint steps,
            double? C, 
            double? E,
            SlidingWindow expected, 
            List<Observation> expectedObservations, 
            List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings
                = new UnivariateForecastingSettings(
                    loggingAction: (message) => fakeLogger.Log(message),
                    idCreationFunction: ObjectMother.FaC_IdCreationFunction
                    );
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            List<Observation> actualObservations = null;
            SlidingWindow actual = univariateForecaster.ForecastAndCombine(slidingWindow, steps, out actualObservations, C, E);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.True(
                ObjectMother.AreEqual(expectedObservations, actualObservations));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(forecastNextValueTestCases))]
        public void ForecastNextValue_ShouldReturnExpectedValueAndLogExpectedMessages_WhenProperValues
            (List<double> values, double? C, double? E, double expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            double actual = univariateForecaster.ForecastNextValue(values, C, E);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/
