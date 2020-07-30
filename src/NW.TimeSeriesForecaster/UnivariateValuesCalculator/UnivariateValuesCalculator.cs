using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.TimeSeriesForecaster
{

    public class UnivariateValuesCalculator : IUnivariateValuesCalculator
    {

        // Fields
        private double _defaultDenominator = 0.001;

        // Properties
        public double AlternativeDenominator { get; private set; }

        // Constructors
        public UnivariateValuesCalculator(double alternativeDenominator = 0.001)
        {
            
            if (alternativeDenominator < _defaultDenominator)
                throw new ArgumentException($"{nameof(alternativeDenominator)} can't be less than {_defaultDenominator.ToString()}.");

            AlternativeDenominator = alternativeDenominator;

        }

        // Methods (public)
        public void CalculateValues
            (List<SlidingWindowTimeSeries> timeSeriesList,
            ref UnivariateForecastedObservation forecastedObservation,
            Func<double, double> rounderFunction = null)
        {

            forecastedObservation.X_Actual = GetTargetXActual(timeSeriesList);

            List<SlidingWindowTimeSeries> listExceptTarget = RemoveTargetXActual(timeSeriesList);
            forecastedObservation.C = CalculateC(listExceptTarget);
            forecastedObservation.E = CalculateE(listExceptTarget, forecastedObservation.C);

            double CX = CalculateCX(forecastedObservation.C, forecastedObservation.X_Actual);
            forecastedObservation.Y1_Forecasted = CalculateY1(CX, forecastedObservation.E);

            if (rounderFunction != null)
            {

                forecastedObservation.C = rounderFunction(forecastedObservation.C);
                forecastedObservation.E = rounderFunction(forecastedObservation.E);
                forecastedObservation.Y1_Forecasted = rounderFunction(forecastedObservation.Y1_Forecasted);

            }

        }

        // Methods (private)
        private double GetTargetXActual(List<SlidingWindowTimeSeries> timeSeriesList)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y1_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             *      568,34      null            << Target X_Actual, to retrieve from the list
             *      
             */

            return timeSeriesList
                    .Where(Item => Item.Y1_Forecasted == null)
                    .Select(Item => Item.X_Actual)
                    .Last();

        }
        private List<SlidingWindowTimeSeries> RemoveTargetXActual(List<SlidingWindowTimeSeries> timeSeriesList)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y1_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             *      568,34      null            << Target X_Actual, so we need to remove it from the list
             *      
             */

            return timeSeriesList.Where(Item => Item.Y1_Forecasted != null).ToList();

        }
        private double CalculateC(List<SlidingWindowTimeSeries> timeSeriesList)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y1_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             * 
             * Step 2:
             * 
             *      DivideXByY1
             *      1,04
             *      0,97
             *      1,04
             *      0,97
             *      1,00
             *      1,11
             *  
             * Step 3:
             * 
             *      AVG(DivideXByY1) = C
             *      1,02
             * 
             */

            double sum = 0;
            for (int i = 0; i < timeSeriesList.Count; i++)
                sum += DivideXByY1(
                    timeSeriesList[i]);

            return sum / timeSeriesList.Count;

        }
        private double CalculateE(List<SlidingWindowTimeSeries> timeSeriesList, double C)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y1_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             * 
             * Step 2:
             * 
             *      C = 1,02
             * 
             *      DivideXByY1 DivideXByY1-C
             *      1,04        0,02
             *      0,97        -0,05
             *      1,04        0,02
             *      0,97        -0,05
             *      1,00        -0,02
             *      1,11        0,09
             *  
             * Step 3:
             * 
             *      MODE(DivideXByY1 - C) = E
             *      0,02
             * 
             */

            List<double> values = new List<double>();
            for (int i = 0; i < timeSeriesList.Count; i++)
                values.Add(
                    DivideXByY1(timeSeriesList[i]) - C);

            return CalculateMODE(values);

        }
        private double CalculateCX(double C, double X) 
            => C * X;
        private double CalculateY1(double CX, double E) 
            => CX + E;
        private double DivideXByY1(SlidingWindowTimeSeries timeSeries)
        {

            double X = timeSeries.X_Actual;
            double Y1 = (double)timeSeries.Y1_Forecasted;

            if (Y1 == 0)
                Y1 = AlternativeDenominator;

            return X / Y1;

        }
        private double CalculateMODE(List<double> values)
        {

            /* "The MODE of a set of values is the value that appears most often." */

            return values.GroupBy(value => value)
                             .OrderByDescending(group => group.Count())
                             .First()
                             .Key;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020    

*/
