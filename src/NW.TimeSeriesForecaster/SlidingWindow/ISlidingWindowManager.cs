using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowManager
    {
        SlidingWindow Create(string id, string observationName, Interval interval, List<SlidingWindowItem> items);
        SlidingWindow Create(string id, string observationName, List<double> values, uint steps, IntervalUnits intervalUnit, DateTime startDate);
        bool IsValid(SlidingWindow slidingWindow);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.09.2020

*/