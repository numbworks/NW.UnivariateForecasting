using System.Collections.Generic;

namespace NW.TimeSeriesForecaster
{
    public interface IUnivariateForecaster
    {

        List<UnivariateForecastedObservation> Do(SlidingWindow objSlidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
