using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public interface IFileManager
    {
        IEnumerable<string> ReadAllLines(IFileInfoAdapter file);
        string ReadAllText(IFileInfoAdapter file);
        void WriteAllLines(IFileInfoAdapter file, IEnumerable<string> content);
        void WriteAllText(IFileInfoAdapter file, string content);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.11.2020

*/