using System;
using System.Reflection;
using NW.Shared.Files;
using NW.Shared.Serialization;
using NW.UnivariateForecasting.Filenames;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Observations;
using NW.UnivariateForecasting.SlidingWindows;

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
        public static Func<DateTime> DefaultUTCNowFunction { get; }= () => DateTime.UtcNow;
        public static Func<string> DefaultVersionFunction { get; }
            = () =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Version version = assembly.GetName().Version;
               
                if (version is not null)
                    return $"{version.Major}.{version.Minor}.{version.Build}";

                return "0.0.0";
                
            };

        public IObservationManager ObservationManager { get; private set; }
        public ISlidingWindowManager SlidingWindowManager { get; private set; }
        public IFileManager FileManager { get; private set; }
        public Func<double, uint, double> RoundingFunction { get; private set; }
        public Action<string> LoggingAction { get; private set; }
        public Action<string> LoggingActionAsciiBanner { get; }
        public IFilenameFactory FilenameFactory { get; }
        public Func<DateTime> NowFunction { get; }
        public IForecastingInitManager ForecastingInitManager { get; }
        public ISerializerFactory SerializerFactory { get; }
        public Func<string> VersionFunction { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="ComponentBag"/>.</summary>
        public ComponentBag(
                ISlidingWindowManager slidingWindowManager = null,
                IObservationManager observationManager = null,
                IFileManager fileManager = null,
                Func<double, uint, double> roundingFunction = null,
                Action<string> loggingAction = null,
                Action<string> loggingActionAsciiBanner = null,
                IFilenameFactory filenameFactory = null,
                Func<DateTime> nowFunction = null,
                IForecastingInitManager forecastingInitManager = null,
                ISerializerFactory serializerFactory = null,
                Func<string> versionFunction = null
            )
        {

            SlidingWindowManager = slidingWindowManager ?? new SlidingWindowManager();
            ObservationManager = observationManager ?? new ObservationManager();
            FileManager = fileManager ?? new FileManager();
            RoundingFunction = roundingFunction ?? DefaultRoundingFunction;
            LoggingAction = loggingAction ?? DefaultLoggingAction;
            LoggingActionAsciiBanner = loggingActionAsciiBanner ?? DefaultLoggingActionAsciiBanner;
            FilenameFactory = filenameFactory ?? new FilenameFactory();
            NowFunction = nowFunction ?? DefaultNowFunction;
            ForecastingInitManager = forecastingInitManager ?? new ForecastingInitManager();
            SerializerFactory = serializerFactory ?? new SerializerFactory();
            VersionFunction = versionFunction ?? DefaultVersionFunction;

        }

        #endregion

        #region Methods (public)
        #endregion

    }
}
