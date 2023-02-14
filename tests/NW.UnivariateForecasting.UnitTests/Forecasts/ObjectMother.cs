using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.UnitTests.Utilities;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    public static class ObjectMother
    {

        #region Properties

        internal static UnivariateForecaster UnivariateForecaster = new UnivariateForecaster();

        internal static IFileAdapter FakeFileAdapter_ReadAllTextReturnsSlidingWindowWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw Files.ObjectMother.FileAdapterIOException,
                    fakeReadAllText: () => Properties.Resources.SlidingWindowWithDummyValues
                );
        internal static IFileAdapter FakeFileAdapter_ReadAllTextReturnsObservationWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw Files.ObjectMother.FileAdapterIOException,
                    fakeReadAllText: () => Properties.Resources.ObservationWithDummyValues
                );

        internal static string ForecastingInit_ObservationName = "Sales USD";
        internal static List<decimal> ForecastingInit_Values = new List<decimal>() { 58.5M, 615.26M, 659.84M, 635.69M, 612.27M, 632.94M };
        internal static decimal ForecastingInit_Coefficient = 0.5M;
        internal static decimal ForecastingInit_Error = 0.01M;
        internal static ForecastingInit ForecastingInit
            = new ForecastingInit(
                    observationName: ForecastingInit_ObservationName,
                    values: ForecastingInit_Values,
                    coefficient: ForecastingInit_Coefficient,
                    error: ForecastingInit_Error
                    );

        internal static ForecastingInit ForecastingInit_Minimal
            = new ForecastingInit(
                    observationName: null,
                    values: ForecastingInit_Values,
                    coefficient: null,
                    error: null
                    );

        internal static decimal NextValue = 519.23M;
        internal static List<decimal> ExpandedValues = new List<decimal>() { 58.5M, 615.26M, 659.84M, 635.69M, 612.27M, 632.94M, NextValue };
        internal static ForecastingInit ForecastingInit_WithExpandedValues
            = new ForecastingInit(
                    observationName: ForecastingInit_ObservationName,
                    values: ExpandedValues,
                    coefficient: ForecastingInit_Coefficient,
                    error: ForecastingInit_Error
                    );

        internal static string ForecastingInitAsJson_Content = Properties.Resources.ForecastingInitAsJson;
        internal static string ForecastingInitMinimalAsJson_Content = Properties.Resources.ForecastingInitMinimalAsJson;


        internal static string ForecastingSession_Version = "3.0.0.0";
        internal static uint ForecastingSession_Single_Steps = 1;
        internal static ForecastingSession ForecastingSession_Single = new ForecastingSession(
                    init: ForecastingInit,
                    observations: Observations.ObjectMother.Observations_With01,
                    steps: ForecastingSession_Single_Steps,
                    version: ForecastingSession_Version
                );
        internal static ForecastingSession ForecastingSession_SingleMinimal = new ForecastingSession(
                    init: ForecastingInit_Minimal,
                    observations: Observations.ObjectMother.Observations_With01,
                    steps: ForecastingSession_Single_Steps,
                    version: ForecastingSession_Version
                );

        internal static uint ForecastingSession_Multiple_Steps = 2;
        internal static ForecastingSession ForecastingSession_Multiple = new ForecastingSession(
                    init: ForecastingInit,
                    observations: Observations.ObjectMother.Observations_With0102,
                    steps: ForecastingSession_Multiple_Steps,
                    version: ForecastingSession_Version
                );

        internal static string ForecastingSessionSingleAsJson_Content = Properties.Resources.ForecastingSessionSingleAsJson;
        internal static string ForecastingSessionSingleMinimalAsJson_Content = Properties.Resources.ForecastingSessionSingleMinimalAsJson;
        internal static string ForecastingSessionMultipleAsJson_Content = Properties.Resources.ForecastingSessionMultipleAsJson;

        #endregion

        #region Methods

        internal static bool AreEqual(decimal dec1, decimal dec2)
            => Math.Abs(dec1 - dec2) < 0.0001M;
        internal static bool AreEqual(List<decimal> list1, List<decimal> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        internal static bool AreEqual(decimal? dec1, decimal? dec2)
        {

            if (dec1 == null && dec2 != null)
                return false;
            if (dec1 != null && dec2 == null)
                return false;

            if (dec1 == null && dec2 == null)
                return true;

            return AreEqual((decimal)dec1, (decimal)dec2);

        }
        internal static bool AreEqual(ForecastingInit obj1, ForecastingInit obj2)
        {

            return string.Equals(obj1.ObservationName, obj2.ObservationName, StringComparison.InvariantCulture)
                        && AreEqual(obj1.Values, obj2.Values)
                        && AreEqual(obj1.Coefficient, obj2.Coefficient)
                        && AreEqual(obj1.Error, obj2.Error);

        }
        internal static bool AreEqual(ForecastingSession obj1, ForecastingSession obj2)
        {

            return AreEqual(obj1.Init, obj2.Init)
                        && Observations.ObjectMother.AreEqual(obj1.Observations, obj2.Observations)
                        && Equals(obj1.Steps, obj2.Steps)
                        && string.Equals(obj1.Version, obj2.Version, StringComparison.InvariantCulture);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/