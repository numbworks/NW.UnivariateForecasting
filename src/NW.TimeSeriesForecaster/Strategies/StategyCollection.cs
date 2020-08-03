using System;

namespace NW.UnivariateForecasting
{
    public static class StategyCollection
    {

        // Fields
        // Properties
        public static Func<double, double> TwoDecimalDigitsRounding { get; }
                        = new Func<double, double>(x => Math.Round(x, 2, MidpointRounding.AwayFromZero));

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
