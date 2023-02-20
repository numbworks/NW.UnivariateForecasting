using System;
using System.Collections.Generic;
using System.Reflection;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Forecasts;
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

        public double ForecastNextValue(List<double> values, double? C = null, double? E = null)
        {

            Validator.ValidateList(values, nameof(values));

            _components.LoggingAction(Forecasts.MessageCollection.ForecastNextValueRunningForProvidedValues(values));

            SlidingWindow slidingWindow = _components.SlidingWindowManager.Create(values);
            double nextValue = _components.ObservationManager.Create(slidingWindow, C, E).NextValue;

            _components.LoggingAction(Forecasts.MessageCollection.ForecastNextValueSuccessfullyRun(nextValue));

            return nextValue;

        }

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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.02.2023
*/
