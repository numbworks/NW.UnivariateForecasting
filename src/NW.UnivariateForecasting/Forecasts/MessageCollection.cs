using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.Forecasts
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.Forecasts"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<SlidingWindow, string> ExtractingValuesOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Extracting X_Values out of the provided '{typeof(SlidingWindow).Name}': {slidingWindow.ToString(false)}...";
        public static Func<List<double>, string> ValuesHaveBeenSuccessfullyExtracted { get; }
            = (values) => $"X_Values have been successfully extracted: '{RollOutCollection(values)}'.";
        public static Func<SlidingWindow, string> ExtractingStartDatesOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Extracting StartDates out of the provided '{typeof(SlidingWindow).Name}': {slidingWindow.ToString(false)}...";
        public static Func<List<DateTime>, string> StartDatesHaveBeenSuccessfullyExtracted { get; }
            = (startDates) => $"StartDates have been successfully extracted: '{startDates.Count}'.";
        
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
            = (nextValue) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' has been successfully run. The next value is: '{nextValue}'.";
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

        public static Func<string, double, string> DenominatorCantBeLessThan { get; }
            = (variableName, defaultDenominator) => $"'{variableName}' can't be less than '{defaultDenominator.ToString()}'.";

        #endregion 

        #region Methods

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
