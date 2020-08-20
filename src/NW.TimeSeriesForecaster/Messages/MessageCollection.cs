using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public static class MessageCollection
    {

        // Fields
        // Properties
        public static Func<Type, string> ProvidedTypeObjectNotValid { get; } 
            = (type) => $"The provided {type.Name} object is not valid.";
        
        public static Func<string, double, string> DenominatorCantBeLessThan { get; } 
            = (variableName, defaultDenominator) => $"'{variableName}' can't be less than '{defaultDenominator.ToString()}'.";

        public static Func<string, string> VariableCantBeLessThanOne { get; }
            = (variableName) => $"'{variableName}' can't be less than one.";
        public static Func<string, string> VariableCantBeEmptyOrNull { get; }
            = (variableName) => $"'{variableName}' can't be empty or null.";
        public static string DividingSizeByStepsMustReturnWholeNumber { get; }
            = "Dividing size by steps must return a whole number.";
        public static Func<string, string> NoStrategyToCalculateDateDifferenceUnit { get; }
            = (unitName) => $"There is no strategy to calculate the date difference for the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";
        public static Func<string, string> NoStrategyToCreateItemsUnit { get; }
            = (unitName) => $"There is no strategy to create a {typeof(List<SlidingWindowItem>).Name} object out of the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";
        public static string SubIntervalsCantBeLessThanTwo { get; }
            = "Subintervals can't be less than two";

        public static string IntervalNullOrInvalid { get; }
            = "The provided interval is null or invalid.";
        public static Func<string, string> VariableContainsZeroItems{ get; }
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> NoStrategyToCalculateNextDateUnit { get; }
            = (unitName) => $"There is no strategy to calculate the next date for the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";
        public static Func<int, Interval, string> ItemsDontMatchSubintervals { get; }
            = (items, interval) => $"The number of items ('{items.ToString()}') doesn't match with the expected number of subintervals ('{interval.SubIntervals.ToString()}').";
        public static Func<string, string> NoStrategyToCalculateSubIntervalsUnit { get; }
            = (unitName) => $"There is no strategy to calculate subintervals for the provided {typeof(IntervalUnits).Name} item: '{unitName}'.";

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.07.2020

*/
