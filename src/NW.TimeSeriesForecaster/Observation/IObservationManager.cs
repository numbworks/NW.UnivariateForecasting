namespace NW.UnivariateForecasting
{
    public interface IObservationManager
    {

        /// <summary>
        /// It calculates the unknown values in Y=F(X)+E => Y=CX+E, and assigns them to a <seealso cref="Observation"/> object.
        /// </summary>
        Observation Create(SlidingWindow slidingWindow, double? C = null, double? E = null);

        bool IsValid(Observation observation);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.09.2020    

*/