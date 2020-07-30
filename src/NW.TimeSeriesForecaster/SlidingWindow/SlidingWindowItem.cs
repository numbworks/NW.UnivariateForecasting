using System;

namespace NW.TimeSeriesForecaster
{
    public class SlidingWindowItem
    {

        // Fields
        // Properties
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public double X_Actual { get; set; }
        public double? Y_Forecasted { get; set; }

        // Constructors
        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
