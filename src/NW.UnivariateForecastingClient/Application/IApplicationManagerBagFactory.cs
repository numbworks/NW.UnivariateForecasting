using System;
using NW.UnivariateForecastingClient.ApplicationSession;
using NW.UnivariateForecastingClient.Shared;

namespace NW.UnivariateForecastingClient.Application
{
    /// <summary>A factory for <see cref="ApplicationManagerBag"/>.</summary>
    public interface IApplicationManagerBagFactory
    {

        /// <summary>Creates a <see cref="ApplicationManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        ApplicationManagerBag Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/