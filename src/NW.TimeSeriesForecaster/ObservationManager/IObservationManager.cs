using System;

namespace NW.UnivariateForecasting
{
    public interface IObservationManager
    {

        /// <summary>
        /// It calculate the unknown values in the following equation: Y1=F(X)+E => Y1=CX+E.
        /// It assigns them to a given ForecastedObservationUnivariate object.
        /// If fRound is defined, it gets used to round C, E and Y1_Forecasted.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>        
        /// </summary>
        Observation Create(
            SlidingWindow slidingWindow,
            Func<double, double> roundingFunction = null);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020   

*/
