namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>Collects all the data required by the <c>forecast</c> sub-command.</summary>
    public class ForecastData
    {

        #region Fields

        #endregion

        #region Properties

        public string Init { get; }
        public string FolderPath { get; }
        public bool SaveSession { get; }
        public uint? RoundingDigits { get; }
        public double? ForecastingDenominator { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastData"/> instance.</summary>	
        public ForecastData(
                string init,
                string folderPath,
                bool saveSession,
                uint? roundingDigits,
                double? forecastingDenominator
            ) 
        {

            Init = init;
            FolderPath = folderPath;
            SaveSession = saveSession;
            RoundingDigits = roundingDigits;
            ForecastingDenominator = forecastingDenominator;

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
    Last Update: 07.03.2023
*/