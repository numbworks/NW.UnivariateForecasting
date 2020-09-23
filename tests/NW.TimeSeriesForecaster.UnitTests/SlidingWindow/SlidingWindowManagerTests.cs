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
        private static TestCaseData[] constructorExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            null, 
                            new IntervalManager(), 
                            new SlidingWindowItemManager())),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            new UnivariateForecastingSettings(),
                            null,
                            new SlidingWindowItemManager())),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                ),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowManager(
                            new UnivariateForecastingSettings(),
                            new IntervalManager(),
                            null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindowItemManager").Message
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
                typeof(Exception),
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
                typeof(Exception),
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
                typeof(Exception),
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
                typeof(Exception),
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
                typeof(Exception),
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
                typeof(Exception),
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
            UnivariateForecastingSettings settings = new UnivariateForecastingSettings(loggingAction: (message) => fakeLogger.Log(message));
            SlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);

            // Act
            SlidingWindow actual = slidingWindowManager.Create(id, observationName, values, steps, intervalUnit, startDate);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 23.09.2020

*/
