using System;
using System.IO;
using NW.Shared.Files;
using NW.Shared.Validation;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.CLI.AsciiBanners;
using NW.UnivariateForecasting.CLI.TerminalWindows;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using McMaster.Extensions.CommandLineUtils.HelpText;

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

        public IOptionValidator ForecastingDenominatorValidator { get; }
        public IOptionValidator RoundingDigitsValidator { get; }
        public IAsciiBannerManager AsciiBannerManager { get; }
        public ITerminalWindowManager TerminalWindowManager { get; }
        public IUnivariateForecasterFactory UnivariateForecasterFactory { get; }
        public IComponentBagFactory ComponentBagFactory { get; }
        public ISettingBagFactory SettingBagFactory { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ArgumentManager"/> instance.</summary>
        public ArgumentManager(
            IOptionValidator forecastingDenominatorValidator = null,
            IOptionValidator roundingDigitsValidator = null,
            IAsciiBannerManager asciiBannerManager = null,
            ITerminalWindowManager terminalWindowManager = null,
            IUnivariateForecasterFactory univariateForecasterFactory = null,
            IComponentBagFactory componentBagFactory = null,
            ISettingBagFactory settingBagFactory = null)
        {

            ForecastingDenominatorValidator = forecastingDenominatorValidator ?? new ForecastingDenominatorValidator();
            RoundingDigitsValidator = roundingDigitsValidator ?? new RoundingDigitsValidator();
            AsciiBannerManager = asciiBannerManager ?? new AsciiBannerManager();
            TerminalWindowManager = terminalWindowManager ?? new TerminalWindowManager();
            UnivariateForecasterFactory = univariateForecasterFactory ?? new UnivariateForecasterFactory();
            ComponentBagFactory = componentBagFactory ?? new ComponentBagFactory();
            SettingBagFactory = settingBagFactory ?? new SettingBagFactory();

            _componentBag = ComponentBagFactory.Create();

        }

        #endregion

        #region Methods (public)

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

            AddCustomHelpTextGenerator(app);
            AddValidationErrorHandling(app);

            return app;

        }

        #endregion

        #region Methods (private)

        private uint? ParseRoundingDigitsOrDefault(string value)
        {

            try
            {

                if (value == null)
                    return OptionValidatorValue.RoundingDigitsDefaultValue;

                return uint.Parse(value);

            }
            catch
            {

                return OptionValidatorValue.RoundingDigitsDefaultValue;

            }

        }
        private double? ParseForecastingDenominatorOrDefault(string value)
        {

            if (value == null)
                return OptionValidatorValue.ForecastingDenominatorDefaultValue;

            return double.Parse(value);

        }

        private CommandLineApplication AddRoot(CommandLineApplication app)
        {

            app.OnExecute(() =>
            {

                app.ShowHelp();

                return Success;

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

                command.ShowHelp();

                return Success;

            });

            return command;

        }
        private CommandLineApplication AddSessionForecast(CommandLineApplication command)
        {

            command.Command(CommandLineString.SUBCOMMAND_FORECAST_NAME, subCommand =>
            {

                subCommand.Description = CommandLineString.SUBCOMMAND_FORECAST_DESCR;

                CommandOption initOption = CreateInitOption(subCommand);
                CommandOption folderPathOption = CreateFolderPathOption(subCommand);
                CommandOption saveSessionOption = CreateSaveSessionOption(subCommand);
                CommandOption roundingDigitsOption = CreateRoundingDigitsOption(subCommand);
                CommandOption forecastingDenominatorOption = CreateForecastingDenominatorOption(subCommand);

                subCommand.OnExecute(() =>
                {

                    ForecastData forecastData
                        = new ForecastData(
                                init: initOption.Value(),
                                folderPath: folderPathOption.Value(),
                                saveSession: saveSessionOption.HasValue(),
                                roundingDigits: ParseRoundingDigitsOrDefault(roundingDigitsOption.Value()),
                                forecastingDenominator: ParseForecastingDenominatorOrDefault(forecastingDenominatorOption.Value())
                        );

                    return RunSessionForecast(forecastData);

                });

            });

            return command;

        }

        private CommandOption CreateInitOption(CommandLineApplication subCommand)
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
        private CommandOption CreateFolderPathOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_FOLDERPATH_TEMPL,
                        CommandLineString.OPTION_FOLDERPATH_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.ExistingDirectory());

        }
        private CommandOption CreateSaveSessionOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_SAVESESSION_TEMPL,
                        CommandLineString.OPTION_SAVESESSION_DESCR,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateRoundingDigitsOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_ROUNDINGDIGITS_TEMPL,
                        CommandLineString.OPTION_ROUNDINGDIGITS_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(RoundingDigitsValidator));

        }
        private CommandOption CreateForecastingDenominatorOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_FORECASTINGDENOMINATOR_TEMPL,
                        CommandLineString.OPTION_FORECASTINGDENOMINATOR_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(ForecastingDenominatorValidator));

        }

        private string CreateAsciiBanner()
        {

            string version = _componentBag.VersionFunction();
            uint terminalWidth = TerminalWindowManager.GetOrCutoff();
            string asciiBanner = AsciiBannerManager.Create(version, terminalWidth);

            return asciiBanner;
        
        }
        private void LogAsciiBanner()
        {

            string asciiBanner = CreateAsciiBanner();

            _componentBag.LoggingActionAsciiBanner(SeparatorLine);
            _componentBag.LoggingActionAsciiBanner(asciiBanner);
            _componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void LogFooter()
            => _componentBag.LoggingActionAsciiBanner(SeparatorLine);
        private int LogAndReturnFailure(Exception e)
        {

            _componentBag.LoggingAction(ErrorMessageFormatter(e.Message));

            if (e.InnerException != null)
                _componentBag.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            LogFooter();

            return Failure;

        }

        private void AddCustomHelpTextGenerator(CommandLineApplication command, IHelpTextGenerator helpTextGenerator)
        {

            command.HelpTextGenerator = helpTextGenerator;
            
            foreach (CommandLineApplication subCommand in command.Commands)
                AddCustomHelpTextGenerator(subCommand, helpTextGenerator);

        }
        private void AddCustomHelpTextGenerator(CommandLineApplication app)
        {

            CustomHelpTextGenerator customHelpTextGenerator = new CustomHelpTextGenerator(CreateAsciiBanner);
            app.HelpTextGenerator = customHelpTextGenerator;

            foreach (CommandLineApplication command in app.Commands)
                AddCustomHelpTextGenerator(command, customHelpTextGenerator);
        
        }   
        private void AddValidationErrorHandling(CommandLineApplication app)
        {

            app.OnValidationError(validationResult =>
            {

                ShowHeader();
                _componentBag.LoggingAction(ErrorMessageFormatter(validationResult.ErrorMessage));
                LogFooter();

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
        
        private int ShowHeader()
        {
            
            LogAsciiBanner();

            return Success;

        }
        private int RunSessionForecast(ForecastData forecastData)
        {

            try
            {

                Validator.ValidateObject(forecastData, nameof(forecastData));

                forecastData = Defaultize(forecastData);

                SettingBag settingBag = SettingBagFactory.Create(forecastData);
                UnivariateForecaster univariateForecaster = UnivariateForecasterFactory.Create(settingBag, _componentBag);

                ShowHeader();

                string filePath = Path.Combine(forecastData.FolderPath, forecastData.Init);
                IFileInfoAdapter initFile = univariateForecaster.Convert(filePath);

                ForecastingInit init = univariateForecaster.LoadInitOrDefault(jsonFile: initFile);
                if (init == default(ForecastingInit))
                    throw new Exception(Messages.MessageCollection.LoadingFileNameReturnedDefault(initFile.Name));

                ForecastingSession session = univariateForecaster.Forecast(init: init);

                if (forecastData.SaveSession)
                    univariateForecaster.SaveSession(session, forecastData.FolderPath);

                LogFooter();

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
