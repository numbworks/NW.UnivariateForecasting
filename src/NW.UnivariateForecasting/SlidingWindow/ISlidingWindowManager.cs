using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Collects all the methods useful to manipulate an <see cref="SlidingWindow"/>.
    /// </summary>
    public interface ISlidingWindowManager
    {
        /// <summary>
        /// Creates a <seealso cref="SlidingWindow"/> object.
        /// </summary>
        SlidingWindow Create
            (string id, string observationName, Interval interval, List<SlidingWindowItem> items);

        /// <summary>
        /// Creates a <seealso cref="SlidingWindow"/> object.
        /// </summary>
        SlidingWindow Create
            (string id, string observationName, List<double> values, uint steps, IntervalUnits intervalUnit, DateTime startDate);

        /// <summary>
        /// Creates a <seealso cref="SlidingWindow"/> object out of the <seealso cref="UnivariateForecastingSettings"/> properties.
        /// </summary>
        SlidingWindow Create(List<double> values);

        /// <summary>
        /// Checks the properties of the provided <seealso cref="SlidingWindow"/> object for validity.
        /// </summary>
        bool IsValid(SlidingWindow slidingWindow);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021

*/
