using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public static class MessageCollection
    {

        // Fields
        // Properties
        public static string ProvidedSlidingWindowNotValid { get; } 
            = "The provided SlidingWindow object is not valid.";
        
        public static Func<string, double, string> DenominatorCantBeLessThan { get; } 
            = (variableName, defaultDenominator) => $"'{variableName}' can't be less than '{defaultDenominator.ToString()}'.";

        public static Func<string, string> NoStrategyToCalculateDateDifferenceUnit { get; }
            = (unitName) => $"There is no strategy to calculate the date difference for the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";
        public static Func<string, string> NoStrategyToCalculateNextDateUnit { get; }
            = (unitName) => $"There is no strategy to calculate the next date for the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";
        public static Func<string, string> NoStrategyToCreateItemsUnit { get; }
            = (unitName) => $"There is no strategy to create a {typeof(List<SlidingWindowItem>).Name} object out of the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";
        public static Func<string, string> StringCantBeEmptyOrNull { get; }
            = (variableName) => $"'{variableName}' can't be empty or null.";
        public static string StepsCantBeLessThanOne { get; }
            = "Steps can't be less than one.";

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.07.2020

*/
