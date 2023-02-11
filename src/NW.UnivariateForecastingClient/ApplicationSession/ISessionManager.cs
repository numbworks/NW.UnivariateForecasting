﻿using McMaster.Extensions.CommandLineUtils;

namespace NW.UnivariateForecastingClient.ApplicationSession
{
    /// <summary>Represents the <c>Session</c> command of the CLI application.</summary>
    public interface ISessionManager
    {
        /// <summary>Add the <c>Session</c> command to the CLI application.</summary>
        CommandLineApplication Add(CommandLineApplication app);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/