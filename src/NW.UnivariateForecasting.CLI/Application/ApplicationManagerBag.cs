using System;
using NW.UnivariateForecasting.CLI.ApplicationSession;
using NW.Shared.Validation;

namespace NW.UnivariateForecasting.CLI.Application
{
    /// <summary>Collects all the dependencies required by <see cref="ApplicationManager"/>.</summary>
    public class ApplicationManagerBag
    {

        #region Fields
        #endregion

        #region Properties

        public ISessionManager SessionManager { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationManagerBag(ISessionManager sessionManager)
        {

            Validator.ValidateObject(sessionManager, nameof(sessionManager));

            SessionManager = sessionManager;

        }

        #endregion

        #region Methods_public
        #endregion

        #region Methods_private
        #endregion

    }
}