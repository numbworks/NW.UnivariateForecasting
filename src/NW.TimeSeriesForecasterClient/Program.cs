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

            // RunExample1();
            // RunExample2();
            // RunExample3();
            // RunExample4();

            Console.ReadKey();

        }

        public static void RunExample1()
        {

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings();

            string slidingWindowId = settings.IdCreationFunction.Invoke(); // SW{yyyyMMddhhmmsss}
            string observationName = "Total Monthly Sales USD";
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            uint steps = 1;
            IntervalUnits intervalUnit = IntervalUnits.Months;
            DateTime startDate = new DateTime(2019, 01, 31, 00, 00, 00);

            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);
            SlidingWindow slidingWindow
                = slidingWindowManager.Create(slidingWindowId, observationName, values, steps, intervalUnit, startDate);

            IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
            Observation observation = forecaster.Forecast(slidingWindow);

        }
        public static void RunExample2()
        {

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);
            SlidingWindow slidingWindow = slidingWindowManager.Create(values);
            IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
            Observation observation = forecaster.Forecast(slidingWindow);

        }
        public static void RunExample3()
        {

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);
            SlidingWindow slidingWindow = slidingWindowManager.Create(values);
            IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
            Observation observation = forecaster.Forecast(slidingWindow);

            SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow, 3);
            List<double> results = forecaster.ExtractXActualValues(newSlidingWindow);

        }
        public static void RunExample4()
        {

            UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
            double nextValue = forecaster.ForecastNextValue(values);

        }


    }
}
