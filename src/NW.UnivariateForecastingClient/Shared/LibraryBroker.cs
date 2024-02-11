using System;
using System.IO;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Bags;
using NW.Shared.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.Shared.Validation;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="ILibraryBroker"/>
    public class LibraryBroker : ILibraryBroker
    {

        #region Fields

        private IComponentBagFactory _componentBagFactory { get; }
        private ISettingBagFactory _settingBagFactory { get; }
        private IUnivariateForecasterFactory _univariateForecasterFactory { get; }

        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public LibraryBroker(
                IComponentBagFactory componentBagFactory, 
                ISettingBagFactory settingBagFactory, 
                IUnivariateForecasterFactory univariateForecasterFactory
            )
        {

            Validator.ValidateObject(componentBagFactory, nameof(componentBagFactory));
            Validator.ValidateObject(settingBagFactory, nameof(settingBagFactory));
            Validator.ValidateObject(univariateForecasterFactory, nameof(univariateForecasterFactory));

            _componentBagFactory = componentBagFactory;
            _settingBagFactory = settingBagFactory;
            _univariateForecasterFactory = univariateForecasterFactory;

        }

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance using default parameters.</summary>
        public LibraryBroker()
            : this(
                  new ComponentBagFactory(), 
                  new SettingBagFactory(), 
                  new UnivariateForecasterFactory()
                  ) { }

        #endregion

        #region Methods_public

        public int ShowHeader()
        {

            SettingBag settingBag = _settingBagFactory.Create();
            ComponentBag componentBag = _componentBagFactory.Create();
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(settingBag, componentBag);

            ShowHeader(componentBag, univariateForecaster);

            return Success;

        }
        public int RunAboutMain()
        {

            ComponentBag componentBag = _componentBagFactory.Create();
            SettingBag settingBag = _settingBagFactory.Create();
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(settingBag, componentBag);

            ShowHeader(componentBag, univariateForecaster);

            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.Application_Description);
            componentBag.LoggingActionAsciiBanner(SeparatorLine);

            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Author);
            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Email);
            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Url);
            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_License);

            ShowFooter(componentBag);

            return Success;

        }
        public int RunSessionForecast(ForecastData forecastData)
        {

            try
            {

                Validator.ValidateObject(forecastData, nameof(forecastData));

                forecastData = Defaultize(forecastData);

                ComponentBag componentBag = _componentBagFactory.Create();
                SettingBag settingBag = _settingBagFactory.Create(forecastData);
                UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(settingBag, componentBag);

                ShowHeader(componentBag, univariateForecaster);

                string filePath = Path.Combine(forecastData.FolderPath, forecastData.Init);
                IFileInfoAdapter initFile = univariateForecaster.Convert(filePath);

                ForecastingInit init = univariateForecaster.LoadInitOrDefault(jsonFile: initFile);
                if (init == default(ForecastingInit))
                    throw new Exception(MessageCollection.LoadingFileNameReturnedDefault(initFile.Name));

                ForecastingSession session = univariateForecaster.Forecast(init: init);

                if (forecastData.SaveSession)
                    univariateForecaster.SaveSession(session, forecastData.FolderPath);

                ShowFooter(componentBag);

                return Success;

            }
            catch (Exception e)
            {

                return LogAndReturnFailure(e);

            }

        }

        #endregion

        #region Methods_private

        private int LogAndReturnFailure(Exception e)
        {

            ComponentBag componentBag = _componentBagFactory.Create();

            componentBag.LoggingAction(ErrorMessageFormatter(e.Message));
            if (e.InnerException != null)
                componentBag.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            ShowFooter(componentBag);

            return Failure;

        }
        private void ShowHeader(ComponentBag componentBag, UnivariateForecaster univariateForecaster)
        {

            componentBag.LoggingActionAsciiBanner(SeparatorLine);
            componentBag.LoggingActionAsciiBanner(univariateForecaster.AsciiBanner);
            componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void ShowFooter(ComponentBag componentBag)
        {

            componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }

        private ForecastData Defaultize(ForecastData forecastData)
        {

            ForecastData updated = new ForecastData(
                    init: forecastData.Init,
                    folderPath: forecastData.FolderPath ?? SettingBag.DefaultFolderPath,
                    saveSession: forecastData.SaveSession,
                    roundingDigits: forecastData.RoundingDigits ?? SettingBag.DefaultRoundingDigits,
                    forecastingDenominator: forecastData.ForecastingDenominator ?? SettingBag.DefaultForecastingDenominator
                );

            return updated;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/