using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowItemManagerTests
    {

        #region Fields

        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(
                null, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindowItem_InvalidDueOfSize, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(
                ObjectMother.SlidingWindow01_Item01, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_03")

        };
        private static TestCaseData[] slidingWindowItemManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager(null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                ).SetArgDisplayNames($"{nameof(slidingWindowItemManagerExceptionTestCases)}_01")

        };
        private static TestCaseData[] createItemExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItem(1, null, 58.65, 639.10)),
                typeof(ArgumentException),
                UnivariateForecasting.Intervals.MessageCollection.IntervalNullOrInvalid
                ).SetArgDisplayNames($"{nameof(createItemExceptionTestCases)}_01")

        };
        private static TestCaseData[] createItemTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow01_Item01_Id,
                Intervals.ObjectMother.Interval_SixMonths_SubInterval01,
                ObjectMother.SlidingWindow01_Item01_XActual,
                ObjectMother.SlidingWindow01_Item01_YForecasted,
                ObjectMother.SlidingWindow01_Item01
                ).SetArgDisplayNames($"{nameof(createItemTestCases)}_01")

        };
        private static TestCaseData[] createItemsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(
                            ObjectMother.SlidingWindow01_Item01.Interval.StartDate,
                            null,
                            ObjectMother.SlidingWindow01_Item01.Interval.Unit
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(
                            ObjectMother.SlidingWindow01_Item01.Interval.StartDate,
                            new List<double>(),
                            ObjectMother.SlidingWindow01_Item01.Interval.Unit
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableContainsZeroItems("values")
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(
                            ObjectMother.SlidingWindow01_Item01.Interval.StartDate,
                            ObjectMother.SlidingWindow01_Values,
                            Intervals.ObjectMother.IntervalUnit_NonExistant
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.ProvidedIntervalUnitNotSupported(Intervals.ObjectMother.IntervalUnit_NonExistant.ToString())
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (SlidingWindowItem slidingWindowItem, bool expected)
        {

            // Arrange
            // Act
            bool actual = ObjectMother.SlidingWindowItemManager.IsValid(slidingWindowItem);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(slidingWindowItemManagerExceptionTestCases))]
        public void SlidingWindowItemManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createItemExceptionTestCases))]
        public void CreateItem_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createItemTestCases))]
        public void CreateItem_ShouldReturnExpectedSlidingWindowItem_WhenProperArguments
            (uint id, Interval interval, double X_Actual, double? Y_Forecasted, SlidingWindowItem expected)
        {

            // Arrange
            // Act
            SlidingWindowItem actual = new SlidingWindowItemManager().CreateItem(id, interval, X_Actual, Y_Forecasted);

            // Assert
            Assert.True(
                    ObjectMother.AreEqual(expected, actual));

        }

        [Test]
        public void CreateItem_ShouldThrowACertainException_WhenUnproperIntervalUnit()
        {

            // Arrange
            TestDelegate del = () =>
                new SlidingWindowItemManager().CreateItem(
                                                    ObjectMother.SlidingWindow01_Item01_Id,
                                                    ObjectMother.SlidingWindow01_Item01.Interval.StartDate,
                                                    Intervals.ObjectMother.IntervalUnit_NonExistant,
                                                    ObjectMother.SlidingWindow01_Item01.X_Actual,
                                                    ObjectMother.SlidingWindow01_Item01.Y_Forecasted);
            Type expectedType = typeof(ArgumentException);
            string expectedMessage 
                = UnivariateForecasting.Validation.MessageCollection.ProvidedIntervalUnitNotSupported(Intervals.ObjectMother.IntervalUnit_NonExistant.ToString());

            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [Test]
        public void CreateItem_ShouldReturnExpectedSlidingWindowItem_WhenProperStartDateEtc()
        {

            // Arrange
            // Act
            SlidingWindowItem actual
                 = new SlidingWindowItemManager().CreateItem(
                                                    ObjectMother.SlidingWindow01_Item01_Id,
                                                    ObjectMother.SlidingWindow01_Item01.Interval.StartDate,
                                                    ObjectMother.SlidingWindow01_Item01.Interval.Unit,
                                                    ObjectMother.SlidingWindow01_Item01.X_Actual,
                                                    ObjectMother.SlidingWindow01_Item01.Y_Forecasted);
            // Assert
            Assert.True(
                    ObjectMother.AreEqual(ObjectMother.SlidingWindow01_Item01, actual));

        }

        [TestCaseSource(nameof(createItemsExceptionTestCases))]
        public void CreateItems_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void CreateItems_ShouldReturnExpectedListSlidingWindowItem_WhenProperStartDateEtc()
        {

            // Arrange
            // Act
            List<SlidingWindowItem> actual
                = new SlidingWindowItemManager().CreateItems(
                                                    ObjectMother.SlidingWindow01_Item01.Interval.StartDate,
                                                    ObjectMother.SlidingWindow01_Values,
                                                    ObjectMother.SlidingWindow01_Item01.Interval.Unit);

            // Assert
            Assert.True(
                    ObjectMother.AreEqual(ObjectMother.SlidingWindow01_Items, actual));

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: rua@sitecore.net
    Last Update: 14.11.2022
*/
