using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.SlidingWindows;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.Observations
{
    /// <inheritdoc cref="IObservationManager"/>
    public class ObservationManager : IObservationManager
    {

        #region Fields

        private UnivariateForecastingSettings _settings;
        private Func<double, uint, double> _roundingFunction;
        private Action<string> _loggingAction;

        #endregion

        #region Properties

        public static Func<double, uint, double> DefaultRoundingFunction { get; }
            = UnivariateForecastingComponents.DefaultRoundingFunction;
        public static Action<string> DefaultLoggingAction { get; }
            = UnivariateForecastingComponents.DefaultLoggingAction;

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="ObservationManager"/>.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public ObservationManager(
            UnivariateForecastingSettings settings,
            Func<double, uint, double> roundingFunction,
            Action<string> loggingAction)
        {

            Validator.ValidateObject(settings, nameof(settings));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));

            _settings = settings;
            _roundingFunction = roundingFunction;
            _loggingAction = loggingAction;

        }

        /// <summary>Initializes an instance of <see cref="ObservationManager"/> using default values.</summary>
        public ObservationManager()
            : this(
                  new UnivariateForecastingSettings(),
                  DefaultRoundingFunction,
                  DefaultLoggingAction
                  ) { }

        #endregion

        #region Methods_public

        public Observation Create(SlidingWindow slidingWindow, double? coefficient = null, double? error = null)
        {

            Validator.ValidateObject(slidingWindow, nameof(slidingWindow));

            _loggingAction(MessageCollection.CreatingObservationOutOfProvidedSlidingWindow(slidingWindow));

            double X_Actual = GetTargetXActual(slidingWindow.Items);
            List<SlidingWindowItem> itemsExceptTarget = RemoveTargetXActual(slidingWindow.Items);

            coefficient = coefficient ?? CalculateCoefficient(itemsExceptTarget, _settings.ForecastingDenominator);
            error = error ?? CalculateError(itemsExceptTarget, (double)coefficient, _settings.ForecastingDenominator);

            double CX = CalculateCX((double)coefficient, X_Actual);
            double nextValue = CalculateNextValue(CX, (double)error);

            Observation observation = new Observation(
                    coefficient: (double)coefficient,
                    error: (double)error,
                    nextValue: nextValue
                );

            _loggingAction(MessageCollection.FollowingObservationHasBeenCreated(observation));

            return observation;

        }

        #endregion

        #region Methods_private

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
        private double CalculateCoefficient(List<SlidingWindowItem> items, double denominator)
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

            return _roundingFunction(result, _settings.RoundingDigits);

        }
        private double CalculateError(List<SlidingWindowItem> items, double coefficient, double denominator)
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
                        DivideXByY(items[i], denominator) - coefficient);

            double result = CalculateMODE(values);

            return _roundingFunction(result, _settings.RoundingDigits);

        }
        private double CalculateCX(double coefficient, double X) 
            => _roundingFunction(coefficient * X, _settings.RoundingDigits);
        private double CalculateNextValue(double CX, double error) 
            => _roundingFunction(CX + error, _settings.RoundingDigits);
        private double DivideXByY(SlidingWindowItem item, double denominator)
        {

            double X = item.X_Actual;
            double Y = (double)item.Y_Forecasted;

            if (Y == 0)
                Y = denominator;

            double result = X / Y;

            return _roundingFunction(result, _settings.RoundingDigits);

        }
        private double CalculateMODE(List<double> values)
        {

            /* "The MODE of a set of values is the value that appears most often." */

            return values.GroupBy(value => value)
                             .OrderByDescending(group => group.Count())
                             .First()
                             .Key;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/