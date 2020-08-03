using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowCreator
    {

        SlidingWindow CreateSlidingWindow
            (string id, DateTime startDate, List<double> values, SlidingWindowIntervalUnits intervalUnit, string observationName);
        SlidingWindow CreateSlidingWindow
            (DateTime startDate, List<double> values, SlidingWindowIntervalUnits intervalUnit, string observationName);

        DateTime CalculateNext(DateTime date, SlidingWindowIntervalUnits intervalUnit, int steps);
        string CreateId(string prefix, DateTime date);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
