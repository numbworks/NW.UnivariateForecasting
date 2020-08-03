using System;

namespace NW.UnivariateForecasting
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
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id.ToString()}'",
                    $"{nameof(StartDate)}: '{StartDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(EndDate)}: '{EndDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(TargetDate)}: '{TargetDate?.ToString("yyyy-MM-dd") ?? "null"}'",
                    $"{nameof(X_Actual)}: '{X_Actual.ToString()}'",
                    $"{nameof(Y_Forecasted)}: '{Y_Forecasted.ToString() ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
