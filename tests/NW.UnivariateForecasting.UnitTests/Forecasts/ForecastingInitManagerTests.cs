using System;
using NW.UnivariateForecasting.Forecasts;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    [TestFixture]
    public class ForecastingInitManagerTests
    {

        #region Fields

        private static TestCaseData[] expandValuesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingInitManager().ExpandValues(
                                forecastingInit: null, 
                                nextValue: ObjectMother.NextValue
                            )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("forecastingInit").Message
            ).SetArgDisplayNames($"{nameof(expandValuesExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(expandValuesExceptionTestCases))]
        public void ExpandValues_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ForecastingInitManager_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            ForecastingInitManager actual = new ForecastingInitManager();

            // Assert
            Assert.IsInstanceOf<ForecastingInitManager>(actual);

        }

        [Test]
        public void ExpandValues_ShouldReturnExpectedForecastingInit_WhenProperArgument()
        {

            // Arrange
            // Act
            ForecastingInit actual = new ForecastingInitManager().ExpandValues(
                                forecastingInit: ObjectMother.ForecastingInit_SingleWithCE,
                                nextValue: ObjectMother.NextValue
                            );

            // Assert
            Assert.True(
                    ObjectMother.AreEqual(ObjectMother.ForecastingInit_SingleWithCEAndExpandedValues, actual)
                );

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2023
*/
