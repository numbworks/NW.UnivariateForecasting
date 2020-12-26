using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowManager
    {
        SlidingWindow Create
            (string id, string observationName, Interval interval, List<SlidingWindowItem> items);
        SlidingWindow Create
            (string id, string observationName, List<double> values, uint steps, IntervalUnits intervalUnit, DateTime startDate);
        SlidingWindow Create(List<double> values);
        bool IsValid(SlidingWindow slidingWindow);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/