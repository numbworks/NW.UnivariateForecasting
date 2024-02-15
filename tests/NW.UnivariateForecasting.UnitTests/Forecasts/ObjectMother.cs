using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Forecasts;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    public static class ObjectMother
    {

        #region Properties

        public static UnivariateForecaster UnivariateForecaster = new UnivariateForecaster();

        public static string ForecastingInit_ObservationName = "Sales USD";
        public static List<double> ForecastingInit_Values = new List<double>() { 58.5, 615.26, 659.84, 635.69, 612.27, 632.94 };
        public static double ForecastingInit_Coefficient = 0.5;
        public static double ForecastingInit_Error = 0.01;
        public static uint ForecastingInit_Steps_Single = 1;
        public static uint ForecastingInit_Steps_MultipleDouble = 2;

        public static string ForecastingInitSingleWithCEAsJson_Content = Properties.Resources.ForecastingInitSingleWithCEAsJson;
        public static ForecastingInit ForecastingInit_SingleWithCE
            = new ForecastingInit(
                    observationName: ForecastingInit_ObservationName,
                    values: ForecastingInit_Values,
                    coefficient: ForecastingInit_Coefficient,
                    error: ForecastingInit_Error,
                    steps: ForecastingInit_Steps_Single
                    );

        public static string ForecastingInitSingleWithoutCEAsJson_Content = Properties.Resources.ForecastingInitSingleWithoutCEAsJson;
        public static ForecastingInit ForecastingInit_SingleWithoutCE
            = new ForecastingInit(
                    observationName: ForecastingInit_ObservationName,
                    values: ForecastingInit_Values,
                    coefficient: null,
                    error: null,
                    steps: ForecastingInit_Steps_Single
                    );

        public static string ForecastingInitBareMinimumAsJson_Content = Properties.Resources.ForecastingInitBareMinimumAsJson;
        public static ForecastingInit ForecastingInit_BareMinimum
            = new ForecastingInit(
                    observationName: null,
                    values: new List<double>() { 58.5, 615.26 },
                    coefficient: null,
                    error: null,
                    steps: ForecastingInit_Steps_Single
                    );

        public static ForecastingInit ForecastingInit_DoubleWithCE
            = new ForecastingInit(
                    observationName: ForecastingInit_ObservationName,
                    values: ForecastingInit_Values,
                    coefficient: ForecastingInit_Coefficient,
                    error: ForecastingInit_Error,
                    steps: ForecastingInit_Steps_MultipleDouble
                    );

        public static string ForecastingSession_Version = "4.2.0.0";

        public static double NextValue = 519.23;
        public static List<double> ExpandedValues = new List<double>() { 58.5, 615.26, 659.84, 635.69, 612.27, 632.94, NextValue };

        public static ForecastingInit ForecastingInit_SingleWithCEAndExpandedValues
            = new ForecastingInit(
                    observationName: ForecastingInit_ObservationName,
                    values: ExpandedValues,
                    coefficient: ForecastingInit_Coefficient,
                    error: ForecastingInit_Error,
                    steps: ForecastingInit_Steps_Single
                    );

        public static string ForecastingSessionSingleWithCEAsJson_Content = Properties.Resources.ForecastingSessionSingleWithCEAsJson;
        public static ForecastingSession ForecastingSession_SingleWithCE = new ForecastingSession(
                init: ForecastingInit_SingleWithCE,
                observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                version: ForecastingSession_Version
            );

        public static ForecastingSession ForecastingSession_SingleWithoutCE = new ForecastingSession(
                init: ForecastingInit_SingleWithoutCE,
                observations: Observations.ObjectMother.Observations_Containing01_WithoutInitCE,
                version: ForecastingSession_Version
            );

        public static ForecastingSession ForecastingSession_DoubleWithCE = new ForecastingSession(
                init: ForecastingInit_DoubleWithCE,
                observations: Observations.ObjectMother.Observations_Containing0102_WithInitCE,
                version: ForecastingSession_Version
            );

        public static ForecastingSession ForecastingSession_BareMinimum = new ForecastingSession(
                init: ForecastingInit_BareMinimum,
                observations: Observations.ObjectMother.Observations_BareMinimum,
                version: ForecastingSession_Version
            );

        #endregion

        #region Methods

        public static bool AreEqual(double double01, double double02, double delta = 0.00000000000001D)
            => Math.Abs(double01 - double02) < delta;
        public static bool AreEqual(List<double> list1, List<double> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(double? double01, double? double02)
        {

            if (double01 == null && double02 != null)
                return false;
            if (double01 != null && double02 == null)
                return false;

            if (double01 == null && double02 == null)
                return true;

            return AreEqual((double)double01, (double)double02);

        }
        public static bool AreEqual(ForecastingInit obj1, ForecastingInit obj2)
        {

            return string.Equals(obj1.ObservationName, obj2.ObservationName, StringComparison.InvariantCulture)
                        && AreEqual(obj1.Values, obj2.Values)
                        && AreEqual(obj1.Coefficient, obj2.Coefficient)
                        && AreEqual(obj1.Error, obj2.Error)
                        && Equals(obj1.Steps, obj2.Steps);

        }
        public static bool AreEqual(ForecastingSession obj1, ForecastingSession obj2)
        {

            return AreEqual(obj1.Init, obj2.Init)
                        && Observations.ObjectMother.AreEqual(obj1.Observations, obj2.Observations)
                        && string.Equals(obj1.Version, obj2.Version, StringComparison.InvariantCulture);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.03.2023
*/