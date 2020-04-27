namespace NW.TimeSeriesForecaster
{
    public class ForecasterFactory : IForecasterFactory
    {

        // Fields
        // Properties
        // Constructors
        public ForecasterFactory() { }

        // Methods (public)
        public IUnivariateForecaster CreateUnivariateForecaster()
            => new UnivariateForecaster();
        public IUnivariateForecaster CreateUnivariateForecaster(
                IUnivariateValuesCalculator valuesCalculator,
                IRoundingStategies roundingStrategies,
                ISlidingWindowManager slidingWindowManager)
            => new UnivariateForecaster(valuesCalculator, roundingStrategies, slidingWindowManager);

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2018

*/