﻿using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Files;

namespace NW.UnivariateForecasting.Validation
{
    /// <summary>Collects all the validation methods.</summary>
    public static class Validator
    {

        #region Methods_private

        private static TException CreateException<TException>(string message) where TException : Exception
            => (TException)Activator.CreateInstance(typeof(TException), message);

        #endregion

        #region ThrowIfFirst

        /// <summary>Throws an exception of type TException when <paramref name="value1"/> is greater or equal than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreaterOrEqual<TException>(int value1, string variableName1, int value2, string variableName2) where TException : Exception
        {

            if (value1 >= value2)
                throw CreateException<TException>(MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue(variableName1, variableName2));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="value1"/> is greater or equal than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreaterOrEqual(int value1, string variableName1, int value2, string variableName2)
            => ThrowIfFirstIsGreaterOrEqual<ArgumentException>(value1, variableName1, value2, variableName2);

        /// <summary>Throws an exception of type TException when <paramref name="value1"/> is greater than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreater<TException>(int value1, string variableName1, int value2, string variableName2) where TException : Exception
        {

            if (value1 > value2)
                throw CreateException<TException>(MessageCollection.FirstValueIsGreaterThanSecondValue(variableName1, variableName2));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="value1"/> is greater than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreater(int value1, string variableName1, int value2, string variableName2)
            => ThrowIfFirstIsGreater<ArgumentException>(value1, variableName1, value2, variableName2);

        #endregion

        #region ThrowIfLess

        /// <summary>Throws an exception of type TException when <paramref name="value"/> is less than one.</summary>
        public static void ThrowIfLessThanOne<TException>(uint value, string variableName) where TException : Exception
        {

            int threshold = 1;
            if (value < threshold)
                throw CreateException<TException>(MessageCollection.VariableCantBeLessThan(variableName, threshold));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name=" value"/> is less than one.</summary>
        public static void ThrowIfLessThanOne(uint value, string variableName)
            => ThrowIfLessThanOne<ArgumentException>(value, variableName);

        /// <summary>Throws an exception of type TException when <paramref name="value"/> is less than <paramref name="threshold"/>.</summary>
        public static void ThrowIfLessThan<TException>(int value, int threshold, string variableName) where TException : Exception
        {

            if (value < threshold)
                throw CreateException<TException>(MessageCollection.VariableCantBeLessThan(variableName, threshold));

        }

        /// <summary>Throws an exception of type TException when <paramref name="value"/> is less than <paramref name="threshold"/>.</summary>
        public static void ThrowIfLessThan(int value, int threshold, string variableName)
            => ThrowIfLessThan<ArgumentException>(value, threshold, variableName);

        #endregion

        #region ThrowIfModulo

        /// <summary>Throws an exception of type TException when the modulo between <paramref name="value1"/> and <paramref name="value2"/> is not zero.</summary> 
        public static void ThrowIfModuloIsNotZero<TException>(uint value1, string variableName1, uint value2, string variableName2) where TException : Exception
        {

            if (value1 % value2 != 0)
                throw CreateException<TException>(MessageCollection.DividingMustReturnWholeNumber(variableName1, variableName2));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when the modulo between <paramref name="value1"/> and <paramref name="value2"/> is not zero.</summary> 
        public static void ThrowIfModuloIsNotZero(uint value1, string variableName1, uint value2, string variableName2)
            => ThrowIfModuloIsNotZero<ArgumentException>(value1, variableName1, value2, variableName2);

        #endregion

        #region ValidateArray

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="arr"/> is null or of type <see cref="ArgumentException"/> when <paramref name="arr"/> is empty.</summary>
        public static void ValidateArray<UArray>(UArray[] arr, string variableName)
        {

            ValidateArrayNull<ArgumentNullException, UArray>(arr, variableName);
            ValidateArrayEmpty<ArgumentException, UArray>(arr, variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="arr"/> is null.</summary>
        public static void ValidateArrayNull<TException, UArray>(UArray[] arr, string variableName) where TException : Exception
        {

            if (arr == null)
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="arr"/> is empty.</summary>
        public static void ValidateArrayEmpty<TException, UArray>(UArray[] arr, string variableName) where TException : Exception
        {

            if (arr.Length == 0)
                throw CreateException<TException>(MessageCollection.VariableContainsZeroItems(variableName));

        }

        #endregion

        #region ValidateFileExistance

        /// <summary>Throws an exception of type TException when <paramref name="file"/> doesn't exist.</summary>
        public static void ValidateFileExistance<TException>(IFileInfoAdapter file) where TException : Exception
        {

            if (!file.Exists)
                throw CreateException<TException>(MessageCollection.ProvidedPathDoesntExist(file));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="file"/> doesn't exist.</summary>      
        public static void ValidateFileExistance(IFileInfoAdapter file)
            => ValidateFileExistance<ArgumentException>(file);

        #endregion

        #region ValidateLength

        /// <summary>Throws an exception of type TException when <paramref name="length"/> is less than one.</summary>
        public static void ValidateLength<TException>(uint length) where TException : Exception
        {

            int threshold = 1;
            if (length < threshold)
                throw CreateException<TException>(MessageCollection.VariableCantBeLessThan(nameof(length), threshold));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="length"/> is less than one.</summary>        
        public static void ValidateLength(uint length)
            => ValidateLength<ArgumentException>(length);

        #endregion

        #region ValidateList

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="list"/> is null or of type <see cref="ArgumentException"/> when <paramref name="list"/> is empty.</summary>
        public static void ValidateList<UList>(List<UList> list, string variableName)
        {

            ValidateListNull<ArgumentNullException, UList>(list, variableName);
            ValidateListEmpty<ArgumentException, UList>(list, variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="list"/> is null.</summary>        
        public static void ValidateListNull<TException, UList>(List<UList> list, string variableName) where TException : Exception
        {

            if (list == null)
                throw CreateException<ArgumentNullException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="list"/> is empty.</summary>        
        public static void ValidateListEmpty<T, UList>(List<UList> list, string variableName) where T : Exception
        {

            if (list.Count == 0)
                throw CreateException<ArgumentException>(MessageCollection.VariableContainsZeroItems(variableName));

        }

        #endregion

        #region ValidateObject

        /// <summary>Throws an exception of type TException when <paramref name="obj"/> is null.</summary>
        public static void ValidateObject<TException>(object obj, string variableName) where TException : Exception
        {

            if (obj == null)
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="obj"/> is null.</summary>
        public static void ValidateObject(object obj, string variableName)
            => ValidateObject<ArgumentNullException>(obj, variableName);

        #endregion
        
        #region ValidateStringNullOrWhiteSpace

        /// <summary>Throws an exception of type TException when <paramref name="str"/> is null or whitespace.</summary> 
        public static void ValidateStringNullOrWhiteSpace<TException>(string str, string variableName) where TException : Exception
        {

            if (string.IsNullOrWhiteSpace(str))
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="str"/> is null or whitespace.</summary>         
        public static void ValidateStringNullOrWhiteSpace(string str, string variableName)
            => ValidateStringNullOrWhiteSpace<ArgumentNullException>(str, variableName);

        /// <summary>Throws an exception of type TException when <paramref name="str"/> is null or empty.</summary>
        public static void ValidateStringNullOrEmpty<TException>(string str, string variableName) where TException : Exception
        {

            if (string.IsNullOrEmpty(str))
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="str"/> is null or empty.</summary>  
        public static void ValidateStringNullOrEmpty(string str, string variableName)
            => ValidateStringNullOrEmpty<ArgumentNullException>(str, variableName);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.03.2023
*/