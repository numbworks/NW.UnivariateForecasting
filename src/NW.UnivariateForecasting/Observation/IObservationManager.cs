namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Collects all the methods useful to manipulate an <see cref="Observation"/>.
    /// </summary>
    public interface IObservationManager
    {

        /// <summary>
        /// Calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.
        /// </summary>
        Observation Create(SlidingWindow slidingWindow, double? C = null, double? E = null);

        /// <summary>
        /// Checks the properties of the provided <seealso cref="Observation"/> object for validity.
        /// </summary>
        bool IsValid(Observation observation);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021  

*/
