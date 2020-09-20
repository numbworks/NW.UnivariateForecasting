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
        public static Func<SlidingWindow, string> CreatingObservationOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Creating an {typeof(Observation).Name} out of the provided {typeof(SlidingWindow).Name}: '{slidingWindow.ToString(false)}'...";
        public static Func<Observation, string> FollowingObservationHasBeenCreated { get; }
            = (observation) => $"The following {typeof(Observation).Name} has been created: '{observation.ToString(false)}'.";

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

        public static string CreatingSlidingWindowOutOfFollowingArguments { get; }
            = $"Creating a {typeof(SlidingWindow).Name} out of the provided arguments...";
        public static Func<string, string> ProvidedIdIs { get; }
            = (id) => $"The provided {nameof(SlidingWindow.Id)} is: '{id}'.";
        public static Func<string, string> ProvidedObservationNameIs { get; }
            = (observationName) => $"The provided {nameof(SlidingWindow.ObservationName)} is: '{observationName}'.";
        public static Func<Interval, string> ProvidedIntervalIs { get; }
            = (interval) => $"The provided {nameof(SlidingWindow.Interval)} is: '{interval.ToString()}'.";
        public static Func<List<SlidingWindowItem>, string> ProvidedItemsCountIs { get; }
            = (items) => $"The provided {nameof(SlidingWindow.Items)} count is: '{items.Count.ToString()}'.";
        public static string CreatingIntervalOutOfFollowingArguments { get; }
            = $"Creating a {typeof(Interval).Name} out of the provided arguments...";
        public static Func<List<double>, string> ProvidedValuesAre { get; }
            = (values) => $"The provided values are: '{RollOutCollection((IEnumerable<object>)values)}'.";
        public static Func<uint, string> ProvidedStepsAre { get; }
            = (steps) => $"The provided steps are: '{steps.ToString()}'.";
        public static Func<IntervalUnits, string> ProvidedIntervalUnitsIs { get; }
            = (intervalUnits) => $"The provided {typeof(IntervalUnits).Name} is: '{intervalUnits}'.";
        public static Func<SlidingWindow, string> FollowingSlidingWindowHasBeenCreated { get; }
            = (slidingWindow) => $"The following {typeof(SlidingWindow).Name} has been created: '{slidingWindow.ToString(true)}'.";

        public static string RollOutCollection(IEnumerable<object> coll)
        {

            List<string> list = new List<string>();

            foreach (object obj in coll)
                list.Add(obj.ToString());

            return $"[{string.Join(", ", list)}]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.09.2020

*/
