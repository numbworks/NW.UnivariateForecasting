using System;
using System.Collections.Generic;
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
            Interval = Intervals.ObjectMother.Interval_SixMonths_SubInterval01, // Whatever valid Interval
            SlidingWindowId = null

        };

        internal static Observation Observation_Empty = new Observation();
        internal static string Observation_Empty_AsString
            = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";
        internal static string Observation_Empty_AsStringOnlyDates
            = "[ Name: 'null', Interval: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";

        #endregion

        #region Methods

        internal static bool AreEqual(Observation obj1, Observation obj2)
        {

            return string.Equals(obj1.Name, obj2.Name, StringComparison.InvariantCulture)
                        && Intervals.ObjectMother.AreEqual(obj1.Interval, obj2.Interval)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.C, obj2.C)
                        && Equals(obj1.E, obj2.E)
                        && Equals(obj1.Y_Forecasted, obj2.Y_Forecasted)
                        && string.Equals(obj1.SlidingWindowId, obj2.SlidingWindowId, StringComparison.InvariantCulture);

        }
        internal static bool AreEqual(List<Observation> list1, List<Observation> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/