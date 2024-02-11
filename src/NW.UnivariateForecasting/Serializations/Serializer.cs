using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using NW.Shared.Validation;

namespace NW.UnivariateForecasting.Serializations
{
    /// <inheritdoc cref="ISerializer{T}"/>
    public class Serializer<T> : ISerializer<T>
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Serializer{T}"/> instance using default parameters.</summary>	
        public Serializer() { }

        #endregion

        #region Methods_public

        public string Serialize(T obj)
        {

            Validator.ValidateObject(obj, nameof(obj));

            string json = JsonSerializer.Serialize(obj, CreateJsonSerializerOptions());

            return json;

        }
        public T DeserializeOrDefault(string json)
        {

            try
            {

                T obj = JsonSerializer.Deserialize<T>(json, CreateJsonSerializerOptions());

                return obj;

            }
            catch
            {

                return default(T);

            }

        }

        #endregion

        #region Methods_private

        private JsonSerializerOptions CreateJsonSerializerOptions()
        {

            JsonSerializerOptions options = new JsonSerializerOptions();

            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            options.WriteIndented = true;

            return options;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/