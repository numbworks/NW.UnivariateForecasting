using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NW.UnivariateForecasting.Bags;
using NW.Shared.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.Serializations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.Shared.Validation;
using NW.Shared.Files.Validation;

namespace NW.UnivariateForecasting
{
    /// <inheritdoc cref="IUnivariateForecaster"/>
    public class UnivariateForecaster : IUnivariateForecaster
    {

        #region Fields

        private SettingBag _settingBag;
        private ComponentBag _componentBag;

        #endregion

        #region Properties

        public string Version { get; }
        public string AsciiBanner { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="UnivariateForecaster"/>.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public UnivariateForecaster(SettingBag settingBag, ComponentBag componentBag)
        {

            Validator.ValidateObject(settingBag, nameof(settingBag));
            Validator.ValidateObject(componentBag, nameof(componentBag));

            _settingBag = settingBag;
            _componentBag = componentBag;

            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AsciiBanner = _componentBag.AsciiBannerManager.Create(Version);

        }

        /// <summary>Initializes an instance of <see cref="UnivariateForecaster"/> using default values.</summary>
        public UnivariateForecaster()
            : this (
                  new SettingBag(), 
                  new ComponentBag()) { }

        #endregion

        #region Methods_public

        public void LogAsciiBanner()
            => _componentBag.LoggingActionAsciiBanner(AsciiBanner);
        public IFileInfoAdapter Convert(string filePath)
            => _componentBag.FileManager.Create(filePath);

        public ForecastingSession Forecast(ForecastingInit init)
        {

            Validator.ValidateObject(init, nameof(init));
            Validator.ThrowIfLessThan(init.Values.Count, 2, nameof(init.Values));

            _componentBag.LoggingAction(Forecasts.MessageCollection.AttemptingToForecast);
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedFolderPathIs(_settingBag.FolderPath));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedForecastingDenominatorIs(_settingBag.ForecastingDenominator));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedRoundingDigitsAre(_settingBag.RoundingDigits));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedObservationNameIs(init.ObservationName));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedValuesAre(init.Values.Count));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedCoefficientIs(init.Coefficient));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedErrorIs(init.Error));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ProvidedStepsAre(init.Steps));

            _componentBag.LoggingAction(Forecasts.MessageCollection.ProcessingStepNr(1));

            Observation observation = CreateObservation(init);
            List<Observation> observations = Convert(observation);

            _componentBag.LoggingAction(Forecasts.MessageCollection.ObservationCoefficientIs(observations.Last().Coefficient));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ObservationErrorIs(observations.Last().Error));
            _componentBag.LoggingAction(Forecasts.MessageCollection.ObservationNextValueIs(observations.Last().NextValue));

            if (init.Steps > 1)
            {

                ForecastingInit nextInit = DuplicateInit(init);
                double nextValue = observations.Last().NextValue;

                for (uint step = 2; step <= init.Steps; step++)
                {

                    _componentBag.LoggingAction(Forecasts.MessageCollection.ProcessingStepNr(step));

                    nextInit = _componentBag.ForecastingInitManager.ExpandValues(nextInit, nextValue);
                    Observation nextObservation = CreateObservation(nextInit);
                    nextValue = nextObservation.NextValue;

                    _componentBag.LoggingAction(Forecasts.MessageCollection.ObservationNextValueIs(nextValue));

                    observations.Add(nextObservation);

                }

            }

            _componentBag.LoggingAction(Forecasts.MessageCollection.ForecastSuccessfullyCompleted);

            ForecastingSession session = new ForecastingSession(
                    init: init,
                    observations: observations,
                    version: Version
                );

            return session;

        }

        public ForecastingInit LoadInitOrDefault(IFileInfoAdapter jsonFile)
            => LoadOrDefault<ForecastingInit>(jsonFile);
        public void SaveSession(ForecastingSession session, string folderPath)
            => Save(obj: session, jsonFile: Create<ForecastingSession>(folderPath: folderPath, now: _componentBag.NowFunction()));

        #endregion

        #region Methods_private

        private T LoadOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            FilesValidator.ValidateFileExistance(jsonFile);

            _componentBag.LoggingAction(Forecasts.MessageCollection.AttemptingToLoadObjectFrom(typeof(T), jsonFile));

            string content = _componentBag.FileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _componentBag.SerializerFactory.Create<T>();
            T obj = serializer.DeserializeOrDefault(content);

            if (EqualityComparer<T>.Default.Equals(obj, default(T)))
                _componentBag.LoggingAction(Forecasts.MessageCollection.ObjectFailedToLoad(typeof(T)));
            else
                _componentBag.LoggingAction(Forecasts.MessageCollection.ObjectSuccessfullyLoaded(typeof(T)));

            return obj;

        }
        private void Save<T>(T obj, IFileInfoAdapter jsonFile)
        {

            _componentBag.LoggingAction(Forecasts.MessageCollection.AttemptingToSaveObjectAs(typeof(T), jsonFile));

            try
            {

                ISerializer<T> serializer = _componentBag.SerializerFactory.Create<T>();
                string content = serializer.Serialize(obj);

                _componentBag.FileManager.WriteAllText(jsonFile, content);

                _componentBag.LoggingAction(Forecasts.MessageCollection.ObjectSuccessfullySaved(typeof(T)));

            }
            catch
            {

                _componentBag.LoggingAction(Forecasts.MessageCollection.ObjectFailedToSave(typeof(T)));

            }

        }
        private IFileInfoAdapter Create<T>(string folderPath, DateTime now)
        {

            string filePath;

            if (typeof(T) == typeof(ForecastingSession))
                filePath = _componentBag.FilenameFactory.CreateForSessionJson(folderPath: folderPath, now: now);
            else
                throw new Exception(Forecasts.MessageCollection.ThereIsNoStrategyOutOfType(typeof(T)));

            IFileInfoAdapter jsonFile = new FileInfoAdapter(fileName: filePath);

            return jsonFile;

        }

        private Observation CreateObservation(ForecastingInit init)
        {

            SlidingWindow slidingWindow = _componentBag.SlidingWindowManager.Create(init.Values, _settingBag.RoundingDigits);
            Observation observation 
                = _componentBag.ObservationManager.Create(
                        slidingWindow: slidingWindow,
                        forecastingDenominator: _settingBag.ForecastingDenominator,
                        roundingDigits: _settingBag.RoundingDigits,
                        coefficient: init.Coefficient, 
                        error: init.Error);

            return observation;

        }
        private List<Observation> Convert(Observation observation)
            => new List<Observation>() { observation };
        private ForecastingInit DuplicateInit(ForecastingInit init)
        {

            ForecastingInit duplicated = new ForecastingInit(
                observationName: init.ObservationName,
                values: init.Values,
                coefficient: init.Coefficient,
                error: init.Error,
                steps: init.Steps
                );

            return duplicated;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/
