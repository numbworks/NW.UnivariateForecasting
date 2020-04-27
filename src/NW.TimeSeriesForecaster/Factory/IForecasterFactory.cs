namespace NW.TimeSeriesForecaster
{
    public interface IForecasterFactory
    {

        IUnivariateForecaster Create(ForecasterTypes forecasterType);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/