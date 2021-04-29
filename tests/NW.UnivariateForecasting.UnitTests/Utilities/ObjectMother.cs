using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    public static class ObjectMother
    {

        internal static IntervalUnits NonExistantIntervalUnit = (IntervalUnits)(-1); // Emulates a non-existant enum value
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
            Unit = NonExistantIntervalUnit, // <= invalid
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

        internal static Interval NewInterval = new Interval();
        internal static string NewInterval_ToString = "0:Months:00010101:00010101:00010101:0:0";
        internal static string NewInterval_ToStringOnlyDates = "00010101:00010101:00010101";
        internal static string SlidingWindow1_Interval_ToString = "6:Months:20190131:20190731:20190831:1:6";
        internal static string SlidingWindow1_Interval_ToStringOnlyDates = "20190131:20190731:20190831";
        internal static string SlidingWindow1_SubInterval1_ToString = "1:Months:20190131:20190228:20190331:1:1";
        internal static string SlidingWindow1_SubInterval1_ToStringOnlyDates = "20190131:20190228:20190331";

        internal static string SlidingWindow1_Id = "SW20200906090516";
        internal static string SlidingWindow1_ObservationName = "Total Monthly Sales USD";
        internal static Interval Observation1_Interval = new Interval()
        {
            Size = 1,
            Unit = IntervalUnits.Months,
            StartDate = new DateTime(2019, 07, 31),
            EndDate = new DateTime(2019, 08, 31),
            TargetDate = new DateTime(2019, 09, 30),
            Steps = 1,
            SubIntervals = 1
        };
        internal static Observation Observation1 = new Observation()
        {

            Name = SlidingWindow1_ObservationName,
            Interval = Observation1_Interval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = SlidingWindow1_Id

        };
        internal static string Observation1_ToString 
            = $"[ Name: 'Total Monthly Sales USD', Interval: '1:Months:20190731:20190831:20190930:1:1', X_Actual: '{632.94.ToString()}', C: '{0.82.ToString()}', E: '{0.22.ToString()}', Y_Forecasted: '{519.23.ToString()}', SlidingWindowId: 'SW20200906090516' ]";
        internal static string Observation1_ToStringOnlyDates 
            = $"[ Name: 'Total Monthly Sales USD', Interval: '20190731:20190831:20190930', X_Actual: '{632.94.ToString()}', C: '{0.82.ToString()}', E: '{0.22.ToString()}', Y_Forecasted: '{519.23.ToString()}', SlidingWindowId: 'SW20200906090516' ]";
        internal static Observation NewObservation = new Observation();
        internal static string NewObservation_ToString = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";
        internal static string NewObservation_ToStringOnlyDates = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";

        internal static List<double> SlidingWindow1_Values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
        internal static uint SlidingWindow1_Steps = 1;
        internal static SlidingWindowItem SlidingWindow1_Item1 = new SlidingWindowItem()
        {
            Id = 1,
            Interval = SlidingWindow1_SubInterval1,
            X_Actual = 58.5,
            Y_Forecasted = 615.26
        };
        internal static SlidingWindowItem SlidingWindow1_Item2 = new SlidingWindowItem()
        {
            Id = 2,
            Interval = SlidingWindow1_SubInterval2,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };
        internal static SlidingWindowItem SlidingWindow1_Item3 = new SlidingWindowItem()
        {
            Id = 3,
            Interval = SlidingWindow1_SubInterval3,
            X_Actual = 659.84,
            Y_Forecasted = 635.69
        };
        internal static SlidingWindowItem SlidingWindow1_Item4 = new SlidingWindowItem()
        {
            Id = 4,
            Interval = SlidingWindow1_SubInterval4,
            X_Actual = 635.69,
            Y_Forecasted = 612.27
        };
        internal static SlidingWindowItem SlidingWindow1_Item5 = new SlidingWindowItem()
        {
            Id = 5,
            Interval = SlidingWindow1_SubInterval5,
            X_Actual = 612.27,
            Y_Forecasted = 632.94
        };
        internal static SlidingWindowItem SlidingWindow1_Item6 = new SlidingWindowItem()
        {
            Id = 6,
            Interval = SlidingWindow1_SubInterval6,
            X_Actual = 632.94,
            Y_Forecasted = null
        };
        internal static List<SlidingWindowItem> SlidingWindow1_Items = new List<SlidingWindowItem>()
        {
            SlidingWindow1_Item1,
            SlidingWindow1_Item2,
            SlidingWindow1_Item3,
            SlidingWindow1_Item4,
            SlidingWindow1_Item5,
            SlidingWindow1_Item6
        };
        internal static SlidingWindow SlidingWindow1 = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_Interval,
            Items = SlidingWindow1_Items
        };

        // ObservationManagerTests
        internal static ObservationManager ObservationManager_Default = new ObservationManager();
        internal static Observation Observation_InvalidDueOfNullName = new Observation()
        {

            Name = null

        };
        internal static Observation Observation_InvalidDueOfNullInterval = new Observation()
        {

            Name = SlidingWindow1_ObservationName,
            Interval = null   
            
        };
        internal static Observation Observation_InvalidDueOfNullSlidingWindow = new Observation()
        {

            Name = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_SubInterval1, // Whatever valid Interval
            SlidingWindowId = null

        };
        internal static double Observation1withCustomCE_C = 0.92;
        internal static double Observation1withCustomCE_E = 0.12;
        internal static double Observation1withCustomCE_Y = 582.42;
        internal static Observation Observation1withCustomCE = new Observation()
        {
            Name = Observation1.Name,
            Interval = Observation1.Interval,
            SlidingWindowId = Observation1.SlidingWindowId,
            X_Actual = Observation1.X_Actual,
            C = Observation1withCustomCE_C,
            E = Observation1withCustomCE_E,
            Y_Forecasted = Observation1withCustomCE_Y
        };

        // SlidingWindowTests
        internal static string SlidingWindow1_ToString = "[ Id: 'SW20200906090516', ObservationName: 'Total Monthly Sales USD', Interval: '6:Months:20190131:20190731:20190831:1:6', Items: '6' ]";
        internal static string SlidingWindow1_ToStringRolloutItems 
            = string.Join(
                Environment.NewLine,
                SlidingWindow1_ToString,
                SlidingWindow1_Item1.ToString(),
                SlidingWindow1_Item2.ToString(),
                SlidingWindow1_Item3.ToString(),
                SlidingWindow1_Item4.ToString(),
                SlidingWindow1_Item5.ToString(),
                SlidingWindow1_Item6.ToString()
                );
        internal static SlidingWindow NewSlidingWindow = new SlidingWindow();
        internal static string NewSlidingWindow_ToString = "[ Id: 'null', ObservationName: 'null', Interval: 'null', Items: 'null' ]";
        internal static string NewSlidingWindow_ToStringRolloutItems = NewSlidingWindow_ToString;

        // SlidingWindowItemTests
        internal static string SlidingWindow1_Item1_ToString 
            = $"[ Id: '1', Interval: '20190131:20190228:20190331', X_Actual: '{58.5.ToString()}', Y_Forecasted: '{615.26.ToString()}' ]";
        internal static SlidingWindowItem NewSlidingWindowItem = new SlidingWindowItem();
        internal static string NewSlidingWindowItem_ToString = "[ Id: '0', Interval: 'null', X_Actual: '0', Y_Forecasted: 'null' ]";

        // SlidingWindowItemManagerTests
        internal static SlidingWindowItemManager SlidingWindowItemManager_Default = new SlidingWindowItemManager();
        internal static SlidingWindowItem SlidingWindowItem_InvalidDueOfSize = new SlidingWindowItem()
        {
            Id = 2,
            Interval = Interval_InvalidDueOfSize,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };
        internal static uint SlidingWindow1_Item1_Id = 1;
        internal static Interval SlidingWindow1_Item1_Interval = SlidingWindow1_SubInterval1;
        internal static double SlidingWindow1_Item1_XActual = 58.5;
        internal static double? SlidingWindow1_Item1_YForecasted = 615.26;

        // SlidingWindowManagerTests
        internal static SlidingWindowManager SlidingWindowManager_Default = new SlidingWindowManager();
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullId = new SlidingWindow()
        {
            Id = null,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_Interval,
            Items = SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullObservationName = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = null,
            Interval = SlidingWindow1_Interval,
            Items = SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfInvalidInterval = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = null, // Whatever other invalid interval would do the trick
            Items = SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullItems = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_Interval,
            Items = null
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfItemsCountZero = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_Interval,
            Items = new List<SlidingWindowItem>()
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfSubInterval = new SlidingWindow()
        {
            Id = SlidingWindow1_Id,
            ObservationName = SlidingWindow1_ObservationName,
            Interval = SlidingWindow1_Interval,
            Items = SlidingWindow1_Items.Where(item => item.Id != 6).ToList() // Removes a random item
        };

        // UnivariateForecasterTests
        internal static UnivariateForecaster UnivariateForecaster_Default = new UnivariateForecaster();
        internal static List<DateTime> SlidingWindow1_StartDates = new List<DateTime>()
        {

            SlidingWindow1_Item1.Interval.StartDate,
            SlidingWindow1_Item2.Interval.StartDate,
            SlidingWindow1_Item3.Interval.StartDate,
            SlidingWindow1_Item4.Interval.StartDate,
            SlidingWindow1_Item5.Interval.StartDate,
            SlidingWindow1_Item6.Interval.StartDate

        };
        internal static string FaC_Id = "SW20200925000000";
        internal static Func<string> FaC_IdCreationFunction = () => FaC_Id;
        internal static SlidingWindow FaCSteps1_Final = new SlidingWindow()
        {
            Id = FaC_Id,
            ObservationName = SlidingWindow1_ObservationName,
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
                        SlidingWindow1_Item1,
                        SlidingWindow1_Item2,
                        SlidingWindow1_Item3,
                        SlidingWindow1_Item4,
                        SlidingWindow1_Item5,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = SlidingWindow1_SubInterval6,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Observation1_Interval,
                            X_Actual = 519.23,
                            Y_Forecasted = null
                        }
                    }
        };
        internal static SlidingWindow FaCSteps3_MidwaySlidingWindow_1 = FaCSteps1_Final;
        internal static Observation FaCSteps3_MidwayObservation_1 = new Observation()
        {

            Name = SlidingWindow1_ObservationName,
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
            SlidingWindowId = FaC_Id

        };
        internal static SlidingWindow FaCSteps3_MidwaySlidingWindow_2 = new SlidingWindow()
        {
            Id = FaC_Id,
            ObservationName = SlidingWindow1_ObservationName,
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
                        SlidingWindow1_Item1,
                        SlidingWindow1_Item2,
                        SlidingWindow1_Item3,
                        SlidingWindow1_Item4,
                        SlidingWindow1_Item5,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = SlidingWindow1_SubInterval6,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Observation1_Interval,
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
        internal static Observation FaCSteps3_MidwayObservation_2 = new Observation()
        {

            Name = SlidingWindow1_ObservationName,
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
            SlidingWindowId = FaC_Id

        };
        internal static SlidingWindow FaCSteps3_Final = new SlidingWindow()
        {
            Id = FaC_Id,
            ObservationName = SlidingWindow1_ObservationName,
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
                        SlidingWindow1_Item1,
                        SlidingWindow1_Item2,
                        SlidingWindow1_Item3,
                        SlidingWindow1_Item4,
                        SlidingWindow1_Item5,
                        new SlidingWindowItem()
                        {
                            Id = 6,
                            Interval = SlidingWindow1_SubInterval6,
                            X_Actual = 632.94,
                            Y_Forecasted = 519.23
                        },
                        new SlidingWindowItem()
                        {
                            Id = 7,
                            Interval = Observation1_Interval,
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
        internal static Interval SlidingWindow1_DummyInterval
            = new IntervalManager().Create(
                    (uint)SlidingWindow1_Values.Count,
                    UnivariateForecastingSettings.DefaultDummyIntervalUnit,
                    UnivariateForecastingSettings.DefaultDummyStartDate,
                    UnivariateForecastingSettings.DefaultDummySteps
                    );
        internal static List<SlidingWindowItem> SlidingWindow1_DefaultDummyItems
            = new SlidingWindowItemManager().CreateItems(
                    UnivariateForecastingSettings.DefaultDummyStartDate,
                    SlidingWindow1_Values,
                    UnivariateForecastingSettings.DefaultDummyIntervalUnit
                );
        internal static SlidingWindow SlidingWindow1_WithDefaultDummyFields = new SlidingWindow()
        {

            Id = UnivariateForecastingSettings.DefaultDummyId,
            ObservationName = UnivariateForecastingSettings.DefaultDummyObservationName,
            Interval = SlidingWindow1_DummyInterval,
            Items = SlidingWindow1_DefaultDummyItems
        };
        internal static Interval Observation1_DefaultDummyInterval = new Interval()
        {
            Size = 1,
            Unit = UnivariateForecastingSettings.DefaultDummyIntervalUnit,
            StartDate = new DateTime(2020, 07, 01),
            EndDate = new DateTime(2020, 08, 01),
            TargetDate = new DateTime(2020, 09, 01),
            Steps = UnivariateForecastingSettings.DefaultDummySteps,
            SubIntervals = 1
        };
        internal static Observation Observation1_WithDefaultDummyFields = new Observation()
        {

            Name = UnivariateForecastingSettings.DefaultDummyObservationName,
            Interval = Observation1_DefaultDummyInterval,
            X_Actual = 632.94,
            C = 0.82,
            E = 0.22,
            Y_Forecasted = 519.23,
            SlidingWindowId = UnivariateForecastingSettings.DefaultDummyId

        };
        internal static IFileAdapter FileAdapter_ReadAllTextReturnsSlidingWindowWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw FileAdapter_IOException,
                    fakeReadAllText: () => Properties.Resources.SlidingWindowWithDummyValues
                );
        internal static IFileAdapter FileAdapter_ReadAllTextReturnsObservationWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw FileAdapter_IOException,
                    fakeReadAllText: () => Properties.Resources.ObservationWithDummyValues
                );

        // FileManager
        internal static string Content_SingleLine = "First line";
        internal static IEnumerable<string> Content_MultipleLines =
            new List<string>() {
                "First line",
                "Second line"
            };
        internal static string FileInfoAdapter_FullName = @"C:\somefile.txt";
        internal static IFileInfoAdapter FileInfoAdapter_DoesntExist
            => new FakeFileInfoAdapter(false, FileInfoAdapter_FullName);
        internal static IFileInfoAdapter FileInfoAdapter_Exists
            => new FakeFileInfoAdapter(true, FileInfoAdapter_FullName);
        internal static IOException FileAdapter_IOException = new IOException("Impossible to access the file.");
        internal static IFileAdapter FileAdapter_ReadAllMethodsThrowIOException
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw FileAdapter_IOException,
                    fakeReadAllText: () => throw FileAdapter_IOException
                );
        internal static IFileAdapter FileAdapter_WriteAllMethodsThrowIOException
            => new FakeFileAdapter(
                    fakeWriteAllLines: () => throw FileAdapter_IOException,
                    fakeWriteAllText: () => throw FileAdapter_IOException
                );
        internal static IFileAdapter FileAdapter_AllMethodsWork
            => new FakeFileAdapter(
                    fakeReadAllLines: () => Content_MultipleLines.ToArray(),
                    fakeReadAllText: () => Content_SingleLine,
                    fakeWriteAllLines: () => { },
                    fakeWriteAllText: () => { }
                );

        // ValidatorTests
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

        // Methods
        internal static bool AreEqual(Interval obj1, Interval obj2)
        {

            return Equals(obj1.Size, obj2.Size)
                        && Equals(obj1.Unit, obj2.Unit)
                        && Equals(obj1.StartDate, obj2.StartDate)
                        && Equals(obj1.Steps, obj2.Steps);

        }
        internal static bool AreEqual(List<Interval> list1, List<Interval> list2)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (AreEqual(list1[i], list2[i]) == false)
                    return false;

            return true;

        }
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
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (AreEqual(list1[i], list2[i]) == false)
                    return false;

            return true;

        }
        internal static bool AreEqual(SlidingWindowItem obj1, SlidingWindowItem obj2)
        {

            return Equals(obj1.Id, obj2.Id)
                        && AreEqual(obj1.Interval, obj2.Interval)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted);

        }
        internal static bool AreEqual(List<SlidingWindowItem> list1, List<SlidingWindowItem> list2)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (AreEqual(list1[i], list2[i]) == false)
                    return false;

            return true;

        }
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

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.09.2020

*/
