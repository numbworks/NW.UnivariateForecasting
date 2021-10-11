using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Messages;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class UnivariateForecasterTests
    {

        // Fields
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
        private static TestCaseData[] forecastExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.Forecast(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(ArgumentException),
                MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(forecastExceptionTestCases)}_01")

        };
        private static TestCaseData[] extractXActualValuesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ExtractXActualValues(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(ArgumentException),
                MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(extractXActualValuesExceptionTestCases)}_01")

        };
        private static TestCaseData[] extractStartDatesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ExtractStartDates(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(ArgumentException),
                MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(extractStartDatesExceptionTestCases)}_01")

        };
        private static TestCaseData[] combineExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.Combine(
                            ObjectMother.SlidingWindow1,
                            ObjectMother.Observation_InvalidDueOfNullName
                        )),
                typeof(ArgumentException),
                MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(Observation))
                ).SetArgDisplayNames($"{nameof(combineExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.Combine(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval,
                            ObjectMother.Observation1
                        )),
                typeof(ArgumentException),
                MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(combineExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastAndCombineExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastAndCombine(
                            ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval,
                            1
                        )),
                typeof(ArgumentException),
                MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(forecastAndCombineExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastAndCombine(
                            ObjectMother.SlidingWindow1,
                            0
                        )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableCantBeLessThanOne.Invoke("steps")
                ).SetArgDisplayNames($"{nameof(forecastAndCombineExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastNextValueExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastNextValue(null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("values").Message
                ).SetArgDisplayNames($"{nameof(forecastNextValueExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => ObjectMother.UnivariateForecaster_Default.ForecastNextValue(new List<double>() { })),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("values")
                ).SetArgDisplayNames($"{nameof(forecastNextValueExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                null,
                null,
                ObjectMother.Observation1,
                new List<string>() {
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1)
                    }
                ).SetArgDisplayNames($"{nameof(forecastTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.Observation1.C,
                ObjectMother.Observation1.E,
                ObjectMother.Observation1,
                new List<string>() {
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1)
                    }
                ).SetArgDisplayNames($"{nameof(forecastTestCases)}_02")

        };
        private static TestCaseData[] extractXActualValuesTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.SlidingWindow1_Values,
                new List<string>() {
                    MessageCollection.UnivariateForecaster_ExtractingValuesOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.UnivariateForecaster_ValuesHaveBeenSuccessfullyExtracted.Invoke(ObjectMother.SlidingWindow1_Values)
                    }
                ).SetArgDisplayNames($"{nameof(extractXActualValuesTestCases)}_01")

        };
        private static TestCaseData[] extractStartDatesTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.SlidingWindow1_StartDates,
                new List<string>() {
                    MessageCollection.UnivariateForecaster_ExtractingStartDatesOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.UnivariateForecaster_StartDatesHaveBeenSuccessfullyExtracted.Invoke(ObjectMother.SlidingWindow1_StartDates)
                    }
                ).SetArgDisplayNames($"{nameof(extractStartDatesTestCases)}_01")

        };
        private static TestCaseData[] combineTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                ObjectMother.Observation1,
                ObjectMother.FaCSteps1_Final,
                new List<string>() {
                    MessageCollection.UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.UnivariateForecaster_ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.UnivariateForecaster_ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final)
                    }
                ).SetArgDisplayNames($"{nameof(combineTestCases)}_01")

        };
        private static TestCaseData[] forecastAndCombineTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                (uint)1,
                null,
                null,
                ObjectMother.FaCSteps1_Final,
                new List<Observation>()
                {
                    ObjectMother.Observation1
                },
                new List<string>() {
                    MessageCollection.UnivariateForecaster_RunningForecastAndCombineForSteps.Invoke(1),
                    MessageCollection.UnivariateForecaster_ForecastingAndCombineForStepNr.Invoke(1),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1),
                    MessageCollection.UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.UnivariateForecaster_ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.UnivariateForecaster_ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final),
                    MessageCollection.UnivariateForecaster_ForecastAndCombineSuccessfullyRunForSteps.Invoke(1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final)
                    }
                ).SetArgDisplayNames($"{nameof(forecastAndCombineTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                (uint)1,
                ObjectMother.Observation1.C,
                ObjectMother.Observation1.E,
                ObjectMother.FaCSteps1_Final,
                new List<Observation>()
                {
                    ObjectMother.Observation1
                },
                new List<string>() {
                    MessageCollection.UnivariateForecaster_RunningForecastAndCombineForSteps.Invoke(1),
                    MessageCollection.UnivariateForecaster_ForecastingAndCombineForStepNr.Invoke(1),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1),
                    MessageCollection.UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.UnivariateForecaster_ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.UnivariateForecaster_ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final),
                    MessageCollection.UnivariateForecaster_ForecastAndCombineSuccessfullyRunForSteps.Invoke(1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps1_Final)
                    }
                ).SetArgDisplayNames($"{nameof(forecastAndCombineTestCases)}_02"),

            new TestCaseData(
                ObjectMother.SlidingWindow1,
                (uint)3,
                null,
                null,
                ObjectMother.FaCSteps3_Final,
                new List<Observation>()
                {
                    ObjectMother.Observation1,
                    ObjectMother.FaCSteps3_MidwayObservation_1,
                    ObjectMother.FaCSteps3_MidwayObservation_2
                },
                new List<string>() {
                    MessageCollection.UnivariateForecaster_RunningForecastAndCombineForSteps.Invoke(3),
                    // i = 1
                    MessageCollection.UnivariateForecaster_ForecastingAndCombineForStepNr.Invoke(1),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1),
                    MessageCollection.UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.UnivariateForecaster_ProvidedSlidingWindowIs.Invoke(ObjectMother.SlidingWindow1),
                    MessageCollection.UnivariateForecaster_ProvidedObservationIs.Invoke(ObjectMother.Observation1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_1),
                    // i = 2
                    MessageCollection.UnivariateForecaster_ForecastingAndCombineForStepNr.Invoke(2),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_1),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwayObservation_1),
                    MessageCollection.UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.UnivariateForecaster_ProvidedSlidingWindowIs.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_1),
                    MessageCollection.UnivariateForecaster_ProvidedObservationIs.Invoke(ObjectMother.FaCSteps3_MidwayObservation_1),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_2),
                    // i = 3
                    MessageCollection.UnivariateForecaster_ForecastingAndCombineForStepNr.Invoke(3),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_2),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.FaCSteps3_MidwayObservation_2),
                    MessageCollection.UnivariateForecaster_CombiningProvidedSlidingWindowWithObservation,
                    MessageCollection.UnivariateForecaster_ProvidedSlidingWindowIs.Invoke(ObjectMother.FaCSteps3_MidwaySlidingWindow_2),
                    MessageCollection.UnivariateForecaster_ProvidedObservationIs.Invoke(ObjectMother.FaCSteps3_MidwayObservation_2),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_Final),
                    // final
                    MessageCollection.UnivariateForecaster_ForecastAndCombineSuccessfullyRunForSteps.Invoke(3),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.FaCSteps3_Final)
                    }
                ).SetArgDisplayNames($"{nameof(forecastAndCombineTestCases)}_03")

        };
        private static TestCaseData[] forecastNextValueTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1_Values,
                null,
                null,
                ObjectMother.Observation1_WithDefaultDummyFields.Y_Forecasted,
                new List<string>() {
                    MessageCollection.UnivariateForecaster_ForecastNextValueRunningForProvidedValues.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.SlidingWindowManager_CreatingIntervalOutOfFollowingArguments,
                    MessageCollection.SlidingWindowManager_ProvidedValuesAre.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.SlidingWindowManager_ProvidedStepsAre.Invoke(new UnivariateForecastingSettings().DummySteps),
                    MessageCollection.SlidingWindowManager_ProvidedIntervalUnitsIs.Invoke(new UnivariateForecastingSettings().DummyIntervalUnit),
                    MessageCollection.SlidingWindowManager_CreatingSlidingWindowOutOfFollowingArguments,
                    MessageCollection.SlidingWindowManager_ProvidedIdIs.Invoke(new UnivariateForecastingSettings().DummyId),
                    MessageCollection.SlidingWindowManager_ProvidedObservationNameIs.Invoke(new UnivariateForecastingSettings().DummyObservationName),
                    MessageCollection.SlidingWindowManager_ProvidedIntervalIs.Invoke(ObjectMother.SlidingWindow1_DummyInterval),
                    MessageCollection.SlidingWindowManager_ProvidedItemsCountIs.Invoke(ObjectMother.SlidingWindow1_DefaultDummyItems),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.SlidingWindow1_WithDefaultDummyFields),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1_WithDefaultDummyFields),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1_WithDefaultDummyFields),
                    MessageCollection.UnivariateForecaster_ForecastNextValueSuccessfullyRun.Invoke(ObjectMother.Observation1.Y_Forecasted)
                    }
                ).SetArgDisplayNames($"{nameof(forecastNextValueTestCases)}_01"),

            new TestCaseData(
                ObjectMother.SlidingWindow1_Values,
                ObjectMother.Observation1_WithDefaultDummyFields.C,
                ObjectMother.Observation1_WithDefaultDummyFields.E,
                ObjectMother.Observation1_WithDefaultDummyFields.Y_Forecasted,
                new List<string>() {
                    MessageCollection.UnivariateForecaster_ForecastNextValueRunningForProvidedValues.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.SlidingWindowManager_CreatingIntervalOutOfFollowingArguments,
                    MessageCollection.SlidingWindowManager_ProvidedValuesAre.Invoke(ObjectMother.SlidingWindow1_Values),
                    MessageCollection.SlidingWindowManager_ProvidedStepsAre.Invoke(new UnivariateForecastingSettings().DummySteps),
                    MessageCollection.SlidingWindowManager_ProvidedIntervalUnitsIs.Invoke(new UnivariateForecastingSettings().DummyIntervalUnit),
                    MessageCollection.SlidingWindowManager_CreatingSlidingWindowOutOfFollowingArguments,
                    MessageCollection.SlidingWindowManager_ProvidedIdIs.Invoke(new UnivariateForecastingSettings().DummyId),
                    MessageCollection.SlidingWindowManager_ProvidedObservationNameIs.Invoke(new UnivariateForecastingSettings().DummyObservationName),
                    MessageCollection.SlidingWindowManager_ProvidedIntervalIs.Invoke(ObjectMother.SlidingWindow1_DummyInterval),
                    MessageCollection.SlidingWindowManager_ProvidedItemsCountIs.Invoke(ObjectMother.SlidingWindow1_DefaultDummyItems),
                    MessageCollection.SlidingWindowManager_FollowingSlidingWindowHasBeenCreated.Invoke(ObjectMother.SlidingWindow1_WithDefaultDummyFields),
                    MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(ObjectMother.SlidingWindow1_WithDefaultDummyFields),
                    MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(ObjectMother.Observation1_WithDefaultDummyFields),
                    MessageCollection.UnivariateForecaster_ForecastNextValueSuccessfullyRun.Invoke(ObjectMother.Observation1.Y_Forecasted)
                    }
                ).SetArgDisplayNames($"{nameof(forecastNextValueTestCases)}_02")

        };
        private static TestCaseData[] saveSlidingWindowAsJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                null,
                                ObjectMother.FileManager_FileInfoAdapterDoesntExist)),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindow").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                ObjectMother.SlidingWindow1,
                                (IFileInfoAdapter)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfoAdapter").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                ObjectMother.SlidingWindow1,
                                (FileInfo)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfo").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                ObjectMother.SlidingWindow1,
                                (string)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("filePath").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_04")

        };
        private static TestCaseData[] saveObservationAsJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                null,
                                ObjectMother.FileManager_FileInfoAdapterDoesntExist)),
                typeof(ArgumentNullException),
                new ArgumentNullException("observation").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                ObjectMother.Observation1,
                                (IFileInfoAdapter)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfoAdapter").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                ObjectMother.Observation1,
                                (FileInfo)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfo").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                ObjectMother.Observation1,
                                (string)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("filePath").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_04")

        };
        private static TestCaseData[] loadSlidingWindowFromJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .LoadSlidingWindowFromJson(
                                (IFileInfoAdapter)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfoAdapter").Message
                ).SetArgDisplayNames($"{nameof(loadSlidingWindowFromJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .LoadSlidingWindowFromJson(
                                ObjectMother.FileManager_FileInfoAdapterDoesntExist)),
                typeof(ArgumentException),
                MessageCollection.Validator_ProvidedPathDoesntExist.Invoke(ObjectMother.FileManager_FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(loadSlidingWindowFromJsonExceptionTestCases)}_02")

        };
        private static TestCaseData[] loadObservationFromJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .LoadObservationFromJson(
                                (IFileInfoAdapter)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfoAdapter").Message
                ).SetArgDisplayNames($"{nameof(loadObservationFromJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .LoadObservationFromJson(
                                ObjectMother.FileManager_FileInfoAdapterDoesntExist)),
                typeof(ArgumentException),
                MessageCollection.Validator_ProvidedPathDoesntExist.Invoke(ObjectMother.FileManager_FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(loadObservationFromJsonExceptionTestCases)}_02")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(univariateForecasterExceptionTestCases))]
        public void UnivariateForecaster_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastExceptionTestCases))]
        public void Forecast_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(extractXActualValuesExceptionTestCases))]
        public void ExtractXActualValues_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(extractStartDatesExceptionTestCases))]
        public void ExtractStartDates_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(combineExceptionTestCases))]
        public void Combine_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastAndCombineExceptionTestCases))]
        public void ForecastAndCombine_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastNextValueExceptionTestCases))]
        public void ForecastNextValue_ShouldThrowAnException_WhenValues
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastTestCases))]
        public void Forecast_ShouldReturnExpectedObservationAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, double? C, double? E, Observation expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: new FileManager(),
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            Observation actual = univariateForecaster.Forecast(slidingWindow, C, E);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(extractXActualValuesTestCases))]
        public void ExtractXActualValues_ShouldReturnExpectedValuesAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, List<double> expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: new ObservationManager(),
                        intervalManager: new IntervalManager(),
                        fileManager: new FileManager(),
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            List<double> actual = univariateForecaster.ExtractXActualValues(slidingWindow);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(extractStartDatesTestCases))]
        public void ExtractStartDates_ShouldReturnExpectedDatesAndLogExpectedMessages_WhenProperSlidingWindow
            (SlidingWindow slidingWindow, List<DateTime> expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: new ObservationManager(),
                        intervalManager: new IntervalManager(),
                        fileManager: new FileManager(),
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            List<DateTime> actual = univariateForecaster.ExtractStartDates(slidingWindow);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(combineTestCases))]
        public void Combine_ShouldReturnExpectedSlidingWindowAndLogExpectedMessages_WhenProperArguments
            (SlidingWindow slidingWindow, Observation observation, SlidingWindow expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: new SlidingWindowManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: new ObservationManager(),
                        intervalManager: new IntervalManager(),
                        fileManager: new FileManager(),
                        idCreationFunction: ObjectMother.FaC_IdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: (message) => fakeLogger.Log(message));
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            SlidingWindow actual = univariateForecaster.Combine(slidingWindow, observation);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(forecastAndCombineTestCases))]
        public void ForecastAndCombine_ShouldReturnExpectedObjectsAndLogExpectedMessages_WhenProperArguments
            (SlidingWindow slidingWindow,
            uint steps,
            double? C,
            double? E,
            SlidingWindow expected,
            List<Observation> expectedObservations,
            List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: new FileManager(),
                        idCreationFunction: ObjectMother.FaC_IdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            List<Observation> actualObservations = null;
            SlidingWindow actual = univariateForecaster.ForecastAndCombine(slidingWindow, steps, out actualObservations, C, E);

            // Assert
            Assert.True(
                ObjectMother.AreEqual(expected, actual));
            Assert.True(
                ObjectMother.AreEqual(expectedObservations, actualObservations));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [TestCaseSource(nameof(forecastNextValueTestCases))]
        public void ForecastNextValue_ShouldReturnExpectedValueAndLogExpectedMessages_WhenProperValues
            (List<double> values, double? C, double? E, double expected, List<string> expectedMessages)
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: new FileManager(),
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
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

        [TestCaseSource(nameof(saveSlidingWindowAsJsonExceptionTestCases))]
        public void SaveSlidingWindowAsJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(saveObservationAsJsonExceptionTestCases))]
        public void SaveObservationAsJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(loadSlidingWindowFromJsonExceptionTestCases))]
        public void LoadSlidingWindowFromJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(loadObservationFromJsonExceptionTestCases))]
        public void LoadObservationFromJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadSlidingWindowFromJson_ShouldReturnExpectedSlidingWindow_WhenProperJSONFile()
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            IFileManager fakeFileManager 
                = new FileManager(ObjectMother.FileAdapter_ReadAllTextReturnsSlidingWindowWithDummyValues);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: fakeFileManager,
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                MessageCollection.UnivariateForecaster_DeserializingProvidedFileAsSlidingWindowObject.Invoke(ObjectMother.FileManager_FileInfoAdapterExists),
                MessageCollection.UnivariateForecaster_ProvidedFileHasBeenSuccessfullyDeserialized

            };

            // Act
            SlidingWindow actual 
                = univariateForecaster.LoadSlidingWindowFromJson(ObjectMother.FileManager_FileInfoAdapterExists);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.SlidingWindow1_WithDefaultDummyFields,
                        actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void LoadObservationFromJson_ShouldReturnExpectedObservation_WhenProperJSONFile()
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            IFileManager fakeFileManager
                = new FileManager(ObjectMother.FileAdapter_ReadAllTextReturnsObservationWithDummyValues);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: fakeFileManager,
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                MessageCollection.UnivariateForecaster_DeserializingProvidedFileAsObservationObject.Invoke(ObjectMother.FileManager_FileInfoAdapterExists),
                MessageCollection.UnivariateForecaster_ProvidedFileHasBeenSuccessfullyDeserialized

            };

            // Act
            Observation actual
                = univariateForecaster.LoadObservationFromJson(ObjectMother.FileManager_FileInfoAdapterExists);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.Observation1_WithDefaultDummyFields,
                        actual));
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void SaveSlidingWindowAsJson_ShouldSaveExpectedJSONFile_WhenProperSlidingWindow()
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            IFileAdapter fakeFileAdapter = new FakeFileAdapterWithInternalMemory();
            IFileManager fakeFileManager = new FileManager(fakeFileAdapter);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: fakeFileManager,
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                MessageCollection.UnivariateForecaster_SerializingProvidedSlidingWindowAsJsonAndSavingItTo.Invoke(ObjectMother.FileManager_FileInfoAdapterExists),
                MessageCollection.UnivariateForecaster_ProvidedObjectHasBeenSuccessfullySavedAsJson

            };

            // Act
            univariateForecaster.SaveSlidingWindowAsJson(
                                    ObjectMother.SlidingWindow1_WithDefaultDummyFields,
                                    ObjectMother.FileManager_FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(
                    Properties.Resources.SlidingWindowWithDummyValues,
                    fakeFileAdapter.ReadAllText(ObjectMother.FileManager_FileInfoAdapterExists.FullName)); // whatever argument will work
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        [Test]
        public void SaveObservationAsJson_ShouldSaveExpectedJSONFile_WhenProperObservation()
        {

            // Arrange
            FakeLogger fakeLogger = new FakeLogger();
            Action<string> fakeLoggingAction = (message) => fakeLogger.Log(message);
            SlidingWindowManager slidingManager
                = new SlidingWindowManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            ObservationManager observationManager
                = new ObservationManager(
                        settings: new UnivariateForecastingSettings(),
                        intervalManager: new IntervalManager(),
                        slidingWindowManager: slidingManager,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction
                    );
            IFileAdapter fakeFileAdapter = new FakeFileAdapterWithInternalMemory();
            IFileManager fakeFileManager = new FileManager(fakeFileAdapter);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: fakeFileManager,
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction);
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                MessageCollection.UnivariateForecaster_SerializingProvidedObservationAsJsonAndSavingItTo.Invoke(ObjectMother.FileManager_FileInfoAdapterExists),
                MessageCollection.UnivariateForecaster_ProvidedObjectHasBeenSuccessfullySavedAsJson

            };

            // Act
            univariateForecaster.SaveObservationAsJson(
                                    ObjectMother.Observation1_WithDefaultDummyFields,
                                    ObjectMother.FileManager_FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(
                    Properties.Resources.ObservationWithDummyValues,
                    fakeFileAdapter.ReadAllText(ObjectMother.FileManager_FileInfoAdapterExists.FullName)); // whatever argument will work
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.12.2020

*/
