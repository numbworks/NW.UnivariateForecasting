using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.TimeSeriesForecaster
{

    public class ForecastingStrategiesUnivariate : IForecastingStrategiesUnivariate
    {

        // Fields
        // Properties
        public double AlternativeDenominator { get; } = 0.001;

        // Constructors
        public ForecastingStrategiesUnivariate() { }

        // Methods (public)
        public void CalculateValues
            (List<SlidingWindowTimeSeries> listTimeSeries,
            ref ForecastedObservationUnivariate objForecasted,
            Func<double, double> fRound = null)
        {

            objForecasted.X_Actual = GetTargetXActual(listTimeSeries);

            List<SlidingWindowTimeSeries> listExceptTarget = RemoveTargetXActual(listTimeSeries);
            objForecasted.C = CalculateC(listExceptTarget);
            objForecasted.E = CalculateE(listExceptTarget, objForecasted.C);

            double dblCX = CalculateCX(objForecasted.C, objForecasted.X_Actual);
            objForecasted.Y1_Forecasted = CalculateY1(dblCX, objForecasted.E);

            if (fRound != null)
            {

                objForecasted.C = fRound(objForecasted.C);
                objForecasted.E = fRound(objForecasted.E);
                objForecasted.Y1_Forecasted = fRound(objForecasted.Y1_Forecasted);

            }

        }

        // Methods (private)
        private double GetTargetXActual(List<SlidingWindowTimeSeries> listTimeSeries)
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

            return listTimeSeries
                    .Where(Item => Item.Y1_Forecasted == null)
                    .Select(Item => Item.X_Actual)
                    .Last();

        }
        private List<SlidingWindowTimeSeries> RemoveTargetXActual(List<SlidingWindowTimeSeries> listTimeSeries)
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

            List<SlidingWindowTimeSeries> listExceptTarget = new List<SlidingWindowTimeSeries>();
            listExceptTarget.AddRange(
                listTimeSeries.Where(Item => Item.Y1_Forecasted != null));

            return listExceptTarget;

        }
        private double CalculateC(List<SlidingWindowTimeSeries> listTimeSeries)
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

            double dblSum = 0;
            for (int i = 0; i < listTimeSeries.Count; i++)
                dblSum += DivideXByY1(
                    listTimeSeries[i]);

            return dblSum / listTimeSeries.Count;

        }
        private double CalculateE(List<SlidingWindowTimeSeries> listTimeSeries, double dblC)
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

            List<double> listDivideXByY1MinusC = new List<double>();
            for (int i = 0; i < listTimeSeries.Count; i++)
                listDivideXByY1MinusC.Add(
                    DivideXByY1(listTimeSeries[i]) - dblC);

            return CalculateMODE(listDivideXByY1MinusC);

        }
        private double CalculateCX(double dblC, double dblX) => dblC * dblX;
        private double CalculateY1(double dblCX, double dblE) => dblCX + dblE;
        private double DivideXByY1(SlidingWindowTimeSeries objTimeSeries)
        {

            double dblX = objTimeSeries.X_Actual;
            double dblY1 = (double)objTimeSeries.Y1_Forecasted;

            if (dblY1 == 0)
                dblY1 = AlternativeDenominator;

            return dblX / dblY1;

        }
        private double CalculateMODE(List<double> listValues)
        {

            /* "The MODE of a set of values is the value that appears most often." */

            return listValues.GroupBy(value => value)
                             .OrderByDescending(group => group.Count())
                             .First()
                             .Key;

        }

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 25.04.2018     

*/