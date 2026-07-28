using System;

namespace NW.UnivariateForecasting.CLI.Messages
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="UnivariateForecasting.CLI"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string PressAButtonToCloseTheWindow = "Press a button to close the window.";

        public static Func<string, string> LoadingFileNameReturnedDefault
            = (fileName) => $"Loading the content of '{fileName}' returned a default value. Please check the content of the file, it may be null or invalid.";
        public static Func<string, string, string> ValueIsInvalidOrNotWithinRange
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range.";

        #endregion

    }
}