using System;

namespace NW.UnivariateForecasting
{
    public class StategyProvider : IStategyProvider
    {

        // Fields
        // Properties
        public Func<double, double> TwoDecimalDigitsRounding { get; }
            = new Func<double, double>(x => Math.Round(x, 2, MidpointRounding.AwayFromZero));

        // Constructors
        public StategyProvider() { }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
