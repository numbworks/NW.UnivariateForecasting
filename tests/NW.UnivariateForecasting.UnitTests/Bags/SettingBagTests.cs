using System;
using NW.UnivariateForecasting.Bags;
using NW.Shared.Validation;
using NUnit.Framework;

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
                Shared.Validation.MessageCollection.VariableCantBeLessThanDouble(
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
                Shared.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("roundingDigits", "DefaultRoundingDigits")
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
            Assert.That(actual, Is.InstanceOf<SettingBag>());
            Assert.That(actual.ForecastingDenominator, Is.InstanceOf<double>());
            Assert.That(actual.FolderPath, Is.InstanceOf<string>());
            Assert.That(actual.RoundingDigits, Is.InstanceOf<uint>());

            Assert.That(SettingBag.DefaultForecastingDenominator, Is.InstanceOf<double>());
            Assert.That(SettingBag.DefaultFolderPath, Is.InstanceOf<string>());
            Assert.That(SettingBag.DefaultRoundingDigits, Is.InstanceOf<uint>());

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