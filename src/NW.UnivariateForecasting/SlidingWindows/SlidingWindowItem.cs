using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.SlidingWindows
{
    public class SlidingWindowItem
    {

        #region Fields
        #endregion

        #region Properties

        public uint Id { get; set; }
        public Interval Interval { get; set; }
        public double X_Actual { get; set; }
        public double? Y_Forecasted { get; set; }

        #endregion

        #region Constructors

        public SlidingWindowItem() { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            // [ Id: '0', Interval: 'null', X_Actual: '0', Y_Forecasted: 'null' ]
            // [ Id: '1', Interval: '20190131:20190228:20190331', X_Actual: '58,5', Y_Forecasted: '615,26' ]

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id}'",
                    $"{nameof(Interval)}: '{Interval?.ToString(true) ?? "null"}'",
                    $"{nameof(X_Actual)}: '{X_Actual}'",
                    $"{nameof(Y_Forecasted)}: '{Y_Forecasted?.ToString() ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/
