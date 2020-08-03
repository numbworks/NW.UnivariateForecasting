namespace NW.UnivariateForecasting
{
    public interface IUnivariateForecaster
    {

        Observation Do(SlidingWindow objSlidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 03.08.2021

*/
