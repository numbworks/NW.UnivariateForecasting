﻿using System;
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

            Console.ReadKey();

        }
    }
}
