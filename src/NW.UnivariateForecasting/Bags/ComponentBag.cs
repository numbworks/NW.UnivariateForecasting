using System;
using NW.UnivariateForecasting.AsciiBanner;
using NW.UnivariateForecasting.Filenames;
using NW.Shared.Files;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.Shared.Serialization;
using NW.UnivariateForecasting.SlidingWindows;
using NW.Shared.Validation;

namespace NW.UnivariateForecasting.Bags
{
    /// <summary>Collects all the dependencies required by the library.</summary>
    public class ComponentBag
    {

        #region Fields
        #endregion

        #region Properties

        public static Func<double, uint, double> DefaultRoundingFunction { get; }
            = (x, digits) => Math.Round(x, (int)digits, MidpointRounding.AwayFromZero);
        public static string DefaultLoggingActionDateFormat { get; } = "yyyy-MM-dd HH:mm:ss:fff";
        public static Action<string> DefaultLoggingAction { get; }
            = (message) => Console.WriteLine($"[{DateTime.UtcNow.ToString(DefaultLoggingActionDateFormat)}] {message}");
        public static Action<string> DefaultLoggingActionAsciiBanner { get; }
            = (message) => Console.WriteLine($"{message}");
        public static Func<DateTime> DefaultNowFunction { get; } = () => DateTime.Now;

        public IObservationManager ObservationManager { get; private set; }
        public ISlidingWindowManager SlidingWindowManager { get; private set; }
        public IFileManager FileManager { get; private set; }
        public Func<double, uint, double> RoundingFunction { get; private set; }
        public Action<string> LoggingAction { get; private set; }
        public Action<string> LoggingActionAsciiBanner { get; }
        public IAsciiBannerManager AsciiBannerManager { get; }
        public IFilenameFactory FilenameFactory { get; }
        public Func<DateTime> NowFunction { get; }
        public IForecastingInitManager ForecastingInitManager { get; }
        public ISerializerFactory SerializerFactory { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of <see cref="ComponentBag"/>. Hover the mouse over the parameters for details.
        /// </summary>
        /// <param name="slidingWindowManager"></param>
        /// <param name="slidingWindowItemManager"></param>
        /// <param name="observationManager"></param>
        /// <param name="fileManager"></param>
        /// <param name="roundingFunction">All the values processed by <see cref="UnivariateForecasting"/> will be rounded according to this function. Default: two double digits.</param>
        /// <param name="loggingAction">Default: Console.WriteLine(message).</param>
        /// <exception cref="ArgumentNullException"/> 
        public ComponentBag(
                ISlidingWindowManager slidingWindowManager,
                IObservationManager observationManager,
                IFileManager fileManager,
                Func<double, uint, double> roundingFunction,
                Action<string> loggingAction,
                Action<string> loggingActionAsciiBanner,
                IAsciiBannerManager asciiBannerManager,
                IFilenameFactory filenameFactory,
                Func<DateTime> nowFunction,
                IForecastingInitManager forecastingInitManager,
                ISerializerFactory serializerFactory
            )
        {

            Validator.ValidateObject(slidingWindowManager, nameof(slidingWindowManager));
            Validator.ValidateObject(observationManager, nameof(observationManager));
            Validator.ValidateObject(fileManager, nameof(fileManager));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));
            Validator.ValidateObject(loggingActionAsciiBanner, nameof(loggingActionAsciiBanner));
            Validator.ValidateObject(asciiBannerManager, nameof(asciiBannerManager));
            Validator.ValidateObject(filenameFactory, nameof(filenameFactory));
            Validator.ValidateObject(nowFunction, nameof(nowFunction));
            Validator.ValidateObject(forecastingInitManager, nameof(forecastingInitManager));
            Validator.ValidateObject(serializerFactory, nameof(serializerFactory));

            ObservationManager = observationManager;
            SlidingWindowManager = slidingWindowManager;
            FileManager = fileManager;
            RoundingFunction = roundingFunction;
            LoggingAction = loggingAction;
            LoggingActionAsciiBanner = loggingActionAsciiBanner;
            AsciiBannerManager = asciiBannerManager;
            FilenameFactory = filenameFactory;
            NowFunction = nowFunction;
            ForecastingInitManager = forecastingInitManager;
            SerializerFactory = serializerFactory;

        }

        /// <summary>
        /// Initializes an instance of <see cref="ComponentBag"/> using default values.
        /// <para>The default rounding function is: <see cref="DefaultRoundingFunctionTwoDigits"/>.</para>
        /// </summary>
        public ComponentBag()
            : this(
                  new SlidingWindowManager(),
                  new ObservationManager(),
                  new FileManager(),
                  DefaultRoundingFunction,
                  DefaultLoggingAction,
                  DefaultLoggingActionAsciiBanner,
                  new AsciiBannerManager(),
                  new FilenameFactory(),
                  DefaultNowFunction,
                  new ForecastingInitManager(),
                  new SerializerFactory()
                )
        { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/
