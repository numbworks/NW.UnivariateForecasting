using System;
using System.Collections.Generic;
using System.Linq;
using NW.Shared.Serialization;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.CLI.ArgumentParsing;
using NW.UnivariateForecasting.CLI.AsciiBanners;
using NW.UnivariateForecasting.CLI.TerminalWindows;
using NW.UnivariateForecasting.CLI.UnitTests.Utilities;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using McMaster.Extensions.CommandLineUtils.Validation;
using NUnit.Framework;

namespace NW.UnivariateForecasting.CLI.UnitTests.ArgumentParsing
{
    [TestFixture]
    public class ArgumentManagerTests
    {

        #region Fields

        private static TestCaseData[] createExecuteTestCases =
        {

            new TestCaseData(
                new string[] { },
                ArgumentManager.Success
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_01"),

            new TestCaseData(
                new string[] { CommandLineString.COMMAND_SESSION_NAME },
                ArgumentManager.Success
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_02")

        };
        private static TestCaseData[] parseRoundingDigitsOrDefaultTestCases =
        {

            new TestCaseData("0", (uint?)0)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_01"),

            new TestCaseData("1", (uint?)1)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_02"),

            new TestCaseData("15", (uint?)15)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_03"),

            new TestCaseData(null, OptionValidatorValue.RoundingDigitsDefaultValue)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_04"),

            new TestCaseData(string.Empty, OptionValidatorValue.RoundingDigitsDefaultValue)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_05"),

            new TestCaseData("Some message", OptionValidatorValue.RoundingDigitsDefaultValue)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_06"),

            new TestCaseData("-1", OptionValidatorValue.RoundingDigitsDefaultValue)
                .SetArgDisplayNames($"{nameof(parseRoundingDigitsOrDefaultTestCases)}_07")

        };
        private static TestCaseData[] parseForecastingDenominatorOrDefaultTestCases =
        {

            new TestCaseData("0.0001", (double?)0.0001)
                .SetArgDisplayNames($"{nameof(parseForecastingDenominatorOrDefaultTestCases)}_01"),

            new TestCaseData("1", (double?)1)
                .SetArgDisplayNames($"{nameof(parseForecastingDenominatorOrDefaultTestCases)}_02"),

            new TestCaseData(null, OptionValidatorValue.ForecastingDenominatorDefaultValue)
                .SetArgDisplayNames($"{nameof(parseForecastingDenominatorOrDefaultTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void ArgumentManager_ShouldCreateAnObjectOfTypeArgumentManager_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();

            // Act
            ArgumentManager actual = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Assert
            Assert.That(actual, Is.InstanceOf<ArgumentManager>());
            Assert.That(actual.ForecastingDenominatorValidator, Is.InstanceOf<IOptionValidator>());
            Assert.That(actual.RoundingDigitsValidator, Is.InstanceOf<IOptionValidator>());
            Assert.That(actual.AsciiBannerManager, Is.InstanceOf<IAsciiBannerManager>());
            Assert.That(actual.TerminalWindowManager, Is.InstanceOf<ITerminalWindowManager>());
            Assert.That(actual.UnivariateForecasterFactory, Is.InstanceOf<IUnivariateForecasterFactory>());
            Assert.That(actual.ComponentBagFactory, Is.InstanceOf<IComponentBagFactory>());
            Assert.That(actual.SettingBagFactory, Is.InstanceOf<ISettingBagFactory>());
            Assert.That(ArgumentManager.Success, Is.InstanceOf<int>());
            Assert.That(ArgumentManager.Failure, Is.InstanceOf<int>());
            Assert.That(ArgumentManager.SeparatorLine, Is.InstanceOf<string>());
            Assert.That(ArgumentManager.ErrorMessageFormatter, Is.InstanceOf<Func<string, string>>());

        }

        [TestCaseSource(nameof(createExecuteTestCases))]
        public void Create_ShouldReturnExpectedExitCode_WhenExecuted(string[] args, int expected)
        {

            // Arrange
            CommandLineApplication app = CreateFakeArgumentManager().Create();

            // Act
            int actual = app.Execute(args);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeCommandLineApplication_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            // Act
            CommandLineApplication actual = argumentManager.Create();

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandLineApplication>());

        }

        [Test]
        public void Create_ShouldSetExpectedApplicationInformation_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            // Act
            CommandLineApplication actual = argumentManager.Create();

            // Assert
            Assert.That(actual.Name, Is.EqualTo(CommandLineString.APPLICATION_NAME));
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.APPLICATION_DESCRIPTION));

        }

