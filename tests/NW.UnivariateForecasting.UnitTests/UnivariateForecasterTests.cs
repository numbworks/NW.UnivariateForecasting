using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.Serializations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecasterTests
    {

        #region Fields

        private static TestCaseData[] univariateForecasterExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            settings: null,
                            components: new UnivariateForecastingComponents())),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ).SetArgDisplayNames($"{nameof(univariateForecasterExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster(
                            settings: new UnivariateForecastingSettings(),
                            components: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("components").Message
                ).SetArgDisplayNames($"{nameof(univariateForecasterExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastNextValueExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.ForecastNextValue(null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(forecastNextValueExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.ForecastNextValue(new List<double>() { })),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableContainsZeroItems("values")
                ).SetArgDisplayNames($"{nameof(forecastNextValueExceptionTestCases)}_02")

        };
        
        private static TestCaseData[] forecastNextValueTestCases =
        {
            /*

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01_Values,
                null,
                null,
                Observations.ObjectMother.Observation01_WithDefaultDummyFields.Y_Forecasted,
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastNextValueRunningForProvidedValues(SlidingWindows.ObjectMother.SlidingWindow01_Values),
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedValuesAre(SlidingWindows.ObjectMother.SlidingWindow01_Values),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedStepsAre(new UnivariateForecastingSettings().DummySteps),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIntervalUnitsIs(new UnivariateForecastingSettings().DummyIntervalUnit),
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIdIs(new UnivariateForecastingSettings().DummyId),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedObservationNameIs(new UnivariateForecastingSettings().DummyObservationName),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIntervalIs(SlidingWindows.ObjectMother.SlidingWindow01_DummyInterval),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedItemsCountIs(SlidingWindows.ObjectMother.SlidingWindow01_DefaultDummyItems),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(SlidingWindows.ObjectMother.SlidingWindow01_WithDefaultDummyFields),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01_WithDefaultDummyFields),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01_WithDefaultDummyFields),
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastNextValueSuccessfullyRun(Observations.ObjectMother.Observation01.Y_Forecasted)
                    }
                ).SetArgDisplayNames($"{nameof(forecastNextValueTestCases)}_01"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01_Values,
                Observations.ObjectMother.Observation01_WithDefaultDummyFields.C,
                Observations.ObjectMother.Observation01_WithDefaultDummyFields.E,
                Observations.ObjectMother.Observation01_WithDefaultDummyFields.Y_Forecasted,
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastNextValueRunningForProvidedValues(SlidingWindows.ObjectMother.SlidingWindow01_Values),
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingIntervalOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedValuesAre(SlidingWindows.ObjectMother.SlidingWindow01_Values),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedStepsAre(new UnivariateForecastingSettings().DummySteps),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIntervalUnitsIs(new UnivariateForecastingSettings().DummyIntervalUnit),
                    UnivariateForecasting.SlidingWindows.MessageCollection.CreatingSlidingWindowOutOfFollowingArguments,
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIdIs(new UnivariateForecastingSettings().DummyId),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedObservationNameIs(new UnivariateForecastingSettings().DummyObservationName),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedIntervalIs(SlidingWindows.ObjectMother.SlidingWindow01_DummyInterval),
                    UnivariateForecasting.SlidingWindows.MessageCollection.ProvidedItemsCountIs(SlidingWindows.ObjectMother.SlidingWindow01_DefaultDummyItems),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(SlidingWindows.ObjectMother.SlidingWindow01_WithDefaultDummyFields),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01_WithDefaultDummyFields),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01_WithDefaultDummyFields),
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastNextValueSuccessfullyRun(Observations.ObjectMother.Observation01.Y_Forecasted)
                    }
                ).SetArgDisplayNames($"{nameof(forecastNextValueTestCases)}_02")

            */

        };
        
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void UnivariateForecaster_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            UnivariateForecaster actual = new UnivariateForecaster();

            // Assert
            Assert.IsInstanceOf<UnivariateForecaster>(actual);

            Assert.IsInstanceOf<string>(actual.AsciiBanner);
            Assert.IsInstanceOf<string>(actual.Version);

        }

        [TestCaseSource(nameof(univariateForecasterExceptionTestCases))]
        public void UnivariateForecaster_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Ignore("")]
        [TestCaseSource(nameof(forecastNextValueTestCases))]
        public void ForecastNextValue_ShouldReturnExpectedValueAndLogExpectedMessages_WhenProperValues
            (List<double> values, double? C, double? E, double expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        observationManager: observationManager,
                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            double actual = univariateForecaster.ForecastNextValue(values, C, E);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void LogAsciiBanner_ShouldLogAsExpected_WhenInvoked()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingActionAsciiBanner = (message) => actualLogMessages.Add(message);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FileManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: UnivariateForecastingComponents.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory()
                    );
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            List<string> expectedMessages = new List<string>()
            {

                new AsciiBannerManager().Create(univariateForecaster.Version)

            };

            // Act            
            univariateForecaster.LogAsciiBanner();

            // Assert
            Assert.AreEqual(expectedMessages, actualLogMessages);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/