using System;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.UnitTests.Bags
{
    [TestFixture]
    public class SettingBagTests
    {

        #region Fields

        private static TestCaseData[] settingBagExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SettingBag(
                                    forecastingDenominator: 0,
                                    folderPath: SettingBag.DefaultFolderPath,
                                    roundingDigits: SettingBag.DefaultRoundingDigits
                                    )
                ),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanDouble(
                        "forecastingDenominator",
                        SettingBag.DefaultForecastingDenominator)
                ).SetArgDisplayNames($"{nameof(settingBagExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SettingBag(
                                    forecastingDenominator: SettingBag.DefaultForecastingDenominator,
                                    folderPath: null,
                                    roundingDigits: SettingBag.DefaultRoundingDigits
                                    )),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
                ).SetArgDisplayNames($"{nameof(settingBagExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new SettingBag(
                                    forecastingDenominator: SettingBag.DefaultForecastingDenominator,
                                    folderPath: SettingBag.DefaultFolderPath,
                                    roundingDigits: 16
                                    )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("roundingDigits", "DefaultRoundingDigits")
                ).SetArgDisplayNames($"{nameof(settingBagExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(settingBagExceptionTestCases))]
        public void SettingBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SettingBag_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            SettingBag actual = new SettingBag();

            // Assert
            Assert.IsInstanceOf<SettingBag>(actual);
            Assert.IsInstanceOf<double>(actual.ForecastingDenominator);
            Assert.IsInstanceOf<string>(actual.FolderPath);
            Assert.IsInstanceOf<uint>(actual.RoundingDigits);

            Assert.IsInstanceOf<double>(SettingBag.DefaultForecastingDenominator);
            Assert.IsInstanceOf<string>(SettingBag.DefaultFolderPath);
            Assert.IsInstanceOf<uint>(SettingBag.DefaultRoundingDigits);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/