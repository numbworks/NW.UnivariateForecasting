using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowItemManagerTests
    {

        #region Fields

        private static TestCaseData[] createItemTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow01_Item01_Id,
                ObjectMother.SlidingWindow01_Item01_XActual,
                ObjectMother.SlidingWindow01_Item01_YForecasted,
                ObjectMother.SlidingWindow01_Item01
                ).SetArgDisplayNames($"{nameof(createItemTestCases)}_01")

        };
        private static TestCaseData[] createItemsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItems(new List<double>())),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableContainsZeroItems("values")
                ).SetArgDisplayNames($"{nameof(createItemsExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(createItemTestCases))]
        public void CreateItem_ShouldReturnExpectedSlidingWindowItem_WhenProperArguments
            (uint id, double X_Actual, double? Y_Forecasted, SlidingWindowItem expected)
        {

            // Arrange
            // Act
            SlidingWindowItem actual = new SlidingWindowItemManager().CreateItem(id, X_Actual, Y_Forecasted);

            // Assert
            Assert.True(
                    ObjectMother.AreEqual(expected, actual));

        }

        [Test]
        public void CreateItem_ShouldReturnExpectedSlidingWindowItem_WhenProperStartDateEtc()
        {

            // Arrange
            // Act
            SlidingWindowItem actual
                 = new SlidingWindowItemManager().CreateItem(
                                                    ObjectMother.SlidingWindow01_Item01_Id,
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
                = new SlidingWindowItemManager().CreateItems(ObjectMother.SlidingWindow01_Values);

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
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
