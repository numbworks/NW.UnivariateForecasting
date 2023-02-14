﻿using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.Observations
{
    /// <summary>A data structure representing the next value of a certain <see cref="SlidingWindow"/> according to Univariate Forecasting.</summary>
    public class Observation
    {

        #region Fields

        public string Name { get; set; }
        public double X_Actual { get; set; }
        public double C { get; set; }
        public double E { get; set; }
        public double Y_Forecasted { get; set; }
        public string SlidingWindowId { get; set; }

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes an <see cref="Observation"/> instance.</summary>
        public Observation() { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            // "[ Name: 'Total Monthly Sales USD', X_Actual: '632,94', C: '0,82', E: '0,22', Y_Forecasted: '519,23', SlidingWindowId: 'SW20200906090516' ]"
            // "[ Name: 'Total Monthly Sales USD', X_Actual: '632,94', C: '0,82', E: '0,22', Y_Forecasted: '519,23', SlidingWindowId: 'SW20200906090516' ]"
            // "[ Name: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]"
            // ...

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Name)}: '{Name ?? "null"}'",
                    $"{nameof(X_Actual)}: '{X_Actual}'",
                    $"{nameof(C)}: '{C}'",
                    $"{nameof(E)}: '{E}'",
                    $"{nameof(Y_Forecasted)}: '{Y_Forecasted}'",
                    $"{nameof(SlidingWindowId)}: '{SlidingWindowId ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
