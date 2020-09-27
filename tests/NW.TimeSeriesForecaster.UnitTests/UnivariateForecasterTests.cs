using System;
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


        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 27.09.2020

*/
