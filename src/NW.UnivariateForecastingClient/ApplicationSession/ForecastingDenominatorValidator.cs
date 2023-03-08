using System.ComponentModel.DataAnnotations;
using NW.UnivariateForecasting;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecastingClient.ApplicationSession
{
    /// <inheritdoc cref="IOptionValidator"/>
    public class ForecastingDenominatorValidator : IOptionValidator
    {

        #region Fields

        private string _valueName;

        #endregion

        #region Properties

        public static double? DefaultValue { get; } = null;
        public static double MininumValue { get; } 
            = UnivariateForecastingSettings.DefaultForecastingDenominator;

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

            return new ValidationResult(Shared.MessageCollection.ValueIsInvalidOrNotWithinRange(_valueName, option.Value()));

        }

        /// <summary>Checks if <paramref name="value"/> is valid or not.</summary>        
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

        /// <summary>Parses <paramref name="value"/> or returns <see cref="DefaultValue"/> if <paramref name="value"/> is not valid.</summary>        
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

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/