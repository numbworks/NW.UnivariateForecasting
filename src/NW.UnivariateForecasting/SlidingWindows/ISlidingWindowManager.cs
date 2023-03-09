using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <summary>Collects all the methods useful to manipulate an <see cref="SlidingWindow"/>.</summary>
    public interface ISlidingWindowManager
    {

        /// <summary>Creates a <seealso cref="SlidingWindow"/> object.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        SlidingWindow Create(List<double> values, uint roundingDigits);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/
