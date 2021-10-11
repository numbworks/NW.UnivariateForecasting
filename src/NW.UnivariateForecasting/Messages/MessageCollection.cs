using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.Messages
{
    ///<summary>Collects all the messages used for logging and exceptions.</summary>
    public static class MessageCollection
    {

        #region FileManager
        
        public static Func<IFileInfoAdapter, Exception, string> FileManager_NotPossibleToRead
            = (file, e) => $"It hasn't been possible to read from the provided file: '{file.FullName}': '{e.Message}'.";
        public static Func<IFileInfoAdapter, Exception, string> FileManager_NotPossibleToWrite
            = (file, e) => $"It hasn't been possible to write to the provided file: '{file.FullName}': '{e.Message}'.";

        #endregion

        #region IntervalManager

        public static string IntervalManager_IntervalNullOrInvalid { get; }
            = "The provided interval is null or invalid.";
        public static Func<int, Interval, string> IntervalManager_ItemsDontMatchSubintervals { get; }
            = (items, interval) => $"The number of items ('{items.ToString()}') doesn't match with the expected number of subintervals ('{interval.SubIntervals.ToString()}').";

        #endregion

        #region ObservationManager

        public static Func<Type, string> ObservationManager_ProvidedTypeObjectNotValid { get; }
            = (type) => $"The provided {type.Name} object is not valid.";
        public static Func<SlidingWindow, string> ObservationManager_CreatingObservationOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Creating an {typeof(Observation).Name} out of the provided {typeof(SlidingWindow).Name}: '{slidingWindow.ToString(false)}'...";
        public static Func<Observation, string> ObservationManager_FollowingObservationHasBeenCreated { get; }
            = (observation) => $"The following {typeof(Observation).Name} has been created: '{observation.ToString(false)}'.";

        #endregion 

        #region SlidingWindowManager

        public static string SlidingWindowManager_CreatingSlidingWindowOutOfFollowingArguments { get; }
            = $"Creating a {typeof(SlidingWindow).Name} out of the provided arguments...";
        public static Func<string, string> SlidingWindowManager_ProvidedIdIs { get; }
            = (id) => $"The provided {nameof(SlidingWindow.Id)} is: '{id}'.";
        public static Func<string, string> SlidingWindowManager_ProvidedObservationNameIs { get; }
            = (observationName) => $"The provided {nameof(SlidingWindow.ObservationName)} is: '{observationName}'.";
        public static Func<Interval, string> SlidingWindowManager_ProvidedIntervalIs { get; }
            = (interval) => $"The provided {nameof(SlidingWindow.Interval)} is: '{interval.ToString()}'.";
        public static Func<List<SlidingWindowItem>, string> SlidingWindowManager_ProvidedItemsCountIs { get; }
            = (items) => $"The provided {nameof(SlidingWindow.Items)} count is: '{items.Count.ToString()}'.";
        public static string SlidingWindowManager_CreatingIntervalOutOfFollowingArguments { get; }
            = $"Creating a {typeof(Interval).Name} out of the provided arguments...";
        public static Func<List<double>, string> SlidingWindowManager_ProvidedValuesAre { get; }
            = (values) => $"The provided values are: '{RollOutCollection(values)}'.";
        public static Func<uint, string> SlidingWindowManager_ProvidedStepsAre { get; }
            = (steps) => $"The provided steps are: '{steps.ToString()}'.";
        public static Func<IntervalUnits, string> SlidingWindowManager_ProvidedIntervalUnitsIs { get; }
            = (intervalUnits) => $"The provided {typeof(IntervalUnits).Name} is: '{intervalUnits}'.";
        public static Func<SlidingWindow, string> SlidingWindowManager_FollowingSlidingWindowHasBeenCreated { get; }
            = (slidingWindow) => $"The following {typeof(SlidingWindow).Name} has been created: '{slidingWindow.ToString(true)}'.";

        #endregion

        #region  UnivariateForecaster

        public static Func<SlidingWindow, string> UnivariateForecaster_ExtractingValuesOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Extracting X_Values out of the provided '{typeof(SlidingWindow).Name}': {slidingWindow.ToString(false)}...";
        public static Func<List<double>, string> UnivariateForecaster_ValuesHaveBeenSuccessfullyExtracted { get; }
            = (values) => $"X_Values have been successfully extracted: '{RollOutCollection(values)}'.";
        public static Func<SlidingWindow, string> UnivariateForecaster_ExtractingStartDatesOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Extracting StartDates out of the provided '{typeof(SlidingWindow).Name}': {slidingWindow.ToString(false)}...";
        public static Func<List<DateTime>, string> UnivariateForecaster_StartDatesHaveBeenSuccessfullyExtracted { get; }
            = (startDates) => $"StartDates have been successfully extracted: '{startDates.Count.ToString()}'.";
        public static Func<uint, string> UnivariateForecaster_RunningForecastAndCombineForSteps { get; }
            = (steps) => $"Running '{nameof(UnivariateForecaster.ForecastAndCombine)}' for '{steps}' steps...";
        public static Func<uint, string> UnivariateForecaster_ForecastingAndCombineForStepNr { get; }
            = (steps) => $"Forecasting and combine for step nr. '{steps}'...";
        public static Func<uint, string> UnivariateForecaster_ForecastAndCombineSuccessfullyRunForSteps { get; }
            = (steps) => $"'{nameof(UnivariateForecaster.ForecastAndCombine)}' has been successfully run for '{steps}' steps.";
        public static string UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation { get; }
            = $"Combining the provided '{nameof(SlidingWindow)}' with the provided '{nameof(Observation)}'...";
        public static Func<SlidingWindow, string> UnivariateForecaster_ProvidedSlidingWindowIs { get; }
            = (slidingWindow) => $"The provided '{nameof(SlidingWindow)}' is: '{slidingWindow.ToString(false)}'.";
        public static Func<Observation, string> UnivariateForecaster_ProvidedObservationIs { get; }
            = (observation) => $"The provided '{nameof(Observation)}' is: '{observation.ToString(true)}'.";
        public static Func<List<double>, string> UnivariateForecaster_ForecastNextValueRunningForProvidedValues { get; }
            = (values) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' running for provided values: '{RollOutCollection(values)}'...";
        public static Func<double, string> UnivariateForecaster_ForecastNextValueSuccessfullyRun { get; }
            = (nextValue) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' has been successfully run. The next value is: '{nextValue.ToString()}'.";
        public static Func<IFileInfoAdapter, string> UnivariateForecaster_SerializingProvidedSlidingWindowAsJsonAndSavingItTo
            = (fileInfoAdapter) => $"Serializing the provided '{typeof(SlidingWindow).Name}' as JSON and saving it to '{fileInfoAdapter.FullName}'...";
        public static Func<IFileInfoAdapter, string> UnivariateForecaster_SerializingProvidedObservationAsJsonAndSavingItTo
            = (fileInfoAdapter) => $"Serializing the provided '{typeof(Observation).Name}' as JSON and saving it to '{fileInfoAdapter.FullName}'...";
        public static string UnivariateForecaster_ProvidedObjectHasBeenSuccessfullySavedAsJson
            = "The provided object has been successfully saved as JSON.";
        public static Func<IFileInfoAdapter, string> UnivariateForecaster_DeserializingProvidedFileAsSlidingWindowObject
            = (fileInfoAdapter) => $"Deserializing the provided file ('{fileInfoAdapter.FullName}') as '{typeof(SlidingWindow).Name}' object...";
        public static Func<IFileInfoAdapter, string> UnivariateForecaster_DeserializingProvidedFileAsObservationObject
            = (fileInfoAdapter) => $"Deserializing the provided file ('{fileInfoAdapter.FullName}') as '{typeof(Observation).Name}' object...";
        public static string UnivariateForecaster_ProvidedFileHasBeenSuccessfullyDeserialized
            = "The provided file has been successfully deserialized.";

        #endregion

        #region UnivariateForecastingSettings

        public static Func<string, double, string> UnivariateForecastingSettings_DenominatorCantBeLessThan { get; }
            = (variableName, defaultDenominator) => $"'{variableName}' can't be less than '{defaultDenominator.ToString()}'.";

        #endregion 

        #region Validator

        public static Func<string, string, string> Validator_FirstValueIsGreaterOrEqualThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater or equal than '{variableName2}''s value.";
        public static Func<string, string, string> Validator_FirstValueIsGreaterThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater than '{variableName2}''s value.";
        public static Func<string, string> Validator_VariableContainsZeroItems
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> Validator_VariableCantBeLessThanOne
            = (variableName) => $"'{variableName}' can't be less than one.";
        public static Func<string, string, string> Validator_DividingMustReturnWholeNumber { get; }
            = (variableName1, variableName2) => $"Dividing '{variableName1}' by '{variableName2}' must return a whole number.";
        public static Func<string, string> Validator_ProvidedIntervalUnitNotSupported { get; }
            = (unitName) => $"The provided '{typeof(IntervalUnits).Name}' is not supported: '{unitName}'.";
        public static string Validator_SubIntervalsCantBeLessThanTwo { get; }
            = "Subintervals can't be less than two";
        public static Func<IFileInfoAdapter, string> Validator_ProvidedPathDoesntExist
            = (file) => $"The provided path doesn't exist: '{file.FullName}'.";

        #endregion

        #region SupportMethods

        private static string RollOutCollection(List<double> coll)
            => RollOutCollection(coll.Cast<object>().ToList());
        private static string RollOutCollection(IEnumerable<object> coll)
        {

            List<string> list = new List<string>();

            foreach (object obj in coll)
                list.Add(obj.ToString());

            return $"[{string.Join(", ", list)}]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/
