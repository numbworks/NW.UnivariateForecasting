using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.Intervals;
using NW.UnivariateForecasting.Messages;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.Observations
{
    /// <summary>
    /// Collects all the methods useful to manipulate an <see cref="Observation"/>.
    /// </summary>
    public class ObservationManager : IObservationManager
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IIntervalManager _intervalManager;
        private ISlidingWindowManager _slidingWindowManager;
        private Func<double, double> _roundingFunction;
        private Action<string> _loggingAction;

        // Constructors
        /// <summary>
        /// Initializes an instance of <see cref="ObservationManager"/>.
        /// </summary>
        public ObservationManager(
            UnivariateForecastingSettings settings,
            IIntervalManager intervalManager,
            ISlidingWindowManager slidingWindowManager,
            Func<double, double> roundingFunction,
            Action<string> loggingAction)
        {

            Validator.ValidateObject(settings, nameof(settings));
            Validator.ValidateObject(intervalManager, nameof(intervalManager));
            Validator.ValidateObject(slidingWindowManager, nameof(slidingWindowManager));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));

            _settings = settings;
            _intervalManager = intervalManager;
            _slidingWindowManager = slidingWindowManager;
            _roundingFunction = roundingFunction;
            _loggingAction = loggingAction;

        }

        /// <summary>
        /// Initializes an instance of <see cref="ObservationManager"/> using default values.
        /// </summary>
        public ObservationManager()
            : this(
                  new UnivariateForecastingSettings(),
                  new IntervalManager(), 
                  new SlidingWindowManager(),
                  UnivariateForecastingComponents.DefaultRoundingFunction,
                  UnivariateForecastingComponents.DefaultLoggingAction
                  ) { }

        // Methods (public)

        /// <summary>
        /// Calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.
        /// </summary>
        public Observation Create(SlidingWindow slidingWindow, double? C = null, double? E = null)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new ArgumentException(MessageCollection.ObservationManager_ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            _loggingAction.Invoke(MessageCollection.ObservationManager_CreatingObservationOutOfProvidedSlidingWindow.Invoke(slidingWindow));

            Observation observation = new Observation();
            observation.SlidingWindowId = slidingWindow.Id;
            observation.Name = slidingWindow.ObservationName;

            uint size = 1;
            IntervalUnits unit = slidingWindow.Interval.Unit;
            uint steps = slidingWindow.Interval.Steps;
            DateTime startDate = GetObservationStartDate(slidingWindow);
            observation.Interval = _intervalManager.Create(size, unit, startDate, steps);

            observation.X_Actual = GetTargetXActual(slidingWindow.Items);

            List<SlidingWindowItem> itemsExceptTarget = RemoveTargetXActual(slidingWindow.Items);
            observation.C = C ?? CalculateC(itemsExceptTarget, _settings.ForecastingDenominator);
            observation.E = E ?? CalculateE(itemsExceptTarget, observation.C, _settings.ForecastingDenominator);

            double CX = CalculateCX(observation.C, observation.X_Actual);
            observation.Y_Forecasted = CalculateY(CX, observation.E);

            _loggingAction.Invoke(MessageCollection.ObservationManager_FollowingObservationHasBeenCreated.Invoke(observation));

            return observation;

        }

        /// <summary>
        /// Checks the properties of the provided <seealso cref="Observation"/> object for validity.
        /// </summary>
        public bool IsValid(Observation observation)
        {

            if (observation == null)
                return false;
            if (string.IsNullOrWhiteSpace(observation.Name))
                return false;
            if (observation.Interval == null)
                return false;
            if (string.IsNullOrWhiteSpace(observation.SlidingWindowId))
                return false;

            return true;

        }

        // Methods (private)
        private DateTime GetObservationStartDate(SlidingWindow slidingWindow)
            => slidingWindow.Items.OrderBy(item => item.Interval.EndDate).Last().Interval.EndDate;
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
        private double CalculateC(List<SlidingWindowItem> items, double denominator)
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
                sum += DivideXByY(items[i], denominator);

            double result = sum / items.Count;

            return _roundingFunction.Invoke(result);

        }
        private double CalculateE(List<SlidingWindowItem> items, double C, double denominator)
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
                        DivideXByY(items[i], denominator) - C);

            double result = CalculateMODE(values);

            return _roundingFunction.Invoke(result);

        }
        private double CalculateCX(double C, double X) 
            => _roundingFunction.Invoke(C * X);
        private double CalculateY(double CX, double E) 
            => _roundingFunction.Invoke(CX + E);
        private double DivideXByY(SlidingWindowItem item, double denominator)
        {

            double X = item.X_Actual;
            double Y = (double)item.Y_Forecasted;

            if (Y == 0)
                Y = denominator;

            double result = X / Y;

            return _roundingFunction.Invoke(result);

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
    Last Update: 28.04.2021  

*/
