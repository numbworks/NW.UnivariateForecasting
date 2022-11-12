using System;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.Validation
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.Validation"/>.</summary>
    public static class MessageCollection
    {

        #region Validator

        public static Func<string, string, string> FirstValueIsGreaterOrEqualThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater or equal than '{variableName2}''s value.";
        public static Func<string, string, string> FirstValueIsGreaterThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater than '{variableName2}''s value.";
        public static Func<string, string> VariableContainsZeroItems
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> VariableCantBeLessThanOne
            = (variableName) => $"'{variableName}' can't be less than one.";
        public static Func<string, string, string> DividingMustReturnWholeNumber { get; }
            = (variableName1, variableName2) => $"Dividing '{variableName1}' by '{variableName2}' must return a whole number.";
        public static Func<IFileInfoAdapter, string> ProvidedPathDoesntExist
            = (file) => $"The provided path doesn't exist: '{file.FullName}'.";

        public static Func<string, string> ProvidedIntervalUnitNotSupported { get; }
            = (unitName) => $"The provided '{typeof(IntervalUnits).Name}' is not supported: '{unitName}'.";
        public static string SubIntervalsCantBeLessThanTwo { get; }
            = "Subintervals can't be less than two";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/