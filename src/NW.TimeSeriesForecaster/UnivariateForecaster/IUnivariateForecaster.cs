using System.Collections.Generic;

namespace NW.TimeSeriesForecaster
{
    public interface IUnivariateForecaster
    {

        /// <summary>
        /// It obtains a List<ForecastedObservation*> out of the provided SlidingWindow object.
        /// </summary>
        List<UnivariateForecastedObservation> Do(SlidingWindow objSlidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/
