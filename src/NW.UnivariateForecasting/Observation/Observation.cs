﻿namespace NW.UnivariateForecasting
{
    /// <summary>
    /// A data structure representing the next value of a certain <see cref="SlidingWindow"/> according to Univariate Forecasting.
    /// </summary>
    public class Observation
    {

        // Fields
        // Properties
        public string Name { get; set; }
        public Interval Interval { get; set; }
        public double X_Actual { get; set; }
        public double C { get; set; }
        public double E { get; set; }
        public double Y_Forecasted { get; set; }
        public string SlidingWindowId { get; set; }

        // Constructors
        public Observation() { }

        // Methods
        public override string ToString()
            => ToString(true);
        public string ToString(bool onlyDates)
        {

            // "[ Name: 'Total Monthly Sales USD', Interval: '1:Months:20190831:20190930:20191031:1:1', X_Actual: '632,94', C: '0,82', E: '0,22', Y_Forecasted: '519,23', SlidingWindowId: 'SW20200906090516' ]"
            // "[ Name: 'Total Monthly Sales USD', Interval: '20190831:20190930:20191031', X_Actual: '632,94', C: '0,82', E: '0,22', Y_Forecasted: '519,23', SlidingWindowId: 'SW20200906090516' ]"
            // "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]"
            // ...

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Name)}: '{Name ?? "null"}'",
                    $"{nameof(Interval)}: '{Interval?.ToString(onlyDates) ?? "null"}'",
                    $"{nameof(X_Actual)}: '{X_Actual.ToString()}'",
                    $"{nameof(C)}: '{C.ToString()}'",
                    $"{nameof(E)}: '{E.ToString()}'",
                    $"{nameof(Y_Forecasted)}: '{Y_Forecasted.ToString()}'",
                    $"{nameof(SlidingWindowId)}: '{SlidingWindowId ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
