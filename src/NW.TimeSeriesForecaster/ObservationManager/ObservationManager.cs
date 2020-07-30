using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{

    public class ObservationManager : IObservationManager
    {

        // Fields
        private ISlidingWindowManager _slidingWindowManager;
        private double _denominator;

        // Properties
        /// <summary>
        /// Since Y_Forecasted = 0 is a totally legit value, when it happens we replace it with a comparably 
        /// small amount to avoid errors when dividing by zero.
        /// </summary>
        public const double DefaultDenominator = 0.001;

        // Constructors
        public ObservationManager(ISlidingWindowManager slidingWindowManager, double denominator = DefaultDenominator)
        {

            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));
            if (denominator < DefaultDenominator)
                throw new ArgumentException($"{nameof(denominator)} can't be less than {DefaultDenominator.ToString()}.");

            _slidingWindowManager = slidingWindowManager;
            _denominator = denominator;

        }

        // Methods (public)
        public Observation Create
            (SlidingWindow slidingWindow,
            Func<double, double> roundingStrategy = null)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedSlidingWindowNotValid);

            Observation observation = new Observation();

            observation.SlidingWindowId = slidingWindow.Id;
            observation.Name = slidingWindow.ObservatioName;
            observation.StartDate = GetObservationStartDate(slidingWindow);
            observation.EndDate = slidingWindow.TargetDate;
            observation.X_Actual = GetTargetXActual(slidingWindow.Items);

            List<SlidingWindowItem> itemsExceptTarget = RemoveTargetXActual(slidingWindow.Items);
            observation.C = CalculateC(itemsExceptTarget);
            observation.E = CalculateE(itemsExceptTarget, observation.C);

            double CX = CalculateCX(observation.C, observation.X_Actual);
            observation.Y_Forecasted = CalculateY(CX, observation.E);

            if (roundingStrategy != null)
            {

                observation.C = roundingStrategy(observation.C);
                observation.E = roundingStrategy(observation.E);
                observation.Y_Forecasted = roundingStrategy(observation.Y_Forecasted);

            }

            return observation;

        }

        // Methods (private)
        private DateTime GetObservationStartDate(SlidingWindow slidingWindow)
            => slidingWindow.Items.OrderBy(item => item.EndDate).Last().EndDate;
        private double GetTargetXActual(List<SlidingWindowItem> items)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             *      568,34      null            << Target X_Actual, to retrieve from the list
             *      
             */

            return items
                    .Where(Item => Item.Y_Forecasted == null)
                    .Select(Item => Item.X_Actual)
                    .Last();

        }
        private List<SlidingWindowItem> RemoveTargetXActual(List<SlidingWindowItem> items)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             *      568,34      null            << Target X_Actual, so we need to remove it from the list
             *      
             */

            return items.Where(Item => Item.Y_Forecasted != null).ToList();

        }
        private double CalculateC(List<SlidingWindowItem> items)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y_Forecasted
             *      635,69	    612,27
             *      612,27	    632,94
             *      632,94	    609,96
             *      609,96	    629,64
             *      629,64	    629,66
             *      629,66	    568,34
             * 
             * Step 2:
             * 
             *      DivideXByY
             *      1,04
             *      0,97
             *      1,04
             *      0,97
             *      1,00
             *      1,11
             *  
             * Step 3:
             * 
             *      AVG(DivideXByY) = C
             *      1,02
             * 
             */

            double sum = 0;
            for (int i = 0; i < items.Count; i++)
                sum += DivideXByY(
                    items[i]);

            return sum / items.Count;

        }
        private double CalculateE(List<SlidingWindowItem> items, double C)
        {

            /*
             * 
             * Step 1:
             * 
             *      X_Actual	Y_Forecasted
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
             *      DivideXByY DivideXByY-C
             *      1,04        0,02
             *      0,97        -0,05
             *      1,04        0,02
             *      0,97        -0,05
             *      1,00        -0,02
             *      1,11        0,09
             *  
             * Step 3:
             * 
             *      MODE(DivideXByY - C) = E
             *      0,02
             * 
             */

            List<double> values = new List<double>();
            for (int i = 0; i < items.Count; i++)
                values.Add(
                    DivideXByY(items[i]) - C);

            return CalculateMODE(values);

        }
        private double CalculateCX(double C, double X) 
            => C * X;
        private double CalculateY(double CX, double E) 
            => CX + E;
        private double DivideXByY(SlidingWindowItem item)
        {

            double X = item.X_Actual;
            double Y = (double)item.Y_Forecasted;

            if (Y == 0)
                Y = _denominator;

            return X / Y;

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
