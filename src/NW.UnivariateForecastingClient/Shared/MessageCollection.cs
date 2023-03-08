using System;
using NW.UnivariateForecasting;
using NW.UnivariateForecastingClient.ApplicationSession;

namespace NW.UnivariateForecastingClient.Shared
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="UnivariateForecastingClient"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string Application_Name { get; } = "unifor";
        public static string Application_Description { get; } = "Command-line application to perform univariate forecasting tasks.";

        public static string About_Name { get; } = "about";
        public static string About_Description { get; } = "About this application.";
        public static string About_Information_Author = "Author: numbworks";
        public static string About_Information_Email = "Email: numbworks [AT] gmail [DOT] com";
        public static string About_Information_Url = @"Github: http://www.github.com/numbworks";
        public static string About_Information_License = "License: MIT License";

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
            = $"The path of the working folder. If not specified, '{UnivariateForecastingSettings.DefaultFolderPath}' will be used.";

        public static string Session_Option_SaveSession_Template { get; } = "--savesession";
        public static string Session_Option_SaveSession_Description { get; }
            = $"If provided, the forecasting session will be saved as JSON in the working folder.";

        public static string Session_Option_RoundingDigits_Template { get; } = "--roundingdigits";
        public static string Session_Option_RoundingDigits_Description { get; }
            = string.Concat(
                "When coefficient and error are not provided by the user, they are generated and rounded.",
                $"If not specified, '{UnivariateForecastingSettings.DefaultRoundingDigits}' will be used."
            );

        public static string Session_Option_ForecastingDenominator_Template { get; } = "--forecastingdenominator";
        public static string Session_Option_ForecastingDenominator_Description { get; }
            = string.Concat(
                "'Y_Forecasted = 0' is a totally legit value. To avoid a 'divide-by-zero' error, we replace it with a comparably small amount while forecasting.",
                $"If not specified, '{UnivariateForecastingSettings.DefaultForecastingDenominator}' will be used."
            );


        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/