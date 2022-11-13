using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.UnitTests.Intervals
{
    public static class ObjectMother
    {

        #region Propriertes

        internal static Interval Interval_Empty = new Interval();
        internal static string Interval_Empty_AsString = "0:Months:00010101:00010101:00010101:0:0";
        internal static string Interval_Empty_AsStringOnlyDates = "00010101:00010101:00010101";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.11.2022
*/