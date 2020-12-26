using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface IIntervalManager
    {
        DateTime CalculateNext(DateTime date, IntervalUnits unit, uint steps);
        Interval Create(uint size, IntervalUnits unit, DateTime startDate, uint steps);
        bool IsEndOfTheMonth(DateTime datetime);
        bool IsValid(Interval interval);
        DateTime MoveToEndOfTheMonth(DateTime datetime);
        List<Interval> CalculateSubIntervals(Interval interval);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.08.2020

*/