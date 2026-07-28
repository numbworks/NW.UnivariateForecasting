using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <inheritdoc cref="IOptionValidator"/>
    public class ForecastingDenominatorValidator : IOptionValidator
    {

        #region Fields

        private string _valueName;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ForecastingDenominatorValidator"/> instance.</summary>
        public ForecastingDenominatorValidator()
        {

            _valueName = nameof(ForecastingDenominatorValidator).Replace("Validator", string.Empty);

        }

        #endregion

        #region Methods (public)

        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {

            // We need to accept also nulls because the rounding digits option is optional

            if (string.IsNullOrWhiteSpace(option.Value()))
                return ValidationResult.Success;

            if (IsValid(option.Value()))
                return ValidationResult.Success;

            return new ValidationResult(Messages.MessageCollection.ValueIsInvalidOrNotWithinRange(_valueName, option.Value()));

        }

        #endregion

        #region Methods (private)

        private bool IsValid(string value)
        {

            try
            {
                double parsed = double.Parse(value);

                return parsed >= OptionValidatorValue.ForecastingDenominatorMininumValue;

            }
            catch
            {

                return false;

            }

        }

        #endregion

    }
}