using System;
using NW.UnivariateForecasting.Validation;
using NW.UnivariateForecastingClient.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace NW.UnivariateForecastingClient.ApplicationSession
{
    /// <inheritdoc cref="ISessionManager"/>
    public class SessionManager : ISessionManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;
        private DependencyBag _dependencyBag;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManager(ILibraryBroker libraryBroker, DependencyBag dependencyBag)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(dependencyBag, nameof(dependencyBag));

            _libraryBroker = libraryBroker;
            _dependencyBag = dependencyBag;

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Add(CommandLineApplication app)
        {

            Validator.ValidateObject(app, nameof(app));

            app.Command(Shared.MessageCollection.Session_Name, sessionCommand =>
            {

                sessionCommand = AddMain(sessionCommand);
                sessionCommand = AddForecast(sessionCommand);

            });

            return app;

        }

        #endregion

        #region Methods_private

        private CommandLineApplication AddMain(CommandLineApplication command)
        {

            command.Description = Shared.MessageCollection.Session_Description;
            command.OnExecute(() =>
            {

                int exitCode = _libraryBroker.ShowHeader();
                command.ShowHelp();

                return exitCode;

            });

            return command;

        }
        private CommandLineApplication AddForecast(CommandLineApplication command)
        {

            command.Command(Shared.MessageCollection.Session_Forecast_Name, subCommand =>
            {

                subCommand.Description = Shared.MessageCollection.Session_Forecast_Description;

                CommandOption initOption = CreateRequiredInitOption(subCommand);
                CommandOption folderPathOption = CreateOptionalFolderPathOption(subCommand);
                CommandOption saveSessionOption = CreateOptionalSaveSessionOption(subCommand);
                CommandOption roundingDigitsOption = CreateOptionalRoundingDigitsOption(subCommand);
                CommandOption forecastingDenominatorOption = CreateOptionalForecastingDenominatorOption(subCommand);

                subCommand.OnExecute(() =>
                {

                    ForecastData classifyData
                        = new ForecastData(
                                init: initOption.Value(),
                                folderPath: folderPathOption.Value(),
                                saveSession: saveSessionOption.HasValue(),
                                roundingDigits: _dependencyBag.RoundingDigitsValidator.ParseOrDefault(roundingDigitsOption.Value()),
                                forecastingDenominator: _dependencyBag.ForecastingDenominatorValidator.ParseOrDefault(forecastingDenominatorOption.Value())
                        );

                    return _libraryBroker.RunSessionForecast(classifyData);

                });

            });

            return command;

        }

        private CommandOption CreateRequiredInitOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_Init_Template,
                        Shared.MessageCollection.Session_Option_Init_Description,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        Shared.MessageCollection.Session_Option_Init_ErrorMessage);

            return result;

        }
        private CommandOption CreateOptionalFolderPathOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_FolderPath_Template,
                        Shared.MessageCollection.Session_Option_FolderPath_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.ExistingDirectory());

        }
        private CommandOption CreateOptionalSaveSessionOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_SaveSession_Template,
                        Shared.MessageCollection.Session_Option_SaveSession_Description,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateOptionalRoundingDigitsOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_RoundingDigits_Template,
                        Shared.MessageCollection.Session_Option_RoundingDigits_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_dependencyBag.RoundingDigitsValidator));

        }
        private CommandOption CreateOptionalForecastingDenominatorOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_ForecastingDenominator_Template,
                        Shared.MessageCollection.Session_Option_ForecastingDenominator_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_dependencyBag.ForecastingDenominatorValidator));

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/