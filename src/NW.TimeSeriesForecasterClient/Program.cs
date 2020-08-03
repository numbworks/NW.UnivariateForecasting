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
            Console.WriteLine(slidingWindow.ToString(true));

            IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
            Observation observation = forecaster.Forecast(slidingWindow);
            Console.WriteLine(observation.ToString());

            //SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow);
            //Console.WriteLine(newSlidingWindow.ToString(true));

            SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow, 3);
            Console.WriteLine(newSlidingWindow.ToString(true));

            List<double> results = forecaster.ExtractValues(newSlidingWindow);
            Console.WriteLine(results);

            /*
             * 
             * 1. Add log messages all around.
             * 2. Add tests
             * 3. Add buildscript
             * 4. Add documentation
             * 5. CLI in the client?
             * 6. Shallow copy-like bug if ForecastAndCombine() sequences
             * 
             */

            Console.ReadKey();

        }
    }
}
