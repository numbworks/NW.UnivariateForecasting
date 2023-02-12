using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Forecasts;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    [TestFixture]
    public class ForecastingInitTests
    {

        #region Fields

        private static TestCaseData[] forecastingInitExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingInit(
                                observationName: ObjectMother.ForecastingInit_ObservationName,
                                values: null,
                                coefficient: ObjectMother.ForecastingInit_Coefficient,
                                error: ObjectMother.ForecastingInit_Error
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
            ).SetArgDisplayNames($"{nameof(forecastingInitExceptionTestCases)}_01")

        };
        private static TestCaseData[] forecastingInitTestCases =
        {

            new TestCaseData(
                    null,
                    ObjectMother.ForecastingInit_Values,
                    null,
                    null
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    null,
                    null
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    ObjectMother.ForecastingInit_Coefficient,
                    null
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    ObjectMother.ForecastingInit_Coefficient,
                    ObjectMother.ForecastingInit_Error
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_04")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(forecastingInitExceptionTestCases))]
        public void ForecastingInit_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastingInitTestCases))]
        public void ForecastingInit_ShouldCreateAnInstanceOfThisType_WhenProperArgument
            (string observationName, List<double> values, double? coefficient, double? error)
        {

            // Arrange
            // Act
            ForecastingInit actual 
                = new ForecastingInit(
                        observationName: observationName, 
                        values: values,
                        coefficient: coefficient,
                        error: error
                        );

            // Assert
            Assert.IsInstanceOf<ForecastingInit>(actual);

            if(actual.ObservationName != null) 
                Assert.IsInstanceOf<string>(actual.ObservationName);

            Assert.IsInstanceOf<List<double>>(actual.Values);

            if (actual.Coefficient != null)
                Assert.IsInstanceOf<double?>(actual.Coefficient);

            if (actual.Error != null)
                Assert.IsInstanceOf<double?>(actual.Error);

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
