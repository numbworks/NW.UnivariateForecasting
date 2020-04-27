namespace NW.TimeSeriesForecaster
{
    public class SlidingWindowManager : ISlidingWindowManager
    {

        // Fields
        // Properties
        // Constructors
        public SlidingWindowManager() { }

        // Methods
        public bool IsValid(SlidingWindow slidingWindow)
        {

            if (slidingWindow == null)
                return false;

            if (slidingWindow.ObservationDescription == null
                && slidingWindow.StepsAhead == 0
                && slidingWindow.Duration == 0
                && slidingWindow.IsSuccess == false
                && slidingWindow.ErrorMessage == null
                && slidingWindow.SlidingWindowId == null
                && slidingWindow.TimeSeriesCollection == null
                && slidingWindow.StartDate == null
                && slidingWindow.EndDate == null
                && slidingWindow.TargetDate == null
                && slidingWindow.DurationUnit == null)
                return false;

            return true;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
