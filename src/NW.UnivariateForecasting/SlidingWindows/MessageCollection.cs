using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.SlidingWindows
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.SlidingWindows"/>.</summary>
    public static class MessageCollection
    {

        #region SlidingWindowManager

        public static string CreatingSlidingWindowOutOfFollowingArguments { get; }
            = $"Creating a {typeof(SlidingWindow).Name} out of the provided arguments...";
        public static Func<string, string> ProvidedIdIs { get; }
            = (id) => $"The provided {nameof(SlidingWindow.Id)} is: '{id}'.";
        public static Func<string, string> ProvidedObservationNameIs { get; }
            = (observationName) => $"The provided {nameof(SlidingWindow.ObservationName)} is: '{observationName}'.";
        public static Func<Interval, string> ProvidedIntervalIs { get; }
            = (interval) => $"The provided {nameof(SlidingWindow.Interval)} is: '{interval}'.";
        public static Func<List<SlidingWindowItem>, string> ProvidedItemsCountIs { get; }
            = (items) => $"The provided {nameof(SlidingWindow.Items)} count is: '{items.Count}'.";
        public static string CreatingIntervalOutOfFollowingArguments { get; }
            = $"Creating a {typeof(Interval).Name} out of the provided arguments...";
        public static Func<uint, string> ProvidedStepsAre { get; }
            = (steps) => $"The provided steps are: '{steps}'.";
        public static Func<List<double>, string> ProvidedValuesAre { get; }
            = (values) => $"The provided steps are: '{values.Count}'.";
        public static Func<IntervalUnits, string> ProvidedIntervalUnitsIs { get; }
            = (intervalUnits) => $"The provided {typeof(IntervalUnits).Name} is: '{intervalUnits}'.";
        public static Func<SlidingWindow, string> FollowingSlidingWindowHasBeenCreated { get; }
            = (slidingWindow) => $"The following {typeof(SlidingWindow).Name} has been created: '{slidingWindow.ToString(true)}'.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/