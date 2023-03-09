using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecastingClient.ApplicationSession
{
    /// <inheritdoc cref="IOptionValidator"/>
    public class RoundingDigitsValidator : IOptionValidator
    {

        #region Fields

        private string _valueName;

        #endregion

        #region Properties

        public static uint? DefaultValue { get; } = null;
        public static uint MininumValue { get; } = 0;
        public static uint MaximumValue { get; } = 15;

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

            return new ValidationResult(Shared.MessageCollection.ValueIsInvalidOrNotWithinRange(_valueName, option.Value()));

        }

        /// <summary>Checks if <paramref name="value"/> is valid or not.</summary>        
        public bool IsValid(string value)
        {

            try
            {
                uint parsed = uint.Parse(value);

                return parsed >= MininumValue && parsed <= MaximumValue;

            }
            catch
            {

                return false;

            }

        }

        /// <summary>Parses <paramref name="value"/> or returns <see cref="DefaultValue"/> if <paramref name="value"/> is not valid.</summary>        
        public uint? ParseOrDefault(string value)
        {

            try
            {

                if (value == null)
                    return DefaultValue;

                return uint.Parse(value);

            }
            catch
            {

                return DefaultValue;

            }

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/