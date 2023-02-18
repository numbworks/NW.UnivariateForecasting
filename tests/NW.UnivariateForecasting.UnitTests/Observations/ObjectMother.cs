using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    public static class ObjectMother
    {

        #region Properties

        internal static Observation Observation01_WithInitCE = new Observation(coefficient: 0.5, error: 0.01, nextValue: 582.42);
        internal static Observation Observation01_WithoutInitCE = new Observation(coefficient: 0.82, error: 0.22, nextValue: 519.23);       

        internal static Observation Observation02_WithInitCE = new Observation(coefficient: 0.5, error: 0.01, nextValue: 613.11);

        internal static string Observation01_AsString = $"[ Coefficient: '{0.82}', Error: '{0.22}', NextValue: '{519.23}' ]";

        internal static List<Observation> Observations_Containing01_WithInitCE = new List<Observation>()
        {

            Observation01_WithInitCE

        };
        internal static List<Observation> Observations_Containing01_WithoutInitCE = new List<Observation>()
        {

            Observation01_WithoutInitCE

        };

        internal static List<Observation> Observations_Containing0102_WithInitCE = new List<Observation>()
        {

            Observation01_WithInitCE,
            Observation02_WithInitCE

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