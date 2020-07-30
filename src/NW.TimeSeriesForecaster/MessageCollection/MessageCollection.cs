using System;

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
            = (unitName) => $"There is no strategy to calculate the date difference for the provided {typeof(SlidingWindowIntervalUnits).Name} item: '{unitName}'.";

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.07.2020

*/
