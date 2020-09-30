namespace NW.UnivariateForecasting
{
    public interface IObservationManager
    {
        Observation Create(SlidingWindow slidingWindow, double? C = null, double? E = null);
        bool IsValid(Observation observation);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.09.2020    

*/