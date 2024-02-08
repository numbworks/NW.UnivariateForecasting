using System;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.UnitTests.Bags
{
    [TestFixture]
    public class UnivariateForecastingSettingsTests
    {

        #region Fields

        private static TestCaseData[] univariateForecastingSettingsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecastingSettings(
                                    forecastingDenominator: 0,
                                    folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                                    roundingDigits: UnivariateForecastingSettings.DefaultRoundingDigits
                                    )
                ),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanDouble(
                        "forecastingDenominator",
                        UnivariateForecastingSettings.DefaultForecastingDenominator)
                ).SetArgDisplayNames($"{nameof(univariateForecastingSettingsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecastingSettings(
                                    forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                                    folderPath: null,
                                    roundingDigits: UnivariateForecastingSettings.DefaultRoundingDigits
                                    )),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
                ).SetArgDisplayNames($"{nameof(univariateForecastingSettingsExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecastingSettings(
                                    forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                                    folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                                    roundingDigits: 16
                                    )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("roundingDigits", "DefaultRoundingDigits")
                ).SetArgDisplayNames($"{nameof(univariateForecastingSettingsExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(univariateForecastingSettingsExceptionTestCases))]
        public void UnivariateForecastingSettings_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void UnivariateForecastingSettings_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            UnivariateForecastingSettings actual = new UnivariateForecastingSettings();

            // Assert
            Assert.IsInstanceOf<UnivariateForecastingSettings>(actual);
            Assert.IsInstanceOf<double>(actual.ForecastingDenominator);
            Assert.IsInstanceOf<string>(actual.FolderPath);
            Assert.IsInstanceOf<uint>(actual.RoundingDigits);

            Assert.IsInstanceOf<double>(UnivariateForecastingSettings.DefaultForecastingDenominator);
            Assert.IsInstanceOf<string>(UnivariateForecastingSettings.DefaultFolderPath);
            Assert.IsInstanceOf<uint>(UnivariateForecastingSettings.DefaultRoundingDigits);

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