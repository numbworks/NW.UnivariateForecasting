namespace NW.UnivariateForecasting
{
    public interface ISlidingWindowItemManager
    {
        SlidingWindowItem Create(uint id, Interval interval, double X_Actual, double? Y_Forecasted);
        bool IsValid(SlidingWindowItem slidingWindowItem);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.08.2020

*/