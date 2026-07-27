using NW.UnivariateForecasting.CLI.Application;

namespace NW.UnivariateForecasting.CLI
{
    class Program
    {
        static int Main(string[] args)
        {

            ApplicationManager applicationManager = new ApplicationManager();

            return applicationManager.Execute(args);

        }

    }
}