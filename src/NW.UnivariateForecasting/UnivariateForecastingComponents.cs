using System;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting
{

    /// <summary>
    /// Collects all the dependencies required by the library.
    /// </summary>
    public class UnivariateForecastingComponents
    {

        // Fields
        // Properties (static)
        public static Func<string> DefaultIdCreationFunction { get; } = () 
            => $"SW{DateTime.Now.ToString("yyyyMMddhhmmsss")}";
        public static Func<double, double> DefaultRoundingFunction { get; }
            = new Func<double, double>(x => Math.Round(x, 2, MidpointRounding.AwayFromZero));
        public static Action<string> DefaultLoggingAction { get; }
            = (message) => Console.WriteLine(message);

        // Properties
        public IObservationManager ObservationManager { get; private set; }
        public ISlidingWindowManager SlidingWindowManager { get; private set; }
        public ISlidingWindowItemManager SlidingWindowItemManager { get; private set; }
        public IIntervalManager IntervalManager { get; private set; }
        public IFileManager FileManager { get; private set; }
        public Func<string> IdCreationFunction { get; private set; }
        public Func<double, double> RoundingFunction { get; private set; }
        public Action<string> LoggingAction { get; private set; }

        // Constructors
        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingComponents"/>. Hover the mouse over the parameters for details.
        /// </summary>
        /// <param name="slidingWindowManager"></param>
        /// <param name="slidingWindowItemManager"></param>
        /// <param name="observationManager"></param>
        /// <param name="intervalManager"></param>
        /// <param name="fileManager"></param>
        /// <param name="idCreationFunction">The <see cref="SlidingWindow.Id"/> will be generated according to this. Default: "SW{yyyyMMddhhmmsss}" using current datetime.</param>
        /// <param name="roundingFunction">All the values processed by <see cref="NW.UnivariateForecasting"/> will be rounded according to this function. Default: two decimal digits.</param>
        /// <param name="loggingAction">Default: Console.WriteLine(message).</param>
        public UnivariateForecastingComponents(
                ISlidingWindowManager slidingWindowManager,
                ISlidingWindowItemManager slidingWindowItemManager,
                IObservationManager observationManager,
                IIntervalManager intervalManager,
                IFileManager fileManager,
                Func<string> idCreationFunction,
                Func<double, double> roundingFunction,
                Action<string> loggingAction
            )
        {

            Validator.ValidateObject(slidingWindowManager, nameof(slidingWindowManager));
            Validator.ValidateObject(slidingWindowItemManager, nameof(slidingWindowItemManager));
            Validator.ValidateObject(observationManager, nameof(observationManager));
            Validator.ValidateObject(intervalManager, nameof(intervalManager));
            Validator.ValidateObject(fileManager, nameof(fileManager));
            Validator.ValidateObject(idCreationFunction, nameof(idCreationFunction));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));

            ObservationManager = observationManager;
            SlidingWindowManager = slidingWindowManager;
            SlidingWindowItemManager = slidingWindowItemManager;
            IntervalManager = intervalManager;
            FileManager = fileManager;
            IdCreationFunction = idCreationFunction;
            RoundingFunction = roundingFunction;
            LoggingAction = loggingAction;

        }

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecastingComponents"/> using default values.
        /// </summary>
        public UnivariateForecastingComponents()
            : this(
                  new SlidingWindowManager(),
                  new SlidingWindowItemManager(),
                  new ObservationManager(),
                  new IntervalManager(),
                  new FileManager(),
                  DefaultIdCreationFunction,
                  DefaultRoundingFunction,
                  DefaultLoggingAction) { } 

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.04.2021

*/
