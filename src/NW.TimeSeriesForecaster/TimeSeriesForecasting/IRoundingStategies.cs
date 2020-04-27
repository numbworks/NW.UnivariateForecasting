using System;

namespace NW.TimeSeriesForecaster
{
    public interface IRoundingStategies
    {
        Func<double, double> GetTwoDecimalDigitStrategy();
    }
}

/*

    Author: rua@sitecore.net
    Last Update: 01.05.2018

*/