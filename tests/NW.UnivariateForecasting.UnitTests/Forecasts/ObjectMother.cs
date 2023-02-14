using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    public static class ObjectMother
    {

        #region Properties

        internal static UnivariateForecaster UnivariateForecaster = new UnivariateForecaster();

        internal static string ForecastAndCombine_Id = "SW20200925000000";
        internal static Func<string> ForecastAndCombine_IdCreationFunction = () => ForecastAndCombine_Id;

        internal static SlidingWindow SlidingWindow_ForecastAndCombineSteps1_Final = new SlidingWindow()
        {
            Id = ForecastAndCombine_Id,
            ObservationName = SlidingWindows.ObjectMother.SlidingWindow01_ObservationName,
            Interval = new Interval()
            {

                Size = 7,
                Unit = IntervalUnits.Months,
                StartDate = new DateTime(2019, 01, 31),
                EndDate = new DateTime(2019, 08, 31),
                TargetDate = new DateTime(2019, 09, 30),
                Steps = 1,
                SubIntervals = 7

            },
            Items = new List<SlidingWindowItem>()
                    {
                        SlidingWindows.ObjectMother.SlidingWindow01_Item01,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item02,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item03,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item04,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item05,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval06,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Observations.ObjectMother.Observation01_Interval,
                            X_Actual = 519.23,
                            Y_Forecasted = null
                        }
                    }
        };

        internal static SlidingWindow SlidingWindow_ForecastAndCombineSteps1_01 = SlidingWindow_ForecastAndCombineSteps1_Final;
        internal static Observation SlidingWindow_ForecastAndCombineSteps1_01_Observation = new Observation()
        {

            Name = SlidingWindows.ObjectMother.SlidingWindow01_ObservationName,
            Interval = new Interval()
            {
                Size = 1,
                Unit = IntervalUnits.Months,
                StartDate = new DateTime(2019, 08, 31),
                EndDate = new DateTime(2019, 09, 30),
                TargetDate = new DateTime(2019, 10, 31),
                Steps = 1,
                SubIntervals = 1
            },
            X_Actual = 519.23,
            C = 0.88,
            E = 0.16,
            Y_Forecasted = 457.08,
            SlidingWindowId = ForecastAndCombine_Id

        };
        
        internal static SlidingWindow SlidingWindow_ForecastAndCombineSteps3_02 = new SlidingWindow()
        {
            Id = ForecastAndCombine_Id,
            ObservationName = SlidingWindows.ObjectMother.SlidingWindow01_ObservationName,
            Interval = new Interval()
            {

                Size = 8,
                Unit = IntervalUnits.Months,
                StartDate = new DateTime(2019, 01, 31),
                EndDate = new DateTime(2019, 09, 30),
                TargetDate = new DateTime(2019, 10, 31),
                Steps = 1,
                SubIntervals = 8

            },
            Items = new List<SlidingWindowItem>()
                    {
                        SlidingWindows.ObjectMother.SlidingWindow01_Item01,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item02,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item03,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item04,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item05,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval06,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Observations.ObjectMother.Observation01_Interval,
                            X_Actual = 519.23,
                            Y_Forecasted = 457.08
                        },
                        new SlidingWindowItem()
                        {
                            Id = 8,
                            Interval = new Interval()
                                        {
                                            Size = 1,
                                            Unit = IntervalUnits.Months,
                                            StartDate = new DateTime(2019, 08, 31),
                                            EndDate = new DateTime(2019, 09, 30),
                                            TargetDate = new DateTime(2019, 10, 31),
                                            Steps = 1,
                                            SubIntervals = 1
                                        },
                            X_Actual = 457.08,
                            Y_Forecasted = null
                        }
                    }
        };
        internal static Observation SlidingWindow_ForecastAndCombineSteps3_02_Observation = new Observation()
        {

            Name = SlidingWindows.ObjectMother.SlidingWindow01_ObservationName,
            Interval = new Interval()
            {
                Size = 1,
                Unit = IntervalUnits.Months,
                StartDate = new DateTime(2019, 09, 30),
                EndDate = new DateTime(2019, 10, 31),
                TargetDate = new DateTime(2019, 11, 30),
                Steps = 1,
                SubIntervals = 1
            },
            X_Actual = 457.08,
            C = 0.92,
            E = 0.12,
            Y_Forecasted = 420.63,
            SlidingWindowId = ForecastAndCombine_Id

        };
        
        internal static SlidingWindow SlidingWindow_ForecastAndCombineSteps3_Final = new SlidingWindow()
        {
            Id = ForecastAndCombine_Id,
            ObservationName = SlidingWindows.ObjectMother.SlidingWindow01_ObservationName,
            Interval = new Interval()
            {

                Size = 9,
                Unit = IntervalUnits.Months,
                StartDate = new DateTime(2019, 01, 31),
                EndDate = new DateTime(2019, 10, 31),
                TargetDate = new DateTime(2019, 11, 30),
                Steps = 1,
                SubIntervals = 9

            },
            Items = new List<SlidingWindowItem>()
                    {
                        SlidingWindows.ObjectMother.SlidingWindow01_Item01,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item02,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item03,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item04,
                        SlidingWindows.ObjectMother.SlidingWindow01_Item05,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval06,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Observations.ObjectMother.Observation01_Interval,
                            X_Actual = 519.23,
                            Y_Forecasted = 457.08
                        },
                        new SlidingWindowItem()
                        {
                            Id = 8,
                            Interval = new Interval()
                                        {
                                            Size = 1,
                                            Unit = IntervalUnits.Months,
                                            StartDate = new DateTime(2019, 08, 31),
                                            EndDate = new DateTime(2019, 09, 30),
                                            TargetDate = new DateTime(2019, 10, 31),
                                            Steps = 1,
                                            SubIntervals = 1
                                        },
                            X_Actual = 457.08,
                            Y_Forecasted = 420.63
                        },
                        new SlidingWindowItem()
                        {
                            Id = 9,
                            Interval = new Interval()
                                        {
                                            Size = 1,
                                            Unit = IntervalUnits.Months,
                                            StartDate = new DateTime(2019, 09, 30),
                                            EndDate = new DateTime(2019, 10, 31),
                                            TargetDate = new DateTime(2019, 11, 30),
                                            Steps = 1,
                                            SubIntervals = 1
                                        },
                            X_Actual = 420.63,
                            Y_Forecasted = null
                        }

                    }
        };

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

        internal static uint ForecastingSession_Steps = 1;
        internal static string ForecastingSession_Version = "3.0.0.0";
        internal static ForecastingSession ForecastingSession = new ForecastingSession(
                    init: ForecastingInit,
                    observations: Observations.ObjectMother.Observations,
                    steps: ForecastingSession_Steps,
                    version: ForecastingSession_Version
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

        internal static string ForecastingSessionSingleAsJson_Content = Properties.Resources.ForecastingSessionSingleAsJson;
        internal static string ForecastingSessionSingleMinimalAsJson_Content = Properties.Resources.ForecastingSessionSingleMinimalAsJson;
        internal static string ForecastingSessionMultipleAsJson_Content = Properties.Resources.ForecastingSessionMultipleAsJson;

        #endregion

        #region Methods

        internal static bool AreEqual(decimal double1, decimal double2)
            => Math.Abs(double1 - double2) < 0.0001M;
        internal static bool AreEqual(List<decimal> list1, List<decimal> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        internal static bool AreEqual(decimal? double1, decimal? double2)
        {

            if (double1 == null && double2 != null)
                return false;
            if (double1 != null && double2 == null)
                return false;

            if (double1 == null && double2 == null)
                return true;

            return AreEqual((decimal)double1, (decimal)double2);

        }
        internal static bool AreEqual(ForecastingInit obj1, ForecastingInit obj2)
        {

            return string.Equals(obj1.ObservationName, obj2.ObservationName, StringComparison.InvariantCulture)
                        && AreEqual(obj1.Values, obj2.Values)
                        && AreEqual(obj1.Coefficient, obj2.Coefficient)
                        && AreEqual(obj1.Error, obj2.Error);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.02.2023
*/