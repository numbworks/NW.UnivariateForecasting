using System;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.Messages
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="UnivariateForecasting.CLI"/>.</summary>
    public static class MessageCollection
    {

        #region Application

        public static string APPLICATION_NAME { get; } = "nwunif";
        public static string APPLICATION_DESCRIPTION { get; } = "Command-line application to perform univariate forecasting tasks.";

        #endregion

        #region Session

        public static string COMMAND_SESSION_NAME { get; } = "session";
        public static string COMMAND_SESSION_DESCR { get; } 
            = "Groups all the features related to a single forecasting session.";

        #endregion

        #region Session Forecast

        public static string SUBCOMMAND_FORECAST_NAME { get; } = "forecast";
        public static string SUBCOMMAND_FORECAST_DESCR { get; }
            = "Forecasts the next x values for the provided init file.";

        public static string OPTION_INIT_TEMPL { get; } = "--init";
        public static string OPTION_INIT_DESCR { get; }
            = $"The filename of the JSON file containing the initialization data. The file needs to be stored in the working folder.";
        public static string OPTION_INIT_ERRORMESSAGE { get; }
            = $"{OPTION_INIT_TEMPL} is mandatory.";

        public static string OPTION_FOLDERPATH_TEMPL { get; } = "--folderpath";
        public static string OPTION_FOLDERPATH_DESCR { get; }
            = $"The path of the working folder. If not specified, '{SettingBag.DefaultFolderPath}' will be used.";

        public static string OPTION_SAVESESSION_TEMPL { get; } = "--savesession";
        public static string OPTION_SAVESESSION_DESCR { get; }
            = $"If provided, the forecasting session will be saved as JSON in the working folder.";

        public static string OPTION_ROUNDINGDIGITS_TEMPL { get; } = "--roundingdigits";
        public static string OPTION_ROUNDINGDIGITS_DESCR { get; }
            = string.Concat(
                "When coefficient and error are not provided by the user, they are generated and rounded. ",
                $"If not specified, '{SettingBag.DefaultRoundingDigits}' will be used."
            );

        public static string OPTION_FORECASTINGDENOMINATOR_TEMPL { get; } = "--forecastingdenominator";
        public static string OPTION_FORECASTINGDENOMINATOR_DESCR { get; }
            = string.Concat(
                "'Y_Forecasted = 0' is a totally legit value. To avoid a 'divide-by-zero' error, we replace it with a comparably small amount while forecasting. ",
                $"If not specified, '{SettingBag.DefaultForecastingDenominator.ToString("0.###############")}' will be used."
            );

        #endregion

        #region Properties

        public static string PressAButtonToCloseTheWindow = "Press a button to close the window.";

        public static Func<string, string> LoadingFileNameReturnedDefault
            = (fileName) => $"Loading the content of '{fileName}' returned a default value. Please check the content of the file, it may be null or invalid.";
        public static Func<string, string, string> ValueIsInvalidOrNotWithinRange
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range.";

        #endregion

    }
}