namespace NW.UnivariateForecasting
{
    public interface IObservationManager
    {

        /// <summary>
        /// <para>It calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.</para>
        /// </summary>
        /// <param name="roundingFunction">If provided, the values get rounded accordingly.</param>
        Observation Create(SlidingWindow slidingWindow);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020   

*/
