using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NUnit.Framework;
using NW.UnivariateForecasting.Bags;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    [TestFixture]
    public class ObservationManagerTests
    {

        #region Fields

        private static TestCaseData[] observationManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            roundingFunction: null,
                            loggingAction: ComponentBag.DefaultLoggingAction
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager(
                            roundingFunction: ComponentBag.DefaultRoundingFunction,
                            loggingAction: null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(observationManagerExceptionTestCases)}_02")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager().Create(
                                slidingWindow: null,
                                forecastingDenominator: SettingBag.DefaultForecastingDenominator,
                                roundingDigits: SettingBag.DefaultRoundingDigits
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindow").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager().Create(
                                slidingWindow: SlidingWindows.ObjectMother.SlidingWindow01,
                                forecastingDenominator: 0,
                                roundingDigits: SettingBag.DefaultRoundingDigits
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanDouble(
                        "forecastingDenominator",
                        SettingBag.DefaultForecastingDenominator)
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new ObservationManager().Create(
                                slidingWindow: SlidingWindows.ObjectMother.SlidingWindow01,
                                forecastingDenominator: SettingBag.DefaultForecastingDenominator,
                                roundingDigits: 16
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("roundingDigits", "DefaultRoundingDigits")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_03")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                SettingBag.DefaultForecastingDenominator,
                (uint)2,
                null,
                null,
                ObjectMother.Observation01_WithoutInitCE,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(ObjectMother.Observation01_WithoutInitCE)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                SettingBag.DefaultForecastingDenominator,
                (uint)2,
                ObjectMother.Observation01_WithInitCE.Coefficient,
                ObjectMother.Observation01_WithInitCE.Error,
                ObjectMother.Observation01_WithInitCE,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(ObjectMother.Observation01_WithInitCE)
                    }
                ).SetArgDisplayNames($"{nameof(createTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(observationManagerExceptionTestCases))]
        public void ObservationManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow(
                SlidingWindow slidingWindow, 
                double forecastingDenominator, 
                uint roundingDigits, 
                double? coefficient, 
                double? error, 
                Observation expected, 
                List<string> expectedMessages
            )
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            ObservationManager observationManager 
                = new ObservationManager(
                            roundingFunction: ComponentBag.DefaultRoundingFunction,
                            loggingAction: (message) => fakeLogger.Log(message)
                        );

            // Act
            Observation actual 
                = observationManager.Create(
                        slidingWindow: slidingWindow,
                        forecastingDenominator: forecastingDenominator,
                        roundingDigits: roundingDigits,
                        coefficient: coefficient,
                        error: error
                    );

            // Assert
            Assert.That(
                ObjectMother.AreEqual(expected, actual),
                Is.True);
            Assert.That(fakeLogger.Messages, Is.EqualTo(expectedMessages));

        }

        [Test]
        public void ObservationManager_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            ObservationManager actual = new ObservationManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<ObservationManager>());

            Assert.That(ObservationManager.DefaultForecastingDenominator, Is.InstanceOf<double>());
            Assert.That(ObservationManager.DefaultRoundingDigits, Is.InstanceOf<uint>());
            Assert.That(ObservationManager.DefaultRoundingFunction, Is.InstanceOf<Func<double, uint, double>>());
            Assert.That(ObservationManager.DefaultLoggingAction, Is.InstanceOf<Action<string>>());

        }

        [Test]
        public void DivideXByY_ShouldUseYAsDenominator_WhenYEqualsToZero()
        {

            // Arrange
            SlidingWindowItem slidingWindowItem = new SlidingWindowItem(id: 1, X_Actual: 583.23, Y_Forecasted: 0);
            double denominator = SettingBag.DefaultForecastingDenominator;
            double expected = 583.23 / denominator;
            uint roundingDigits = 2;

            // Act
            double actual = Utilities.ObjectMother.CallPrivateMethod<ObservationManager, double>(
                    obj: ObjectMother.ObservationManager_WithTwoRoundingDigits,
                    methodName: "DivideXByY",
                    args: new object[] { slidingWindowItem, denominator, roundingDigits }
                );

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

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