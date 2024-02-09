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
                                error: ObjectMother.ForecastingInit_Error,
                                steps: ObjectMother.ForecastingInit_Steps_Single
                        )
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
            ).SetArgDisplayNames($"{nameof(forecastingInitExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ForecastingInit(
                                observationName: ObjectMother.ForecastingInit_ObservationName,
                                values: ObjectMother.ForecastingInit_Values,
                                coefficient: ObjectMother.ForecastingInit_Coefficient,
                                error: ObjectMother.ForecastingInit_Error,
                                steps: 0
                        )
                ),
                typeof(ArgumentException),
                "'steps' can't be less than '1'."
            ).SetArgDisplayNames($"{nameof(forecastingInitExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastingInitTestCases =
        {

            new TestCaseData(
                    null,
                    ObjectMother.ForecastingInit_Values,
                    null,
                    null,
                    ObjectMother.ForecastingInit_Steps_Single
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    null,
                    null,
                    ObjectMother.ForecastingInit_Steps_Single
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    ObjectMother.ForecastingInit_Coefficient,
                    null,
                    ObjectMother.ForecastingInit_Steps_Single
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    ObjectMother.ForecastingInit_Coefficient,
                    ObjectMother.ForecastingInit_Error,
                    ObjectMother.ForecastingInit_Steps_Single
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.ForecastingInit_ObservationName,
                    ObjectMother.ForecastingInit_Values,
                    ObjectMother.ForecastingInit_Coefficient,
                    ObjectMother.ForecastingInit_Error,
                    ObjectMother.ForecastingInit_Steps_MultipleDouble
                ).SetArgDisplayNames($"{nameof(forecastingInitTestCases)}_05")

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
            (string observationName, List<double> values, double? coefficient, double? error, uint steps)
        {

            // Arrange
            // Act
            ForecastingInit actual 
                = new ForecastingInit(
                        observationName: observationName, 
                        values: values,
                        coefficient: coefficient,
                        error: error,
                        steps: steps
                        );

            // Assert
            Assert.That(actual, Is.InstanceOf<ForecastingInit>());

            if(actual.ObservationName != null) 
                Assert.That(actual.ObservationName, Is.InstanceOf<string>());

            Assert.That(actual.Values, Is.InstanceOf<List<double>>());

            if (actual.Coefficient != null)
                Assert.That(actual.Coefficient, Is.InstanceOf<double?>());

            if (actual.Error != null)
                Assert.That(actual.Error, Is.InstanceOf<double?>());

            Assert.That(actual.Steps, Is.InstanceOf<uint>());

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
