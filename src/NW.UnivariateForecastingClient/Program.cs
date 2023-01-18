using NW.UnivariateForecastingClient.Application;

namespace NW.UnivariateForecastingClient
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

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/