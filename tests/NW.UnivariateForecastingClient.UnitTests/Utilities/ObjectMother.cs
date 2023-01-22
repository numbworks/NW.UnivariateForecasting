﻿using System;
using System.Reflection;
using NUnit.Framework;

namespace NW.UnivariateForecastingClient.UnitTests
{

    public static class ObjectMother
    {

        #region Methods

        public static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }
        public static void Method_ShouldThrowACertainInnnerException_WhenCallPrivateMethodAndUnproperArguments
            (TestDelegate del, Type expectedInnerType, string expectedInnerMessage)
        {

            // Arrange
            // Act
            Exception outerException = Assert.Throws(typeof(TargetInvocationException), del);
            Exception actual = outerException.InnerException;

            // Assert
            Assert.AreEqual(expectedInnerType, actual.GetType());
            Assert.AreEqual(expectedInnerMessage, actual.Message);

        }
        public static TReturn CallPrivateMethod<TClass, TReturn>
            (TClass obj, string methodName, object[] args)
        {

            Type type = typeof(TClass);

            return (TReturn)type.GetTypeInfo().GetDeclaredMethod(methodName).Invoke(obj, args);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.01.2023
*/