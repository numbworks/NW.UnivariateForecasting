using System;
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
        private static TestCaseData[] forecastTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.Observation1,
                new List<string>() {
                    MessageCollection.CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1)
                    }
                )

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

        [TestCaseSource(nameof(forecastTestCases))]
        public void Forecast_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, Observation expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster = new UnivariateForecaster(settings);

            // Act
            Observation actual = univariateForecaster.Forecast(slidingWindow);

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

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 27.09.2020

*/
