using System;
using NW.Shared.Validation;
using NW.UnivariateForecasting.CLI.ArgumentParsing;
using NW.UnivariateForecasting.CLI.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace NW.UnivariateForecasting.CLI.ApplicationSession
{
    /// <inheritdoc cref="ISessionManager"/>
    public class SessionManager : ISessionManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;
        private SessionManagerBag _sessionManagerBag;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManager(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sessionManagerBag, nameof(sessionManagerBag));

            _libraryBroker = libraryBroker;
            _sessionManagerBag = sessionManagerBag;

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Add(CommandLineApplication app)
        {

            Validator.ValidateObject(app, nameof(app));

            app.Command(CommandLineString.COMMAND_SESSION_NAME, sessionCommand =>
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

            command.Description = CommandLineString.COMMAND_SESSION_DESCR;
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

            command.Command(CommandLineString.SUBCOMMAND_FORECAST_NAME, subCommand =>
            {

                subCommand.Description = CommandLineString.SUBCOMMAND_FORECAST_DESCR;

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
                                roundingDigits: _sessionManagerBag.RoundingDigitsValidator.ParseOrDefault(roundingDigitsOption.Value()),
                                forecastingDenominator: _sessionManagerBag.ForecastingDenominatorValidator.ParseOrDefault(forecastingDenominatorOption.Value())
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
                        CommandLineString.OPTION_INIT_TEMPL,
                        CommandLineString.OPTION_INIT_DESCR,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        CommandLineString.OPTION_INIT_ERRORMESSAGE);

            return result;

        }
        private CommandOption CreateOptionalFolderPathOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_FOLDERPATH_TEMPL,
                        CommandLineString.OPTION_FOLDERPATH_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.ExistingDirectory());

        }
        private CommandOption CreateOptionalSaveSessionOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_SAVESESSION_TEMPL,
                        CommandLineString.OPTION_SAVESESSION_DESCR,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateOptionalRoundingDigitsOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_ROUNDINGDIGITS_TEMPL,
                        CommandLineString.OPTION_ROUNDINGDIGITS_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerBag.RoundingDigitsValidator));

        }
        private CommandOption CreateOptionalForecastingDenominatorOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_FORECASTINGDENOMINATOR_TEMPL,
                        CommandLineString.OPTION_FORECASTINGDENOMINATOR_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerBag.ForecastingDenominatorValidator));

        }

        #endregion

    }
}