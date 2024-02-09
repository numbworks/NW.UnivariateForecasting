using System.ComponentModel.DataAnnotations;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.Shared;
using McMaster.Extensions.CommandLineUtils;
using NUnit.Framework;
using System;

namespace NW.UnivariateForecastingClient.UnitTests.ApplicationSession
{
    [TestFixture]
    public class RoundingDigitsValidatorTests
    {

        #region Fields

        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData("0", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData("1", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData("15", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData("16", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData("-1", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_05"),

            new TestCaseData(null, false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_06"),

            new TestCaseData(string.Empty, false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_07"),

            new TestCaseData("Some message", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_08")

        };
        private static TestCaseData[] parseOrDefaultTestCases =
        {

            new TestCaseData("0", (uint?)0)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_01"),

            new TestCaseData("1", (uint?)1)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_02"),

            new TestCaseData("15", (uint?)15)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_03"),

            new TestCaseData("16", (uint?)16)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_04"),

            new TestCaseData("-1", RoundingDigitsValidator.DefaultValue)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_05"),

            new TestCaseData(null, RoundingDigitsValidator.DefaultValue)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_06"),

            new TestCaseData(string.Empty, RoundingDigitsValidator.DefaultValue)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_07"),

            new TestCaseData("Some message", RoundingDigitsValidator.DefaultValue)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_08"),

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [Test]
        public void RoundingDigitsValidator_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            RoundingDigitsValidator actual = new RoundingDigitsValidator();

            // Assert
            Assert.That(actual, Is.InstanceOf<RoundingDigitsValidator>());

            Assert.That(ForecastingDenominatorValidator.DefaultValue, Is.Null);
            Assert.That(RoundingDigitsValidator.MininumValue, Is.InstanceOf<uint>());
            Assert.That(RoundingDigitsValidator.MaximumValue, Is.InstanceOf<uint>());

        }

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenProperArgument(string value, bool expected)
        {

            // Arrange
            // Act
            bool actual = new RoundingDigitsValidator().IsValid(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [TestCaseSource(nameof(parseOrDefaultTestCases))]
        public void ParseOrDefault_ShouldReturnExpectedUint_WhenProperArgument(string value, uint? expected)
        {

            // Arrange
            // Act
            uint? actual = new RoundingDigitsValidator().ParseOrDefault(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [TestCase("somegarbage")]
        public void GetValidationResult_ShouldReturnExpectedErrorMessage_WhenInvalidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(MessageCollection.Session_Option_RoundingDigits_Template, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);
            string valueName = nameof(RoundingDigitsValidator).Replace("Validator", string.Empty);
            string expected = MessageCollection.ValueIsInvalidOrNotWithinRange(valueName, option.Value());

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
            CommandOption option = new CommandOption(MessageCollection.Session_Option_RoundingDigits_Template, CommandOptionType.SingleValue);
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

/*
    Author: numbworks@gmail.com
    Last Update: 09.02.2024
*/
