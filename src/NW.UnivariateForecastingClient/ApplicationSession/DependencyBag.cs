namespace NW.UnivariateForecastingClient.ApplicationSession
{
    /// <summary>Collects all the dependencies required by <see cref="SessionManager"/>.</summary>
    public class DependencyBag
    {

        #region Fields
        #endregion

        #region Properties

        public ForecastingDenominatorValidator ForecastingDenominatorValidator { get; }
        public RoundingDigitsValidator RoundingDigitsValidator { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="DependencyBag"/> instance.</summary>
        public DependencyBag() 
        {

            ForecastingDenominatorValidator = new ForecastingDenominatorValidator();
            RoundingDigitsValidator = new RoundingDigitsValidator();

        }

        #endregion

        #region Methods_public
        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/