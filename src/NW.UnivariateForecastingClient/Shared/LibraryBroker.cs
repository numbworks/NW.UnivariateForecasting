using System;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="ILibraryBroker"/>
    public class LibraryBroker : ILibraryBroker
    {

        #region Fields

        private IUnivariateForecastingComponentsFactory _componentsFactory { get; }
        private IUnivariateForecastingSettingsFactory _settingsFactory { get; }
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
                IUnivariateForecastingComponentsFactory componentsFactory, 
                IUnivariateForecastingSettingsFactory settingsFactory, 
                IUnivariateForecasterFactory univariateForecasterFactory
            )
        {

            Validator.ValidateObject(componentsFactory, nameof(componentsFactory));
            Validator.ValidateObject(settingsFactory, nameof(settingsFactory));
            Validator.ValidateObject(univariateForecasterFactory, nameof(univariateForecasterFactory));

            _componentsFactory = componentsFactory;
            _settingsFactory = settingsFactory;
            _univariateForecasterFactory = univariateForecasterFactory;

        }

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance using default parameters.</summary>
        public LibraryBroker()
            : this(
                  new UnivariateForecastingComponentsFactory(), 
                  new UnivariateForecastingSettingsFactory(), 
                  new UnivariateForecasterFactory()
                  ) { }

        #endregion

        #region Methods_public

        public int ShowHeader()
        {

            UnivariateForecastingSettings settings = _settingsFactory.Create();
            UnivariateForecastingComponents components = _componentsFactory.Create();
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(settings, components);

            ShowHeader(components, univariateForecaster);

            return Success;

        }
        public int RunAboutMain()
        {

            UnivariateForecastingComponents components = _componentsFactory.Create();
            UnivariateForecastingSettings settings = _settingsFactory.Create();
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(settings, components);

            ShowHeader(components, univariateForecaster);

            components.LoggingActionAsciiBanner(Shared.MessageCollection.Application_Description);
            components.LoggingActionAsciiBanner(SeparatorLine);

            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Author);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Email);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Url);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_License);

            ShowFooter(components);

            return Success;

        }
        public int RunSessionForecast(ForecastData forecastData)
        {

            Validator.ValidateObject(forecastData, nameof(forecastData));

            forecastData = Defaultize(forecastData);

            UnivariateForecastingComponents components = _componentsFactory.Create();
            UnivariateForecastingSettings settings = _settingsFactory.Create(forecastData);
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(settings, components);


            // ...

            throw new Exception();

        }


        #endregion

        #region Methods_private

        private int LogAndReturnFailure(Exception e)
        {

            UnivariateForecastingComponents components = _componentsFactory.Create();

            components.LoggingAction(ErrorMessageFormatter(e.Message));
            if (e.InnerException != null)
                components.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            ShowFooter(components);

            return Failure;

        }
        private void ShowHeader(UnivariateForecastingComponents components, UnivariateForecaster univariateForecaster)
        {

            components.LoggingActionAsciiBanner(SeparatorLine);
            components.LoggingActionAsciiBanner(univariateForecaster.AsciiBanner);
            components.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void ShowFooter(UnivariateForecastingComponents components)
        {

            components.LoggingActionAsciiBanner(SeparatorLine);

        }

        private ForecastData Defaultize(ForecastData forecastData)
        {

            ForecastData updated = new ForecastData(
                    init: forecastData.Init,
                    folderPath: forecastData.FolderPath ?? UnivariateForecastingSettings.DefaultFolderPath,
                    saveSession: forecastData.SaveSession,
                    roundingDigits: forecastData.RoundingDigits ?? UnivariateForecastingSettings.DefaultRoundingDigits,
                    forecastingDenominator: forecastData.ForecastingDenominator ?? UnivariateForecastingSettings.DefaultForecastingDenominator
                );

            return updated;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/