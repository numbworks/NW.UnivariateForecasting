using NW.UnivariateForecasting;
using NW.UnivariateForecastingClient.Application;

namespace NW.UnivariateForecastingClient.Shared
{
    /// <summary>The middleware between <see cref="ApplicationManager"/> and <see cref="UnivariateForecaster"/>.</summary>
    public interface ILibraryBroker
    {

        /// <summary>Shows the application's ASCII banner.</summary>
        /// <returns>Always <see cref="ExitCodes.Success"/></returns>
        int ShowHeader();

        /// <summary>Runs the <c>about</c> command of the CLI application.</summary>
        /// <returns>Always <see cref="ExitCodes.Success"/></returns>
        int RunAboutMain();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/