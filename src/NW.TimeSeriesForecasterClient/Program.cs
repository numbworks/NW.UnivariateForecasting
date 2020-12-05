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

            Run(() => RunExample1(), nameof(RunExample1));
            Run(() => RunExample2(), nameof(RunExample2));
            Run(() => RunExample3(), nameof(RunExample3));
            Run(() => RunExample4(), nameof(RunExample4));
            Run(() => RunExample5(), nameof(RunExample5));

            Console.ReadKey();

        }

        public static void Run(Action action, string actionName)
        {

            Console.WriteLine(new string('=', 60));
            Console.WriteLine(actionName);
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(Environment.NewLine);

            action.Invoke();
            
            Console.WriteLine(Environment.NewLine);

        }
        public static void RunExample1()
        {          

            string slidingWindowId = UnivariateForecastingComponents.DefaultIdCreationFunction.Invoke(); // SW{yyyyMMddhhmmsss}
            string observationName = "Total Monthly Sales USD";
            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            uint steps = 1;
            IntervalUnits intervalUnit = IntervalUnits.Months;
            DateTime startDate = new DateTime(2019, 01, 31, 00, 00, 00);

            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager();
            SlidingWindow slidingWindow
                = slidingWindowManager.Create(slidingWindowId, observationName, values, steps, intervalUnit, startDate);

            IUnivariateForecaster forecaster = new UnivariateForecaster();
            Observation observation = forecaster.Forecast(slidingWindow);

        }
        public static void RunExample2()
        {

            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager();
            SlidingWindow slidingWindow = slidingWindowManager.Create(values);
            IUnivariateForecaster forecaster = new UnivariateForecaster();
            Observation observation = forecaster.Forecast(slidingWindow);

        }
        public static void RunExample3()
        {

            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

            ISlidingWindowManager slidingWindowManager = new SlidingWindowManager();
            SlidingWindow slidingWindow = slidingWindowManager.Create(values);
            IUnivariateForecaster forecaster = new UnivariateForecaster();
            Observation observation = forecaster.Forecast(slidingWindow);

            SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow, 3);
            List<double> results = forecaster.ExtractXActualValues(newSlidingWindow);

        }
        public static void RunExample4()
        {

            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            IUnivariateForecaster forecaster = new UnivariateForecaster();
            double nextValue = forecaster.ForecastNextValue(values);

        }
        public static void RunExample5()
        {

            List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
            IUnivariateForecaster forecaster = new UnivariateForecaster();
            double pessimisticNextValue = forecaster.ForecastNextValue(values, C: 0.82, E: 0.00); // 519.01
            double optimisticNextValue = forecaster.ForecastNextValue(values, C: 1.11, E: 0.22); // 702.78

        }

    }
}
