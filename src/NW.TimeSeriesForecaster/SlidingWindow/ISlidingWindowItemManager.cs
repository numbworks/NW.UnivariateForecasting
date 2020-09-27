using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowItemManager
    {
        SlidingWindowItem CreateItem(uint id, Interval interval, double X_Actual, double? Y_Forecasted);
        SlidingWindowItem CreateItem(uint id, DateTime startDate, IntervalUnits intervalUnit, double X_Actual, double? Y_Forecasted);
        List<SlidingWindowItem> CreateItems(DateTime startDate, List<double> values, IntervalUnits intervalUnit);

        bool IsValid(SlidingWindowItem slidingWindowItem);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 27.09.2020

*/