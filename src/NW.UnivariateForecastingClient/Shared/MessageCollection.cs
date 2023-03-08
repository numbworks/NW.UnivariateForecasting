﻿using System;
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

        public static Func<string, string, string> ValueIsInvalidOrNotWithinRange
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range.";

        public static Func<string, string> InvalidInitContent
            = (filePath) => $"Invalid init content ('{filePath}').";

        public static string Session_Option_FolderPath_Template { get; } = "--folderpath";
        public static string Session_Option_FolderPath_Description { get; }
            = $"The path of the working folder. If not specified, '{UnivariateForecastingSettings.DefaultFolderPath}' will be used.";

        public static string Session_Option_SaveSession_Template { get; } = "--savesession";
        public static string Session_Option_SaveSession_Description { get; }
            = $"If provided, the forecasting session will be saved as JSON in the working folder.";

        public static string Session_Option_RoundingDigits_Template { get; } = "--roundingdigits";
        public static string Session_Option_RoundingDigits_Description { get; }
            = string.Concat(
                "Every decimal value processed by the application will be rounded to this number of digits.",
                $"If not specified, '{UnivariateForecastingSettings.DefaultRoundingDigits}' will be used."
            );

        public static string Session_Option_ForecastingDenominator_Template { get; } = "--forecastingdenominator";
        public static string Session_Option_ForecastingDenominator_Description { get; }
            = string.Concat(
                "Every decimal value processed by the application will be rounded to this number of digits.",
                $"If not specified, '{UnivariateForecastingSettings.DefaultRoundingDigits}' will be used."
            );


        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/