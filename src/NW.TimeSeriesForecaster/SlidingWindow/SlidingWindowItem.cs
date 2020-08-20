using System;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowItem
    {

        // Fields
        // Properties
        public uint Id { get; }
        public Interval Interval { get; }
        public double X_Actual { get; }
        public double? Y_Forecasted { get; }

        // Constructors
        public SlidingWindowItem(
            uint id,
            Interval interval,
            double X_Actual,
            double? Y_Forecasted)
        {

            if (interval == null)
                throw new ArgumentNullException(nameof(interval));

            Id = id;
            Interval = interval;
            this.X_Actual = X_Actual;
            this.Y_Forecasted = Y_Forecasted;

        }

        // Methods
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id.ToString()}'",
                    $"{nameof(Interval)}: '{Interval.ToString(true)}'",
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
