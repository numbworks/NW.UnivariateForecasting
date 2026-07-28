using McMaster.Extensions.CommandLineUtils;

namespace NW.UnivariateForecasting.CLI.ArgumentParsing
{
    /// <summary>Manages the CLI application's arguments.</summary>
    public interface IArgumentManager
    {

        /// <summary>Creates the CLI application.</summary>
        CommandLineApplication Create();

    }
}
