using System;

namespace NW.UnivariateForecasting
{
    public class RoundingStategies : IRoundingStategies
    {

        // Fields
        // Properties
        // Constructors
        public RoundingStategies() { }

        // Methods
        public Func<double, double> GetTwoDecimalDigitStrategy()
            => new Func<double, double>(x => Math.Round(x, 2, MidpointRounding.AwayFromZero));

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
