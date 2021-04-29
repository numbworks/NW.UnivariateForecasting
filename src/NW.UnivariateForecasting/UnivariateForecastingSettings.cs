using System;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Collects all the global settings required by the library.
    /// </summary>
    public class UnivariateForecastingSettings
    {

        // Fields
        // Properties (static)
        public const double DefaultForecastingDenominator = 0.001;
        public const string DefaultDummyId = "Dummy Id";
        public const string DefaultDummyObservationName = "Dummy Observation";
        public static DateTime DefaultDummyStartDate = new DateTime(2020, 01, 01);
        public const uint DefaultDummySteps = 1;
        public const IntervalUnits DefaultDummyIntervalUnit = IntervalUnits.Months;

        // Properties
        public double ForecastingDenominator { get; private set; }
        public string DummyId{ get; private set; }
        public string DummyObservationName { get; private set; }
        public DateTime DummyStartDate { get; private set; }
        public uint DummySteps { get; private set; }
        public IntervalUnits DummyIntervalUnit { get; private set; }

        // Constructors
        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingSettings"/>. Hover the mouse over the parameters for details.
        /// </summary>
        /// <param name="forecastingDenominator">Y_Forecasted = 0 in a <see cref="SlidingWindowItem"/> is a totally legit value. To avoid "divide-by-zero" error, we replace it with a comparably small amount while forecasting. Default: 0.001.</param>
        /// <param name="dummyId">For <see cref="SlidingWindow"/>. Default: "Dummy Id".</param>
        /// <param name="dummyObservationName">For <see cref="SlidingWindow"/>. Default: "Default Observation".</param>
        /// <param name="dummyStartDate">For <see cref="SlidingWindow"/>. Default: 2020-01-01.</param>
        /// <param name="dummySteps">For <see cref="SlidingWindow"/>. Default: 1.</param>
        /// <param name="dummyIntervalUnit">For <see cref="SlidingWindow"/>. Default: <see cref="IntervalUnits.Months"/>.</param>
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
                throw new ArgumentException(MessageCollection.UnivariateForecastingSettings_DenominatorCantBeLessThan(nameof(forecastingDenominator), DefaultForecastingDenominator));

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

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021

*/