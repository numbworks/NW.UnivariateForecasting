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
        public IForecaster Create(ForecasterTypes forecasterType)
        {

            if (forecasterType == ForecasterTypes.Univariate)
                return new ForecasterUnivariate();

            throw new NotImplementedException($"The '{ForecasterTypes.Multivariate.ToString()}' forecaster hasn't been implemented yet.");

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/