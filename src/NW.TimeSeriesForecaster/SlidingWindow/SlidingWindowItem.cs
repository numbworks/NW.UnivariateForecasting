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

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id.ToString()}'",
                    $"{nameof(Interval)}: '{Interval.ToString(true) ?? "null"}'",
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
