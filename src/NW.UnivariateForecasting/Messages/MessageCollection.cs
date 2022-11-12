﻿using System;
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
    Last Update: 12.11.2022
*/
