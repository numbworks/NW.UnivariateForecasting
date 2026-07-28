using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <inheritdoc cref="IOptionValidator"/>
    public class RoundingDigitsValidator : IOptionValidator
    {

        #region Fields

        private string _valueName;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="RoundingDigitsValidator"/> instance.</summary>
        public RoundingDigitsValidator()
        {

            _valueName = nameof(RoundingDigitsValidator).Replace("Validator", string.Empty);

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

        #endregion

        #region Methods_private

        private bool IsValid(string value)
        {

            try
            {
                uint parsed = uint.Parse(value);

                return parsed >= OptionValidatorValue.RoundingDigitsMininumValue 
                        && parsed <= OptionValidatorValue.RoundingDigitsMaximumValue;

            }
            catch
            {

                return false;

            }

        }

        #endregion

    }
}