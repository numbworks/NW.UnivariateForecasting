using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Represents the library's entry-point.
    /// </summary>
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private UnivariateForecastingComponents _components;

        // Properties
        // Constructors
        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecaster"/>.
        /// </summary>
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            UnivariateForecastingComponents components)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            _settings = settings;
            _components = components;

        }

        /// <summary>
        /// Initializes an instance of <see cref="UnivariateForecaster"/> using default values.
        /// </summary>
        public UnivariateForecaster()
            : this (
                  new UnivariateForecastingSettings(), 
                  new UnivariateForecastingComponents()) { }

        // Methods (public)
        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/>.
        /// </summary>
        public Observation Forecast(SlidingWindow slidingWindow, double? C = null, double? E = null)
        {

            if (!_components.SlidingWindowManager.IsValid(slidingWindow))
                throw new ArgumentException(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return _components.ObservationManager.Create(slidingWindow, C, E);

        }

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        public SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, out List<Observation> observations, double? C = null, double? E = null)
        {

            if (!_components.SlidingWindowManager.IsValid(slidingWindow))
                throw new ArgumentException(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (steps < 1)
                throw new ArgumentException(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));

            _components.LoggingAction.Invoke(MessageCollection.RunningForecastAndCombineForSteps.Invoke(steps));

            SlidingWindow newSlidingWindow = DeepCloneSlidingWindow(slidingWindow);
            List<Observation> temp = new List<Observation>();

            for (uint i = 1; i <= steps; i++)
            {

                _components.LoggingAction.Invoke(MessageCollection.ForecastingAndCombineForStepNr.Invoke(i));

                Observation observation = Forecast(newSlidingWindow, C, E);
                newSlidingWindow = Combine(newSlidingWindow, observation);

                temp.Add(observation);

            };

            _components.LoggingAction.Invoke(MessageCollection.ForecastAndCombineSuccessfullyRunForSteps.Invoke(steps));
            _components.LoggingAction.Invoke(MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(newSlidingWindow));

            observations = temp;
            return newSlidingWindow;

        }

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        public SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, uint steps, double? C = null, double? E = null)
        {

            List<Observation> observations = null;
            return ForecastAndCombine(slidingWindow, steps, out observations, C, E);

        }

        /// <summary>
        /// Forecasts the next value, adds it back to <see cref="SlidingWindow"/> and forecasts the next value again.
        /// <para>Emulates the Multivariate Forecasting technique by repeating the Univariate Forecasting technique for x steps.</para>
        /// </summary>
        public SlidingWindow ForecastAndCombine
            (SlidingWindow slidingWindow, double? C = null, double? E = null)
                => ForecastAndCombine(slidingWindow, 1, C, E);

        /// <summary>
        /// Forecasts the next value for the provided list of values.
        /// </summary>
        public double ForecastNextValue(List<double> values, double? C = null, double? E = null)
        {

            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Count == 0)
                throw new ArgumentException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(values)));

            _components.LoggingAction.Invoke(MessageCollection.ForecastNextValueRunningForProvidedValues.Invoke(values));

            SlidingWindow slidingWindow = _components.SlidingWindowManager.Create(values);
            double nextValue = _components.ObservationManager.Create(slidingWindow, C, E).Y_Forecasted;

            _components.LoggingAction.Invoke(MessageCollection.ForecastNextValueSuccessfullyRun.Invoke(nextValue));

            return nextValue;

        }

        /// <summary>
        /// Add the provided <see cref="Observation"/> to the provided <see cref="SlidingWindow"/> object.
        /// </summary>
        public SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation)
        {

            /*

                SlidingWindow:

                    [ Id: 'SW20200803063734', ObservationName: 'Some_Identifier', Interval: '6:Months:20190131:20190731:20190831:1:6', Items: '6' ]          

                newSlidingWindow:

                    [ Id: 'SW20200803063734', ObservationName: 'Some_Identifier', Interval: '7:Months:20190131:20190831:20190930:1:7', Items: '7' ]

             */

            if (!_components.SlidingWindowManager.IsValid(slidingWindow))
                throw new ArgumentException(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (!_components.ObservationManager.IsValid(observation))
                throw new ArgumentException(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(Observation)));

            _components.LoggingAction.Invoke(MessageCollection.CombiningProvidedSlidingWindowWithObservation);
            _components.LoggingAction.Invoke(MessageCollection.ProvidedSlidingWindowIs.Invoke(slidingWindow));
            _components.LoggingAction.Invoke(MessageCollection.ProvidedObservationIs.Invoke(observation));

            uint steps = (uint)(slidingWindow.Interval.Size / slidingWindow.Items.Count);
            SlidingWindow newSlidingWindow = new SlidingWindow()
            {

                Id = _components.IdCreationFunction.Invoke(),
                ObservationName = slidingWindow.ObservationName,
                Interval = _components.IntervalManager.Create(
                                                    slidingWindow.Interval.Size + 1,
                                                    slidingWindow.Interval.Unit,
                                                    slidingWindow.Interval.StartDate,
                                                    steps),
                Items = CombineItems(slidingWindow.Items, slidingWindow.Interval.Unit, steps, observation)

            };

            _components.LoggingAction.Invoke(MessageCollection.FollowingSlidingWindowHasBeenCreated.Invoke(newSlidingWindow));

            return newSlidingWindow;

        }

        public List<double> ExtractXActualValues(SlidingWindow slidingWindow)
        {

            if (!_components.SlidingWindowManager.IsValid(slidingWindow))
                throw new ArgumentException(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            _components.LoggingAction.Invoke(MessageCollection.ExtractingValuesOutOfProvidedSlidingWindow.Invoke(slidingWindow));

            List<double> values = slidingWindow.Items.Select(item => item.X_Actual).ToList();
            _components.LoggingAction.Invoke(MessageCollection.ValuesHaveBeenSuccessfullyExtracted.Invoke(values));

            return values;

        }
        public List<DateTime> ExtractStartDates(SlidingWindow slidingWindow)
        {

            if (!_components.SlidingWindowManager.IsValid(slidingWindow))
                throw new ArgumentException(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            _components.LoggingAction.Invoke(MessageCollection.ExtractingStartDatesOutOfProvidedSlidingWindow.Invoke(slidingWindow));

            List<DateTime> startDates = slidingWindow.Items.Select(item => item.Interval.StartDate).ToList();
            _components.LoggingAction.Invoke(MessageCollection.StartDatesHaveBeenSuccessfullyExtracted.Invoke(startDates));

            return startDates;

        }

        public void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, IFileInfoAdapter fileInfoAdapter)
        {

            if (slidingWindow == null)
                throw new ArgumentNullException(nameof(slidingWindow));
            if (fileInfoAdapter == null)
                throw new ArgumentNullException(nameof(fileInfoAdapter));

            _components.LoggingAction.Invoke(MessageCollection.SerializingProvidedSlidingWindowAsJsonAndSavingItTo.Invoke(fileInfoAdapter));

            SaveAsJson(slidingWindow, fileInfoAdapter);

            _components.LoggingAction.Invoke(MessageCollection.ProvidedObjectHasBeenSuccessfullySavedAsJson);

        }
        public void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, FileInfo fileInfo)
            => SaveSlidingWindowAsJson(slidingWindow, _components.FileManager.Create(fileInfo));
        public void SaveSlidingWindowAsJson(SlidingWindow slidingWindow, string filePath)
            => SaveSlidingWindowAsJson(slidingWindow, _components.FileManager.Create(filePath));
        public void SaveObservationAsJson(Observation observation, IFileInfoAdapter fileInfoAdapter)
        {

            if (observation == null)
                throw new ArgumentNullException(nameof(observation));
            if (fileInfoAdapter == null)
                throw new ArgumentNullException(nameof(fileInfoAdapter));

            _components.LoggingAction.Invoke(MessageCollection.SerializingProvidedObservationAsJsonAndSavingItTo.Invoke(fileInfoAdapter));

            SaveAsJson(observation, fileInfoAdapter);

            _components.LoggingAction.Invoke(MessageCollection.ProvidedObjectHasBeenSuccessfullySavedAsJson);

        }
        public void SaveObservationAsJson(Observation observation, FileInfo fileInfo)
            => SaveObservationAsJson(observation, _components.FileManager.Create(fileInfo));
        public void SaveObservationAsJson(Observation observation, string filePath)
            => SaveObservationAsJson(observation, _components.FileManager.Create(filePath));

        public SlidingWindow LoadSlidingWindowFromJson(IFileInfoAdapter fileInfoAdapter)
        {

            if (fileInfoAdapter == null)
                throw new ArgumentNullException(nameof(fileInfoAdapter));
            if (!fileInfoAdapter.Exists)
                throw new ArgumentException(MessageCollection.ProvidedFileDoesntExist.Invoke(fileInfoAdapter));

            _components.LoggingAction.Invoke(MessageCollection.DeserializingProvidedFileAsSlidingWindowObject.Invoke(fileInfoAdapter));

            SlidingWindow slidingWindow = GetFromJson<SlidingWindow>(fileInfoAdapter);

            _components.LoggingAction.Invoke(MessageCollection.ProvidedFileHasBeenSuccessfullyDeserialized);

            return slidingWindow;
        }
        public SlidingWindow LoadSlidingWindowFromJson(FileInfo fileInfo)
            => LoadSlidingWindowFromJson(_components.FileManager.Create(fileInfo));
        public SlidingWindow LoadSlidingWindowFromJson(string filePath)
            => LoadSlidingWindowFromJson(_components.FileManager.Create(filePath));
        public Observation LoadObservationFromJson(IFileInfoAdapter fileInfoAdapter)
        {

            if (fileInfoAdapter == null)
                throw new ArgumentNullException(nameof(fileInfoAdapter));
            if (!fileInfoAdapter.Exists)
                throw new ArgumentException(MessageCollection.ProvidedFileDoesntExist.Invoke(fileInfoAdapter));

            _components.LoggingAction.Invoke(MessageCollection.DeserializingProvidedFileAsObservationObject.Invoke(fileInfoAdapter));

            Observation observation = GetFromJson<Observation>(fileInfoAdapter);

            _components.LoggingAction.Invoke(MessageCollection.ProvidedFileHasBeenSuccessfullyDeserialized);

            return observation;

        }
        public Observation LoadObservationFromJson(FileInfo fileInfo)
            => LoadObservationFromJson(_components.FileManager.Create(fileInfo));
        public Observation LoadObservationFromJson(string filePath)
            => LoadObservationFromJson(_components.FileManager.Create(filePath));

        // Methods (private)
        private List<SlidingWindowItem> DeepCloneSlidingWindowItems(List<SlidingWindowItem> slidingWindowItems)
        {

            return slidingWindowItems.ConvertAll(item =>
                                            new SlidingWindowItem()
                                            {
                                                Id = item.Id,
                                                Interval = item.Interval,
                                                X_Actual = item.X_Actual,
                                                Y_Forecasted = item.Y_Forecasted
                                            });

        }
        private SlidingWindow DeepCloneSlidingWindow(SlidingWindow slidingWindow)
        {

            return new SlidingWindow()
            {
                Id = slidingWindow.Id,
                ObservationName = slidingWindow.ObservationName,
                Interval = slidingWindow.Interval,
                Items = slidingWindow.Items
            };

        }
        private List<SlidingWindowItem> CombineItems(
            List<SlidingWindowItem> slidingWindowItems, IntervalUnits intervalUnits, uint steps, Observation observation)
        {

            /*
             
                Items:

                    [ Id: '1', Interval: '2019-01-31:2019-02-28:2019-03-31', X_Actual: '58,5', Y_Forecasted: '615,26' ]
                    [ Id: '2', Interval: '2019-02-28:2019-03-31:2019-04-30', X_Actual: '615,26', Y_Forecasted: '659,84' ]
                    ...                  
                    [ Id: '6', Interval: '2019-06-30:2019-07-31:2019-08-31', X_Actual: '632,94', Y_Forecasted: '' ]
 
                Observation:

                    [ ..., Y_Forecasted: '519,23', ... ]

                newItems:

                    [ Id: '1', Interval: '2019-01-31:2019-02-28:2019-03-31', X_Actual: '58,5', Y_Forecasted: '615,26' ]
                    [ Id: '2', Interval: '2019-02-28:2019-03-31:2019-04-30', X_Actual: '615,26', Y_Forecasted: '659,84' ]
                    ...
                    => [ Id: '6', Interval: '2019-06-30:2019-07-31:2019-08-31', X_Actual: '632,94', Y_Forecasted: '519,23' ]
                    => [ Id: '7', Interval: '2019-07-31:2019-08-31:2019-09-30', X_Actual: '519,23', Y_Forecasted: '' ]

             */

            List<SlidingWindowItem> newItems = DeepCloneSlidingWindowItems(slidingWindowItems);

            SlidingWindowItem oldLastItem = newItems.OrderBy(item => item.Id).Last();
            newItems.Remove(oldLastItem);
            oldLastItem.Y_Forecasted = observation.Y_Forecasted;
            newItems.Add(oldLastItem);

            SlidingWindowItem newLastItem =
                _components.SlidingWindowItemManager.CreateItem(
                                            oldLastItem.Id + 1,
                                            _components.IntervalManager.CalculateNext(oldLastItem.Interval.StartDate, intervalUnits, steps),
                                            intervalUnits,
                                            observation.Y_Forecasted,
                                            null);
            newItems.Add(newLastItem);

            return newItems;

        }
        private void SaveAsJson(object obj, IFileInfoAdapter fileInfoAdapter)
        {

            string content = JsonConvert.SerializeObject(obj, GetJsonSerializerSettings());
            _components.FileManager.WriteAllText(fileInfoAdapter, content);

        }
        private T GetFromJson<T>(IFileInfoAdapter fileInfoAdapter)
        {

            string content = _components.FileManager.ReadAllText(fileInfoAdapter);

            return JsonConvert.DeserializeObject<T>(content, GetJsonSerializerSettings());

        }
        private JsonSerializerSettings GetJsonSerializerSettings()
        {

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateFormatString = "yyyy-MM-dd";
            settings.Converters.Add(new StringEnumConverter());

            return settings;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021

*/
