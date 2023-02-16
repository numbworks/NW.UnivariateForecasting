using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    public static class ObjectMother
    {

        #region Properties

        internal static string SlidingWindow01_Id = "SW20200906090516";
        internal static string SlidingWindow01_ObservationName = "Total Monthly Sales USD";

        internal static ObservationManager ObservationManager = new ObservationManager();
        internal static Observation Observation_InvalidDueOfNullName = new Observation()
        {

            Name = null

        };

        internal static Observation Observation_Empty = new Observation();
        internal static string Observation_Empty_AsString
            = "[ Name: 'null', X_Actual: '0', C: '0', E: '0', Y_Forecasted: '0', SlidingWindowId: 'null' ]";

        internal static Observation Observation01 = new Observation()
        {

            Name = SlidingWindow01_ObservationName,
            X_Actual = 632.94,
            Coefficient = 0.82,
            Error = 0.22,
            NextValue = 519.23,
            SlidingWindowId = SlidingWindow01_Id

        };
        internal static string Observation01_AsString
            = $"[ Name: 'Total Monthly Sales USD', X_Actual: '{632.94}', C: '{0.82}', E: '{0.22}', Y_Forecasted: '{519.23}', SlidingWindowId: 'SW20200906090516' ]";
        internal static string Observation01_AsStringOnlyDates
            = $"[ Name: 'Total Monthly Sales USD', X_Actual: '{632.94}', C: '{0.82}', E: '{0.22}', Y_Forecasted: '{519.23}', SlidingWindowId: 'SW20200906090516' ]";

        internal static double Observation01_WithCustomCE_C = 0.92;
        internal static double Observation01_WithCustomCE_E = 0.12;
        internal static double Observation01_WithCustomCE_Y = 582.42;
        internal static Observation Observation01_WithCustomCE = new Observation()
        {

            Name = Observation01.Name,
            SlidingWindowId = Observation01.SlidingWindowId,
            X_Actual = Observation01.X_Actual,
            Coefficient = Observation01_WithCustomCE_C,
            Error = Observation01_WithCustomCE_E,
            NextValue = Observation01_WithCustomCE_Y

        };

        internal static List<Observation> Observations_With01 = new List<Observation>()
        {

            Observation01

        };
        internal static List<Observation> Observations_With0102 = new List<Observation>()
        {

            Observation01,
            Observation01 // To change with Observation02

        };

        #endregion

        #region Methods

        internal static bool AreEqual(Observation obj1, Observation obj2)
        {

            return string.Equals(obj1.Name, obj2.Name, StringComparison.InvariantCulture)
                        && Equals(obj1.X_Actual, obj2.X_Actual)
                        && Equals(obj1.Coefficient, obj2.Coefficient)
                        && Equals(obj1.Error, obj2.Error)
                        && Equals(obj1.NextValue, obj2.NextValue)
                        && string.Equals(obj1.SlidingWindowId, obj2.SlidingWindowId, StringComparison.InvariantCulture);

        }
        internal static bool AreEqual(List<Observation> list1, List<Observation> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/