using System;
using System.Collections.Generic;
using System.Text;

namespace NW.UnivariateForecasting.UnitTests
{
    public class FakeFileAdapter : IFileAdapter
    {

        // Fields
        private Func<string[]> _fakeReadAllLines;
        private Func<string> _fakeReadAllText;
        private Action _fakeWriteAllLines;
        private Action _fakeWriteAllText;

        // Properties
        // Constructors
        public FakeFileAdapter(
                Func<string[]> fakeReadAllLines = null,
                Func<string> fakeReadAllText = null,
                Action fakeWriteAllLines = null,
                Action fakeWriteAllText = null
            )
        {

            _fakeReadAllLines = fakeReadAllLines;
            _fakeReadAllText = fakeReadAllText;
            _fakeWriteAllLines = fakeWriteAllLines;
            _fakeWriteAllText = fakeWriteAllText;

        }

        // Methods (public)
        public string[] ReadAllLines(string path)
        {

            if (_fakeReadAllLines != null)
                _fakeReadAllLines.Invoke();

            throw new NotImplementedException();

        }
        public string ReadAllText(string path)
        {

            if (_fakeReadAllText != null)
                _fakeReadAllText.Invoke();

            throw new NotImplementedException();

        }
        public void WriteAllLines(string path, IEnumerable<string> contents)
        {

            if (_fakeWriteAllLines != null)
                _fakeWriteAllLines.Invoke();

            throw new NotImplementedException();

        }
        public void WriteAllText(string path, string contents)
        {

            if (_fakeWriteAllText != null)
                _fakeWriteAllText.Invoke();

            throw new NotImplementedException();

        }

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
    Last Update: 23.11.2020

*/
