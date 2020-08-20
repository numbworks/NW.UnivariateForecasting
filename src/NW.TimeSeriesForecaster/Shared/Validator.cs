﻿using System;

namespace NW.UnivariateForecasting
{
    public class Validator : IValidator
    {

        // Fields
        // Properties
        // Constructors
        public Validator() { }

        // Methods (public)
        public bool IsValid(SlidingWindow slidingWindow)
        {

            if (slidingWindow == null)
                return false;
            if (string.IsNullOrWhiteSpace(slidingWindow.Id))
                return false;
            if (string.IsNullOrWhiteSpace(slidingWindow.ObservationName))
                return false;
            if (slidingWindow.Interval == null)
                return false;
            if (slidingWindow.Items == null)
                return false;
            if (slidingWindow.Items.Count < 1)
                return false;
            if (slidingWindow.Items.Count != slidingWindow.Interval.SubIntervals)
                return false;

            return true;

        }
        public bool IsValid(Observation observation)
        {

            if (observation == null)
                return false;
            if (string.IsNullOrWhiteSpace(observation.Name))
                return false;
            if (observation.StartDate >= observation.EndDate)
                return false;
            if (observation.Interval < 1)
                return false;
            if (observation.Interval != CalculateDifference(observation.StartDate, observation.EndDate, observation.IntervalUnit))
                return false;
            if (string.IsNullOrWhiteSpace(observation.SlidingWindowId))
                return false;

            return true;

        }

        // Methods (private)
        private int CalculateDifference(DateTime date1, DateTime date2, IntervalUnits intervalUnit)
        {

            if (intervalUnit == IntervalUnits.Months)
                return Math.Abs(((date1.Year - date2.Year) * 12) + date1.Month - date2.Month);

            throw new Exception(MessageCollection.NoStrategyToCalculateDateDifferenceUnit.Invoke(intervalUnit.ToString()));

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
