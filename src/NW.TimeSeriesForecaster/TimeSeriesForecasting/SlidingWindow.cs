using System;
using System.Collections.Generic;

namespace NW.TimeSeriesForecaster
{
    public class SlidingWindow
    {

        // Fields
        // Properties
        public string ObservationDescription { get; set; }
        public ushort StepsAhead { get; set; }
        public ushort Duration { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string SlidingWindowId { get; set; }
        public List<SlidingWindowTimeSeries> TimeSeriesCollection { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public string DurationUnit { get; set; }

        // Constructors
        // Methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 24.04.2018

*/