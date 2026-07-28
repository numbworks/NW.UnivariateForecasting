using System;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.Messages
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="UnivariateForecasting.CLI"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string Application_Name { get; } = "unifor";
        public static string Application_Description { get; } = "Command-line application to perform univariate forecasting tasks.";

        public static string Session_Name { get; } = "session";
        public static string Session_Description { get; } 
            = "Groups all the features related to a single forecasting session.";

        public static string PressAButtonToCloseTheWindow = "Press a button to close the window.";

        public static Func<string, string> LoadingFileNameReturnedDefault
            = (fileName) => $"Loading the content of '{fileName}' returned a default value. Please check the content of the file, it may be null or invalid.";
        public static Func<string, string, string> ValueIsInvalidOrNotWithinRange
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range.";

        public static string Session_Forecast_Name { get; } = "forecast";
        public static string Session_Forecast_Description { get; }
            = "Forecasts the next x values for the provided init file.";

        public static string Session_Option_Init_Template { get; } = "--init";
        public static string Session_Option_Init_Description { get; }
            = $"The filename of the JSON file containing the initialization data. The file needs to be stored in the working folder.";
        public static string Session_Option_Init_ErrorMessage { get; }
            = $"{Session_Option_Init_Template} is mandatory.";

        public static string Session_Option_FolderPath_Template { get; } = "--folderpath";
        public static string Session_Option_FolderPath_Description { get; }
            = $"The path of the working folder. If not specified, '{SettingBag.DefaultFolderPath}' will be used.";

        public static string Session_Option_SaveSession_Template { get; } = "--savesession";
        public static string Session_Option_SaveSession_Description { get; }
            = $"If provided, the forecasting session will be saved as JSON in the working folder.";

        public static string Session_Option_RoundingDigits_Template { get; } = "--roundingdigits";
        public static string Session_Option_RoundingDigits_Description { get; }
            = string.Concat(
                "When coefficient and error are not provided by the user, they are generated and rounded. ",
                $"If not specified, '{SettingBag.DefaultRoundingDigits}' will be used."
            );

        public static string Session_Option_ForecastingDenominator_Template { get; } = "--forecastingdenominator";
        public static string Session_Option_ForecastingDenominator_Description { get; }
            = string.Concat(
                "'Y_Forecasted = 0' is a totally legit value. To avoid a 'divide-by-zero' error, we replace it with a comparably small amount while forecasting. ",
                $"If not specified, '{SettingBag.DefaultForecastingDenominator.ToString("0.###############")}' will be used."
            );


        #endregion

    }
}