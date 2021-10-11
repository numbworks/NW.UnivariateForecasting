using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Messages;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowManagerTests
    {

        #region Fields

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
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_01"),

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
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_02"),

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
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_03"),

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
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_04"),

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
                ).SetArgDisplayNames($"{nameof(slidingWindowManagerExceptionTestCases)}_05")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            // First Create()
            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                null,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                ObjectMother.Shared_SlidingWindow1_Interval,
                                ObjectMother.Shared_SlidingWindow1_Items
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("id").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                null,
                                ObjectMother.Shared_SlidingWindow1_Interval,
                                ObjectMother.Shared_SlidingWindow1_Items
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("observationName").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                ObjectMother.Shared_IntervalDueOfEndDate, // Whatever invalid Interval
                                ObjectMother.Shared_SlidingWindow1_Items
                                )),
                typeof(ArgumentException),
                MessageCollection.IntervalManager_IntervalNullOrInvalid
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                ObjectMother.Shared_SlidingWindow1_Interval,
                                null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("items").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                ObjectMother.Shared_SlidingWindow1_Interval,
                                new List<SlidingWindowItem>()
                                )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("items")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                ObjectMother.Shared_SlidingWindow1_Interval,
                                ObjectMother.Shared_SlidingWindow1_Items.Where(item => item.Id != 6).ToList() // Removes a random item
                                )),
                typeof(ArgumentException),
                MessageCollection.IntervalManager_ItemsDontMatchSubintervals.Invoke(5, ObjectMother.Shared_SlidingWindow1_Interval)
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_06"),

            // Second Create()
            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                null,
                                ObjectMother.Shared_SlidingWindow1_Steps,
                                ObjectMother.Shared_SlidingWindow1_IntervalUnit,
                                ObjectMother.Shared_SlidingWindow1_StartDate
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Default
                            .Create(
                                ObjectMother.Shared_SlidingWindow1_Id,
                                ObjectMother.Shared_SlidingWindow1_ObservationName,
                                new List<double>(),
                                ObjectMother.Shared_SlidingWindow1_Steps,
                                ObjectMother.Shared_SlidingWindow1_IntervalUnit,
                                ObjectMother.Shared_SlidingWindow1_StartDate
                                )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("values")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_08")

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(
                null, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindow_InvalidDueOfNullId, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(
                ObjectMother.SlidingWindow_InvalidDueOfNullObservationName, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData(
                ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData(
                ObjectMother.SlidingWindow_InvalidDueOfNullItems, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_05"),

            new TestCaseData(
                ObjectMother.SlidingWindow_InvalidDueOfItemsCountZero, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_06"),

            new TestCaseData(
                ObjectMother.SlidingWindow_InvalidDueOfSubInterval, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_07"),

            new TestCaseData(
                ObjectMother.Shared_SlidingWindow1, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_08")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                ObjectMother.Shared_SlidingWindow1_Id,
                ObjectMother.Shared_SlidingWindow1_ObservationName,
                ObjectMother.Shared_SlidingWindow1_Values,
                ObjectMother.Shared_SlidingWindow1_Steps,
                ObjectMother.Shared_SlidingWindow1_IntervalUnit,
                ObjectMother.Shared_SlidingWindow1_StartDate,
                ObjectMother.Shared_SlidingWindow1,
                new List<string>() {
                    MessageCollection.SlidingWindowManager_CreatingIntervalOutOfFollowingArguments,
                    MessageCollection.SlidingWindowManager_ProvidedValuesAre.Invoke(ObjectMother.Shared_SlidingWindow1_Values),
                    MessageCollection.SlidingWindowManager_ProvidedStepsAre.Invoke(ObjectMother.Shared_SlidingWindow1_Steps),
                    MessageCollection.SlidingWindowManager_ProvidedIntervalUnitsIs.Invoke(ObjectMother.Shared_SlidingWindow1_IntervalUnit),
                    MessageCollection.SlidingWindowManager_CreatingSlidingWindowOutOfFollowingArguments,
                    MessageCollection.SlidingWindowManager_ProvidedIdIs.Invoke(ObjectMother.Shared_SlidingWindow1_Id),
                    MessageCollection.SlidingWindowManager_ProvidedObservationNameIs.Invoke(ObjectMother.Shared_SlidingWindow1_ObservationName),
                    MessageCollection.SlidingWindowManager_ProvidedIntervalIs.Invoke(ObjectMother.Shared_SlidingWindow1_Interval),
                    MessageCollection.SlidingWindowManager_ProvidedItemsCountIs.Invoke(ObjectMother.Shared_SlidingWindow1_Items),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.Shared_SlidingWindow1)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(slidingWindowManagerExceptionTestCases))]
        public void SlidingWindowManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
                = new SlidingWindowManager().Create(ObjectMother.Shared_SlidingWindow1_Values);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(
                    ObjectMother.Shared_SlidingWindow1_WithDefaultDummyFields,
                    actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/
