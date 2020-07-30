using System;

namespace NW.TimeSeriesForecaster
{
    public class Observation
    {

        // Fields
        // Properties
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double X_Actual { get; set; }
        public double C { get; set; }
        public double E { get; set; }
        public double Y_Forecasted { get; set; }
        public string SlidingWindowId { get; set; }

        // Constructors
        public Observation() { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
