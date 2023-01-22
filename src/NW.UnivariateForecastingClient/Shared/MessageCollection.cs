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
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range ('{nameof(DoubleManager.MininumValue)}':'{DoubleManager.MininumValue}', '{nameof(DoubleManager.MaximumValue)}':'{DoubleManager.MaximumValue}').";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/