using System;
using NW.UnivariateForecasting.SlidingWindows;
using NW.Shared.Validation;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    [TestFixture]
    public class SlidingWindowItemTests
    {

        #region Fields

        private static TestCaseData[] slidingWindowItemExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SlidingWindowItem(id: 0, X_Actual: 58.50, Y_Forecasted: 615.26)),
                typeof(ArgumentException),
                Shared.Validation.MessageCollection.VariableCantBeLessThan("id", 1)
                ).SetArgDisplayNames($"{nameof(slidingWindowItemExceptionTestCases)}_01")

        };
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow01_Item01,
                ObjectMother.SlidingWindow01_Item01_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(slidingWindowItemExceptionTestCases))]
        public void SlidingWindowItem_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (SlidingWindowItem slidingWindowItem, string expected)
        {

            // Arrange
            // Act
            string actual = slidingWindowItem.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void SlidingWindowItem_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SlidingWindowItem actual = new SlidingWindowItem(id: 1, X_Actual: 58.50, Y_Forecasted: 615.26);

            // Assert
            Assert.That(actual, Is.InstanceOf<SlidingWindowItem>());
            Assert.That(actual.Id, Is.InstanceOf<uint>());
            Assert.That(actual.X_Actual, Is.InstanceOf<double>());
            Assert.That(actual.Y_Forecasted, Is.InstanceOf<double?>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/