﻿using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowCreator
    {

        SlidingWindow CreateSlidingWindow
            (string id, DateTime startDate, List<double> values, IntervalUnits intervalUnit, string observationName);
        SlidingWindow CreateSlidingWindow
            (DateTime startDate, List<double> values, IntervalUnits intervalUnit, string observationName);

        DateTime CalculateNext(DateTime date, IntervalUnits intervalUnit, int steps);
        DateTime CalculateNext(DateTime date, IntervalUnits intervalUnit);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
