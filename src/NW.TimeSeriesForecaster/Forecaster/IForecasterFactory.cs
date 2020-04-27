namespace NW.TimeSeriesForecaster
{
    public interface IForecasterFactory
    {

        IForecaster Create(ushort uintStepsAhead);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/
