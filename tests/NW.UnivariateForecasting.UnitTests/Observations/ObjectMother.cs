using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    public static class ObjectMother
    {

        #region Properties

        internal static Observation Observation01 = new Observation(coefficient: 0.82, error: 0.22, nextValue: 519.23);
        internal static Observation Observation01_WithCustomCE = new Observation(coefficient: 0.92, error: 0.12, nextValue: 519.23);
        internal static Observation Observation02 = new Observation(coefficient: 0.92, error: 0.12, nextValue: 582.42);

        internal static string Observation01_AsString = $"[ Coefficient: '{0.82}', Error: '{0.22}', NextValue: '{519.23}' ]";

        internal static List<Observation> Observations_With01 = new List<Observation>()
        {

            Observation01

        };
        internal static List<Observation> Observations_With0102 = new List<Observation>()
        {

            Observation01,
            Observation02

        };

        #endregion

        #region Methods

        internal static bool AreEqual(Observation obj1, Observation obj2)
        {

            return Equals(obj1.Coefficient, obj2.Coefficient)
                        && Equals(obj1.Error, obj2.Error)
                        && Equals(obj1.NextValue, obj2.NextValue);

        }
        internal static bool AreEqual(List<Observation> list1, List<Observation> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/