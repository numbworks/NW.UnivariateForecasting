using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting.UnitTests
{
    public static class MemberRepository
    {

        // Fields
        // Properties
        internal static string SlidingWindowOne_Id = "SW20200906090516";
        internal static string SlidingWindowOne_ObservationName = "Total Monthly Sales USD";
        internal static List<double> SlidingWindowOne_Values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
        internal static uint SlidingWindowOne_Steps = 1;
        internal static IntervalUnits SlidingWindowOne_IntervalUnit = IntervalUnits.Months;
        internal static DateTime SlidingWindowOne_StartDate = new DateTime(2019, 01, 31, 00, 00, 00);
        internal static SlidingWindow SlidingWindowOne = new SlidingWindow()
        {

            Id = SlidingWindowOne_Id,
            ObservationName = SlidingWindowOne_ObservationName,
            Interval = new Interval()
            {
                Size = 6,
                Unit = IntervalUnits.Months,
                StartDate = new DateTime(2019, 01, 31),
                EndDate = new DateTime(2019, 07, 31),
                TargetDate = new DateTime(2019, 08, 31),
                Steps = 1,
                SubIntervals = 6
            },
            Items = new List<SlidingWindowItem> ()
            {
                new SlidingWindowItem()
                {
                    Id = 1,
                    Interval = new Interval()
                    {
                        Size = 1,
                        Unit = IntervalUnits.Months,
                        StartDate = new DateTime(2019, 01, 31),
                        EndDate = new DateTime(2019, 02, 28),
                        TargetDate = new DateTime(2019, 03, 31),
                        Steps = 1,
                        SubIntervals = 1
                    },
                    X_Actual = 58.5,
                    Y_Forecasted = 615.26
                },
                new SlidingWindowItem()
                {
                    Id = 2,
                    Interval = new Interval()
                    {
                        Size = 1,
                        Unit = IntervalUnits.Months,
                        StartDate = new DateTime(2019, 02, 28),
                        EndDate = new DateTime(2019, 03, 31),
                        TargetDate = new DateTime(2019, 04, 30),
                        Steps = 1,
                        SubIntervals = 1
                    },
                    X_Actual = 615.26,
                    Y_Forecasted = 659.84
                },
                new SlidingWindowItem()
                {
                    Id = 3,
                    Interval = new Interval()
                    {
                        Size = 1,
                        Unit = IntervalUnits.Months,
                        StartDate = new DateTime(2019, 03, 31),
                        EndDate = new DateTime(2019, 04, 30),
                        TargetDate = new DateTime(2019, 05, 31),
                        Steps = 1,
                        SubIntervals = 1
                    },
                    X_Actual = 659.84,
                    Y_Forecasted = 635.69
                },
                new SlidingWindowItem()
                {
                    Id = 4,
                    Interval = new Interval()
                    {
                        Size = 1,
                        Unit = IntervalUnits.Months,
                        StartDate = new DateTime(2019, 04, 30),
                        EndDate = new DateTime(2019, 05, 31),
                        TargetDate = new DateTime(2019, 06, 30),
                        Steps = 1,
                        SubIntervals = 1
                    },
                    X_Actual = 635.69,
                    Y_Forecasted = 612.27
                },
                new SlidingWindowItem()
                {
                    Id = 5,
                    Interval = new Interval()
                    {
                        Size = 1,
                        Unit = IntervalUnits.Months,
                        StartDate = new DateTime(2019, 05, 31),
                        EndDate = new DateTime(2019, 06, 30),
                        TargetDate = new DateTime(2019, 07, 31),
                        Steps = 1,
                        SubIntervals = 1
                    },
                    X_Actual = 612.27,
                    Y_Forecasted = 632.94
                },
                new SlidingWindowItem()
                {
                    Id = 6,
                    Interval = new Interval()
                    {
                        Size = 1,
                        Unit = IntervalUnits.Months,
                        StartDate = new DateTime(2019, 06, 30),
                        EndDate = new DateTime(2019, 07, 31),
                        TargetDate = new DateTime(2019, 08, 31),
                        Steps = 1,
                        SubIntervals = 1
                    },
                    X_Actual = 632.94,
                    Y_Forecasted = null
                }
            }

        };
        internal static Observation ObservationOne = new Observation()
        {
            Name= SlidingWindowOne_ObservationName,
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
            SlidingWindowId = SlidingWindowOne_Id
        };

        // Methods
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.09.2020

*/
