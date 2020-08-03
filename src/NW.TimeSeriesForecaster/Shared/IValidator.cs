namespace NW.UnivariateForecasting
{
    public interface IValidator
    {
        bool IsValid(SlidingWindow slidingWindow);
        bool IsValid(Observation observation);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/