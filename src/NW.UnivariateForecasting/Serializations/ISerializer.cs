using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting.Serializations
{
    /// <summary>A general purpose serializer for this library.</summary>
    public interface ISerializer<T>
    {

        /// <summary>
        /// Serializes the provided object of type T to a Json string. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string Serialize(T obj);

        /// <summary>
        /// Deserializes the provided Json string to an object of type T. 
        /// <para>If <paramref name="json"/> is null/empty/invalid or an exception is thrown, default of T will be returned.</para>
        /// </summary>
        T DeserializeOrDefault(string json);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/