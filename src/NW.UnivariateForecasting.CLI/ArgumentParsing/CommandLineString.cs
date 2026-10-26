using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    public static class CommandLineString
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
            = string.Concat(
                "(Optional) ",
                "The path of the working folder. ",
                $"If not specified, '{SettingBag.DefaultFolderPath}' will be used.");

        public static string OPTION_SAVESESSION_TEMPL { get; } = "--savesession";
        public static string OPTION_SAVESESSION_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "If provided, the forecasting session will be saved as JSON in the working folder.");

        public static string OPTION_ROUNDINGDIGITS_TEMPL { get; } = "--roundingdigits";
        public static string OPTION_ROUNDINGDIGITS_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "When coefficient and error are not provided by the user, they are generated and rounded. ",
                $"If not specified, '{SettingBag.DefaultRoundingDigits}' will be used."
            );

        public static string OPTION_FORECASTINGDENOMINATOR_TEMPL { get; } = "--forecastingdenominator";
        public static string OPTION_FORECASTINGDENOMINATOR_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "'Y_Forecasted = 0' is a totally legit value. To avoid a 'divide-by-zero' error, we replace it with a comparably small amount while forecasting. ",
                $"If not specified, '{SettingBag.DefaultForecastingDenominator.ToString("0.###############")}' will be used."
            );

        public static string OPTION_ENABLEUTC_TEMPL { get; } = "--enableutc";
        public static string OPTION_ENABLEUTC_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "Replaces local time with UTC time in timestamps.");

        #endregion

    }
}