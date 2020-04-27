namespace NW.TimeSeriesForecaster
{
    public class ForecastedObservationUnivariate
    {

        // Fields
        // Properties
        public string SlidingWindowId { get; set; }
        public string ObservationName { get; set; }
        public double X_Actual { get; set; }
        public double C { get; set; }
        public double E { get; set; }
        public double Y1_Forecasted { get; set; }
        public string TagCollection { get; set; }

        // Constructors
        public ForecastedObservationUnivariate() { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 24.04.2018

*/
