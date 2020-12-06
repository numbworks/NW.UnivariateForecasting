using System.Collections.Generic;
using System.IO;

namespace NW.UnivariateForecasting
{
    public interface IFileManager
    {
        IEnumerable<string> ReadAllLines(IFileInfoAdapter file);
        string ReadAllText(IFileInfoAdapter file);
        void WriteAllLines(IFileInfoAdapter file, IEnumerable<string> content);
        void WriteAllText(IFileInfoAdapter file, string content);
        FileInfoAdapter Create(string filePath);
        FileInfoAdapter Create(FileInfo fileInfo);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.12.2020

*/
