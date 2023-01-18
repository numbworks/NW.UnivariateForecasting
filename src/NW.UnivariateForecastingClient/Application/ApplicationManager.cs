﻿using System;
using NW.UnivariateForecasting.Validation;
using NW.UnivariateForecastingClient.Shared;
using NW.UnivariateForecastingClient.ApplicationSession;
using McMaster.Extensions.CommandLineUtils;

namespace NW.UnivariateForecastingClient.Application
{
    /// <inheritdoc cref="IApplicationManager"/>
    public class ApplicationManager : IApplicationManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;
        private ApplicationSections _sections;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationManager
            (ILibraryBroker libraryBroker, IApplicationSectionsFactory sectionsFactory, SessionManagerComponents sessionManagerComponents)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sectionsFactory, nameof(sectionsFactory));
            Validator.ValidateObject(sessionManagerComponents, nameof(sessionManagerComponents));

            _libraryBroker = libraryBroker;
            _sections = sectionsFactory.Create(libraryBroker, sessionManagerComponents);

        }

        /// <summary>Initializes a <see cref="ApplicationManager"/> instance using default parameters.</summary>
        public ApplicationManager()
            : this(new LibraryBroker(), new ApplicationSectionsFactory(), new SessionManagerComponents()) { }

        #endregion

        #region Methods_public

        public int Execute(params string[] args)
        {

            CommandLineApplication app = Create();

            return app.Execute(args);

        }

        #endregion

        #region Methods_private

        private CommandLineApplication Create()
        {

            CommandLineApplication app = new CommandLineApplication
            {

                Name = Shared.MessageCollection.Application_Name,
                Description = Shared.MessageCollection.Application_Description

            };

            app = AddRoot(app);
            app = _sections.AboutManager.Add(app);
            app = _sections.SessionManager.Add(app);

            app.HelpOption(inherited: true);

            return app;

        }
        private CommandLineApplication AddRoot(CommandLineApplication app)
        {

            app.OnExecute(() =>
            {

                int exitCode = _libraryBroker.ShowHeader();
                app.ShowHelp();

                return exitCode;

            });

            return app;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/