using System;

namespace NW.UnivariateForecasting
{
    public class SlidingWindowValidator : ISlidingWindowValidator
    {

        // Fields
        // Properties
        // Constructors
        public SlidingWindowValidator() { }

        // Methods (public)
        public bool IsValid(SlidingWindow slidingWindow)
        {

            if (slidingWindow == null)
                return false;
            if (string.IsNullOrWhiteSpace(slidingWindow.Id))
                return false;
            if (slidingWindow.StartDate >= slidingWindow.EndDate)
                return false;
            if (slidingWindow.StartDate >= slidingWindow.TargetDate)
                return false;
            if (slidingWindow.TargetDate <= slidingWindow.EndDate)
                return false;
            if (slidingWindow.Interval < 1)
                return false;
            if (slidingWindow.Interval != CalculateDifference(slidingWindow.StartDate, slidingWindow.EndDate, slidingWindow.IntervalUnit))
                return false;
            if (1 != CalculateDifference(slidingWindow.EndDate, slidingWindow.TargetDate, slidingWindow.IntervalUnit))
                return false;
            if (slidingWindow.Items == null)
                return false;
            if (slidingWindow.Items.Count < 1)
                return false;
            if (string.IsNullOrWhiteSpace(slidingWindow.ObservationName))
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
