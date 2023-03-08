using System;
using System.IO;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting
{
    /// <summary>Collects all the global settings required by the library.</summary>
    public class UnivariateForecastingSettings
    {

        #region Fields
        #endregion

        #region Properties

        public const double DefaultForecastingDenominator = 0.00000000000001D;
        public static string DefaultFolderPath { get; } = Directory.GetCurrentDirectory();

        public double ForecastingDenominator { get; private set; }
        public string FolderPath { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/>.
        /// </summary>
        /// <param name="forecastingDenominator">Y_Forecasted = 0 in a <see cref="SlidingWindowItem"/> is a totally legit value. To avoid "divide-by-zero" error, we replace it with a comparably small amount while forecasting. Default: 0.00000000000001.</param>
        /// <exception cref="ArgumentException"/> 
        public UnivariateForecastingSettings(double forecastingDenominator, string folderPath)
        {

            Validator.ThrowIfLessThan(forecastingDenominator, DefaultForecastingDenominator, nameof(forecastingDenominator));
            Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));

            ForecastingDenominator = forecastingDenominator;
            FolderPath = folderPath;

        }

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/> using default values.
        /// </summary>
        public UnivariateForecastingSettings()
            : this(
                  forecastingDenominator: DefaultForecastingDenominator,
                  folderPath: DefaultFolderPath
                  ) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/