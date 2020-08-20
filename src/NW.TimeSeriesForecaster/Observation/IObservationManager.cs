namespace NW.UnivariateForecasting
{
    public interface IObservationManager
    {
        Observation Create(SlidingWindow slidingWindow);
        bool IsValid(Observation observation);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.08.2020    

*/