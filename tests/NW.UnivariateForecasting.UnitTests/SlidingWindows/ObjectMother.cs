using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests.SlidingWindows
{
    public static class ObjectMother
    {

        #region Properties

        public static string SlidingWindow01_Id = "SW20200906090516";
        public static string SlidingWindow01_ObservationName = "Total Monthly Sales USD";

        public static List<double> SlidingWindow01_Values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

        public static SlidingWindowItem SlidingWindow01_Item01 = new SlidingWindowItem(id: 1, X_Actual: 58.5, Y_Forecasted: 615.26);
        public static SlidingWindowItem SlidingWindow01_Item02 = new SlidingWindowItem(id: 2, X_Actual: 615.26, Y_Forecasted: 659.84);
        public static SlidingWindowItem SlidingWindow01_Item03 = new SlidingWindowItem(id: 3, X_Actual: 659.84, Y_Forecasted: 635.69);
        public static SlidingWindowItem SlidingWindow01_Item04 = new SlidingWindowItem(id: 4, X_Actual: 635.69, Y_Forecasted: 612.27);
        public static SlidingWindowItem SlidingWindow01_Item05 = new SlidingWindowItem(id: 5, X_Actual: 612.27, Y_Forecasted: 632.94);
        public static SlidingWindowItem SlidingWindow01_Item06 = new SlidingWindowItem(id: 6, X_Actual: 632.94, Y_Forecasted: null);

        public static List<SlidingWindowItem> SlidingWindow01_Items = new List<SlidingWindowItem>()
        {
            SlidingWindow01_Item01,
            SlidingWindow01_Item02,
            SlidingWindow01_Item03,
            SlidingWindow01_Item04,
            SlidingWindow01_Item05,
            SlidingWindow01_Item06
        };
        public static SlidingWindow SlidingWindow01 = new SlidingWindow(items: SlidingWindow01_Items);
        
        public static string SlidingWindow01_AsString = "[ Items: '6' ]";
        public static string SlidingWindow01_AsStringRolloutItems
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

        public static string SlidingWindow01_Item01_AsString
            = $"[ Id: '1', X_Actual: '{58.5}', Y_Forecasted: '{615.26}' ]";

        public static uint SlidingWindow01_RoundingDigits = 2;

        #endregion

        #region Methods

        public static bool AreEqual(SlidingWindowItem obj1, SlidingWindowItem obj2)
        {

            return Equals(obj1.Id, obj2.Id)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted);

        }
        public static bool AreEqual(List<SlidingWindowItem> list1, List<SlidingWindowItem> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(SlidingWindow obj1, SlidingWindow obj2)
            => AreEqual(obj1.Items, obj2.Items);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/