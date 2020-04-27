using System.Collections;

namespace NW.TimeSeriesForecaster
{
    public interface ISlidingWindowJsonDeserializer
    {
        IExceptionMessage ExceptionMessage { get; set; }

        ArrayList Do(string strJson);
    }
}

/*

    Author: rua@sitecore.net
    Last Update: 24.04.2018

*/