using System;
using NW.UnivariateForecasting.SlidingWindows;

namespace NW.UnivariateForecasting.Observations
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.Observations"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<Type, string> ProvidedTypeObjectNotValid { get; }
            = (type) => $"The provided {type.Name} object is not valid.";
        public static Func<SlidingWindow, string> CreatingObservationOutOfProvidedSlidingWindow { get; }
            = (slidingWindow) => $"Creating an {typeof(Observation).Name} out of the provided {typeof(SlidingWindow).Name}: '{slidingWindow.ToString(false)}'...";
        public static Func<Observation, string> FollowingObservationHasBeenCreated { get; }
            = (observation) => $"The following {typeof(Observation).Name} has been created: '{observation}'.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/