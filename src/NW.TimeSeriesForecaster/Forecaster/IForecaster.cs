using System.Collections.Generic;

namespace NW.TimeSeriesForecaster
{
    public interface IForecaster
    {

        /// <summary>
        /// It obtains a List<ForecastedObservation*> out of the provided SlidingWindow object.
        /// </summary>
        List<ForecastedObservationUnivariate> Do(SlidingWindow objSlidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/
