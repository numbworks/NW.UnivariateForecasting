﻿using System;
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
        private SessionManagerComponents _sessionManagerComponents;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManager(ILibraryBroker libraryBroker, SessionManagerComponents sessionManagerComponents)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sessionManagerComponents, nameof(sessionManagerComponents));

            _libraryBroker = libraryBroker;
            _sessionManagerComponents = sessionManagerComponents;

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Add(CommandLineApplication app)
        {

            Validator.ValidateObject(app, nameof(app));

            app.Command(Shared.MessageCollection.Session_Name, sessionCommand =>
            {

                sessionCommand = AddMain(sessionCommand);

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
                    .Accepts(validator => validator.Use(_sessionManagerComponents.RoundingDigitsValidator));

        }
        private CommandOption CreateOptionalForecastingDenominatorOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_ForecastingDenominator_Template,
                        Shared.MessageCollection.Session_Option_ForecastingDenominator_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerComponents.ForecastingDenominatorValidator));

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/