using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    public static class ObjectMother
    {

        #region Properties

        internal static string SlidingWindow01_Id = "SW20200906090516";
        internal static string SlidingWindow01_ObservationName = "Total Monthly Sales USD";

        internal static ObservationManager ObservationManager_Empty = new ObservationManager();
        internal static Observation Observation_InvalidDueOfNullName = new Observation()
        {

            Name = null

        };
        internal static Observation Observation_InvalidDueOfNullInterval = new Observation()
        {

            Name = SlidingWindow01_ObservationName,
            Interval = null

        };
        internal static Observation Observation_InvalidDueOfNullSlidingWindow = new Observation()
        {

            Name = SlidingWindow01_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval01, // Whatever valid Interval
            SlidingWindowId = null

        };

        internal static Observation Observation_Empty = new Observation();
        internal static string Observation_Empty_AsString
            = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";
        internal static string Observation_Empty_AsStringOnlyDates
            = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";


        internal static Interval Observation01_Interval = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 07, 31),
            EndDate = new DateTime(2019, 08, 31),
            TargetDate = new DateTime(2019, 09, 30),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Observation Observation01 = new Observation()
        {

            Name = SlidingWindow01_ObservationName,
            Interval = Observation01_Interval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = SlidingWindow01_Id

        };
        internal static string Observation01_AsString
            = $"[ Name: 'Total Monthly Sales USD', Interval: '1:Months:20190731:20190831:20190930:1:1', X_Actual: '{632.94}', C: '{0.82}', E: '{0.22}', Y_Forecasted: '{519.23}', SlidingWindowId: 'SW20200906090516' ]";
        internal static string Observation01_AsStringOnlyDates
            = $"[ Name: 'Total Monthly Sales USD', Interval: '20190731:20190831:20190930', X_Actual: '{632.94}', C: '{0.82}', E: '{0.22}', Y_Forecasted: '{519.23}', SlidingWindowId: 'SW20200906090516' ]";

        internal static double Observation01_WithCustomCE_C = 0.92;
        internal static double Observation01_WithCustomCE_E = 0.12;
        internal static double Observation01_WithCustomCE_Y = 582.42;
        internal static Observation Observation01_WithCustomCE = new Observation()
        {

            Name = Observation01.Name,
            Interval = Observation01.Interval,
            SlidingWindowId = Observation01.SlidingWindowId,
            X_Actual = Observation01.X_Actual,
            C = Observation01_WithCustomCE_C,
            E = Observation01_WithCustomCE_E,
            Y_Forecasted = Observation01_WithCustomCE_Y

        };

        internal static Interval Observation01_WithDefaultDummyFields_Interval = new Interval()
        {
            Size = 1,
            Unit = UnivariateForecastingSettings.DefaultDummyIntervalUnit,
            StartDate = new DateTime(2020, 07, 01),
            EndDate = new DateTime(2020, 08, 01),
            TargetDate = new DateTime(2020, 09, 01),
            Steps = UnivariateForecastingSettings.DefaultDummySteps,
            SubIntervals = 1
        };
        internal static Observation Observation01_WithDefaultDummyFields = new Observation()
        {

            Name = UnivariateForecastingSettings.DefaultDummyObservationName,
            Interval = Observation01_WithDefaultDummyFields_Interval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = UnivariateForecastingSettings.DefaultDummyId

        };

        #endregion

        #region Methods

        internal static bool AreEqual(Observation obj1, Observation obj2)
        {

            return string.Equals(obj1.Name, obj2.Name, StringComparison.InvariantCulture)
                        && Intervals.ObjectMother.AreEqual(obj1.Interval, obj2.Interval)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.C, obj2.C)
                        && Equals(obj1.E, obj2.E)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted)
                        && string.Equals(obj1.SlidingWindowId, obj2.SlidingWindowId, StringComparison.InvariantCulture);

        }
        internal static bool AreEqual(List<Observation> list1, List<Observation> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/