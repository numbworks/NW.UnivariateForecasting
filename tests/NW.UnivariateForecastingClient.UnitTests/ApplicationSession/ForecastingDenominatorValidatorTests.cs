using System.ComponentModel.DataAnnotations;
using NW.UnivariateForecastingClient.Shared;
using NW.UnivariateForecastingClient.ApplicationSession;
using McMaster.Extensions.CommandLineUtils;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecastingClient.UnitTests.ApplicationSession
{
    [TestFixture]
    public class ForecastingDenominatorValidatorTests
    {

        #region Fields

        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData("0.0001", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData("1", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(SettingBag.DefaultForecastingDenominator.ToString(), true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData(null, false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData(string.Empty, false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_05"),

            new TestCaseData("Some message", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_06")

        };
        private static TestCaseData[] parseOrDefaultTestCases =
        {

            new TestCaseData("0.0001", (double?)0.0001)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_01"),

            new TestCaseData("1", (double?)1)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_02"),

            new TestCaseData(
                    SettingBag.DefaultForecastingDenominator.ToString(), 
                    (double?)SettingBag.DefaultForecastingDenominator)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_03"),

            new TestCaseData(null, ForecastingDenominatorValidator.DefaultValue)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_04")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [Test]
        public void ForecastingDenominatorValidator_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            ForecastingDenominatorValidator actual = new ForecastingDenominatorValidator();

            // Assert
            Assert.That(actual, Is.InstanceOf<ForecastingDenominatorValidator>());

            Assert.That(ForecastingDenominatorValidator.DefaultValue, Is.Null);
            Assert.That(ForecastingDenominatorValidator.MininumValue, Is.InstanceOf<double>());

        }

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenProperArgument(string value, bool expected)
        {

            // Arrange
            // Act
            bool actual = new ForecastingDenominatorValidator().IsValid(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [TestCaseSource(nameof(parseOrDefaultTestCases))]
        public void ParseOrDefault_ShouldReturnExpectedUint_WhenProperArgument(string value, double? expected)
        {

            // Arrange
            // Act
            double? actual = new ForecastingDenominatorValidator().ParseOrDefault(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [TestCase("somegarbage")]
        public void GetValidationResult_ShouldReturnExpectedErrorMessage_WhenInvalidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(MessageCollection.Session_Option_ForecastingDenominator_Template, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);
            string valueName = nameof(ForecastingDenominatorValidator).Replace("Validator", string.Empty);
            string expected = MessageCollection.ValueIsInvalidOrNotWithinRange(valueName, option.Value());

            // Act
            ValidationResult actual = new ForecastingDenominatorValidator().GetValidationResult(option, context);

            // Assert
            Assert.That(actual.ErrorMessage, Is.EqualTo(expected));

        }

        [TestCase("0.0001")]
        [TestCase("1")]
        [TestCase((string)null)]
        public void GetValidationResult_ShouldReturnSuccess_WhenValidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(MessageCollection.Session_Option_ForecastingDenominator_Template, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);

            // Act
            ValidationResult actual = new ForecastingDenominatorValidator().GetValidationResult(option, context);

            // Assert
            Assert.That(actual, Is.EqualTo(ValidationResult.Success));

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/
