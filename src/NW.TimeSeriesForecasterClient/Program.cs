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

            DateTime startDate = new DateTime(2019, 01, 31, 00, 00, 00);
            SlidingWindowIntervalUnits intervalUnit = SlidingWindowIntervalUnits.Months;
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            string observationName = "Some_Identifier";

            Func<double, double> roundingStrategy = new StategyProvider().TwoDecimalDigitsRounding;
            SlidingWindow slidingWindow = new SlidingWindowManager(roundingStrategy).CreateSlidingWindow(startDate, values, intervalUnit, observationName);

            Console.WriteLine(slidingWindow.ToString());

            Observation observation = new UnivariateForecaster(roundingStrategy).Do(slidingWindow);

            Console.WriteLine(observation.Y_Forecasted);

            /*
             * 
             * Add ToString() method to the Sliding Window and the SlidingWindowItem
             * Compile and see if SlidingWindow looks as in "Iteration 8.txt".
             * Add comment to CreateSlidingWindow's body
             * 
             */     

            Console.ReadKey();

        }
    }
}
