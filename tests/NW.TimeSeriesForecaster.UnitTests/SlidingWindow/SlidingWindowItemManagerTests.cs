using System;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowItemManagerTests
    {

        // Fields
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(null, false),
            new TestCaseData(ObjectMother.SlidingWindowItem_InvalidDueOfSize, false),
            new TestCaseData(ObjectMother.SlidingWindow1_Item1, true)

        };
        private static TestCaseData[] constructorExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager(null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("intervalManager").Message
                )

        };
        private static TestCaseData[] createItemExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItemManager().CreateItem(1, null, 58.65, 639.10)),
                typeof(Exception),
                MessageCollection.IntervalNullOrInvalid
                )

        };
        private static TestCaseData[] createItemTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1_Item1_Id,
                ObjectMother.SlidingWindow1_Item1_Interval,
                ObjectMother.SlidingWindow1_Item1_XActual,
                ObjectMother.SlidingWindow1_Item1_YForecasted,
                ObjectMother.SlidingWindow1_Item1
                )

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

        [TestCaseSource(nameof(createItemExceptionTestCases))]
        public void CreateItem_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

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

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 27.09.2020

*/