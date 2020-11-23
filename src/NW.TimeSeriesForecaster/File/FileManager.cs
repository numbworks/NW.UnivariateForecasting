﻿using System;
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
        public IEnumerable<string> ReadAllLines(IFileInfoAdapter file)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(MessageCollection.ProvidedPathDoesntExist.Invoke(file));

            try
            {

                return _fileAdapter.ReadAllLines(file.FullName);

            }
            catch (Exception e)
            {

                throw new Exception(MessageCollection.NotPossibleToRead.Invoke(file, e), e);

            }

        }
        public string ReadAllText(IFileInfoAdapter file)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(MessageCollection.ProvidedPathDoesntExist.Invoke(file));

            try
            {

                return _fileAdapter.ReadAllText(file.FullName);

            }
            catch (Exception e)
            {

                throw new Exception(MessageCollection.NotPossibleToRead.Invoke(file, e), e);

            }

        }
        public void WriteAllLines(IFileInfoAdapter file, IEnumerable<string> content)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(MessageCollection.ProvidedPathDoesntExist.Invoke(file));

            try
            {

                _fileAdapter.WriteAllLines(file.FullName, content);

            }
            catch (Exception e)
            {

                throw new Exception(MessageCollection.NotPossibleToWrite.Invoke(file, e), e);

            }

        }
        public void WriteAllText(IFileInfoAdapter file, string content)
        {

            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                throw new ArgumentException(MessageCollection.ProvidedPathDoesntExist.Invoke(file));

            try
            {

                _fileAdapter.WriteAllText(file.FullName, content);

            }
            catch(Exception e)
            {

                throw new Exception(MessageCollection.NotPossibleToWrite.Invoke(file, e), e);

            }

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.11.2020

*/