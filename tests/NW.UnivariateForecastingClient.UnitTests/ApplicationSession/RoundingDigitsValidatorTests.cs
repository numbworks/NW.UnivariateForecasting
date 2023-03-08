using System;
using NW.UnivariateForecastingClient.ApplicationSession;
using NUnit.Framework;

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
            Assert.IsInstanceOf<RoundingDigitsValidator>(actual);

            Assert.IsNull(ForecastingDenominatorValidator.DefaultValue);
            Assert.IsInstanceOf<uint>(RoundingDigitsValidator.MininumValue);
            Assert.IsInstanceOf<uint>(RoundingDigitsValidator.MaximumValue);

        }

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenProperArgument(string value, bool expected)
        {

            // Arrange
            // Act
            bool actual = new RoundingDigitsValidator().IsValid(value);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(parseOrDefaultTestCases))]
        public void ParseOrDefault_ShouldReturnExpectedUint_WhenProperArgument(string value, uint? expected)
        {

            // Arrange
            // Act
            uint? actual = new RoundingDigitsValidator().ParseOrDefault(value);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/
