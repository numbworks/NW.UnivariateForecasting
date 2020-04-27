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
    Last Update: 28.04.2020

*/
