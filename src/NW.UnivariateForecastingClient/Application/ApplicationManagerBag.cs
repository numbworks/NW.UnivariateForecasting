﻿using System;
using NW.UnivariateForecastingClient.ApplicationAbout;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.Shared.Validation;

namespace NW.UnivariateForecastingClient.Application
{
    /// <summary>Collects all the dependencies required by <see cref="ApplicationManager"/>.</summary>
    public class ApplicationManagerBag
    {

        #region Fields
        #endregion

        #region Properties

        public IAboutManager AboutManager { get; }
        public ISessionManager SessionManager { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationManagerBag(IAboutManager aboutManager, ISessionManager sessionManager)
        {

            Validator.ValidateObject(aboutManager, nameof(aboutManager));
            Validator.ValidateObject(sessionManager, nameof(sessionManager));

            AboutManager = aboutManager;
            SessionManager = sessionManager;

        }

        #endregion

        #region Methods_public
        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/