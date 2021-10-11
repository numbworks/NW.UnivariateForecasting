using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting.Intervals
{
    /// <summary>Collects all the methods useful to manipulate an <see cref="Interval"/>.</summary>
    public interface IIntervalManager
    {

        /// <summary>Creates an <see cref="Interval"/>.</summary>
        /// <exception cref="ArgumentException"/> 
        Interval Create(uint size, IntervalUnits unit, DateTime startDate, uint steps);

        /// <summary>Calculates the next date for an <see cref="Interval"/>.</summary>
        /// <exception cref="ArgumentException"/> 
        DateTime CalculateNext(DateTime date, IntervalUnits unit, uint steps);

        /// <summary>Returns true if <paramref name="datetime"/> is at the end of the month.</summary>
        bool IsEndOfTheMonth(DateTime datetime);

        /// <summary>Returns a date that represents the end of <paramref name="datetime"/>'s month.</summary>
        DateTime MoveToEndOfTheMonth(DateTime datetime);

        /// <summary>Returns true if <paramref name="interval"/> is valid.</summary>
        bool IsValid(Interval interval);

        /// <summary>Calculates all the sub-intervals that <paramref name="interval"/> can possibly contain.</summary>
        /// <exception cref="ArgumentException"/> 
        List<Interval> CalculateSubIntervals(Interval interval);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/
