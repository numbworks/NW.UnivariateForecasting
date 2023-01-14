using System;
using System.Collections.Generic;
using System.IO;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.UnitTests.Utilities;
using NUnit.Framework;
using NW.UnivariateForecasting.AsciiBanner;

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
        private static TestCaseData[] forecastExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.Forecast(
                            SlidingWindows.ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(forecastExceptionTestCases)}_01")

        };
        private static TestCaseData[] extractXActualValuesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.ExtractXActualValues(
                            SlidingWindows.ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(extractXActualValuesExceptionTestCases)}_01")

        };
        private static TestCaseData[] extractStartDatesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.ExtractStartDates(
                            SlidingWindows.ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(extractStartDatesExceptionTestCases)}_01")

        };
        private static TestCaseData[] combineExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.Combine(
                            SlidingWindows.ObjectMother.SlidingWindow01,
                            Observations.ObjectMother.Observation_InvalidDueOfNullName
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid(typeof(Observation))
                ).SetArgDisplayNames($"{nameof(combineExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.Combine(
                            SlidingWindows.ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval,
                            Observations.ObjectMother.Observation01
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(combineExceptionTestCases)}_02")

        };
        private static TestCaseData[] forecastAndCombineExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.ForecastAndCombine(
                            SlidingWindows.ObjectMother.SlidingWindow_InvalidDueOfInvalidInterval,
                            1
                        )),
                typeof(ArgumentException),
               UnivariateForecasting.Observations.MessageCollection.ProvidedTypeObjectNotValid(typeof(SlidingWindow))
                ).SetArgDisplayNames($"{nameof(forecastAndCombineExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => Forecasts.ObjectMother.UnivariateForecaster.ForecastAndCombine(
                            SlidingWindows.ObjectMother.SlidingWindow01,
                            0
                        )),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanOne("steps")
                ).SetArgDisplayNames($"{nameof(forecastAndCombineExceptionTestCases)}_02")

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
        private static TestCaseData[] forecastTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                null,
                null,
                Observations.ObjectMother.Observation01,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01)
                    }
                ).SetArgDisplayNames($"{nameof(forecastTestCases)}_01"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                Observations.ObjectMother.Observation01.C,
                Observations.ObjectMother.Observation01.E,
                Observations.ObjectMother.Observation01,
                new List<string>() {
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01)
                    }
                ).SetArgDisplayNames($"{nameof(forecastTestCases)}_02")

        };
        private static TestCaseData[] extractXActualValuesTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                SlidingWindows.ObjectMother.SlidingWindow01_Values,
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.ExtractingValuesOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Forecasts.MessageCollection.ValuesHaveBeenSuccessfullyExtracted(SlidingWindows.ObjectMother.SlidingWindow01_Values)
                    }
                ).SetArgDisplayNames($"{nameof(extractXActualValuesTestCases)}_01")

        };
        private static TestCaseData[] extractStartDatesTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                SlidingWindows.ObjectMother.SlidingWindow01_StartDates,
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.ExtractingStartDatesOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Forecasts.MessageCollection.StartDatesHaveBeenSuccessfullyExtracted(SlidingWindows.ObjectMother.SlidingWindow01_StartDates)
                    }
                ).SetArgDisplayNames($"{nameof(extractStartDatesTestCases)}_01")

        };
        private static TestCaseData[] combineTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                Observations.ObjectMother.Observation01,
                Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final,
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedSlidingWindowIs(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationIs(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final)
                    }
                ).SetArgDisplayNames($"{nameof(combineTestCases)}_01")

        };
        private static TestCaseData[] forecastAndCombineTestCases =
        {

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                (uint)1,
                null,
                null,
                Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final,
                new List<Observation>()
                {
                    Observations.ObjectMother.Observation01
                },
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.RunningForecastAndCombineForSteps(1),
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastingAndCombineForStepNr(1),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.Forecasts.MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedSlidingWindowIs(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationIs(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final),
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastAndCombineSuccessfullyRunForSteps(1),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final)
                    }
                ).SetArgDisplayNames($"{nameof(forecastAndCombineTestCases)}_01"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                (uint)1,
                Observations.ObjectMother.Observation01.C,
                Observations.ObjectMother.Observation01.E,
                Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final,
                new List<Observation>()
                {
                    Observations.ObjectMother.Observation01
                },
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.RunningForecastAndCombineForSteps(1),
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastingAndCombineForStepNr(1),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.Forecasts.MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedSlidingWindowIs(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationIs(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final),
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastAndCombineSuccessfullyRunForSteps(1),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_Final)
                    }
                ).SetArgDisplayNames($"{nameof(forecastAndCombineTestCases)}_02"),

            new TestCaseData(
                SlidingWindows.ObjectMother.SlidingWindow01,
                (uint)3,
                null,
                null,
                Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_Final,
                new List<Observation>()
                {
                    Observations.ObjectMother.Observation01,
                    Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_01_Observation,
                    Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_02_Observation
                },
                new List<string>() {
                    UnivariateForecasting.Forecasts.MessageCollection.RunningForecastAndCombineForSteps(3),
                    // i = 1
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastingAndCombineForStepNr(1),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.Forecasts.MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedSlidingWindowIs(SlidingWindows.ObjectMother.SlidingWindow01),
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationIs(Observations.ObjectMother.Observation01),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_01),
                    // i = 2
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastingAndCombineForStepNr(2),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_01),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_01_Observation),
                    UnivariateForecasting.Forecasts.MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedSlidingWindowIs(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_01),
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationIs(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps1_01_Observation),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_02),
                    // i = 3
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastingAndCombineForStepNr(3),
                    UnivariateForecasting.Observations.MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_02),
                    UnivariateForecasting.Observations.MessageCollection.FollowingObservationHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_02_Observation),
                    UnivariateForecasting.Forecasts.MessageCollection.CombiningProvidedSlidingWindowWithObservation,
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedSlidingWindowIs(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_02),
                    UnivariateForecasting.Forecasts.MessageCollection.ProvidedObservationIs(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_02_Observation),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_Final),
                    // final
                    UnivariateForecasting.Forecasts.MessageCollection.ForecastAndCombineSuccessfullyRunForSteps(3),
                    UnivariateForecasting.SlidingWindows.MessageCollection.FollowingSlidingWindowHasBeenCreated(Forecasts.ObjectMother.SlidingWindow_ForecastAndCombineSteps3_Final)
                    }
                ).SetArgDisplayNames($"{nameof(forecastAndCombineTestCases)}_03")

        };
        private static TestCaseData[] forecastNextValueTestCases =
        {

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

        };
        private static TestCaseData[] saveSlidingWindowAsJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                null,
                                Files.ObjectMother.FileInfoAdapterDoesntExist)),
                typeof(ArgumentNullException),
                new ArgumentNullException("slidingWindow").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                SlidingWindows.ObjectMother.SlidingWindow01,
                                (IFileInfoAdapter)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfoAdapter").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                SlidingWindows.ObjectMother.SlidingWindow01,
                                (FileInfo)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfo").Message
                ).SetArgDisplayNames($"{nameof(saveSlidingWindowAsJsonExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveSlidingWindowAsJson(
                                SlidingWindows.ObjectMother.SlidingWindow01,
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
                                Files.ObjectMother.FileInfoAdapterDoesntExist)),
                typeof(ArgumentNullException),
                new ArgumentNullException("observation").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                Observations.ObjectMother.Observation01,
                                (IFileInfoAdapter)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfoAdapter").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                Observations.ObjectMother.Observation01,
                                (FileInfo)null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfo").Message
                ).SetArgDisplayNames($"{nameof(saveObservationAsJsonExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                    () => new UnivariateForecaster()
                            .SaveObservationAsJson(
                                Observations.ObjectMother.Observation01,
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
                                Files.ObjectMother.FileInfoAdapterDoesntExist)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.ProvidedPathDoesntExist(Files.ObjectMother.FileInfoAdapterDoesntExist)
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
                                Files.ObjectMother.FileInfoAdapterDoesntExist)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.ProvidedPathDoesntExist(Files.ObjectMother.FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(loadObservationFromJsonExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(univariateForecasterExceptionTestCases))]
        public void UnivariateForecaster_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastExceptionTestCases))]
        public void Forecast_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(extractXActualValuesExceptionTestCases))]
        public void ExtractXActualValues_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(extractStartDatesExceptionTestCases))]
        public void ExtractStartDates_ShouldThrowAnException_WhenInvalidSlidingWindow
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(combineExceptionTestCases))]
        public void Combine_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastAndCombineExceptionTestCases))]
        public void ForecastAndCombine_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(forecastNextValueExceptionTestCases))]
        public void ForecastNextValue_ShouldThrowAnException_WhenValues
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            Observation actual = univariateForecaster.Forecast(slidingWindow, C, E);

            // Assert
            Assert.True(
                    Observations.ObjectMother.AreEqual(expected, actual));
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
                        loggingAction: (message) => fakeLogger.Log(message),
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
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
                        loggingAction: (message) => fakeLogger.Log(message),
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
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
                        idCreationFunction: Forecasts.ObjectMother.ForecastAndCombine_IdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: (message) => fakeLogger.Log(message),
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            SlidingWindow actual = univariateForecaster.Combine(slidingWindow, observation);

            // Assert
            Assert.True(
                    SlidingWindows.ObjectMother.AreEqual(expected, actual));
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
                        idCreationFunction: Forecasts.ObjectMother.ForecastAndCombine_IdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);

            // Act
            List<Observation> actualObservations = null;
            SlidingWindow actual = univariateForecaster.ForecastAndCombine(slidingWindow, steps, out actualObservations, C, E);

            // Assert
            Assert.True(
                    SlidingWindows.ObjectMother.AreEqual(expected, actual));
            Assert.True(
                    Observations.ObjectMother.AreEqual(expectedObservations, actualObservations));
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
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
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
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(saveObservationAsJsonExceptionTestCases))]
        public void SaveObservationAsJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(loadSlidingWindowFromJsonExceptionTestCases))]
        public void LoadSlidingWindowFromJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(loadObservationFromJsonExceptionTestCases))]
        public void LoadObservationFromJson_ShouldThrowAnException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
                = new FileManager(Forecasts.ObjectMother.FakeFileAdapter_ReadAllTextReturnsSlidingWindowWithDummyValues);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: fakeFileManager,
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.DeserializingProvidedFileAsSlidingWindowObject(Files.ObjectMother.FileInfoAdapterExists),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedFileHasBeenSuccessfullyDeserialized

            };

            // Act
            SlidingWindow actual 
                = univariateForecaster.LoadSlidingWindowFromJson(Files.ObjectMother.FileInfoAdapterExists);

            // Assert
            Assert.IsTrue(
                    SlidingWindows.ObjectMother.AreEqual(
                        SlidingWindows.ObjectMother.SlidingWindow01_WithDefaultDummyFields,
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
                = new FileManager(Forecasts.ObjectMother.FakeFileAdapter_ReadAllTextReturnsObservationWithDummyValues);
            UnivariateForecastingComponents components
                = new UnivariateForecastingComponents(
                        slidingWindowManager: slidingManager,
                        slidingWindowItemManager: new SlidingWindowItemManager(),
                        observationManager: observationManager,
                        intervalManager: new IntervalManager(),
                        fileManager: fakeFileManager,
                        idCreationFunction: UnivariateForecastingComponents.DefaultIdCreationFunction,
                        roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.DeserializingProvidedFileAsObservationObject(Files.ObjectMother.FileInfoAdapterExists),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedFileHasBeenSuccessfullyDeserialized

            };

            // Act
            Observation actual
                = univariateForecaster.LoadObservationFromJson(Files.ObjectMother.FileInfoAdapterExists);

            // Assert
            Assert.IsTrue(
                    Observations.ObjectMother.AreEqual(
                        Observations.ObjectMother.Observation01_WithDefaultDummyFields,
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
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.SerializingProvidedSlidingWindowAsJsonAndSavingItTo(Files.ObjectMother.FileInfoAdapterExists),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedObjectHasBeenSuccessfullySavedAsJson

            };

            // Act
            univariateForecaster.SaveSlidingWindowAsJson(
                                    SlidingWindows.ObjectMother.SlidingWindow01_WithDefaultDummyFields,
                                    Files.ObjectMother.FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(
                    Properties.Resources.SlidingWindowWithDummyValues,
                    fakeFileAdapter.ReadAllText(Files.ObjectMother.FileInfoAdapterExists.FullName)); // whatever argument will work
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
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: UnivariateForecastingComponents.DefaultLoggingActionAsciiBanner,
                        asciiBannerManager: new AsciiBannerManager());
            UnivariateForecaster univariateForecaster
                = new UnivariateForecaster(
                        new UnivariateForecastingSettings(),
                        components);
            List<string> expectedMessages = new List<string>()
            {

                UnivariateForecasting.Forecasts.MessageCollection.SerializingProvidedObservationAsJsonAndSavingItTo(Files.ObjectMother.FileInfoAdapterExists),
                UnivariateForecasting.Forecasts.MessageCollection.ProvidedObjectHasBeenSuccessfullySavedAsJson

            };

            // Act
            univariateForecaster.SaveObservationAsJson(
                                    Observations.ObjectMother.Observation01_WithDefaultDummyFields,
                                    Files.ObjectMother.FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(
                    Properties.Resources.ObservationWithDummyValues,
                    fakeFileAdapter.ReadAllText(Files.ObjectMother.FileInfoAdapterExists.FullName)); // whatever argument will work
            Assert.AreEqual(expectedMessages, fakeLogger.Messages);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.01.2023
*/