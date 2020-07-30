using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public class SlidingWindow
    {

        // Fields
        // Properties
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public int Interval { get; set; }
        public SlidingWindowIntervalUnits IntervalUnit { get; set; }
        public List<SlidingWindowItem> Items { get; set; }
        public string ObservatioName { get; set; }

        // Constructors
        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
