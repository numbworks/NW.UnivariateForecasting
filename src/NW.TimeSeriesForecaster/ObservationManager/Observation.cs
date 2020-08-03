using System;

namespace NW.UnivariateForecasting
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

        // Methods
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Name)}: '{Name}'",
                    $"{nameof(StartDate)}: '{StartDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(EndDate)}: '{EndDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(X_Actual)}: '{X_Actual.ToString()}'",
                    $"{nameof(C)}: '{C.ToString()}'",
                    $"{nameof(E)}: '{E.ToString()}'",
                    $"{nameof(Y_Forecasted)}: '{Y_Forecasted.ToString()}'",
                    $"{nameof(SlidingWindowId)}: '{SlidingWindowId.ToString()}'"
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
