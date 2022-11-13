﻿using System.Collections.Generic;
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
            Interval = Utilities.ObjectMother.Shared_IntervalInvalidDueOfSize,
            X_Actual = 615.26,
            Y_Forecasted = 659.84
        };

        internal static SlidingWindowManager SlidingWindowManager_Empty = new SlidingWindowManager();
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullId = new SlidingWindow()
        {
            Id = null,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
            Items = Utilities.ObjectMother.Shared_SlidingWindow1_Items
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfNullObservationName = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = null,
            Interval = Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
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
            Interval = Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
            Items = null
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfItemsCountZero = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
            Items = new List<SlidingWindowItem>()
        };
        internal static SlidingWindow SlidingWindow_InvalidDueOfSubInterval = new SlidingWindow()
        {
            Id = Utilities.ObjectMother.Shared_SlidingWindow1_Id,
            ObservationName = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Utilities.ObjectMother.Shared_SlidingWindow1_Interval,
            Items = Utilities.ObjectMother.Shared_SlidingWindow1_Items.Where(item => item.Id != 6).ToList() // Removes a random item
        };

        #endregion


    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.11.2022
*/