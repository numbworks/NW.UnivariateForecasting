using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <inheritdoc cref="IOptionValidator"/>
    public class ForecastingDenominatorValidator : IOptionValidator, IForecastingDenominatorValidator
    {

        #region Fields

        private string _valueName;

        #endregion

        #region Properties

        public static double? DefaultValue { get; } = null;
        public static double MininumValue { get; } = SettingBag.DefaultForecastingDenominator;

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingDenominatorValidator"/> instance.</summary>
        public ForecastingDenominatorValidator()
        {

            _valueName = nameof(ForecastingDenominatorValidator).Replace("Validator", string.Empty);

        }

        #endregion

        #region Methods_public

        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {

            // We need to accept also nulls because the rounding digits option is optional

            if (string.IsNullOrWhiteSpace(option.Value()))
                return ValidationResult.Success;

            if (IsValid(option.Value()))
                return ValidationResult.Success;

            return new ValidationResult(Messages.MessageCollection.ValueIsInvalidOrNotWithinRange(_valueName, option.Value()));

        }

        /// <inheritdoc cref="IForecastingDenominatorValidator.IsValid"/>
        public bool IsValid(string value)
        {

            try
            {
                double parsed = double.Parse(value);

                return parsed >= MininumValue;

            }
            catch
            {

                return false;

            }

        }

        /// <inheritdoc cref="IForecastingDenominatorValidator.ParseOrDefault"/>
        public double? ParseOrDefault(string value)
        {

            if (value == null)
                return DefaultValue;

            return double.Parse(value);

        }

        #endregion

        #region Methods_private
        #endregion

    }
}