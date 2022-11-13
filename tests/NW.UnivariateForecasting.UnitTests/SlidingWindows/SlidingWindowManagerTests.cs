using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

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
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                null,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Items
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("id").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                null,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Items
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("observationName").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                Utilities.ObjectMother.Shared_IntervalDueOfEndDate, // Whatever invalid Interval
                                Utilities.ObjectMother.Shared_SlidingWindow1_Items
                                )),
                typeof(ArgumentException),
                UnivariateForecasting.Intervals.MessageCollection.IntervalNullOrInvalid
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
                                null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("items").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
                                new List<SlidingWindowItem>()
                                )),
                typeof(ArgumentException),
                Validation.MessageCollection.VariableContainsZeroItems.Invoke("items")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Items.Where(item => item.Id != 6).ToList() // Removes a random item
                                )),
                typeof(ArgumentException),
                UnivariateForecasting.Intervals.MessageCollection.ItemsDontMatchSubintervals.Invoke(5, Utilities.ObjectMother.Shared_SlidingWindow1_Interval)
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_06"),

            // Second Create()
            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                null,
                                Utilities.ObjectMother.Shared_SlidingWindow1_Steps,
                                Utilities.ObjectMother.Shared_SlidingWindow1_IntervalUnit,
                                Utilities.ObjectMother.Shared_SlidingWindow1_StartDate
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                    () => Utilities.ObjectMother.SlidingWindowManager_Default
                            .Create(
                                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                                new List<double>(),
                                Utilities.ObjectMother.Shared_SlidingWindow1_Steps,
                                Utilities.ObjectMother.Shared_SlidingWindow1_IntervalUnit,
                                Utilities.ObjectMother.Shared_SlidingWindow1_StartDate
                                )),
                typeof(ArgumentException),
                Validation.MessageCollection.VariableContainsZeroItems.Invoke("values")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_08")

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(
                null, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData(
                Utilities.ObjectMother.SlidingWindow_InvalidDueOfNullId, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(
                Utilities.ObjectMother.SlidingWindow_InvalidDueOfNullObservationName, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData(
                Utilities.ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData(
                Utilities.ObjectMother.SlidingWindow_InvalidDueOfNullItems, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_05"),

            new TestCaseData(
                Utilities.ObjectMother.SlidingWindow_InvalidDueOfItemsCountZero, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_06"),

            new TestCaseData(
                Utilities.ObjectMother.SlidingWindow_InvalidDueOfSubInterval, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_07"),

            new TestCaseData(
                Utilities.ObjectMother.Shared_SlidingWindow1, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_08")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                Utilities.ObjectMother.Shared_SlidingWindow1_Id,
                Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
                Utilities.ObjectMother.Shared_SlidingWindow1_Values,
                Utilities.ObjectMother.Shared_SlidingWindow1_Steps,
                Utilities.ObjectMother.Shared_SlidingWindow1_IntervalUnit,
                Utilities.ObjectMother.Shared_SlidingWindow1_StartDate,
                Utilities.ObjectMother.Shared_SlidingWindow1,
                new List<string>() {
                    SlidingWindows.MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    SlidingWindows.MessageCollection.ProvidedValuesAre.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_Values),
                    SlidingWindows.MessageCollection.ProvidedStepsAre.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_Steps),
                    SlidingWindows.MessageCollection.ProvidedIntervalUnitsIs.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_IntervalUnit),
                    SlidingWindows.MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    SlidingWindows.MessageCollection.ProvidedIdIs.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_Id),
                    SlidingWindows.MessageCollection.ProvidedObservationNameIs.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName),
                    SlidingWindows.MessageCollection.ProvidedIntervalIs.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_Interval),
                    SlidingWindows.MessageCollection.ProvidedItemsCountIs.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1_Items),
                    SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(Utilities.ObjectMother.Shared_SlidingWindow1)
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
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (SlidingWindow slidingWindow, bool expected)
        {

            // Arrange
            // Act
            bool actual = Utilities.ObjectMother.SlidingWindowManager_Default.IsValid(slidingWindow);

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
                Utilities.ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void Create_ShouldReturnSlidingWindowWithDummyFields_WhenValues()
        {

            // Arrange
            // Act
            SlidingWindow actual 
                = new SlidingWindowManager().Create(Utilities.ObjectMother.Shared_SlidingWindow1_Values);

            // Assert
            Assert.True(
                Utilities.ObjectMother.AreEqual(
                    Utilities.ObjectMother.Shared_SlidingWindow1_WithDefaultDummyFields,
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
    Last Update: 12.11.2022
*/
