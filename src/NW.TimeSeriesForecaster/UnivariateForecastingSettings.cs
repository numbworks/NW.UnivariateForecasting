using System;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecastingSettings
    {

        // Fields
        // Properties
        /// <summary>
        /// The <see cref="SlidingWindow.Id"/> will be generated according to this.
        /// <para>Default: "SW{yyyyMMddhhmmsss}" using current datetime.</para>
        /// </summary>
        public Func<string> IdCreationFunction { get; private set; }

        /// <summary>
        /// All the values processed by <see cref="NW.UnivariateForecasting"/> will be rounded according to this function.
        /// <para>Default: two decimal digits.</para>
        /// </summary>
        public Func<double, double> RoundingFunction { get; private set; }

        /// <summary>
        /// <para>Default: Console.WriteLine(message).</para>
        /// </summary>
        public Action<string> LoggingAction { get; private set; }

        /// <summary>
        /// Y_Forecasted = 0 in a <see cref="SlidingWindowItem"/> is a totally legit value.
        /// To avoid "divide-by-zero" error, we replace it with a comparably small amount while forecasting.
        /// <para>Default: 0.001.</para>
        /// </summary>
        public double ForecastingDenominator { get; private set; }

        /// <summary>
        /// For <see cref="SlidingWindow"/>.
        /// <para>Default: "Default Id".</para>
        /// </summary>
        public string DummyId{ get; private set; }

        /// <summary>
        /// For <see cref="SlidingWindow"/>.
        /// <para>Default: "Default Observation".</para>
        /// </summary>
        public string DummyObservationName { get; private set; }

        /// <summary>
        /// For <see cref="SlidingWindow"/>.
        /// <para>Default: 2020-01-01.</para>
        /// </summary>
        public DateTime DummyStartDate { get; private set; }

        /// <summary>
        /// For <see cref="SlidingWindow"/>.
        /// <para>Default: 1.</para>
        /// </summary>
        public uint DummySteps { get; private set; }

        /// <summary>
        /// For <see cref="SlidingWindow"/>.
        /// <para>Default: <see cref="IntervalUnits.Months"/>.</para>
        /// </summary>
        public IntervalUnits DummyIntervalUnit { get; private set; }

        // Constructors
        /// <summary>
        /// If not provided, pre-definited functions and values are assigned. Hover the mouse over the parameters for details.
        /// </summary>
        public UnivariateForecastingSettings(
                Func<string> idCreationFunction = null,
                Func<double, double> roundingFunction = null,
                Action<string> loggingAction = null,
                double forecastingDenominator = 0.001,
                string dummyId = null,
                string dummyObservationName = null,
                DateTime dummyStartDate = default(DateTime),
                uint dummySteps = default(uint),
                IntervalUnits dummyIntervalUnit = default(IntervalUnits)
            )
        {

            double defaultDenominator = 0.001;
            if (forecastingDenominator < defaultDenominator)
                if (forecastingDenominator < defaultDenominator)
                    throw new ArgumentException(MessageCollection.DenominatorCantBeLessThan(nameof(forecastingDenominator), defaultDenominator));

            IdCreationFunction = idCreationFunction;
            RoundingFunction = roundingFunction;
            LoggingAction = loggingAction;
            ForecastingDenominator = forecastingDenominator;
            DummyId = dummyId;
            DummyObservationName = dummyObservationName;
            DummyStartDate = dummyStartDate;
            DummySteps = dummySteps;
            DummyIntervalUnit = dummyIntervalUnit;

            if (idCreationFunction == null)
                IdCreationFunction = () => $"SW{DateTime.Now.ToString("yyyyMMddhhmmsss")}";
            if (roundingFunction == null)
                RoundingFunction = new Func<double, double>(x => Math.Round(x, 2, MidpointRounding.AwayFromZero));
            if (loggingAction == null)
                LoggingAction = (message) => Console.WriteLine(message);
            if (dummyId == null)
                DummyId = "Dummy Id";
            if (dummyObservationName == null)
                DummyObservationName = "Dummy Observation";
            if (dummyStartDate == default(DateTime))
                DummyStartDate = new DateTime(2020, 01, 01);
            if (dummySteps== default(uint))
                DummySteps = 1;
            if (dummyIntervalUnit == default(IntervalUnits))
                DummyIntervalUnit = IntervalUnits.Months;

        }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/