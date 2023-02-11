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
        ApplicationSections Create(ILibraryBroker libraryBroker, SessionManagerComponents sessionComponents);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/