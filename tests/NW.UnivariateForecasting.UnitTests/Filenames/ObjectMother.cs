using System;
using NW.UnivariateForecasting.Filenames;

namespace NW.UnivariateForecasting.UnitTests.Filenames
{
    public static class ObjectMother
    {

        #region Proprietes

        public static string FakeFilePath = @"C:\";

        public static DateTime FakeNow = new DateTime(2021, 05, 01);
        public static string FakeNowString = FakeNow.ToString(FilenameFactory.DefaultFormatNow);

        public static string Filename_SessionJson
            = string.Concat(
                        FakeFilePath,
                        FilenameFactory.DefaultMainToken,
                        "_",
                        FilenameFactory.DefaultSessionToken,
                        "_",
                        FakeNowString,
                        ".",
                        FilenameFactory.DefaultJsonExtension
                        );

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/