using System;

namespace NW.UnivariateForecasting.Files
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.UnivariateForecasting.Files"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<IFileInfoAdapter, Exception, string> NotPossibleToRead
            = (file, e) => $"It hasn't been possible to read from the provided file: '{file.FullName}': '{e.Message}'.";
        public static Func<IFileInfoAdapter, Exception, string> NotPossibleToWrite
            = (file, e) => $"It hasn't been possible to write to the provided file: '{file.FullName}': '{e.Message}'.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/