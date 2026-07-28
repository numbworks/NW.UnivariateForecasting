using System.ComponentModel.DataAnnotations;
using NW.UnivariateForecasting.CLI.ArgumentParsing;
using McMaster.Extensions.CommandLineUtils;
using NUnit.Framework;

namespace NW.UnivariateForecasting.CLI.UnitTests.ArgumentParsing
{
    [TestFixture]
    public class RoundingDigitsValidatorTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCase("somegarbage")]
        public void GetValidationResult_ShouldReturnExpectedErrorMessage_WhenInvalidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(CommandLineString.OPTION_ROUNDINGDIGITS_TEMPL, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);
            string valueName = nameof(RoundingDigitsValidator).Replace("Validator", string.Empty);
            string expected = Messages.MessageCollection.ValueIsInvalidOrNotWithinRange(valueName, option.Value());

            // Act
            ValidationResult actual = new RoundingDigitsValidator().GetValidationResult(option, context);

            // Assert
            Assert.That(actual.ErrorMessage, Is.EqualTo(expected));

        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("15")]
        [TestCase((string)null)]
        public void GetValidationResult_ShouldReturnSuccess_WhenValidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(CommandLineString.OPTION_ROUNDINGDIGITS_TEMPL, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);

            // Act
            ValidationResult actual = new RoundingDigitsValidator().GetValidationResult(option, context);

            // Assert
            Assert.That(actual, Is.EqualTo(ValidationResult.Success));

        }

        #endregion

        #region TearDown
        #endregion

    }
}