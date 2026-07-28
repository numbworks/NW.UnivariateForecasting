using System;
using System.IO;
using NW.Shared.Files;
using NW.Shared.Validation;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Forecasts;
using McMaster.Extensions.CommandLineUtils;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <inheritdoc cref="IArgumentManager"/>
    public class ArgumentManager : IArgumentManager
    {

        #region Fields

        private ComponentBag _componentBag { get; }

        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";

        public ForecastingDenominatorValidator ForecastingDenominatorValidator { get; }
        public RoundingDigitsValidator RoundingDigitsValidator { get; }
        public IComponentBagFactory ComponentBagFactory { get; }
        public ISettingBagFactory SettingBagFactory { get; }
        public IUnivariateForecasterFactory UnivariateForecasterFactory { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ArgumentManager"/> instance.</summary>
        public ArgumentManager(
            IComponentBagFactory componentBagFactory = null,
            ISettingBagFactory settingBagFactory = null,
            IUnivariateForecasterFactory univariateForecasterFactory = null)
        {

            ForecastingDenominatorValidator = new ForecastingDenominatorValidator();
            RoundingDigitsValidator = new RoundingDigitsValidator();
            ComponentBagFactory = componentBagFactory ?? new ComponentBagFactory();
            SettingBagFactory = settingBagFactory ?? new SettingBagFactory();
            UnivariateForecasterFactory = univariateForecasterFactory ?? new UnivariateForecasterFactory();

            _componentBag = ComponentBagFactory.Create();

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Create()
        {

            CommandLineApplication app = new CommandLineApplication
            {

                Name = CommandLineString.APPLICATION_NAME,
                Description = CommandLineString.APPLICATION_DESCRIPTION

            };

            app = AddRoot(app);
            app = AddSession(app);

            app.HelpOption(inherited: true);

            AddValidationErrorHandling(app);

            return app;

        }

        #endregion

        #region Methods_private

        private CommandLineApplication AddRoot(CommandLineApplication app)
        {

            app.OnExecute(() =>
            {

                int exitCode = ShowHeader();
                app.ShowHelp();

                return exitCode;

            });

            return app;

        }
        private CommandLineApplication AddSession(CommandLineApplication app)
        {

            app.Command(CommandLineString.COMMAND_SESSION_NAME, sessionCommand =>
            {

                sessionCommand = AddSessionMain(sessionCommand);
                sessionCommand = AddSessionForecast(sessionCommand);

            });

            return app;

        }
        private CommandLineApplication AddSessionMain(CommandLineApplication command)
        {

            command.Description = CommandLineString.COMMAND_SESSION_DESCR;
            command.OnExecute(() =>
            {

                int exitCode = ShowHeader();
                command.ShowHelp();

                return exitCode;

            });

            return command;

        }
        private CommandLineApplication AddSessionForecast(CommandLineApplication command)
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

                    ForecastData forecastData
                        = new ForecastData(
                                init: initOption.Value(),
                                folderPath: folderPathOption.Value(),
                                saveSession: saveSessionOption.HasValue(),
                                roundingDigits: RoundingDigitsValidator.ParseOrDefault(roundingDigitsOption.Value()),
                                forecastingDenominator: ForecastingDenominatorValidator.ParseOrDefault(forecastingDenominatorOption.Value())
                        );

                    return RunSessionForecast(forecastData);

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
                    .Accepts(validator => validator.Use(RoundingDigitsValidator));

        }
        private CommandOption CreateOptionalForecastingDenominatorOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_FORECASTINGDENOMINATOR_TEMPL,
                        CommandLineString.OPTION_FORECASTINGDENOMINATOR_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(ForecastingDenominatorValidator));

        }

        private void AddValidationErrorHandling(CommandLineApplication app)
        {

            app.OnValidationError(validationResult =>
            {

                ShowHeader();
                _componentBag.LoggingAction(ErrorMessageFormatter(validationResult.ErrorMessage));
                ShowFooter();

                return Failure;

            });

            foreach (CommandLineApplication command in app.Commands)
                AddValidationErrorHandling(command);

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
        private int LogAndReturnFailure(Exception e)
        {

            _componentBag.LoggingAction(ErrorMessageFormatter(e.Message));

            if (e.InnerException != null)
                _componentBag.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            ShowFooter();

            return Failure;

        }
        private int ShowHeader()
        {

            _componentBag.LoggingActionAsciiBanner(SeparatorLine);
            _componentBag.LoggingActionAsciiBanner(SeparatorLine);

            return Success;

        }
        private void ShowHeader(UnivariateForecaster univariateForecaster)
        {

            _componentBag.LoggingActionAsciiBanner(SeparatorLine);
            _componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void ShowFooter()
        {

            _componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }
        private int RunSessionForecast(ForecastData forecastData)
        {

            try
            {

                Validator.ValidateObject(forecastData, nameof(forecastData));

                forecastData = Defaultize(forecastData);

                SettingBag settingBag = SettingBagFactory.Create(forecastData);
                UnivariateForecaster univariateForecaster = UnivariateForecasterFactory.Create(settingBag, _componentBag);

                ShowHeader(univariateForecaster);

                string filePath = Path.Combine(forecastData.FolderPath, forecastData.Init);
                IFileInfoAdapter initFile = univariateForecaster.Convert(filePath);

                ForecastingInit init = univariateForecaster.LoadInitOrDefault(jsonFile: initFile);
                if (init == default(ForecastingInit))
                    throw new Exception(Messages.MessageCollection.LoadingFileNameReturnedDefault(initFile.Name));

                ForecastingSession session = univariateForecaster.Forecast(init: init);

                if (forecastData.SaveSession)
                    univariateForecaster.SaveSession(session, forecastData.FolderPath);

                ShowFooter();

                return Success;

            }
            catch (Exception e)
            {

                return LogAndReturnFailure(e);

            }

        }

        #endregion

    }
}
