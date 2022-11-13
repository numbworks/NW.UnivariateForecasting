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

        internal static IntervalUnits Shared_NonExistantIntervalUnit = (IntervalUnits)(-1); // Emulates a non-existant enum value

        internal static Interval Shared_IntervalInvalidDueOfSize = new Interval()
        {

            Size = 0, // <= invalid
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Shared_IntervalDueOfUnit = new Interval()
        {

            Size = 6,
            Unit = Shared_NonExistantIntervalUnit, // <= invalid
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Shared_IntervalDueOfSteps = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 0, // <= invalid
            SubIntervals = 6

        };
        internal static Interval Shared_IntervalDueOfSizeBySteps = new Interval()
        {

            Size = 6, // <= invalid
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 4,  // <= invalid
            SubIntervals = 6

        };
        internal static Interval Shared_IntervalDueOfEndDate = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 06, 30), // <= invalid
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Shared_IntervalDueOfTargetDate = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 07, 31), // <= invalid
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Shared_IntervalDueOfSubIntervals = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 5 // <= invalid

        };

        internal static IntervalUnits Shared_SlidingWindow1_IntervalUnit = IntervalUnits.Months;
        internal static DateTime Shared_SlidingWindow1_StartDate = new DateTime(2019, 01, 31, 00, 00, 00);
        internal static Interval Shared_SlidingWindow1_Interval = new Interval()
        {

            Size = 6,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 6

        };
        internal static Interval Shared_SlidingWindow1_SubInterval1 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 01, 31),
            EndDate = new DateTime(2019, 02, 28),
            TargetDate = new DateTime(2019, 03, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Shared_SlidingWindow1_SubInterval2 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 02, 28),
            EndDate = new DateTime(2019, 03, 31),
            TargetDate = new DateTime(2019, 04, 30),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Shared_SlidingWindow1_SubInterval3 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 03, 31),
            EndDate = new DateTime(2019, 04, 30),
            TargetDate = new DateTime(2019, 05, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Shared_SlidingWindow1_SubInterval4 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 04, 30),
            EndDate = new DateTime(2019, 05, 31),
            TargetDate = new DateTime(2019, 06, 30),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Shared_SlidingWindow1_SubInterval5 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 05, 31),
            EndDate = new DateTime(2019, 06, 30),
            TargetDate = new DateTime(2019, 07, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static Interval Shared_SlidingWindow1_SubInterval6 = new Interval()
        {

            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 06, 30),
            EndDate = new DateTime(2019, 07, 31),
            TargetDate = new DateTime(2019, 08, 31),
            Steps = 1,
            SubIntervals = 1

        };
        internal static string Shared_SlidingWindow1_Interval_String = "6:Months:20190131:20190731:20190831:1:6";
        internal static string Shared_SlidingWindow1_Interval_StringOnlyDates = "20190131:20190731:20190831";
        internal static string Shared_SlidingWindow1_SubInterval1_String = "1:Months:20190131:20190228:20190331:1:1";
        internal static string Shared_SlidingWindow1_SubInterval1_StringOnlyDates = "20190131:20190228:20190331";
        internal static string Shared_SlidingWindow1_Id = "SW20200906090516";
        internal static string Shared_SlidingWindow1_ObservationName = "Total Monthly Sales USD";
        internal static uint Shared_SlidingWindow1_Steps = 1;

        internal static uint Shared_SlidingWindow1_Item1_Id = 1;
        internal static Interval Shared_SlidingWindow1_Item1_Interval = Shared_SlidingWindow1_SubInterval1;
        internal static double Shared_SlidingWindow1_Item1_XActual = 58.5;
        internal static double? Shared_SlidingWindow1_Item1_YForecasted = 615.26;

        internal static SlidingWindowItem Shared_SlidingWindow1_Item1 = new SlidingWindowItem()
        {
            Id = Shared_SlidingWindow1_Item1_Id,
            Interval = Shared_SlidingWindow1_SubInterval1,
            X_Actual = Shared_SlidingWindow1_Item1_XActual,
            Y_Forecasted = Shared_SlidingWindow1_Item1_YForecasted
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item2 = new SlidingWindowItem()
        {
            Id = 2,
            Interval = Shared_SlidingWindow1_SubInterval2,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item3 = new SlidingWindowItem()
        {
            Id = 3,
            Interval = Shared_SlidingWindow1_SubInterval3,
            X_Actual = 659.84,
            Y_Forecasted = 635.69
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item4 = new SlidingWindowItem()
        {
            Id = 4,
            Interval = Shared_SlidingWindow1_SubInterval4,
            X_Actual = 635.69,
            Y_Forecasted = 612.27
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item5 = new SlidingWindowItem()
        {
            Id = 5,
            Interval = Shared_SlidingWindow1_SubInterval5,
            X_Actual = 612.27,
            Y_Forecasted = 632.94
        };
        internal static SlidingWindowItem Shared_SlidingWindow1_Item6 = new SlidingWindowItem()
        {
            Id = 6,
            Interval = Shared_SlidingWindow1_SubInterval6,
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
            Interval = Shared_SlidingWindow1_Interval,
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


        #region UnivariateForecasterTests

        internal static UnivariateForecaster UnivariateForecaster_Default = new UnivariateForecaster();
        internal static string UnivariateForecaster_FaC_Id = "SW20200925000000";
        internal static Func<string> UnivariateForecaster_FaC_IdCreationFunction = () => UnivariateForecaster_FaC_Id;
        internal static SlidingWindow UnivariateForecaster_FaCSteps1_Final = new SlidingWindow()
        {
            Id = UnivariateForecaster_FaC_Id,
            ObservationName = Shared_SlidingWindow1_ObservationName,
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
                        Shared_SlidingWindow1_Item1,
                        Shared_SlidingWindow1_Item2,
                        Shared_SlidingWindow1_Item3,
                        Shared_SlidingWindow1_Item4,
                        Shared_SlidingWindow1_Item5,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = Shared_SlidingWindow1_SubInterval6,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Shared_Observation1_Interval,
                            X_Actual = 519.23,
                            Y_Forecasted = null
                        }
                    }
        };
        internal static SlidingWindow UnivariateForecaster_FaCSteps3_MidwaySlidingWindow_1 = UnivariateForecaster_FaCSteps1_Final;
        internal static Observation UnivariateForecaster_FaCSteps3_MidwayObservation_1 = new Observation()
        {

            Name = Shared_SlidingWindow1_ObservationName,
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
            ObservationName = Shared_SlidingWindow1_ObservationName,
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
                        Shared_SlidingWindow1_Item1,
                        Shared_SlidingWindow1_Item2,
                        Shared_SlidingWindow1_Item3,
                        Shared_SlidingWindow1_Item4,
                        Shared_SlidingWindow1_Item5,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = Shared_SlidingWindow1_SubInterval6,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Shared_Observation1_Interval,
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

            Name = Shared_SlidingWindow1_ObservationName,
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
            ObservationName = Shared_SlidingWindow1_ObservationName,
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
                        Shared_SlidingWindow1_Item1,
                        Shared_SlidingWindow1_Item2,
                        Shared_SlidingWindow1_Item3,
                        Shared_SlidingWindow1_Item4,
                        Shared_SlidingWindow1_Item5,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = Shared_SlidingWindow1_SubInterval6,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Shared_Observation1_Interval,
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

        #region ValidatorTests

        internal static string[] Validator_Array1 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        internal static Car Validator_Object1 = new Car()
        {
            Brand = "Dodge",
            Model = "Charger",
            Year = 1966,
            Price = 13500,
            Currency = "USD"
        };
        internal static uint Validator_Length1 = 3;
        internal static string Validator_VariableName_Variable = "variable";
        internal static string Validator_VariableName_Length = "length";
        internal static string Validator_VariableName_N1 = "n1";
        internal static string Validator_VariableName_N2 = "n2";
        internal static List<string> List1 = Validator_Array1.ToList();
        internal static uint Validator_Value = Validator_Length1;
        internal static string Validator_String1 = "Dodge";
        internal static string Validator_StringOnlyWhiteSpaces = "   ";

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
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        internal static bool AreEqual(Observation obj1, Observation obj2)
        {

            return string.Equals(obj1.Name, obj2.Name, StringComparison.InvariantCulture)
                        && AreEqual(obj1.Interval, obj2.Interval)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.C, obj2.C)
                        && Equals(obj1.E, obj2.E)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted)
                        && string.Equals(obj1.SlidingWindowId, obj2.SlidingWindowId, StringComparison.InvariantCulture);

        }
        internal static bool AreEqual(List<Observation> list1, List<Observation> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        internal static bool AreEqual(SlidingWindowItem obj1, SlidingWindowItem obj2)
        {

            return Equals(obj1.Id, obj2.Id)
                        && AreEqual(obj1.Interval, obj2.Interval)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted);

        }
        internal static bool AreEqual(List<SlidingWindowItem> list1, List<SlidingWindowItem> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        
        internal static bool AreEqual(SlidingWindow obj1, SlidingWindow obj2)
        {

            return string.Equals(obj1.Id, obj2.Id, StringComparison.InvariantCulture)
                        && string.Equals(obj1.ObservationName, obj2.ObservationName, StringComparison.InvariantCulture)
                        && AreEqual(obj1.Interval, obj2.Interval)
                        && AreEqual(obj1.Items, obj2.Items);

        }
        
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
