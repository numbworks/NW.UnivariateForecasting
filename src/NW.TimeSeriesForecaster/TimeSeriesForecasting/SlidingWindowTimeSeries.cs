using System;

namespace NW.TimeSeriesForecaster
{
    public class SlidingWindowTimeSeries
    {

        // Fields
        // Properties
        public string ObservationName { get; set; }
        public ushort TimeSeriesId { get; set; }
        public double X_Actual { get; set; }
        public double? Y1_Forecasted { get; set; }
        public double? Y2_Forecasted { get; set; }
        public double? Y3_Forecasted { get; set; }
        public double? Y4_Forecasted { get; set; }
        public double? Y5_Forecasted { get; set; }
        public double? Y6_Forecasted { get; set; }
        public string TagCollection { get; set; }

        // Constructors
        // Methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 24.04.2018

*/