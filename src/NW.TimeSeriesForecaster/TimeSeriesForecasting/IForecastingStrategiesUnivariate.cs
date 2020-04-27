using System;
using System.Collections.Generic;

namespace NW.TimeSeriesForecaster
{
    public interface IForecastingStrategiesUnivariate
    {

        /// <summary>
        /// Since Y1_Forecasted = 0 is a totally legit value, when it happens we replace it with a comparably 
        /// small amount to avoid errors when dividing by zero.
        /// </summary>
        double AlternativeDenominator { get; }

        /// <summary>
        /// It calculate the unknown values in the following equation: Y1=F(X)+E => Y1=CX+E.
        /// It assigns them to a given ForecastedObservationUnivariate object.
        /// If fRound is defined, it gets used to round C, E and Y1_Forecasted.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>        
        /// </summary>
        void CalculateValues(
            List<SlidingWindowTimeSeries> listTimeSeries,
            ref ForecastedObservationUnivariate objForecasted,
            Func<double, double> fRound = null);

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 25.04.2018     

*/