using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <summary>Collects all the methods useful to manipulate an <see cref="SlidingWindowItem"/>.</summary>
    public interface ISlidingWindowItemManager
    {

        /// <summary>Creates a <seealso cref="SlidingWindowItem"/> object.</summary>
        /// <exception cref="ArgumentException"/> 
        SlidingWindowItem CreateItem(uint id, double X_Actual, double? Y_Forecasted);

        /// <summary>Creates a collection of <seealso cref="SlidingWindowItem"/> objects.</summary>
        /// <exception cref="ArgumentNullException"/> 
        /// <exception cref="ArgumentException"/> 
        List<SlidingWindowItem> CreateItems(List<double> values);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
