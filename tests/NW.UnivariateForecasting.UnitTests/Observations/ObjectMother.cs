using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    public static class ObjectMother
    {

        #region Properties

        internal static ObservationManager ObservationManager_Empty = new ObservationManager();
        internal static Observation Observation_InvalidDueOfNullName = new Observation()
        {

            Name = null

        };
        internal static Observation Observation_InvalidDueOfNullInterval = new Observation()
        {

            Name = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = null

        };
        internal static Observation Observation_InvalidDueOfNullSlidingWindow = new Observation()
        {

            Name = Utilities.ObjectMother.Shared_SlidingWindow1_ObservationName,
            Interval = Utilities.ObjectMother.Shared_SlidingWindow1_SubInterval1, // Whatever valid Interval
            SlidingWindowId = null

        };

        internal static Observation Observation_Empty = new Observation();
        internal static string Observation_Empty_AsString
            = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";
        internal static string Observation_Empty_AsStringOnlyDates
            = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.11.2022
*/