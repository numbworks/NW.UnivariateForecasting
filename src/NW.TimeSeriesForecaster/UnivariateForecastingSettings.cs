﻿using System;

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
        /// <para>Default: "Default Observation".</para>
        /// </summary>
        public string DefaultObservationName { get; private set; }

        /// <summary>
        /// For <see cref="SlidingWindow"/>.
        /// <para>Default: 2020-01-01.</para>
        /// </summary>
        public DateTime DefaultStartDate { get; private set; }

        // Constructors
        /// <summary>
        /// If not provided, pre-definited functions and values are assigned. Hover the mouse over the parameters for details.
        /// </summary>
        public UnivariateForecastingSettings(
                Func<string> idCreationFunction = null,
                Func<double, double> roundingFunction = null,
                Action<string> loggingAction = null,
                double forecastingDenominator = 0.001,
                string defaultObservationName = null,
                DateTime defaultStartDate = default(DateTime)
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
            DefaultObservationName = defaultObservationName;
            DefaultStartDate = defaultStartDate;

            if (idCreationFunction == null)
                IdCreationFunction = () => $"SW{DateTime.Now.ToString("yyyyMMddhhmmsss")}";
            if (roundingFunction == null)
                RoundingFunction = new Func<double, double>(x => Math.Round(x, 2, MidpointRounding.AwayFromZero));
            if (loggingAction == null)
                LoggingAction = (message) => Console.WriteLine(message);
            if (defaultObservationName == null)
                DefaultObservationName = "Default Observation";
            if (defaultStartDate == default(DateTime))
                DefaultStartDate = new DateTime(2020, 01, 01);

        }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/