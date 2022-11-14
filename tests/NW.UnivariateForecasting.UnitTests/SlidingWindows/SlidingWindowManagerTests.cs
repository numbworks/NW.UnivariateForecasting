using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
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
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                null,
                                ObjectMother.SlidingWindow01_ObservationName,
                                Intervals.ObjectMother.Interval_SixMonths,
                                ObjectMother.SlidingWindow01_Items
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("id").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                null,
                                Intervals.ObjectMother.Interval_SixMonths,
                                ObjectMother.SlidingWindow01_Items
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("observationName").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                ObjectMother.SlidingWindow01_ObservationName,
                                Intervals.ObjectMother.Interval_InvalidDueOfEndDate, // Whatever invalid Interval
                                ObjectMother.SlidingWindow01_Items
                                )),
                typeof(ArgumentException),
                UnivariateForecasting.Intervals.MessageCollection.IntervalNullOrInvalid
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                ObjectMother.SlidingWindow01_ObservationName,
                                Intervals.ObjectMother.Interval_SixMonths,
                                null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("items").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                ObjectMother.SlidingWindow01_ObservationName,
                                Intervals.ObjectMother.Interval_SixMonths,
                                new List<SlidingWindowItem>()
                                )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableContainsZeroItems.Invoke("items")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                ObjectMother.SlidingWindow01_ObservationName,
                                Intervals.ObjectMother.Interval_SixMonths,
                                ObjectMother.SlidingWindow01_Items.Where(item => item.Id != 6).ToList() // Removes a random item
                                )),
                typeof(ArgumentException),
                UnivariateForecasting.Intervals.MessageCollection.ItemsDontMatchSubintervals.Invoke(5, Intervals.ObjectMother.Interval_SixMonths)
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_06"),

            // Second Create()
            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                ObjectMother.SlidingWindow01_ObservationName,
                                null,
                                ObjectMother.SlidingWindow01_Steps,
                                Intervals.ObjectMother.IntervalUnits_Months,
                                Intervals.ObjectMother.Interval_SixMonths_StartDate
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.SlidingWindowManager_Empty
                            .Create(
                                ObjectMother.SlidingWindow01_Id,
                                ObjectMother.SlidingWindow01_ObservationName,
                                new List<double>(),
                                ObjectMother.SlidingWindow01_Steps,
                                Intervals.ObjectMother.IntervalUnits_Months,
                                Intervals.ObjectMother.Interval_SixMonths_StartDate
                                )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableContainsZeroItems.Invoke("values")
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
                ObjectMother.SlidingWindow01, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_08")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow01_Id,
                ObjectMother.SlidingWindow01_ObservationName,
                ObjectMother.SlidingWindow01_Values,
                ObjectMother.SlidingWindow01_Steps,
                Intervals.ObjectMother.IntervalUnits_Months,
                Intervals.ObjectMother.Interval_SixMonths_StartDate,
               ObjectMother.SlidingWindow01,
                new List<string>() {
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedValuesAre.Invoke(ObjectMother.SlidingWindow01_Values),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedStepsAre.Invoke(ObjectMother.SlidingWindow01_Steps),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIntervalUnitsIs.Invoke(Intervals.ObjectMother.IntervalUnits_Months),
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIdIs.Invoke(ObjectMother.SlidingWindow01_Id),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedObservationNameIs.Invoke(ObjectMother.SlidingWindow01_ObservationName),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIntervalIs.Invoke(Intervals.ObjectMother.Interval_SixMonths),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedItemsCountIs.Invoke(ObjectMother.SlidingWindow01_Items),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.SlidingWindow01)
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
            bool actual = ObjectMother.SlidingWindowManager_Empty.IsValid(slidingWindow);

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
                = new SlidingWindowManager().Create(ObjectMother.SlidingWindow01_Values);

            // Assert
            Assert.True(
                    ObjectMother.AreEqual(
                        ObjectMother.SlidingWindow01_WithDefaultDummyFields,
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
    Last Update: 14.11.2022
*/
