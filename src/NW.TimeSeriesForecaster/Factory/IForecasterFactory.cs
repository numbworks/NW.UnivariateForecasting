namespace NW.TimeSeriesForecaster
{
    public interface IForecasterFactory
    {

        IUnivariateForecaster CreateUnivariateForecaster();
        IUnivariateForecaster CreateUnivariateForecaster(
                IUnivariateValuesCalculator valuesCalculator,
                IRoundingStategies roundingStrategies,
                ISlidingWindowManager slidingWindowManager);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