        [Test]
        public void Create_ShouldAddExpectedCommands_WhenInvoked()
        {

            // Act
            // Arrange
            CommandLineApplication actual = CreateFakeArgumentManager().Create();

            // Assert
            Assert.That(
                actual.Commands.Any(
                    command => command.Name == CommandLineString.COMMAND_SESSION_NAME),
                Is.True);

        }

        [Test]
        public void Create_ShouldAddExpectedSessionCommand_WhenInvoked()
        {

            // Arrange
            CommandLineApplication actual = CreateFakeArgumentManager().Create();

            // Act
            CommandLineApplication sessionCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_SESSION_NAME);

            // Assert
            Assert.That(sessionCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(sessionCommand.Description, Is.EqualTo(CommandLineString.COMMAND_SESSION_DESCR));

        }

        [Test]
        public void Create_ShouldAddExpectedForecastSubCommand_WhenInvoked()
        {

            // Arrange
            CommandLineApplication actual = CreateFakeArgumentManager().Create();
            CommandLineApplication sessionCommand = GetSessionCommand(actual);

            // Act
            CommandLineApplication forecastCommand
                = sessionCommand.Commands.Single(
                    command => command.Name == CommandLineString.SUBCOMMAND_FORECAST_NAME);

            // Assert
            Assert.That(forecastCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(forecastCommand.Description, Is.EqualTo(CommandLineString.SUBCOMMAND_FORECAST_DESCR));

        }

        [Test]
        public void Create_ShouldAddExpectedForecastOptions_WhenInvoked()
        {

            // Arrange
            CommandLineApplication actual = CreateFakeArgumentManager().Create();

            // Act
            CommandLineApplication forecastCommand = GetForecastCommand(actual);

            // Assert
            Assert.That(
                forecastCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_INIT_DESCR),
                Is.InstanceOf<CommandOption>());
            Assert.That(
                forecastCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_FOLDERPATH_DESCR),
                Is.InstanceOf<CommandOption>());
            Assert.That(
                forecastCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_SAVESESSION_DESCR),
                Is.InstanceOf<CommandOption>());
            Assert.That(
                forecastCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_ROUNDINGDIGITS_DESCR),
                Is.InstanceOf<CommandOption>());
            Assert.That(
                forecastCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_FORECASTINGDENOMINATOR_DESCR),
                Is.InstanceOf<CommandOption>());

        }

        [Test]
        public void Create_ShouldSetCustomHelpTextGeneratorForAllCommands_WhenInvoked()
        {

            // Arrange
            CommandLineApplication actual = CreateFakeArgumentManager().Create();
            CommandLineApplication sessionCommand = GetSessionCommand(actual);

            // Act
            CommandLineApplication forecastCommand = GetForecastCommand(actual);

            // Assert
            Assert.That(actual.HelpTextGenerator, Is.InstanceOf<CustomHelpTextGenerator>());
            Assert.That(sessionCommand.HelpTextGenerator, Is.SameAs(actual.HelpTextGenerator));
            Assert.That(forecastCommand.HelpTextGenerator, Is.SameAs(actual.HelpTextGenerator));

        }

        [TestCaseSource(nameof(parseRoundingDigitsOrDefaultTestCases))]
        public void ParseRoundingDigitsOrDefault_ShouldReturnExpectedUint_WhenProperArgument(string value, uint? expected)
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            // Act
            uint? actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, uint?>(
                    argumentManager,
                    "ParseRoundingDigitsOrDefault",
                    new object[] { value });

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [TestCaseSource(nameof(parseForecastingDenominatorOrDefaultTestCases))]
        public void ParseForecastingDenominatorOrDefault_ShouldReturnExpectedDouble_WhenProperArgument(string value, double? expected)
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            // Act
            double? actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, double?>(
                    argumentManager,
                    "ParseForecastingDenominatorOrDefault",
                    new object[] { value });

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void AddRoot_ShouldReturnSameApplicationAndSetExpectedExecution_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication app = new CommandLineApplication();

            // Act
            CommandLineApplication actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandLineApplication>(
                    argumentManager,
                    "AddRoot",
                    new object[] { app });

            // Assert
            Assert.That(actual, Is.SameAs(app));
            Assert.That(actual.Execute(new string[] { }), Is.EqualTo(ArgumentManager.Success));

        }

        [Test]
        public void AddSession_ShouldReturnSameApplicationAndAddExpectedCommand_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication app = new CommandLineApplication();

            // Act
            CommandLineApplication actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandLineApplication>(
                    argumentManager,
                    "AddSession",
                    new object[] { app });

            // Assert
            Assert.That(actual, Is.SameAs(app));
            Assert.That(actual.Commands.Single().Name, Is.EqualTo(CommandLineString.COMMAND_SESSION_NAME));

        }

        [Test]
        public void AddSessionMain_ShouldReturnSameApplicationAndSetExpectedValues_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandLineApplication actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandLineApplication>(
                    argumentManager,
                    "AddSessionMain",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.SameAs(command));
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.COMMAND_SESSION_DESCR));
            Assert.That(actual.Execute(new string[] { }), Is.EqualTo(ArgumentManager.Success));

        }

        [Test]
        public void AddSessionForecast_ShouldReturnSameApplicationAndAddExpectedCommand_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandLineApplication actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandLineApplication>(
                    argumentManager,
                    "AddSessionForecast",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.SameAs(command));
            Assert.That(actual.Commands.Single().Name, Is.EqualTo(CommandLineString.SUBCOMMAND_FORECAST_NAME));
            Assert.That(actual.Commands.Single().Description, Is.EqualTo(CommandLineString.SUBCOMMAND_FORECAST_DESCR));

        }

        [Test]
        public void CreateInitOption_ShouldReturnExpectedOption_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandOption actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandOption>(
                    argumentManager,
                    "CreateInitOption",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandOption>());
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.OPTION_INIT_DESCR));
            Assert.That(command.Options.Single(), Is.SameAs(actual));

        }

        [Test]
        public void CreateFolderPathOption_ShouldReturnExpectedOption_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandOption actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandOption>(
                    argumentManager,
                    "CreateFolderPathOption",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandOption>());
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.OPTION_FOLDERPATH_DESCR));
            Assert.That(command.Options.Single(), Is.SameAs(actual));

        }

        [Test]
        public void CreateSaveSessionOption_ShouldReturnExpectedOption_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandOption actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandOption>(
                    argumentManager,
                    "CreateSaveSessionOption",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandOption>());
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.OPTION_SAVESESSION_DESCR));
            Assert.That(command.Options.Single(), Is.SameAs(actual));

        }

        [Test]
        public void CreateRoundingDigitsOption_ShouldReturnExpectedOption_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandOption actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandOption>(
                    argumentManager,
                    "CreateRoundingDigitsOption",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandOption>());
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.OPTION_ROUNDINGDIGITS_DESCR));
            Assert.That(command.Options.Single(), Is.SameAs(actual));

        }

        [Test]
        public void CreateForecastingDenominatorOption_ShouldReturnExpectedOption_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication command = new CommandLineApplication();

            // Act
            CommandOption actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, CommandOption>(
                    argumentManager,
                    "CreateForecastingDenominatorOption",
                    new object[] { command });

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandOption>());
            Assert.That(actual.Description, Is.EqualTo(CommandLineString.OPTION_FORECASTINGDENOMINATOR_DESCR));
            Assert.That(command.Options.Single(), Is.SameAs(actual));

        }

        [Test]
        public void CreateAsciiBanner_ShouldReturnAString_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, string>(
                    argumentManager,
                    "CreateAsciiBanner",
                    new object[] { });

            // Assert
            Assert.That(actual, Is.InstanceOf<string>());
            Assert.That(actual, Is.Not.Empty);

        }

        [Test]
        public void LogAsciiBanner_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(
                argumentManager,
                "LogAsciiBanner",
                new object[] { });

            // Assert
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));
            Assert.That(messagesAsciiBanner[0], Is.EqualTo(ArgumentManager.SeparatorLine));
            Assert.That(messagesAsciiBanner[1], Is.InstanceOf<string>());
            Assert.That(messagesAsciiBanner[1], Is.Not.Empty);
            Assert.That(messagesAsciiBanner[2], Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void LogFooter_ShouldLogExpectedMessage_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(
                argumentManager,
                "LogFooter",
                new object[] { });

            // Assert
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(1));
            Assert.That(messagesAsciiBanner[0], Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void LogAndReturnFailure_ShouldReturnFailureAndLogException_WhenInvoked()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner);
            Exception exception = new Exception("Some error");

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "LogAndReturnFailure",
                    new object[] { exception });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Failure));
            Assert.That(messages[0], Is.EqualTo(ArgumentManager.ErrorMessageFormatter(exception.Message)));
            Assert.That(messagesAsciiBanner[0], Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void LogAndReturnFailure_ShouldLogInnerException_WhenExceptionHasInnerException()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();
            
            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner);
            Exception innerException = new Exception("Inner error");
            Exception exception = new Exception("Outer error", innerException);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "LogAndReturnFailure",
                    new object[] { exception });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Failure));
            Assert.That(messages[0], Is.EqualTo(ArgumentManager.ErrorMessageFormatter(exception.Message)));
            Assert.That(messages[1], Is.EqualTo(ArgumentManager.ErrorMessageFormatter(innerException.Message)));
            Assert.That(messagesAsciiBanner[0], Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void AddCustomHelpTextGenerator_ShouldSetSameGeneratorForAllCommands_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication app = new CommandLineApplication();
            CommandLineApplication command = app.Command("command", subCommand => subCommand.Command("subcommand", child => { }));
            IHelpTextGenerator expected = new DefaultHelpTextGenerator();

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(
                argumentManager,
                "AddCustomHelpTextGenerator",
                new object[] { app, expected });

            // Assert
            Assert.That(app.HelpTextGenerator, Is.SameAs(expected));
            Assert.That(command.HelpTextGenerator, Is.SameAs(expected));
            Assert.That(command.Commands.Single().HelpTextGenerator, Is.SameAs(expected));

        }

        [Test]
        public void AddCustomHelpTextGenerator_ShouldSetCustomGeneratorForAllCommands_WhenInvoked()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            CommandLineApplication app = new CommandLineApplication();
            CommandLineApplication command = app.Command("command", subCommand => subCommand.Command("subcommand", child => { }));

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(
                argumentManager,
                "AddCustomHelpTextGenerator",
                new object[] { app });

            // Assert
            Assert.That(app.HelpTextGenerator, Is.InstanceOf<CustomHelpTextGenerator>());
            Assert.That(command.HelpTextGenerator, Is.SameAs(app.HelpTextGenerator));
            Assert.That(command.Commands.Single().HelpTextGenerator, Is.SameAs(app.HelpTextGenerator));

        }

        [Test]
        public void AddValidationErrorHandling_ShouldReturnFailureAndLogExpectedMessages_WhenValidationFails()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner);
            CommandLineApplication app = new CommandLineApplication();
            CommandLineApplication command
                = app.Command("command", subCommand =>
                    subCommand
                        .Option(
                            "--value <VALUE>",
                            "Value",
                            CommandOptionType.SingleValue)
                        .Accepts(validator => validator.Use(new RoundingDigitsValidator())));

            ObjectMother.CallPrivateMethod<ArgumentManager, object>(
                argumentManager,
                "AddValidationErrorHandling",
                new object[] { app });

            // Act
            int actual = app.Execute(new string[] { "command", "--value", "16" });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Failure));
            Assert.That(messages.Count, Is.EqualTo(1));
            Assert.That(messages[0], Does.StartWith("ERROR: "));
            Assert.That(messagesAsciiBanner.First(), Is.EqualTo(ArgumentManager.SeparatorLine));
            Assert.That(messagesAsciiBanner.Last(), Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void Defaultize_ShouldReturnExpectedForecastData_WhenValuesAreNotNull()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            ForecastData expected
                = new ForecastData(
                        init: "Init.json",
                        folderPath: @"C:\nwunif\",
                        saveSession: true,
                        roundingDigits: 2,
                        forecastingDenominator: 0.001);

            // Act
            ForecastData actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, ForecastData>(
                    argumentManager,
                    "Defaultize",
                    new object[] { expected });

            // Assert
            Assert.That(actual.Init, Is.EqualTo(expected.Init));
            Assert.That(actual.FolderPath, Is.EqualTo(expected.FolderPath));
            Assert.That(actual.SaveSession, Is.EqualTo(expected.SaveSession));
            Assert.That(actual.RoundingDigits, Is.EqualTo(expected.RoundingDigits));
            Assert.That(actual.ForecastingDenominator, Is.EqualTo(expected.ForecastingDenominator));

        }

        [Test]
        public void Defaultize_ShouldReturnExpectedDefaultValues_WhenValuesAreNull()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();
            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        folderPath: null,
                        saveSession: false,
                        roundingDigits: null,
                        forecastingDenominator: null);

            // Act
            ForecastData actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, ForecastData>(
                    argumentManager,
                    "Defaultize",
                    new object[] { forecastData });

            // Assert
            Assert.That(actual.Init, Is.EqualTo(forecastData.Init));
            Assert.That(actual.FolderPath, Is.EqualTo(SettingBag.DefaultFolderPath));
            Assert.That(actual.SaveSession, Is.EqualTo(forecastData.SaveSession));
            Assert.That(actual.RoundingDigits, Is.EqualTo(SettingBag.DefaultRoundingDigits));
            Assert.That(actual.ForecastingDenominator, Is.EqualTo(SettingBag.DefaultForecastingDenominator));

        }

        [Test]
        public void ShowHeader_ShouldReturnSuccessAndLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "ShowHeader",
                    new object[] { });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Success));
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));
            Assert.That(messagesAsciiBanner.First(), Is.EqualTo(ArgumentManager.SeparatorLine));
            Assert.That(messagesAsciiBanner.Last(), Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void RunSessionForecast_ShouldReturnFailureAndLogException_WhenForecastDataIsNull()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "RunSessionForecast",
                    new object[] { null });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Failure));
            Assert.That(messages[0], Does.StartWith("ERROR: "));
            Assert.That(messagesAsciiBanner.Last(), Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void RunSessionForecast_ShouldReturnSuccess_WhenForecastDataIsNotNull()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours
                = new List<(string fileName, string content)>()
                {

                    (fileName: "Init.json", content: UnivariateForecasting.UnitTests.Forecasts.ObjectMother.ForecastingInitBareMinimumAsJson_Content)

                };
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner,
                    readBehaviours: readBehaviours);
            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        saveSession: false,
                        folderPath: @"C:\nwunif\",
                        roundingDigits: 2,
                        forecastingDenominator: 0.001);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "RunSessionForecast",
                    new object[] { forecastData });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Success));
            Assert.That(messagesAsciiBanner.First(), Is.EqualTo(ArgumentManager.SeparatorLine));
            Assert.That(messagesAsciiBanner.Last(), Is.EqualTo(ArgumentManager.SeparatorLine));

        }

        [Test]
        public void RunSessionForecast_ShouldReturnFailure_WhenInitFailsToLoad()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours
                = new List<(string fileName, string content)>()
                {

                    (fileName: "Init.json", content: @"{ ""SomeField"": ""SomeValue"" }")

                };
            List<string> messages = new List<string>();
            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    readBehaviours: readBehaviours);
            ForecastData forecastData
                = new ForecastData(
                        init: "Init.json",
                        saveSession: false,
                        folderPath: @"C:\nwunif\",
                        roundingDigits: 2,
                        forecastingDenominator: 0.001);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "RunSessionForecast",
                    new object[] { forecastData });

            // Assert
            Assert.That(actual, Is.EqualTo(ArgumentManager.Failure));
            Assert.That(messages.Last(), Is.EqualTo(
                ArgumentManager.ErrorMessageFormatter(
                    CLI.Messages.MessageCollection.LoadingFileNameReturnedDefault("Init.json"))));

        }

        #endregion

        #region TearDown
        #endregion

        #region Support Methods

        private ComponentBag CreateFakeComponentBag(
            List<string> messages = null,
            List<string> messagesAsciiBanner = null,
            List<(string fileName, string content)> readBehaviours = null)
        {

            messages = messages ?? new List<string>();
            messagesAsciiBanner = messagesAsciiBanner ?? new List<string>();
            readBehaviours = readBehaviours ?? new List<(string fileName, string content)>();

            Action<string> fakeLoggingAction = (message) => messages.Add(message);
            Action<string> fakeLoggingActionAsciiBanner = (message) => messagesAsciiBanner.Add(message);

            ComponentBag componentBag
                = new ComponentBag(
                        slidingWindowManager: new SlidingWindowManager(),
                        observationManager: new ObservationManager(),
                        fileManager: new FakeFileManagerWithDynamicRead(readBehaviours),
                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                        loggingAction: fakeLoggingAction,
                        loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                        filenameFactory: new FilenameFactory(),
                        nowFunction: ComponentBag.DefaultNowFunction,
                        forecastingInitManager: new ForecastingInitManager(),
                        serializerFactory: new SerializerFactory(),
                        versionFunction: ComponentBag.DefaultVersionFunction);

            return componentBag;

        }
        private FakeComponentBagFactory CreateFakeComponentBagFactory(
            List<string> messages = null,
            List<string> messagesAsciiBanner = null,
            List<(string fileName, string content)> readBehaviours = null)
        {

            ComponentBag fakeComponentBag
                = CreateFakeComponentBag(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner,
                    readBehaviours: readBehaviours);
            FakeComponentBagFactory fakeComponentBagFactory = new FakeComponentBagFactory(fakeComponentBag);

            return fakeComponentBagFactory;

        }
        private ArgumentManager CreateFakeArgumentManager(
            List<string> messages = null,
            List<string> messagesAsciiBanner = null,
            List<(string fileName, string content)> readBehaviours = null)
        {

            FakeComponentBagFactory fakeComponentBagFactory
                = CreateFakeComponentBagFactory(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner,
                    readBehaviours: readBehaviours);
            ArgumentManager argumentManager
                = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            return argumentManager;

        }
        private CommandLineApplication GetSessionCommand(CommandLineApplication app)
            => app.Commands.Single(
                command => command.Name == CommandLineString.COMMAND_SESSION_NAME);
        private CommandLineApplication GetForecastCommand(CommandLineApplication app)
            => GetSessionCommand(app)
                .Commands
                .Single(command => command.Name == CommandLineString.SUBCOMMAND_FORECAST_NAME);

        #endregion

    }
}
