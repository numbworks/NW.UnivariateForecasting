using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.Serializations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting
{
    /// <inheritdoc cref="IUnivariateForecaster"/>
    public class UnivariateForecaster : IUnivariateForecaster
    {

        #region Fields

        private UnivariateForecastingSettings _settings;
        private UnivariateForecastingComponents _components;

        #endregion

        #region Properties

        public string Version { get; }
        public string AsciiBanner { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="UnivariateForecaster"/>.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            UnivariateForecastingComponents components)
        {

            Validator.ValidateObject(settings, nameof(settings));
            Validator.ValidateObject(components, nameof(components));

            _settings = settings;
            _components = components;

            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AsciiBanner = _components.AsciiBannerManager.Create(Version);

        }

        /// <summary>Initializes an instance of <see cref="UnivariateForecaster"/> using default values.</summary>
        public UnivariateForecaster()
            : this (
                  new UnivariateForecastingSettings(), 
                  new UnivariateForecastingComponents()) { }

        #endregion

        #region Methods_public

        public void LogAsciiBanner()
            => _components.LoggingActionAsciiBanner(AsciiBanner);
        public IFileInfoAdapter Convert(string filePath)
            => _components.FileManager.Create(filePath);

        public ForecastingSession Forecast(ForecastingInit init)
        {

            Validator.ValidateObject(init, nameof(init));
            Validator.ThrowIfLessThan(init.Values.Count, 2, nameof(init.Values));

            _components.LoggingAction(Forecasts.MessageCollection.AttemptingToForecast);
            _components.LoggingAction(Forecasts.MessageCollection.ProvidedObservationNameIs(init.ObservationName));
            _components.LoggingAction(Forecasts.MessageCollection.ProvidedValuesAre(init.Values.Count));
            _components.LoggingAction(Forecasts.MessageCollection.ProvidedCoefficientIs(init.Coefficient));
            _components.LoggingAction(Forecasts.MessageCollection.ProvidedErrorIs(init.Error));
            _components.LoggingAction(Forecasts.MessageCollection.ProvidedStepsAre(init.Steps));

            _components.LoggingAction(Forecasts.MessageCollection.ProcessingStepNr(1));

            Observation observation = CreateObservation(init);
            List<Observation> observations = Convert(observation);

            _components.LoggingAction(Forecasts.MessageCollection.ObservationCoefficientIs(observations.Last().Coefficient));
            _components.LoggingAction(Forecasts.MessageCollection.ObservationErrorIs(observations.Last().Error));
            _components.LoggingAction(Forecasts.MessageCollection.ObservationNextValueIs(observations.Last().NextValue));

            if (init.Steps > 1)
            {

                ForecastingInit nextInit = DuplicateInit(init);
                double nextValue = observations.Last().NextValue;

                for (uint step = 2; step <= init.Steps; step++)
                {

                    _components.LoggingAction(Forecasts.MessageCollection.ProcessingStepNr(step));

                    nextInit = _components.ForecastingInitManager.ExpandValues(nextInit, nextValue);
                    Observation nextObservation = CreateObservation(nextInit);
                    nextValue = nextObservation.NextValue;

                    _components.LoggingAction(Forecasts.MessageCollection.ObservationNextValueIs(nextValue));

                    observations.Add(nextObservation);

                }

            }

            _components.LoggingAction(Forecasts.MessageCollection.ForecastSuccessfullyCompleted);

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
            => Save(obj: session, jsonFile: Create<ForecastingSession>(folderPath: folderPath, now: _components.NowFunction()));

        #endregion

        #region Methods_private

        private T LoadOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            Validator.ValidateFileExistance(jsonFile);

            _components.LoggingAction(Forecasts.MessageCollection.AttemptingToLoadObjectFrom(typeof(T), jsonFile));

            string content = _components.FileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _components.SerializerFactory.Create<T>();
            T obj = serializer.DeserializeOrDefault(content);

            if (EqualityComparer<T>.Default.Equals(obj, default(T)))
                _components.LoggingAction(Forecasts.MessageCollection.ObjectFailedToLoad(typeof(T)));
            else
                _components.LoggingAction(Forecasts.MessageCollection.ObjectSuccessfullyLoaded(typeof(T)));

            return obj;

        }
        private void Save<T>(T obj, IFileInfoAdapter jsonFile)
        {

            _components.LoggingAction(Forecasts.MessageCollection.AttemptingToSaveObjectAs(typeof(T), jsonFile));

            try
            {

                ISerializer<T> serializer = _components.SerializerFactory.Create<T>();
                string content = serializer.Serialize(obj);

                _components.FileManager.WriteAllText(jsonFile, content);

                _components.LoggingAction(Forecasts.MessageCollection.ObjectSuccessfullySaved(typeof(T)));

            }
            catch
            {

                _components.LoggingAction(Forecasts.MessageCollection.ObjectFailedToSave(typeof(T)));

            }

        }
        private IFileInfoAdapter Create<T>(string folderPath, DateTime now)
        {

            string filePath;

            if (typeof(T) == typeof(ForecastingSession))
                filePath = _components.FilenameFactory.CreateForSessionJson(folderPath: folderPath, now: now);
            else
                throw new Exception(Forecasts.MessageCollection.ThereIsNoStrategyOutOfType(typeof(T)));

            IFileInfoAdapter jsonFile = new FileInfoAdapter(fileName: filePath);

            return jsonFile;

        }

        private Observation CreateObservation(ForecastingInit init)
        {

            SlidingWindow slidingWindow = _components.SlidingWindowManager.Create(init.Values, _settings.RoundingDigits);
            Observation observation 
                = _components.ObservationManager.Create(
                        slidingWindow: slidingWindow,
                        forecastingDenominator: _settings.ForecastingDenominator,
                        roundingDigits: _settings.RoundingDigits,
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
    Last Update: 08.03.2023
*/
