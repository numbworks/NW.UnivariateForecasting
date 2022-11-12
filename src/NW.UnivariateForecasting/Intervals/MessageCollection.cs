using System;

namespace NW.UnivariateForecasting.Intervals
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.Intervals"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string IntervalNullOrInvalid { get; }
            = "The provided interval is null or invalid.";
        public static Func<int, Interval, string> ItemsDontMatchSubintervals { get; }
            = (items, interval) => $"The number of items ('{items}') doesn't match with the expected number of subintervals ('{interval.SubIntervals}').";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/
