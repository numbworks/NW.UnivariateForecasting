using System;

namespace NW.UnivariateForecasting
{
    /// <summary>Collects all the global settings required by the library.</summary>
    public class UnivariateForecastingSettings
    {

        #region Fields
        #endregion

        #region Properties

        public const double DefaultForecastingDenominator = 0.00000000000001D;
        public double ForecastingDenominator { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/>. Hover the mouse over the parameters for details.
        /// </summary>
        /// <param name="forecastingDenominator">Y_Forecasted = 0 in a <see cref="SlidingWindowItem"/> is a totally legit value. To avoid "divide-by-zero" error, we replace it with a comparably small amount while forecasting. Default: 0.001.</param>
        /// <exception cref="ArgumentException"/> 
        public UnivariateForecastingSettings(double forecastingDenominator)
        {

            if (forecastingDenominator < DefaultForecastingDenominator)
                throw new ArgumentException(Forecasts.MessageCollection.DenominatorCantBeLessThan(nameof(forecastingDenominator), DefaultForecastingDenominator));

            ForecastingDenominator = forecastingDenominator;

        }

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/> using default values.
        /// </summary>
        public UnivariateForecastingSettings()
            : this(DefaultForecastingDenominator) { }
        
        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.02.2023
*/