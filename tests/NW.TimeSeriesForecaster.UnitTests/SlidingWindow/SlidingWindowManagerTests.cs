using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowManagerTests
    {

        // Fields
        private static TestCaseData[] slidingWindowManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            settings: null,
                            intervalManager: new IntervalManager(),
                            slidingWindowItemManager: new SlidingWindowItemManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: null,
                            slidingWindowItemManager: new SlidingWindowItemManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowItemManager: null,
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowItemManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowItemManager: new SlidingWindowItemManager(),
                            roundingFunction: null,
                            loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowItemManager: new SlidingWindowItemManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                )

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            // First Create()
            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                null,
                                ObjectMother.SlidingWindow1_ObservationName,
                                ObjectMother.SlidingWindow1_Interval,
                                ObjectMother.SlidingWindow1_Items
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeEmptyOrNull.Invoke("id")
                ).SetDescription(MessageCollection.VariableCantBeEmptyOrNull.Invoke("id")),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                null,
                                ObjectMother.SlidingWindow1_Interval,
                                ObjectMother.SlidingWindow1_Items
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeEmptyOrNull.Invoke("observationName")
                ).SetDescription(MessageCollection.VariableCantBeEmptyOrNull.Invoke("observationName")),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                ObjectMother.SlidingWindow1_ObservationName,
                                ObjectMother.Interval_InvalidDueOfEndDate, // Whatever invalid Interval
                                ObjectMother.SlidingWindow1_Items
                                )),
                typeof(ArgumentException),
                MessageCollection.IntervalNullOrInvalid
                ).SetDescription(MessageCollection.IntervalNullOrInvalid),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                ObjectMother.SlidingWindow1_ObservationName,
                                ObjectMother.SlidingWindow1_Interval,
                                null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("items").Message
                ).SetDescription(new ArgumentNullException("items").Message),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                ObjectMother.SlidingWindow1_ObservationName,
                                ObjectMother.SlidingWindow1_Interval,
                                new List<SlidingWindowItem>()
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke("items")
                ).SetDescription(MessageCollection.VariableContainsZeroItems.Invoke("items")),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                ObjectMother.SlidingWindow1_ObservationName,
                                ObjectMother.SlidingWindow1_Interval,
                                ObjectMother.SlidingWindow1_Items.Where(item => item.Id != 6).ToList() // Removes a random item
                                )),
                typeof(ArgumentException),
                MessageCollection.ItemsDontMatchSubintervals.Invoke(5, ObjectMother.SlidingWindow1_Interval)
                ).SetDescription(MessageCollection.ItemsDontMatchSubintervals.Invoke(5, ObjectMother.SlidingWindow1_Interval)),

            // Second Create()
            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                ObjectMother.SlidingWindow1_ObservationName,
                                null,
                                ObjectMother.SlidingWindow1_Steps,
                                ObjectMother.SlidingWindow1_IntervalUnit,
                                ObjectMother.SlidingWindow1_StartDate
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetDescription(new ArgumentNullException("values").Message),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.SlidingWindow1_Id,
                                ObjectMother.SlidingWindow1_ObservationName,
                                new List<double>(),
                                ObjectMother.SlidingWindow1_Steps,
                                ObjectMother.SlidingWindow1_IntervalUnit,
                                ObjectMother.SlidingWindow1_StartDate
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke("values")
                ).SetDescription(MessageCollection.VariableContainsZeroItems.Invoke("values"))

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(null, false),
            new TestCaseData(ObjectMother.SlidingWindow_InvalidDueOfNullId, false),
            new TestCaseData(ObjectMother.SlidingWindow_InvalidDueOfNullObservationName, false),
            new TestCaseData(ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval, false),
            new TestCaseData(ObjectMother.SlidingWindow_InvalidDueOfNullItems, false),
            new TestCaseData(ObjectMother.SlidingWindow_InvalidDueOfItemsCountZero, false),
            new TestCaseData(ObjectMother.SlidingWindow_InvalidDueOfSubInterval, false),
            new TestCaseData(ObjectMother.SlidingWindow1, true)

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1_Id,
                ObjectMother.SlidingWindow1_ObservationName,
                ObjectMother.SlidingWindow1_Values,
                ObjectMother.SlidingWindow1_Steps,
                ObjectMother.SlidingWindow1_IntervalUnit,
                ObjectMother.SlidingWindow1_StartDate,
                ObjectMother.SlidingWindow1,
                new List<string>() {
                    MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    MessageCollection.ProvidedValuesAre.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.ProvidedStepsAre.Invoke(ObjectMother.SlidingWindow1_Steps),
                    MessageCollection.ProvidedIntervalUnitsIs.Invoke(ObjectMother.SlidingWindow1_IntervalUnit),
                    MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    MessageCollection.ProvidedIdIs.Invoke(ObjectMother.SlidingWindow1_Id),
                    MessageCollection.ProvidedObservationNameIs.Invoke(ObjectMother.SlidingWindow1_ObservationName),
                    MessageCollection.ProvidedIntervalIs.Invoke(ObjectMother.SlidingWindow1_Interval),
                    MessageCollection.ProvidedItemsCountIs.Invoke(ObjectMother.SlidingWindow1_Items),
                    MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.SlidingWindow1)
                    }
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(slidingWindowManagerExceptionTestCases))]
        public void SlidingWindowManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (SlidingWindow slidingWindow, bool expected)
        {

            // Arrange
            // Act
            bool actual = ObjectMother.SlidingWindowManager_Default.IsValid(slidingWindow);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedSlidingWindowAndLogExpectedMessages_WhenProperArguments
            (string id,
            string observationName,
            List<double> values,
            uint steps,
            IntervalUnits intervalUnit,
            DateTime startDate,
            SlidingWindow expected, 
            List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            SlidingWindowManager slidingWindowManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                            intervalManager: new IntervalManager(),
                            slidingWindowItemManager: new SlidingWindowItemManager(),
                            roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                            loggingAction: (message) => fakeLogger.Log(message)
                        );

            // Act
            SlidingWindow actual = slidingWindowManager.Create(id, observationName, values, steps, intervalUnit, startDate);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void Create_ShouldReturnSlidingWindowWithDummyFields_WhenValues()
        {

            // Arrange
            // Act
            SlidingWindow actual 
                = new SlidingWindowManager().Create(ObjectMother.SlidingWindow1_Values);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(
                    ObjectMother.SlidingWindow1_WithDefaultDummyFields,
                    actual)
                );

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 04.10.2020

*/
