using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting;

namespace NW.UnivariateForecastingClient
{
    class Program
    {
        static void Main(string[] args)
        {

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
            ISlidingWindowCreator slidingWindowCreator = new SlidingWindowCreator(settings);

            DateTime startDate = new DateTime(2019, 01, 31, 00, 00, 00);
            IntervalUnits intervalUnit = IntervalUnits.Months;
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            string observationName = "Some_Identifier";
            SlidingWindow slidingWindow = slidingWindowCreator.CreateSlidingWindow(startDate, values, intervalUnit, observationName);
            Console.WriteLine(slidingWindow.ToString());

            Observation observation = new UnivariateForecaster(settings).Do(slidingWindow);
            Console.WriteLine(observation.ToString());

            /*
             * 
             * Compile and see if SlidingWindow looks as in "Iteration 8.txt".
             * Add comment to CreateSlidingWindow's body
             * 
             */     

            Console.ReadKey();

        }
    }
}
