using System;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <inheritdoc cref="ILibraryBroker"/>
    public class LibraryBroker : ILibraryBroker
    {

        #region Fields

        private ITextClassifierComponentsFactory _componentsFactory { get; }
        private ITextClassifierSettingsFactory _settingsFactory { get; }
        private ITextClassifierFactory _univariateForecasterFactory { get; }

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

            UnivariateForecastingComponents components = _componentsFactory.Create();
            UnivariateForecastingSettings settings = _settingsFactory.Create();
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(components, settings);

            ShowHeader(components, univariateForecaster);

            return Success;

        }
        public int RunAboutMain()
        {

            UnivariateForecastingComponents components = _componentsFactory.Create();
            UnivariateForecastingSettings settings = _settingsFactory.Create();
            UnivariateForecaster univariateForecaster = _univariateForecasterFactory.Create(components, settings);

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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/