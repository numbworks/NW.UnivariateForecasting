using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using NW.UnivariateForecasting.Files;

namespace NW.UnivariateForecasting.Forecasts
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.Forecasts"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string AttemptingToForecast = 
            $"Attempting to perform a forecasting task out of the provided {nameof(ForecastingInit)}...";

        public static Func<string, string> ProvidedFolderPathIs =
            (folderPath) => $"The provided folder path is: '{folderPath}'.";
        public static Func<double, string> ProvidedForecastingDenominatorIs =
            (forecastingDenominator) => $"The provided forecasting denominator is: '{forecastingDenominator.ToString("0.###############")}'.";
        public static Func<uint, string> ProvidedRoundingDigitsAre =
            (roundingDigits) => $"The provided rounding digits are: '{roundingDigits}'.";

        public static Func<string, string> ProvidedObservationNameIs = 
            (observationName) => $"The provided observation name is: '{observationName}'.";
        public static Func<int, string> ProvidedValuesAre = 
            (count) => $"The provided values are: '{count}'.";
        public static Func<double?, string> ProvidedCoefficientIs = 
            (coefficient) => $"The provided coefficient is: '{coefficient?.ToString() ?? "null"}'.";
        public static Func<double?, string> ProvidedErrorIs =
            (error) => $"The provided error is: '{error?.ToString() ?? "null"}'.";
        public static Func<uint, string> ProvidedStepsAre =
            (steps) => $"The provided steps are: '{steps}'.";

        public static Func<double, string> ObservationCoefficientIs =
            (coefficient) => $"The observation's coefficient is: '{coefficient}'.";
        public static Func<double, string> ObservationErrorIs =
            (error) => $"The observation's error is: '{error}'.";
        public static Func<double, string> ObservationNextValueIs =
            (nextValue) => $"The current observation's next value is: '{nextValue}'.";

        public static Func<uint, string> ProcessingStepNr =
            (step) => $"Processing step nr.: '{step}'.";
        public static string ForecastSuccessfullyCompleted { get; } = 
            "The forecasting task has been successfully completed.";

        public static Func<Type, IFileInfoAdapter, string> AttemptingToLoadObjectFrom =
            (type, jsonFile) => $"Attempting to load a '{type.Name}' object from: {jsonFile.FullName}.";
        public static Func<Type, string> ObjectSuccessfullyLoaded =
            (type) => $"A '{type.Name}' object has been successfully loaded.";
        public static Func<Type, string> ObjectFailedToLoad =
            (type) => $"A '{type.Name}' object failed to load. Default value is returned.";

        public static Func<Type, IFileInfoAdapter, string> AttemptingToSaveObjectAs =
            (type, jsonFile) =>
            {

                if (type == typeof(ExpandoObject))
                    return $"Attempting to save the provided '{typeof(ForecastingSession).Name}' object as: {jsonFile.FullName}.";

                return $"Attempting to save the provided '{type.Name}' object as: {jsonFile.FullName}.";

            };
        public static Func<Type, string> ObjectSuccessfullySaved =
            (type) =>
            {

                if (type == typeof(ExpandoObject))
                    return $"The provided '{typeof(ForecastingSession).Name}' object has been successfully saved.";

                return $"The provided '{type.Name}' object has been successfully saved.";

            };
        public static Func<Type, string> ObjectFailedToSave =
            (type) =>
            {

                if (type == typeof(ExpandoObject))
                    return $"The provided '{typeof(ForecastingSession).Name}' object failed to save.";

                return $"The provided '{type.Name}' object failed to save.";

            };

        public static Func<Type, string> ThereIsNoStrategyOutOfType =
             (type) => $"There is no built-in strategy to create a filename out of a '{type.Name}' object.";

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
    Last Update: 08.03.2023
*/
