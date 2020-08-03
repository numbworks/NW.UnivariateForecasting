namespace NW.UnivariateForecasting
{
    public interface IObservationForecaster
    {

        /// <summary>
        /// It calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.
        /// </summary>
        Observation Create(SlidingWindow slidingWindow, double denominator);

        /// <summary>
        /// It calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.
        /// <para>Uses <see cref="DefaultDenominator"/>.</para>
        /// </summary>
        Observation Create(SlidingWindow slidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020   

*/
