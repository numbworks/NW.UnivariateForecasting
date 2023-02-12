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

        private static TestCaseData[] forecastingInitTestCases =
        {

            new TestCaseData(
                    null,
                    new List<double>() { 58.5, 615.26, 659.84, 635.69, 612.27, 632.94 },
                    null,
                    null
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_01"),

            new TestCaseData(
                    "Sales USD",
                    new List<double>() { 58.5, 615.26, 659.84, 635.69, 612.27, 632.94 },
                    null,
                    null
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_02"),

            new TestCaseData(
                    "Sales USD",
                    new List<double>() { 58.5, 615.26, 659.84, 635.69, 612.27, 632.94 },
                    0.5,
                    null
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_03"),

            new TestCaseData(
                    "Sales USD",
                    new List<double>() { 58.5, 615.26, 659.84, 635.69, 612.27, 632.94 },
                    0.5,
                    0.01
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_04")

        };
        private static TestCaseData[] forecastingInitExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingInit(
                                observationName: "Sales USD",
                                values: null,
                                coefficient: 0.5,
                                error: 0.01
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
            ).SetArgDisplayNames($"{nameof(forecastingInitExceptionTestCases)}_01")

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
                Assert.IsInstanceOf<double?>(actual.Coefficient ?? null);

            if (actual.Error != null)
                Assert.IsInstanceOf<double?>(actual.Error ?? null);

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
