﻿namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <summary>An item within a <see cref="SlidingWindow"/>.</summary>
    public class SlidingWindowItem
    {

        #region Fields
        #endregion

        #region Properties

        public uint Id { get; set; }
        public double X_Actual { get; set; }
        public double? Y_Forecasted { get; set; }

        #endregion

        #region Constructors

        /// <summary>Initializes an <see cref="SlidingWindowItem"/> instance.</summary>
        public SlidingWindowItem() { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            // [ Id: '0', X_Actual: '0', Y_Forecasted: 'null' ]
            // [ Id: '1', X_Actual: '58,5', Y_Forecasted: '615,26' ]

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id}'",
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
    Last Update: 14.02.2023
*/
