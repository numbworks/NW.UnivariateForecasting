using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Collects all the methods useful to manipulate an <see cref="SlidingWindowItem"/>.
    /// </summary>
    public interface ISlidingWindowItemManager
    {

        /// <summary>
        /// Creates a <seealso cref="SlidingWindowItem"/> object.
        /// </summary>
        SlidingWindowItem CreateItem(uint id, Interval interval, double X_Actual, double? Y_Forecasted);

        /// <summary>
        /// Creates a <seealso cref="SlidingWindowItem"/> object.
        /// </summary>
        SlidingWindowItem CreateItem(uint id, DateTime startDate, IntervalUnits intervalUnit, double X_Actual, double? Y_Forecasted);

        /// <summary>
        /// Creates a collection of <seealso cref="SlidingWindowItem"/> objects.
        /// </summary>
        List<SlidingWindowItem> CreateItems(DateTime startDate, List<double> values, IntervalUnits intervalUnit);

        /// <summary>
        /// Checks the properties of the provided <seealso cref="SlidingWindowItem"/> object for validity.
        /// </summary>
        bool IsValid(SlidingWindowItem slidingWindowItem);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021

*/
