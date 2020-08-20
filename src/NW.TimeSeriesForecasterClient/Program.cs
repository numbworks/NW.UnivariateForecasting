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
            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);

            string slidingWindowId = settings.IdCreationFunction.Invoke();
            string observationName = "Total Monthly Sales USD";
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            uint steps = 1;
            IntervalUnits intervalUnit = IntervalUnits.Months;
            DateTime startDate = new DateTime(2019, 01, 31, 00, 00, 00);

            SlidingWindow slidingWindow 
                = slidingWindowManager.Create(slidingWindowId, observationName, values, steps, intervalUnit, startDate);
            Console.WriteLine(slidingWindow.ToString(true));

            IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
            Observation observation = forecaster.Forecast(slidingWindow);
            Console.WriteLine(observation.ToString());

            //SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow);
            //Console.WriteLine(newSlidingWindow.ToString(true));

            //SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow, 3);
            //Console.WriteLine(newSlidingWindow.ToString(true));

            //List<double> results = forecaster.ExtractValues(newSlidingWindow);
            //Console.WriteLine(results);

            /*
             * 
             * 1. Add log messages all around.
             * 2. Add tests
             * 4. Complete documentation
             * 5. CLI in the client?
             * 6. Shallow copy-like bug if ForecastAndCombine() sequences
             * 7. Take as input an array and output just the number creating a dummy List<SlidingWindowItem> with random dates.
             * 8. Edit the <Description> to match the one in the readme?
             * 9. Create a NW.MarkdownTabulizer library to convert stuff to Markdown Tables
             * 10. Test out the .nupkg from another solution.
             * 
             */

            Console.ReadKey();

        }
    }
}
