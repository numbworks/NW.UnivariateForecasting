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

        public static Func<List<double>, string> ForecastNextValueRunningForProvidedValues { get; }
            = (values) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' running for provided values: '{RollOutCollection(values)}'...";
        public static Func<double, string> ForecastNextValueSuccessfullyRun { get; }
            = (nextValue) => $"'{nameof(UnivariateForecaster.ForecastNextValue)}' has been successfully run. The next value is: '{nextValue}'.";

        public static Func<string, double, string> DenominatorCantBeLessThan { get; }
            = (variableName, defaultDenominator) => $"'{variableName}' can't be less than '{defaultDenominator}'.";

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
    Last Update: 19.02.2023
*/
