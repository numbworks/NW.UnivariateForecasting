using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public static class MessageCollection
    {

        // Validator
        public static Func<string, string, string> Validator_FirstValueIsGreaterOrEqualThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater or equal than '{variableName2}''s value.";
        public static Func<string, string, string> Validator_FirstValueIsGreaterThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater than '{variableName2}''s value.";
        public static Func<string, string> Validator_VariableContainsZeroItems
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> Validator_VariableCantBeLessThanOne
            = (variableName) => $"'{variableName}' can't be less than one.";

        // ObservationManager
        public static Func<Type, string> ProvidedTypeObjectNotValid { get; } 
            = (type) => $"The provided {type.Name} object is not valid.";
        public static Func<SlidingWindow, string> CreatingObservationOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Creating an {typeof(Observation).Name} out of the provided {typeof(SlidingWindow).Name}: '{slidingWindow.ToString(false)}'...";
        public static Func<Observation, string> FollowingObservationHasBeenCreated { get; }
            = (observation) => $"The following {typeof(Observation).Name} has been created: '{observation.ToString(false)}'.";

        // UnivariateForecastingSettings
        public static Func<string, double, string> DenominatorCantBeLessThan { get; } 
            = (variableName, defaultDenominator) => $"'{variableName}' can't be less than '{defaultDenominator.ToString()}'.";

        // IntervalManager
        public static Func<string, string> VariableCantBeEmptyOrNull { get; }
            = (variableName) => $"'{variableName}' can't be empty or null.";
        public static string DividingSizeByStepsMustReturnWholeNumber { get; }
            = "Dividing size by steps must return a whole number.";
        public static Func<string, string> ProvidedIntervalUnitNotSupported { get; }
            = (unitName) => $"The provided '{typeof(IntervalUnits).Name}' is not supported: '{unitName}'.";
        public static string SubIntervalsCantBeLessThanTwo { get; }
            = "Subintervals can't be less than two";
        public static string IntervalNullOrInvalid { get; }
            = "The provided interval is null or invalid.";
        public static Func<string, string> VariableContainsZeroItems{ get; }
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<int, Interval, string> ItemsDontMatchSubintervals { get; }
            = (items, interval) => $"The number of items ('{items.ToString()}') doesn't match with the expected number of subintervals ('{interval.SubIntervals.ToString()}').";

        // SlidingWindowManager
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
            = (values) => $"The provided values are: '{RollOutCollection(values)}'.";
        public static Func<uint, string> ProvidedStepsAre { get; }
            = (steps) => $"The provided steps are: '{steps.ToString()}'.";
        public static Func<IntervalUnits, string> ProvidedIntervalUnitsIs { get; }
            = (intervalUnits) => $"The provided {typeof(IntervalUnits).Name} is: '{intervalUnits}'.";
        public static Func<SlidingWindow, string> FollowingSlidingWindowHasBeenCreated { get; }
            = (slidingWindow) => $"The following {typeof(SlidingWindow).Name} has been created: '{slidingWindow.ToString(true)}'.";

        // UnivariateForecaster
        public static Func<SlidingWindow, string> ExtractingValuesOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Extracting X_Values out of the provided '{typeof(SlidingWindow).Name}': {slidingWindow.ToString(false)}...";
        public static Func<List<double>, string> ValuesHaveBeenSuccessfullyExtracted { get; }
            = (values) => $"X_Values have been successfully extracted: '{RollOutCollection(values)}'.";
        public static Func<SlidingWindow, string> ExtractingStartDatesOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Extracting StartDates out of the provided '{typeof(SlidingWindow).Name}': {slidingWindow.ToString(false)}...";
        public static Func<List<DateTime>, string> StartDatesHaveBeenSuccessfullyExtracted { get; }
            = (startDates) => $"StartDates have been successfully extracted: '{startDates.Count.ToString()}'.";
        public static Func<uint, string> RunningForecastAndCombineForSteps { get; }
            = (steps) => $"Running '{nameof(UnivariateForecaster.ForecastAndCombine)}' for '{steps}' steps...";
        public static Func<uint, string> ForecastingAndCombineForStepNr { get; }
            = (steps) => $"Forecasting and combine for step nr. '{steps}'...";
        public static Func<uint, string> ForecastAndCombineSuccessfullyRunForSteps { get; }
            = (steps) => $"'{nameof(UnivariateForecaster.ForecastAndCombine)}' has been successfully run for '{steps}' steps.";
        public static string CombiningProvidedSlidingWindowWithObservation { get; }
            = $"Combining the provided '{nameof(SlidingWindow)}' with the provided '{nameof(Observation)}'...";
        public static Func<SlidingWindow, string> ProvidedSlidingWindowIs { get; }
            = (slidingWindow) => $"The provided '{nameof(SlidingWindow)}' is: '{slidingWindow.ToString(false)}'.";
        public static Func<Observation, string> ProvidedObservationIs { get; }
            = (observation) => $"The provided '{nameof(Observation)}' is: '{observation.ToString(true)}'.";
        public static Func<List<double>, string> ForecastNextValueRunningForProvidedValues { get; }
            = (values) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' running for provided values: '{RollOutCollection(values)}'...";
        public static Func<double, string> ForecastNextValueSuccessfullyRun { get; }
            = (nextValue) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' has been successfully run. The next value is: '{nextValue.ToString()}'.";
        public static Func<IFileInfoAdapter, string> ProvidedFileDoesntExist
            = (fileInfoAdapter) => $"The provided file doesn't exist: '{nameof(fileInfoAdapter.FullName)}'.";
        public static Func<IFileInfoAdapter, string> SerializingProvidedSlidingWindowAsJsonAndSavingItTo
            = (fileInfoAdapter) => $"Serializing the provided '{typeof(SlidingWindow).Name}' as JSON and saving it to '{fileInfoAdapter.FullName}'...";
        public static Func<IFileInfoAdapter, string> SerializingProvidedObservationAsJsonAndSavingItTo
            = (fileInfoAdapter) => $"Serializing the provided '{typeof(Observation).Name}' as JSON and saving it to '{fileInfoAdapter.FullName}'...";
        public static string ProvidedObjectHasBeenSuccessfullySavedAsJson 
            = "The provided object has been successfully saved as JSON.";
        public static Func<IFileInfoAdapter, string> DeserializingProvidedFileAsSlidingWindowObject
            = (fileInfoAdapter) => $"Deserializing the provided file ('{fileInfoAdapter.FullName}') as '{typeof(SlidingWindow).Name}' object...";
        public static Func<IFileInfoAdapter, string> DeserializingProvidedFileAsObservationObject
            = (fileInfoAdapter) => $"Deserializing the provided file ('{fileInfoAdapter.FullName}') as '{typeof(Observation).Name}' object...";
        public static string ProvidedFileHasBeenSuccessfullyDeserialized
            = "The provided file has been successfully deserialized.";

        // FileManager
        public static Func<IFileInfoAdapter, string> ProvidedPathDoesntExist
            = (file) => $"The provided path doesn't exist: '{file.FullName}'.";
        public static Func<IFileInfoAdapter, Exception, string> NotPossibleToRead
            = (file, e) => $"It hasn't been possible to read from the provided file: '{file.FullName}': '{e.Message}'.";
        public static Func<IFileInfoAdapter, Exception, string> NotPossibleToWrite
            = (file, e) => $"It hasn't been possible to write to the provided file: '{file.FullName}': '{e.Message}'.";

        private static string RollOutCollection(List<double> coll)
            => RollOutCollection(coll.Cast<object>().ToList());
        private static string RollOutCollection(IEnumerable<object> coll)
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
    Last Update: 06.12.2020

*/
