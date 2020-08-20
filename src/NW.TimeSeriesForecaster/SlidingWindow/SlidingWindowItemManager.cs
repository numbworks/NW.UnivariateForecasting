using System;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowItemManager : ISlidingWindowItemManager
    {

        // Fields
        private IIntervalManager _intervalManager;

        // Properties
        // Constructors
        public SlidingWindowItemManager(IIntervalManager intervalManager)
        {

            if (intervalManager == null)
                throw new ArgumentNullException(nameof(intervalManager));

            _intervalManager = intervalManager;

        }
        public SlidingWindowItemManager()
            : this(new IntervalManager()) { }

        // Methods (public)
        public SlidingWindowItem Create(
            uint id,
            Interval interval,
            double X_Actual,
            double? Y_Forecasted)
        {

            if (!_intervalManager.IsValid(interval))
                throw new Exception(MessageCollection.IntervalNullOrInvalid);

            return new SlidingWindowItem()
            {
                Id = id,
                Interval = interval,
                X_Actual = X_Actual,
                Y_Forecasted = Y_Forecasted

            };

        }
        public bool IsValid(SlidingWindowItem slidingWindowItem)
        {

            if (!_intervalManager.IsValid(slidingWindowItem.Interval))
                return false;

            return true;

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.08.2020

*/