namespace NW.UnivariateForecasting
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

            if (slidingWindow.ObservatioName == null
                && slidingWindow.StepsAhead == 0
                && slidingWindow.Interval == 0
                && slidingWindow.IsSuccess == false
                && slidingWindow.ErrorMessage == null
                && slidingWindow.Id == null
                && slidingWindow.Items == null
                && slidingWindow.StartDate == null
                && slidingWindow.EndDate == null
                && slidingWindow.TargetDate == null
                && slidingWindow.IntervalUnit == null)
                return false;

            return true;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
