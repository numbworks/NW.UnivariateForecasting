using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.UnivariateForecasting
{
    public class UnivariateForecaster : IUnivariateForecaster
    {

        // Fields
        private UnivariateForecastingSettings _settings;
        private IObservationManager _observationManager;
        private ISlidingWindowManager _slidingWindowManager;

        // Properties
        // Constructors
        public UnivariateForecaster(
            UnivariateForecastingSettings settings,
            ISlidingWindowManager slidingWindowManager,
            IObservationManager observationManager)
        {

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (slidingWindowManager == null)
                throw new ArgumentNullException(nameof(slidingWindowManager));

            _settings = settings;
            _observationManager = observationManager;
            _slidingWindowManager = slidingWindowManager;

        }
        public UnivariateForecaster(UnivariateForecastingSettings settings)
            : this (settings, new SlidingWindowManager(settings), new ObservationManager(settings)) { }

        // Methods (public)
        /// <summary>
        /// Forecasts the next value for the provided <see cref="SlidingWindow"/> according to Univariate Forecasting.
        /// <para>Explaination: "[...] univariate refers to an expression, equation, function or polynomial of only one variable [...]
        /// which consists of observations on only a single characteristic or attribute.".</para>     
        /// </summary>
        public Observation Forecast(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return _observationManager.Create(slidingWindow);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow)
        {

            Observation observation = Forecast(slidingWindow);

            return Combine(slidingWindow, observation);

        }
        public SlidingWindow ForecastAndCombine(SlidingWindow slidingWindow, uint steps)
        {

            if (steps < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));

            SlidingWindow newSlidingWindow = slidingWindow;
            for (int i = 1; i <= steps; i++)
                newSlidingWindow = ForecastAndCombine(newSlidingWindow);

            return newSlidingWindow;

        }
        public SlidingWindow Combine(SlidingWindow slidingWindow, Observation observation)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));
            if (!_observationManager.IsValid(observation))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(Observation)));

            SlidingWindow newSlidingWindow = new SlidingWindow();

            /*
            newSlidingWindow.Id = _settings.IdCreationFunction.Invoke();
            newSlidingWindow.StartDate = slidingWindow.StartDate;

            uint steps = (uint)(slidingWindow.Size / slidingWindow.Items.Count);
            newSlidingWindow.EndDate = CalculateNext(slidingWindow.EndDate, slidingWindow.Unit, steps);
            newSlidingWindow.TargetDate = CalculateNext(slidingWindow.TargetDate, slidingWindow.Unit, steps);
            newSlidingWindow.Size = slidingWindow.Size + 1;
            newSlidingWindow.Unit = slidingWindow.Unit;
            newSlidingWindow.Items = Combine(slidingWindow.Items, slidingWindow.Unit, steps, observation);
            newSlidingWindow.ObservationName = slidingWindow.ObservationName;
            */
            return newSlidingWindow;

        }
        public List<double> ExtractXActualValues(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return slidingWindow.Items.Select(item => item.X_Actual).ToList();

        }
        public List<DateTime> ExtractStartDates(SlidingWindow slidingWindow)
        {

            if (!_slidingWindowManager.IsValid(slidingWindow))
                throw new Exception(MessageCollection.ProvidedTypeObjectNotValid.Invoke(typeof(SlidingWindow)));

            return slidingWindow.Items.Select(item => item.Interval.StartDate).ToList();

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.08.2020

*/
