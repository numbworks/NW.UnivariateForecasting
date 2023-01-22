using System;
using NW.UnivariateForecastingClient.ApplicationSession;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests.ApplicationSession
{
    [TestFixture]
    public class MinimumAccuracyValidatorTests
    {

        #region Fields

        private static TestCaseData[] minimumAccuracyValidatorExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new MinimumAccuracyValidator(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("doubleManager").Message
            ).SetArgDisplayNames($"{nameof(minimumAccuracyValidatorExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(minimumAccuracyValidatorExceptionTestCases))]
        public void MinimumAccuracyValidator_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void MinimumAccuracyValidator_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            MinimumAccuracyValidator actual = new MinimumAccuracyValidator(new DoubleManager());

            // Assert
            Assert.IsInstanceOf<MinimumAccuracyValidator>(actual);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/
