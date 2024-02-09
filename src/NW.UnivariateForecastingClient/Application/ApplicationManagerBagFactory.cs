﻿using System;
using NW.UnivariateForecasting.Validation;
using NW.UnivariateForecastingClient.ApplicationAbout;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.Shared;

namespace NW.UnivariateForecastingClient.Application
{
    /// <inheritdoc cref="IApplicationManagerBagFactory"/>
    public class ApplicationManagerBagFactory : IApplicationManagerBagFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationManagerBagFactory"/> instance.</summary>
        public ApplicationManagerBagFactory() { }

        #endregion

        #region Methods_public

        public ApplicationManagerBag Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sessionManagerBag, nameof(sessionManagerBag));

            IAboutManager aboutManager = new AboutManager(libraryBroker);
            ISessionManager sessionManager = new SessionManager(libraryBroker, sessionManagerBag);

            ApplicationManagerBag applicationManagerBag
                = new ApplicationManagerBag(
                            aboutManager: aboutManager,
                            sessionManager: sessionManager
                        );

            return applicationManagerBag;

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/