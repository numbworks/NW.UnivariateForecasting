using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.UnitTests.Intervals
{
    public static class ObjectMother
    {

        #region Properties

        internal static Interval Interval_Empty = new Interval();
        internal static string Interval_Empty_AsString = "0:Months:00010101:00010101:00010101:0:0";
        internal static string Interval_Empty_AsStringOnlyDates = "00010101:00010101:00010101";

        internal static IntervalUnits IntervalUnit_NonExistant = (IntervalUnits)(-1); // Emulates a non-existant enum value

        internal static Interval Interval_InvalidDueOfSize = new Interval()
        {

            Size = 0, // <= invalid
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Interval_InvalidDueOfUnit = new Interval()
        {

            Size = 6,
            Unit = IntervalUnit_NonExistant, // <= invalid
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Interval_InvalidDueOfSteps = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 0, // <= invalid
            SubIntervals = 6

        };
        internal static Interval Interval_InvalidDueOfSizeBySteps = new Interval()
        {

            Size = 6, // <= invalid
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 4,  // <= invalid
            SubIntervals = 6

        };
        internal static Interval Interval_InvalidDueOfEndDate = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 06, 30), // <= invalid
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Interval_InvalidDueOfTargetDate = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 07, 31), // <= invalid
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Interval_InvalidDueOfSubIntervals = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 5 // <= invalid

        };

        internal static IntervalUnits IntervalUnits_Months = IntervalUnits.Months;

        internal static DateTime Interval_SixMonths_StartDate = new DateTime(2019, 01, 31, 00, 00, 00);
        internal static DateTime Interval_SixMonths_EndDate = new DateTime(2019, 07, 31, 00, 00, 00);
        internal static DateTime Interval_SixMonths_TargetDate = new DateTime(2019, 08, 31, 00, 00, 00);

        internal static Interval Interval_SixMonths = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = Interval_SixMonths_StartDate,
            EndDate = Interval_SixMonths_EndDate,
            TargetDate = Interval_SixMonths_TargetDate,
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Interval_SixMonths_SubInterval01 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = Interval_SixMonths_StartDate,
            EndDate = new DateTime(2019, 02, 28),
            TargetDate = new DateTime(2019, 03, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Interval_SixMonths_SubInterval02 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 02, 28),
            EndDate = new DateTime(2019, 03, 31),
            TargetDate = new DateTime(2019, 04, 30),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Interval_SixMonths_SubInterval03 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 03, 31),
            EndDate = new DateTime(2019, 04, 30),
            TargetDate = new DateTime(2019, 05, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Interval_SixMonths_SubInterval04 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 04, 30),
            EndDate = new DateTime(2019, 05, 31),
            TargetDate = new DateTime(2019, 06, 30),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Interval_SixMonths_SubInterval05 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 05, 31),
            EndDate = new DateTime(2019, 06, 30),
            TargetDate = new DateTime(2019, 07, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Interval_SixMonths_SubInterval06 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 06, 30),
            EndDate = Interval_SixMonths_EndDate,
            TargetDate = Interval_SixMonths_TargetDate,
            Steps = 1,
            SubIntervals = 1

        };

        internal static string Interval_SixMonths_AsString = "6:Months:20190131:20190731:20190831:1:6";
        internal static string Interval_SixMonths_AsStringOnlyDates = "20190131:20190731:20190831";
        internal static string Interval_SixMonths_SubInterval01_AsString = "1:Months:20190131:20190228:20190331:1:1";
        internal static string Interval_SixMonths_SubInterval01_AsStringOnlyDates = "20190131:20190228:20190331";

        #endregion

        #region Methods

        internal static bool AreEqual(Interval obj1, Interval obj2)
        {

            return Equals(obj1.Size, obj2.Size)
                        && Equals(obj1.Unit, obj2.Unit)
                        && Equals(obj1.StartDate, obj2.StartDate)
                        && Equals(obj1.Steps, obj2.Steps);

        }
        internal static bool AreEqual(List<Interval> list1, List<Interval> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/