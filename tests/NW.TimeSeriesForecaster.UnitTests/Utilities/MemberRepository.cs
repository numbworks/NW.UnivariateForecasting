using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting.UnitTests
{
    public static class MemberRepository
    {

        // Fields
        // Properties
        internal static IntervalUnits NonExistantIntervalUnit = (IntervalUnits)(-1); // Emulates a non-existant enum value
        internal static Interval InvalidIntervalDueOfSize = new Interval()
        {
            Size = 0, // <= invalid
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6
        };
        internal static Interval InvalidIntervalDueOfUnit = new Interval()
        {
            Size = 6, 
            Unit = NonExistantIntervalUnit, // <= invalid
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6
        };
        internal static Interval InvalidIntervalDueOfSteps = new Interval()
        {
            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 0, // <= invalid
            SubIntervals = 6
        };
        internal static Interval InvalidIntervalDueOfSizeBySteps = new Interval()
        {
            Size = 6, // <= invalid
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 4,  // <= invalid
            SubIntervals = 6
        };
        internal static Interval InvalidIntervalDueOfEndDate = new Interval()
        {
            Size = 6, 
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 06, 30), // <= invalid
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6
        };
        internal static Interval InvalidIntervalDueOfTargetDate = new Interval()
        {
            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 07, 31), // <= invalid
            Steps = 1,
            SubIntervals = 6
        };
        internal static Interval InvalidIntervalDueOfSubIntervals = new Interval()
        {
            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 5 // <= invalid
        };

        internal static string SlidingWindow1_Id = "SW20200906090516";
        internal static string SlidingWindow1_ObservationName = "Total Monthly Sales USD";
        internal static List<double> SlidingWindow1_Values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
        internal static uint SlidingWindow1_Steps = 1;
        internal static IntervalUnits SlidingWindow1_IntervalUnit = IntervalUnits.Months;
        internal static DateTime SlidingWindow1_StartDate = new DateTime(2019, 01, 31, 00, 00, 00);
        internal static Interval SlidingWindow1_Interval = new Interval()
        {
            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6
        };
        internal static Interval SlidingWindow1_SubInterval1 = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 02, 28),
            TargetDate = new DateTime(2019, 03, 31),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Interval SlidingWindow1_SubInterval2 = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 02, 28),
            EndDate = new DateTime(2019, 03, 31),
            TargetDate = new DateTime(2019, 04, 30),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Interval SlidingWindow1_SubInterval3 = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 03, 31),
            EndDate = new DateTime(2019, 04, 30),
            TargetDate = new DateTime(2019, 05, 31),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Interval SlidingWindow1_SubInterval4 = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 04, 30),
            EndDate = new DateTime(2019, 05, 31),
            TargetDate = new DateTime(2019, 06, 30),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Interval SlidingWindow1_SubInterval5 = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 05, 31),
            EndDate = new DateTime(2019, 06, 30),
            TargetDate = new DateTime(2019, 07, 31),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Interval SlidingWindow1_SubInterval6 = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 06, 30),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 1
        };
        internal static SlidingWindow SlidingWindow1 = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_Interval,
            Items = new List<SlidingWindowItem> ()
            {
                new SlidingWindowItem()
                {
                    Id = 1,
                    Interval = SlidingWindow1_SubInterval1,
                    X_Actual = 58.5,
                    Y_Forecasted = 615.26
                },
                new SlidingWindowItem()
                {
                    Id = 2,
                    Interval = SlidingWindow1_SubInterval2,
                    X_Actual = 615.26,
                    Y_Forecasted = 659.84
                },
                new SlidingWindowItem()
                {
                    Id = 3,
                    Interval = SlidingWindow1_SubInterval3,
                    X_Actual = 659.84,
                    Y_Forecasted = 635.69
                },
                new SlidingWindowItem()
                {
                    Id = 4,
                    Interval = SlidingWindow1_SubInterval4,
                    X_Actual = 635.69,
                    Y_Forecasted = 612.27
                },
                new SlidingWindowItem()
                {
                    Id = 5,
                    Interval = SlidingWindow1_SubInterval5,
                    X_Actual = 612.27,
                    Y_Forecasted = 632.94
                },
                new SlidingWindowItem()
                {
                    Id = 6,
                    Interval = SlidingWindow1_SubInterval6,
                    X_Actual = 632.94,
                    Y_Forecasted = null
                }
            }
        };
        internal static Observation Observation1 = new Observation()
        {
            Name= SlidingWindow1_ObservationName,
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
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = SlidingWindow1_Id
        };

        // Methods
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.09.2020

*/
