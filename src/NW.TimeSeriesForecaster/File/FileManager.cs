using System;
using System.IO;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public class FileManager : IFileManager
    {

        // Fields
        private IFileAdapter _fileAdapter;

        // Properties
        // Constructors
        public FileManager(IFileAdapter fileAdapter)
        {

            if (fileAdapter == null)
                throw new ArgumentNullException(nameof(fileAdapter));

            _fileAdapter = fileAdapter;

        }

        // Methods (public)
        public IEnumerable<string> ReadAllLines(FileInfo file)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(ProvidedPathDoesntExist.Invoke(file));

            try
            {

                return _fileAdapter.ReadAllLines(file.FullName);

            }
            catch (Exception e)
            {

                throw new Exception(NotPossibleToRead.Invoke(file), e);

            }

        }
        public string ReadAllText(FileInfo file)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(ProvidedPathDoesntExist.Invoke(file));

            try
            {

                return _fileAdapter.ReadAllText(file.FullName);

            }
            catch (Exception e)
            {

                throw new Exception(NotPossibleToRead.Invoke(file), e);

            }

        }
        public void WriteAllLines(FileInfo file, IEnumerable<string> content)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(ProvidedPathDoesntExist.Invoke(file));

            try
            {

                _fileAdapter.WriteAllLines(file.FullName, content);

            }
            catch (Exception e)
            {

                throw new Exception(NotPossibleToWrite.Invoke(file), e);

            }

        }
        public void WriteAllText(FileInfo file, string content)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(ProvidedPathDoesntExist.Invoke(file));

            try
            {

                _fileAdapter.WriteAllText(file.FullName, content);

            }
            catch(Exception e)
            {

                throw new Exception(NotPossibleToWrite.Invoke(file), e);

            }

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.10.2020

*/