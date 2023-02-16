using System;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.Observations
{
    /// <summary>Collects all the methods useful to manipulate an <see cref="Observation"/>.</summary>
    public interface IObservationManager
    {

        /// <summary>Calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.</summary>
        /// <exception cref="ArgumentException"/> 
        Observation Create(SlidingWindow slidingWindow, double? coefficient = null, double? error = null);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 16.02.2023 

*/
