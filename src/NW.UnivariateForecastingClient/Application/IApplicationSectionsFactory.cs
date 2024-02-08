using System;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.Shared;

namespace NW.UnivariateForecastingClient.Application
{
    /// <summary>A factory for <see cref="ApplicationSections"/>.</summary>
    public interface IApplicationSectionsFactory
    {

        /// <summary>Creates a <see cref="ApplicationSections"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        ApplicationSections Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/