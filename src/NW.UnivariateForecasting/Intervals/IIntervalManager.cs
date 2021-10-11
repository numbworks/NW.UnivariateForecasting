using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting.Intervals
{
    /// <summary>
    /// Collects all the methods useful to manipulate an <see cref="Interval"/>.
    /// </summary>
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
    Last Update: 25.04.2021

*/
