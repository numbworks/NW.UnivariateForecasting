using System;

namespace NW.TimeSeriesForecaster
{
    public interface IRoundingStategies
    {
        Func<double, double> GetTwoDecimalDigitStrategy();
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.05.2018

*/
