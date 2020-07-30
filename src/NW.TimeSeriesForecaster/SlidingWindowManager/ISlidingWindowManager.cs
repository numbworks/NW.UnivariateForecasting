using System;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowManager
    {
        bool IsValid(SlidingWindow slidingWindow);
        DateTime CalculateNext(DateTime date, SlidingWindowIntervalUnits intervalUnit);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
