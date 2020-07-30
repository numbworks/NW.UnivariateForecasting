using System;
using NW.UnivariateForecasting;

namespace NW.UnivariateForecastingClient
{
    class Program
    {
        static void Main(string[] args)
        {

            IUnivariateForecaster forecaster = new UnivariateForecaster();
            

            Console.ReadKey();

        }
    }
}
