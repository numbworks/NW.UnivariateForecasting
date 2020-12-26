using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NW.UnivariateForecasting.UnitTests
{
    public class FakeFileAdapterWithInternalMemory : IFileAdapter
    {

        // Fields
        private string[] _AllLines;
        private string _AllText;

        // Properties
        // Constructors
        public FakeFileAdapterWithInternalMemory() { }

        // Methods (public)
        public string[] ReadAllLines(string path)
            => _AllLines;
        public string ReadAllText(string path)
            => _AllText;
        public void WriteAllLines(string path, IEnumerable<string> contents)
            => _AllLines = contents.ToArray();
        public void WriteAllText(string path, string contents)
            => _AllText = contents;

        public void AppendAllLines(string path, IEnumerable<string> contents)
            => throw new NotImplementedException();
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => throw new NotImplementedException();
        public void AppendAllText(string path, string contents)
            => throw new NotImplementedException();
        public void AppendAllText(string path, string contents, Encoding encoding)
            => throw new NotImplementedException();
        public string[] ReadAllLines(string path, Encoding encoding)
            => throw new NotImplementedException();
        public string ReadAllText(string path, Encoding encoding)
            => throw new NotImplementedException();
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => throw new NotImplementedException();
        public void WriteAllText(string path, string contents, Encoding encoding)
            => throw new NotImplementedException();

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.12.2020

*/