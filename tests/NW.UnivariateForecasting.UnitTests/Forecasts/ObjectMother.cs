using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests.Forecasts
{
    public static class ObjectMother
    {

        #region Properties

        internal static UnivariateForecaster UnivariateForecaster_Empty = new UnivariateForecaster();

        internal static string UnivariateForecaster_FaC_Id = "SW20200925000000";
        internal static Func<string> UnivariateForecaster_FaC_IdCreationFunction = () => UnivariateForecaster_FaC_Id;

        internal static SlidingWindow UnivariateForecaster_FaCSteps1_Final = new SlidingWindow()
        {
            Id = UnivariateForecaster_FaC_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
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
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item1,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item2,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item3,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item4,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item5,
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
                            Interval = Utilities.ObjectMother.Shared_Observation1_Interval,
                            X_Actual = 519.23,
                            Y_Forecasted = null
                        }
                    }
        };

        internal static SlidingWindow UnivariateForecaster_FaCSteps3_MidwaySlidingWindow_1 = UnivariateForecaster_FaCSteps1_Final;
        internal static Observation UnivariateForecaster_FaCSteps3_MidwayObservation_1 = new Observation()
        {

            Name = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
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
            SlidingWindowId = UnivariateForecaster_FaC_Id

        };
        internal static SlidingWindow UnivariateForecaster_FaCSteps3_MidwaySlidingWindow_2 = new SlidingWindow()
        {
            Id = UnivariateForecaster_FaC_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
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
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item1,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item2,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item3,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item4,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item5,
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
                            Interval = Utilities.ObjectMother.Shared_Observation1_Interval,
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
        internal static Observation UnivariateForecaster_FaCSteps3_MidwayObservation_2 = new Observation()
        {

            Name = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
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
            SlidingWindowId = UnivariateForecaster_FaC_Id

        };
        internal static SlidingWindow UnivariateForecaster_FaCSteps3_Final = new SlidingWindow()
        {
            Id = UnivariateForecaster_FaC_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
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
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item1,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item2,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item3,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item4,
                        Utilities.ObjectMother.Shared_SlidingWindow1_Item5,
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
                            Interval = Utilities.ObjectMother.Shared_Observation1_Interval,
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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/