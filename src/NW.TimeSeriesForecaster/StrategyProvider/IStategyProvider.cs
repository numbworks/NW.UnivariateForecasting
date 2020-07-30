using System;

namespace NW.UnivariateForecasting
{
    public interface IStategyProvider
    {
        Func<double, double> TwoDecimalDigitsRounding { get; }
    }
}