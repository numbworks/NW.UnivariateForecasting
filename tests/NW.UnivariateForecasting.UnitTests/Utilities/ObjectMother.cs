﻿using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Utilities
{
    public static class ObjectMother
    {

        #region Shared

        internal static string Shared_SlidingWindow1_Id = "SW20200906090516";
        internal static string Shared_SlidingWindow1_ObservationName = "Total Monthly Sales USD";
        internal static uint Shared_SlidingWindow1_Steps = 1;

        internal static uint Shared_SlidingWindow1_Item1_Id = 1;
        internal static Interval Shared_SlidingWindow1_Item1_Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval01;
        internal static double Shared_SlidingWindow1_Item1_XActual = 58.5;
        internal static double? Shared_SlidingWindow1_Item1_YForecasted = 615.26;

        internal static SlidingWindowItem Shared_SlidingWindow1_Item1 = new SlidingWindowItem()
        {
            Id = Shared_SlidingWindow1_Item1_Id,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval01,
            X_Actual = Shared_SlidingWindow1_Item1_XActual,
            Y_Forecasted = Shared_SlidingWindow1_Item1_YForecasted
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item2 = new SlidingWindowItem()
        {
            Id = 2,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval02,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item3 = new SlidingWindowItem()
        {
            Id = 3,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval03,
            X_Actual = 659.84,
            Y_Forecasted = 635.69
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item4 = new SlidingWindowItem()
        {
            Id = 4,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval04,
            X_Actual = 635.69,
            Y_Forecasted = 612.27
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item5 = new SlidingWindowItem()
        {
            Id = 5,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval05,
            X_Actual = 612.27,
            Y_Forecasted = 632.94
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item6 = new SlidingWindowItem()
        {
            Id = 6,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval06,
            X_Actual = 632.94,
            Y_Forecasted = null
        };
        internal static List<SlidingWindowItem> Shared_SlidingWindow1_Items = new List<SlidingWindowItem>()
        {
            Shared_SlidingWindow1_Item1,
            Shared_SlidingWindow1_Item2,
            Shared_SlidingWindow1_Item3,
            Shared_SlidingWindow1_Item4,
            Shared_SlidingWindow1_Item5,
            Shared_SlidingWindow1_Item6
        };
        
        internal static SlidingWindow Shared_SlidingWindow1 = new SlidingWindow()
        {
            Id = Shared_SlidingWindow1_Id,
            ObservationName = Shared_SlidingWindow1_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = Shared_SlidingWindow1_Items
        };
        internal static List<double> Shared_SlidingWindow1_Values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
        internal static string Shared_SlidingWindow1_String = "[ Id: 'SW20200906090516', ObservationName: 'Total Monthly Sales USD', Interval: '6:Months:20190131:20190731:20190831:1:6', Items: '6' ]";
        internal static string Shared_SlidingWindow1_StringRolloutItems
            = string.Join(
                Environment.NewLine,
                Shared_SlidingWindow1_String,
                Shared_SlidingWindow1_Item1.ToString(),
                Shared_SlidingWindow1_Item2.ToString(),
                Shared_SlidingWindow1_Item3.ToString(),
                Shared_SlidingWindow1_Item4.ToString(),
                Shared_SlidingWindow1_Item5.ToString(),
                Shared_SlidingWindow1_Item6.ToString()
                );
        internal static string Shared_SlidingWindow1_Item1_String
            = $"[ Id: '1', Interval: '20190131:20190228:20190331', X_Actual: '{58.5}', Y_Forecasted: '{615.26}' ]";
        internal static List<DateTime> Shared_SlidingWindow1_StartDates = new List<DateTime>()
        {

            Shared_SlidingWindow1_Item1.Interval.StartDate,
            Shared_SlidingWindow1_Item2.Interval.StartDate,
            Shared_SlidingWindow1_Item3.Interval.StartDate,
            Shared_SlidingWindow1_Item4.Interval.StartDate,
            Shared_SlidingWindow1_Item5.Interval.StartDate,
            Shared_SlidingWindow1_Item6.Interval.StartDate

        };

        internal static Interval Shared_Observation1_Interval = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 07, 31),
            EndDate = new DateTime(2019, 08, 31),
            TargetDate = new DateTime(2019, 09, 30),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Observation Shared_Observation1 = new Observation()
        {

            Name = Shared_SlidingWindow1_ObservationName,
            Interval = Shared_Observation1_Interval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = Shared_SlidingWindow1_Id

        };
        internal static string Shared_Observation1_String
            = $"[ Name: 'Total Monthly Sales USD', Interval: '1:Months:20190731:20190831:20190930:1:1', X_Actual: '{632.94.ToString()}', C: '{0.82.ToString()}', E: '{0.22.ToString()}', Y_Forecasted: '{519.23.ToString()}', SlidingWindowId: 'SW20200906090516' ]";
        internal static string Shared_Observation1_StringOnlyDates
            = $"[ Name: 'Total Monthly Sales USD', Interval: '20190731:20190831:20190930', X_Actual: '{632.94.ToString()}', C: '{0.82.ToString()}', E: '{0.22.ToString()}', Y_Forecasted: '{519.23.ToString()}', SlidingWindowId: 'SW20200906090516' ]";

        internal static double Shared_Observation1WithCustomCE_C = 0.92;
        internal static double Shared_Observation1WithCustomCE_E = 0.12;
        internal static double Shared_Observation1WithCustomCE_Y = 582.42;
        internal static Observation Shared_Observation1WithCustomCE = new Observation()
        {

            Name = Shared_Observation1.Name,
            Interval = Shared_Observation1.Interval,
            SlidingWindowId = Shared_Observation1.SlidingWindowId,
            X_Actual = Shared_Observation1.X_Actual,
            C = Shared_Observation1WithCustomCE_C,
            E = Shared_Observation1WithCustomCE_E,
            Y_Forecasted = Shared_Observation1WithCustomCE_Y

        };

        internal static Interval Shared_SlidingWindow1_DummyInterval
            = new IntervalManager().Create(
                    (uint)Shared_SlidingWindow1_Values.Count,
                    UnivariateForecastingSettings.DefaultDummyIntervalUnit,
                    UnivariateForecastingSettings.DefaultDummyStartDate,
                    UnivariateForecastingSettings.DefaultDummySteps
                    );
        internal static List<SlidingWindowItem> Shared_SlidingWindow1_DefaultDummyItems
            = new SlidingWindowItemManager().CreateItems(
                    UnivariateForecastingSettings.DefaultDummyStartDate,
                    Shared_SlidingWindow1_Values,
                    UnivariateForecastingSettings.DefaultDummyIntervalUnit
                );
        internal static SlidingWindow Shared_SlidingWindow1_WithDefaultDummyFields = new SlidingWindow()
        {

            Id = UnivariateForecastingSettings.DefaultDummyId,
            ObservationName = UnivariateForecastingSettings.DefaultDummyObservationName,
            Interval = Shared_SlidingWindow1_DummyInterval,
            Items = Shared_SlidingWindow1_DefaultDummyItems
        };

        internal static Interval Shared_Observation1_DefaultDummyInterval = new Interval()
        {
            Size = 1,
            Unit = UnivariateForecastingSettings.DefaultDummyIntervalUnit,
            StartDate = new DateTime(2020, 07, 01),
            EndDate = new DateTime(2020, 08, 01),
            TargetDate = new DateTime(2020, 09, 01),
            Steps = UnivariateForecastingSettings.DefaultDummySteps,
            SubIntervals = 1
        };
        internal static Observation Shared_Observation1_WithDefaultDummyFields = new Observation()
        {

            Name = UnivariateForecastingSettings.DefaultDummyObservationName,
            Interval = Shared_Observation1_DefaultDummyInterval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = UnivariateForecastingSettings.DefaultDummyId

        };

        internal static IFileAdapter Shared_FileAdapter_ReadAllTextReturnsSlidingWindowWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw Files.ObjectMother.FileAdapterIOException,
                    fakeReadAllText: () => Properties.Resources.SlidingWindowWithDummyValues
                );
        internal static IFileAdapter Shared_FileAdapter_ReadAllTextReturnsObservationWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw Files.ObjectMother.FileAdapterIOException,
                    fakeReadAllText: () => Properties.Resources.ObservationWithDummyValues
                );

        #endregion


        #region Methods
        
        internal static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }
        internal static bool AreEqual<T>(List<T> list1, List<T> list2, Func<T, T, bool> comparer)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (comparer(list1[i], list2[i]) == false)
                    return false;

            return true;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.11.2022
*/
