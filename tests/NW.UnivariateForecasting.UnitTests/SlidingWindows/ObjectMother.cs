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
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = Utilities.ObjectMother.Shared_SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullObservationName = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = null,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = Utilities.ObjectMother.Shared_SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfInvalidInterval = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = null, // Whatever other invalid interval would do the trick
            Items = Utilities.ObjectMother.Shared_SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullItems = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = null
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfItemsCountZero = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = new List<SlidingWindowItem>()
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfSubInterval = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Intervals.ObjectMother.Interval_SixMonths,
            Items = Utilities.ObjectMother.Shared_SlidingWindow1_Items.Where(item => item.Id != 6).ToList() // Removes a random item
        };

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