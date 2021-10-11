using System;
using System.Collections.Generic;
using NUnit.Framework;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Messages;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowItemManagerTests
    {

        // Fields
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
                ObjectMother.SlidingWindow1_Item1, 
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
                MessageCollection.IntervalManager_IntervalNullOrInvalid
                ).SetArgDisplayNames($"{nameof(createItemExceptionTestCases)}_01")

        };
        private static TestCaseData[] createItemTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1_Item1_Id,
                ObjectMother.SlidingWindow1_Item1_Interval,
                ObjectMother.SlidingWindow1_Item1_XActual,
                ObjectMother.SlidingWindow1_Item1_YForecasted,
                ObjectMother.SlidingWindow1_Item1
                ).SetArgDisplayNames($"{nameof(createItemTestCases)}_01")

        };
        private static TestCaseData[] createItemsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(
                            ObjectMother.SlidingWindow1_Item1.Interval.StartDate,
                            null,
                            ObjectMother.SlidingWindow1_Item1.Interval.Unit
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(
                            ObjectMother.SlidingWindow1_Item1.Interval.StartDate,
                            new List<double>(),
                            ObjectMother.SlidingWindow1_Item1.Interval.Unit
                        )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("values")
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(
                            ObjectMother.SlidingWindow1_Item1.Interval.StartDate,
                            ObjectMother.SlidingWindow1_Values,
                            ObjectMother.NonExistantIntervalUnit
                        )),
                typeof(ArgumentException),
                MessageCollection.Validator_ProvidedIntervalUnitNotSupported.Invoke(ObjectMother.NonExistantIntervalUnit.ToString())
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_03")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (SlidingWindowItem slidingWindowItem, bool expected)
        {

            // Arrange
            // Act
            bool actual = ObjectMother.SlidingWindowItemManager_Default.IsValid(slidingWindowItem);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(slidingWindowItemManagerExceptionTestCases))]
        public void SlidingWindowItemManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createItemExceptionTestCases))]
        public void CreateItem_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
                                                    ObjectMother.SlidingWindow1_Item1_Id,
                                                    ObjectMother.SlidingWindow1_Item1.Interval.StartDate,
                                                    ObjectMother.NonExistantIntervalUnit,
                                                    ObjectMother.SlidingWindow1_Item1.X_Actual,
                                                    ObjectMother.SlidingWindow1_Item1.Y_Forecasted);
            Type expectedType = typeof(ArgumentException);
            string expectedMessage 
                = MessageCollection.Validator_ProvidedIntervalUnitNotSupported.Invoke(ObjectMother.NonExistantIntervalUnit.ToString());

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
                                                    ObjectMother.SlidingWindow1_Item1_Id,
                                                    ObjectMother.SlidingWindow1_Item1.Interval.StartDate,
                                                    ObjectMother.SlidingWindow1_Item1.Interval.Unit,
                                                    ObjectMother.SlidingWindow1_Item1.X_Actual,
                                                    ObjectMother.SlidingWindow1_Item1.Y_Forecasted);
            // Assert
            Assert.True(
                ObjectMother.AreEqual(ObjectMother.SlidingWindow1_Item1, actual));

        }

        [TestCaseSource(nameof(createItemsExceptionTestCases))]
        public void CreateItems_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void CreateItems_ShouldReturnExpectedListSlidingWindowItem_WhenProperStartDateEtc()
        {

            // Arrange
            // Act
            List<SlidingWindowItem> actual
                = new SlidingWindowItemManager().CreateItems(
                                                    ObjectMother.SlidingWindow1_Item1.Interval.StartDate,
                                                    ObjectMother.SlidingWindow1_Values,
                                                    ObjectMother.SlidingWindow1_Item1.Interval.Unit);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(ObjectMother.SlidingWindow1_Items, actual));

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 29.04.2021

*/
