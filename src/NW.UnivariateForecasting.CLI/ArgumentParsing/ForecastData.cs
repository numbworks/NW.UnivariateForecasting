namespace NW.UnivariateForecasting.CLI.ArgumentParsing
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

        #region Methods (public)

        #endregion

        #region Methods (private)

        #endregion

    }
}