using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowItem
    {

        // Fields
        // Properties
        public uint Id { get; set; }
        public Interval Interval { get; set; }
        public double X_Actual { get; set; }
        public double? Y_Forecasted { get; set; }

        // Constructors
        public SlidingWindowItem() { }

        // Methods
        public override string ToString()
        {

            // [ Id: '0', Interval: 'null', X_Actual: '0', Y_Forecasted: 'null' ]
            // [ Id: '1', Interval: '20190131:20190228:20190331', X_Actual: '58,5', Y_Forecasted: '615,26' ]

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id.ToString()}'",
                    $"{nameof(Interval)}: '{Interval?.ToString(true) ?? "null"}'",
                    $"{nameof(X_Actual)}: '{X_Actual.ToString()}'",
                    $"{nameof(Y_Forecasted)}: '{Y_Forecasted?.ToString() ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.09.2020

*/
