using System.Collections.Generic;
using System.IO;

namespace NW.UnivariateForecasting
{
    public interface IFileManager
    {
        IEnumerable<string> ReadAllLines(FileInfo file);
        string ReadAllText(FileInfo file);
        void WriteAllLines(FileInfo file, IEnumerable<string> content);
        void WriteAllText(FileInfo file, string content);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/