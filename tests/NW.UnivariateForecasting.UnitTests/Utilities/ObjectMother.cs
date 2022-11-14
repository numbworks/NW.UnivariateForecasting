using System;
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

            Name = SlidingWindows.ObjectMother.SlidingWindow01_ObservationName,
            Interval = Shared_Observation1_Interval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = SlidingWindows.ObjectMother.SlidingWindow01_Id

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
                    (uint)SlidingWindows.ObjectMother.SlidingWindow01_Values.Count,
                    UnivariateForecastingSettings.DefaultDummyIntervalUnit,
                    UnivariateForecastingSettings.DefaultDummyStartDate,
                    UnivariateForecastingSettings.DefaultDummySteps
                    );
        internal static List<SlidingWindowItem> Shared_SlidingWindow1_DefaultDummyItems
            = new SlidingWindowItemManager().CreateItems(
                    UnivariateForecastingSettings.DefaultDummyStartDate,
                    SlidingWindows.ObjectMother.SlidingWindow01_Values,
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
    Last Update: 14.11.2022
*/
