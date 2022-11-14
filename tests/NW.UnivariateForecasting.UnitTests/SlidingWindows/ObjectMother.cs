using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    public static class ObjectMother
    {

        #region Properties

        internal static SlidingWindow SlidingWindow_Empty = new SlidingWindow();
        internal static string SlidingWindow_Empty_AsString = "[ Id: 'null', ObservationName: 'null', Interval: 'null', Items: 'null' ]";
        internal static string SlidingWindow_Empty_AsStringRolloutItems = SlidingWindow_Empty_AsString;

        internal static SlidingWindowItem SlidingWindowItem_Empty = new SlidingWindowItem();
        internal static string SlidingWindowItem_Empty_AsString = "[ Id: '0', Interval: 'null', X_Actual: '0', Y_Forecasted: 'null' ]";

        internal static SlidingWindowItemManager SlidingWindowItemManager_Empty = new SlidingWindowItemManager();
        internal static SlidingWindowItem SlidingWindowItem_InvalidDueOfSize = new SlidingWindowItem()
        {
            Id = 2,
            Interval = Intervals.ObjectMother.Interval_InvalidDueOfSize,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };

        internal static SlidingWindowManager SlidingWindowManager_Empty = new SlidingWindowManager();
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullId = new SlidingWindow()
        {
            Id = null,
            ObservationName = SlidingWindow01_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = SlidingWindow01_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullObservationName = new SlidingWindow()
        {
            Id = SlidingWindow01_Id,
            ObservationName = null,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = SlidingWindow01_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfInvalidInterval = new SlidingWindow()
        {
            Id = SlidingWindow01_Id,
            ObservationName = SlidingWindow01_ObservationName,
            Interval = null, // Whatever other invalid interval would do the trick
            Items = SlidingWindow01_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullItems = new SlidingWindow()
        {
            Id = SlidingWindow01_Id,
            ObservationName = SlidingWindow01_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = null
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfItemsCountZero = new SlidingWindow()
        {
            Id = SlidingWindow01_Id,
            ObservationName = SlidingWindow01_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = new List<SlidingWindowItem>()
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfSubInterval = new SlidingWindow()
        {
            Id = SlidingWindow01_Id,
            ObservationName = SlidingWindow01_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = SlidingWindow01_Items.Where(item => item.Id != 6).ToList() // Removes a random item
        };

        internal static string SlidingWindow01_Id = "SW20200906090516";
        internal static uint SlidingWindow01_Steps = 1;
        internal static string SlidingWindow01_ObservationName = "Total Monthly Sales USD";

        internal static uint SlidingWindow01_Item01_Id = 1;
        internal static double SlidingWindow01_Item01_XActual = 58.5;
        internal static double? SlidingWindow01_Item01_YForecasted = 615.26;

        internal static SlidingWindowItem SlidingWindow01_Item01 = new SlidingWindowItem()
        {
            Id = SlidingWindow01_Item01_Id,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval01,
            X_Actual = SlidingWindow01_Item01_XActual,
            Y_Forecasted = SlidingWindow01_Item01_YForecasted
        };
        internal static SlidingWindowItem SlidingWindow01_Item02 = new SlidingWindowItem()
        {
            Id = 2,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval02,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };
        internal static SlidingWindowItem SlidingWindow01_Item03 = new SlidingWindowItem()
        {
            Id = 3,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval03,
            X_Actual = 659.84,
            Y_Forecasted = 635.69
        };
        internal static SlidingWindowItem SlidingWindow01_Item04 = new SlidingWindowItem()
        {
            Id = 4,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval04,
            X_Actual = 635.69,
            Y_Forecasted = 612.27
        };
        internal static SlidingWindowItem SlidingWindow01_Item05 = new SlidingWindowItem()
        {
            Id = 5,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval05,
            X_Actual = 612.27,
            Y_Forecasted = 632.94
        };
        internal static SlidingWindowItem SlidingWindow01_Item06 = new SlidingWindowItem()
        {
            Id = 6,
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval06,
            X_Actual = 632.94,
            Y_Forecasted = null
        };
        internal static List<SlidingWindowItem> SlidingWindow01_Items = new List<SlidingWindowItem>()
        {
            SlidingWindow01_Item01,
            SlidingWindow01_Item02,
            SlidingWindow01_Item03,
            SlidingWindow01_Item04,
            SlidingWindow01_Item05,
            SlidingWindow01_Item06
        };

        internal static SlidingWindow SlidingWindow01 = new SlidingWindow()
        {
            Id = SlidingWindow01_Id,
            ObservationName = SlidingWindow01_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = SlidingWindow01_Items
        };
        internal static List<double> SlidingWindow01_Values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
        internal static List<DateTime> SlidingWindow01_StartDates = new List<DateTime>()
        {

            SlidingWindow01_Item01.Interval.StartDate,
            SlidingWindow01_Item02.Interval.StartDate,
            SlidingWindow01_Item03.Interval.StartDate,
            SlidingWindow01_Item04.Interval.StartDate,
            SlidingWindow01_Item05.Interval.StartDate,
            SlidingWindow01_Item06.Interval.StartDate

        };

        internal static string SlidingWindow01_AsString
            = "[ Id: 'SW20200906090516', ObservationName: 'Total Monthly Sales USD', Interval: '6:Months:20190131:20190731:20190831:1:6', Items: '6' ]";
        internal static string SlidingWindow01_AsStringRolloutItems
            = string.Join(
                Environment.NewLine,
                SlidingWindow01_AsString,
                SlidingWindow01_Item01.ToString(),
                SlidingWindow01_Item02.ToString(),
                SlidingWindow01_Item03.ToString(),
                SlidingWindow01_Item04.ToString(),
                SlidingWindow01_Item05.ToString(),
                SlidingWindow01_Item06.ToString()
                );
        internal static string SlidingWindow01_Item01_AsString
            = $"[ Id: '1', Interval: '20190131:20190228:20190331', X_Actual: '{58.5}', Y_Forecasted: '{615.26}' ]";

        #endregion

        #region Methods

        internal static bool AreEqual(SlidingWindowItem obj1, SlidingWindowItem obj2)
        {

            return Equals(obj1.Id, obj2.Id)
                        && Intervals.ObjectMother.AreEqual(obj1.Interval, obj2.Interval)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted);

        }
        internal static bool AreEqual(List<SlidingWindowItem> list1, List<SlidingWindowItem> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        internal static bool AreEqual(SlidingWindow obj1, SlidingWindow obj2)
        {

            return string.Equals(obj1.Id, obj2.Id, StringComparison.InvariantCulture)
                        && string.Equals(obj1.ObservationName, obj2.ObservationName, StringComparison.InvariantCulture)
                        && Intervals.ObjectMother.AreEqual(obj1.Interval, obj2.Interval)
                        && AreEqual(obj1.Items, obj2.Items);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/