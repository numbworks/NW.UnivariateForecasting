using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowManager
    {
        bool IsValid(SlidingWindow slidingWindow);
        DateTime CalculateNext(DateTime date, SlidingWindowIntervalUnits intervalUnit, int steps);
        string CreateId(string prefix, DateTime date);
        SlidingWindow CreateSlidingWindow
            (string id, DateTime startDate, List<double> values, SlidingWindowIntervalUnits intervalUnit, string observationName);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
