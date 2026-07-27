using System;
using NW.UnivariateForecasting.CLI.ApplicationSession;
using NW.UnivariateForecasting.CLI.Shared;

namespace NW.UnivariateForecasting.CLI.Application
{
    /// <summary>A factory for <see cref="ApplicationManagerBag"/>.</summary>
    public interface IApplicationManagerBagFactory
    {

        /// <summary>Creates a <see cref="ApplicationManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        ApplicationManagerBag Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag);

    }
}