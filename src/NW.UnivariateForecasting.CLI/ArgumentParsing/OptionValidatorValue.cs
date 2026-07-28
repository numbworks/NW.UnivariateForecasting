using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <summary>Collects all the values required by the <see cref="IOptionValidator"/> classes.</summary>
    public static class OptionValidatorValue
    {

        #region Properties

        public static double? ForecastingDenominatorDefaultValue { get; } = null;
        public static double ForecastingDenominatorMininumValue { get; } = SettingBag.DefaultForecastingDenominator;

        public static uint? RoundingDigitsDefaultValue { get; } = null;
        public static uint RoundingDigitsMininumValue { get; } = 0;
        public static uint RoundingDigitsMaximumValue { get; } = 15;

        #endregion

    }
}