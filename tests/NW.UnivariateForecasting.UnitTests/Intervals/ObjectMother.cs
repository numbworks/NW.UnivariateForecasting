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
    Last Update: 13.11.2022
*/