using System;
using NW.Shared.Validation;
using NW.UnivariateForecasting.CLI.ApplicationAbout;
using NW.UnivariateForecasting.CLI.ApplicationSession;
using NW.UnivariateForecasting.CLI.Shared;

namespace NW.UnivariateForecasting.CLI.Application
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