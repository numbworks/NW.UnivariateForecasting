﻿using System;

namespace NW.UnivariateForecasting.Filenames
{
    /// <summary>Collects all the methods related to create filenames for this library.</summary>
    public interface IFilenameFactory
    {

        /// <summary>
        /// Returns a dated filename based on <paramref name="folderPath"/> and <see cref="FilenameFactory.DefaultSessionToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>        
        string CreateForSessionJson(string folderPath, DateTime now);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/