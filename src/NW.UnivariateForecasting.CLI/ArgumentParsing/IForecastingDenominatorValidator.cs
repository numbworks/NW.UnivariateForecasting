namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <summary>Collects the additional utility methods related to <see cref="ForecastingDenominatorValidator"/>.</summary>
    public interface IForecastingDenominatorValidator
    {

        /// <summary>
        /// Checks if <paramref name="value"/> is valid or not.
        /// </summary>
        bool IsValid(string value);

        /// <summary>Parses <paramref name="value"/> or returns null if <paramref name="value"/> is null.</summary>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        double? ParseOrDefault(string value);

    }
}