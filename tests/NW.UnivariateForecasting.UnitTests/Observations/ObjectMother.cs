using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Observations;

namespace NW.UnivariateForecasting.UnitTests.Observations
{
    public static class ObjectMother
    {

        #region Properties

        public static Observation Observation01_WithInitCE = new Observation(coefficient: 0.5, error: 0.01, nextValue: 316.48);
        public static Observation Observation01_WithoutInitCE = new Observation(coefficient: 0.82, error: 0.22, nextValue: 519.23);
        public static string Observation01_WithoutInitCE_AsString = $"[ Coefficient: '{0.82}', Error: '{0.22}', NextValue: '{519.23}' ]";

        public static Observation Observation02_WithInitCE = new Observation(coefficient: 0.5, error: 0.01, nextValue: 158.25);

        public static List<Observation> Observations_Containing01_WithInitCE = new List<Observation>()
        {

            Observation01_WithInitCE

        };
        public static List<Observation> Observations_Containing01_WithoutInitCE = new List<Observation>()
        {

            Observation01_WithoutInitCE

        };

        public static List<Observation> Observations_Containing0102_WithInitCE = new List<Observation>()
        {

            Observation01_WithInitCE,
            Observation02_WithInitCE

        };

        public static Observation Observation_BareMinimum = new Observation(coefficient: 0.1, error: 0, nextValue: 61.53);
        public static List<Observation> Observations_BareMinimum = new List<Observation>()
        {

            Observation_BareMinimum

        };

        public static UnivariateForecastingSettings UnivariateForecastingSettings_WithTwoRoundingDigits = new UnivariateForecastingSettings(
                forecastingDenominator: UnivariateForecastingSettings.DefaultForecastingDenominator,
                folderPath: UnivariateForecastingSettings.DefaultFolderPath,
                roundingDigits: 2
            );

        public static ObservationManager ObservationManager_WithTwoRoundingDigits = new ObservationManager(
                roundingFunction: UnivariateForecastingComponents.DefaultRoundingFunction,
                loggingAction: UnivariateForecastingComponents.DefaultLoggingAction
            );

        #endregion

        #region Methods

        public static bool AreEqual(Observation obj1, Observation obj2)
        {

            return Equals(obj1.Coefficient, obj2.Coefficient)
                        && Equals(obj1.Error, obj2.Error)
                        && Equals(obj1.NextValue, obj2.NextValue);

        }
        public static bool AreEqual(List<Observation> list1, List<Observation> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/