using System;
using System.IO;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.Bags
{
    /// <summary>Collects all the global settings required by the library.</summary>
    public class UnivariateForecastingSettings
    {

        #region Fields
        #endregion

        #region Properties

        public const double DefaultForecastingDenominator = 0.00000000000001D;
        public static string DefaultFolderPath { get; } = Directory.GetCurrentDirectory();
        public static uint DefaultRoundingDigits { get; } = 15;

        public double ForecastingDenominator { get; private set; }
        public string FolderPath { get; }
        public uint RoundingDigits { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/>.
        /// </summary>
        /// <param name="forecastingDenominator">Y_Forecasted = 0 in a <see cref="SlidingWindowItem"/> is a totally legit value. To avoid "divide-by-zero" error, we replace it with a comparably small amount while forecasting. Default: 0.00000000000001.</param>
        /// <param name="roundingDigits">When coefficient and error are not provided by the user, they are generated and rounded. The decimal digits can't be more than <see cref="DefaultRoundingDigits"/>.</param>        
        /// <exception cref="ArgumentException"/> 
        /// <exception cref="ArgumentNullException"/> 
        public UnivariateForecastingSettings(double forecastingDenominator, string folderPath, uint roundingDigits)
        {

            Validator.ThrowIfLessThan(forecastingDenominator, DefaultForecastingDenominator, nameof(forecastingDenominator));
            Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));
            Validator.ThrowIfFirstIsGreater((int)roundingDigits, nameof(roundingDigits), (int)DefaultRoundingDigits, nameof(DefaultRoundingDigits));

            ForecastingDenominator = forecastingDenominator;
            FolderPath = folderPath;
            RoundingDigits = roundingDigits;

        }

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/> using default values.
        /// </summary>
        public UnivariateForecastingSettings()
            : this(
                  forecastingDenominator: DefaultForecastingDenominator,
                  folderPath: DefaultFolderPath,
                  roundingDigits: DefaultRoundingDigits
                  )
        { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/