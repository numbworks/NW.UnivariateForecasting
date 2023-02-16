using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.Observations
{
    /// <summary>A data structure representing the next value of a certain <see cref="SlidingWindow"/> according to Univariate Forecasting.</summary>
    public class Observation
    {

        #region Fields

        public double Coefficient { get; }
        public double Error { get; }
        public double NextValue { get; }

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes an <see cref="Observation"/> instance.</summary>
        public Observation(double coefficient, double error, double nextValue) 
        {

            Coefficient = coefficient;
            Error = error;
            NextValue = nextValue;

        }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            // "[ C: '0,82', Error: '0,22',  NextValue: '519,23' ]"

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Coefficient)}: '{Coefficient}'",
                    $"{nameof(Error)}: '{Error}'",
                    $"{nameof(NextValue)}: '{NextValue}'"
                    );

            return $"[ {content} ]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/
