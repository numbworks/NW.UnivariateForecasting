using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting.UnitTests.Utilities;

namespace NW.UnivariateForecasting.UnitTests.Validation
{
    public static class ObjectMother
    {

        #region Properties

        internal static string[] Array01 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        internal static Car Object01 = new Car()
        {
            Brand = "Dodge",
            Model = "Charger",
            Year = 1966,
            Price = 13500,
            Currency = "USD"
        };
        internal static uint Length01 = 3;
        internal static string VariableName_Variable = "variable";
        internal static string VariableName_Length = "length";
        internal static string VariableName_N1 = "n1";
        internal static string VariableName_N2 = "n2";
        internal static List<string> List01 = Array01.ToList();
        internal static uint Value = Length01;
        internal static string String01 = "Dodge";
        internal static string StringOnlyWhiteSpaces = "   ";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/