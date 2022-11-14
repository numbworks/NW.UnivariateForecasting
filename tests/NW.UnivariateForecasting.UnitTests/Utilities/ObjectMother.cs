using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Files;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Utilities
{
    public static class ObjectMother
    {

        #region Properties

        internal static IFileAdapter FakeFileAdapter_ReadAllTextReturnsSlidingWindowWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw Files.ObjectMother.FileAdapterIOException,
                    fakeReadAllText: () => Properties.Resources.SlidingWindowWithDummyValues
                );
        internal static IFileAdapter FakeFileAdapter_ReadAllTextReturnsObservationWithDummyValues
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw Files.ObjectMother.FileAdapterIOException,
                    fakeReadAllText: () => Properties.Resources.ObservationWithDummyValues
                );

        #endregion

        #region Methods
        
        internal static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }
        internal static bool AreEqual<T>(List<T> list1, List<T> list2, Func<T, T, bool> comparer)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (comparer(list1[i], list2[i]) == false)
                    return false;

            return true;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/
