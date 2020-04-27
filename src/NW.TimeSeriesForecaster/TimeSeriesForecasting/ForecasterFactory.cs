using System;

namespace NW.TimeSeriesForecaster
{
    public class ForecasterFactory : IForecasterFactory
    {

        // Fields
        // Properties
        // Constructors
        public ForecasterFactory() { }

        // Methods (public)
        public IForecaster Create(ushort uintStepsAhead)
        {

            if (uintStepsAhead == 1)
                return new ForecasterUnivariate();

            throw new NotImplementedException("Multivariate forecasters haven't been implemented yet.");

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/
