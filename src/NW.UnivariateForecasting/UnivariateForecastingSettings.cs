using System;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting
{
    /// <summary>Collects all the global settings required by the library.</summary>
    public class UnivariateForecastingSettings
    {

        #region Fields
        #endregion

        #region Properties

        public const double DefaultForecastingDenominator = 0.001;
        public const string DefaultDummyId = "Dummy Id";
        public const string DefaultDummyObservationName = "Dummy Observation";
        public static DateTime DefaultDummyStartDate = new DateTime(2020, 01, 01);
        public const uint DefaultDummySteps = 1;
        public const IntervalUnits DefaultDummyIntervalUnit = IntervalUnits.Months;

        public double ForecastingDenominator { get; private set; }
        public string DummyId { get; private set; }
        public string DummyObservationName { get; private set; }
        public DateTime DummyStartDate { get; private set; }
        public uint DummySteps { get; private set; }
        public IntervalUnits DummyIntervalUnit { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/>. Hover the mouse over the parameters for details.
        /// </summary>
        /// <param name="forecastingDenominator">Y_Forecasted = 0 in a <see cref="SlidingWindowItem"/> is a totally legit value. To avoid "divide-by-zero" error, we replace it with a comparably small amount while forecasting. Default: 0.001.</param>
        /// <param name="dummyId">For <see cref="SlidingWindow"/>. Default: "Dummy Id".</param>
        /// <param name="dummyObservationName">For <see cref="SlidingWindow"/>. Default: "Default Observation".</param>
        /// <param name="dummyStartDate">For <see cref="SlidingWindow"/>. Default: 2020-01-01.</param>
        /// <param name="dummySteps">For <see cref="SlidingWindow"/>. Default: 1.</param>
        /// <param name="dummyIntervalUnit">For <see cref="SlidingWindow"/>. Default: <see cref="IntervalUnits.Months"/>.</param>
        /// <exception cref="ArgumentException"/> 
        public UnivariateForecastingSettings(
                double forecastingDenominator,
                string dummyId,
                string dummyObservationName,
                DateTime dummyStartDate,
                uint dummySteps,
                IntervalUnits dummyIntervalUnit
            )
        {

            if (forecastingDenominator < DefaultForecastingDenominator)
                throw new ArgumentException(Forecasts.MessageCollection.DenominatorCantBeLessThan(nameof(forecastingDenominator), DefaultForecastingDenominator));

            ForecastingDenominator = forecastingDenominator;
            DummyId = dummyId;
            DummyObservationName = dummyObservationName;
            DummyStartDate = dummyStartDate;
            DummySteps = dummySteps;
            DummyIntervalUnit = dummyIntervalUnit;

        }

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/> using default values.
        /// </summary>
        public UnivariateForecastingSettings()
            : this(
                  DefaultForecastingDenominator,
                  DefaultDummyId,
                  DefaultDummyObservationName,
                  DefaultDummyStartDate,
                  DefaultDummySteps,
                  DefaultDummyIntervalUnit) { }
        
        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/