using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting.SlidingWindows
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.SlidingWindows"/>.</summary>
    public static class MessageCollection
    {

        #region SlidingWindowManager

        public static string CreatingSlidingWindowOutOfFollowingArguments { get; }
            = $"Creating a {typeof(SlidingWindow).Name} out of the provided arguments...";
        public static Func<List<SlidingWindowItem>, string> ProvidedItemsCountIs { get; }
            = (items) => $"The provided {nameof(SlidingWindow.Items)} count is: '{items.Count}'.";
        public static Func<List<double>, string> ProvidedValuesAre { get; }
            = (values) => $"The provided steps are: '{values.Count}'.";
        public static Func<SlidingWindow, string> FollowingSlidingWindowHasBeenCreated { get; }
            = (slidingWindow) => $"The following {typeof(SlidingWindow).Name} has been created: '{slidingWindow.ToString(true)}'.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/