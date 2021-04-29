using System;
using System.Collections.Generic;
using System.IO;

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

            Validator.ValidateObject(fileAdapter, nameof(fileAdapter));

            _fileAdapter = fileAdapter;

        }
        public FileManager()
        {

            _fileAdapter = new FileAdapter();

        }

        // Methods (public)
        public IEnumerable<string> ReadAllLines(IFileInfoAdapter file)
        {

            Validator.ValidateObject(file, nameof(file));
            Validator.ValidateFileExistance(file);

            try
            {

                return _fileAdapter.ReadAllLines(file.FullName);

            }
            catch (Exception e)
            {

                throw new Exception(MessageCollection.FileManager_NotPossibleToRead.Invoke(file, e), e);

            }

        }
        public string ReadAllText(IFileInfoAdapter file)
        {

            Validator.ValidateObject(file, nameof(file));
            Validator.ValidateFileExistance(file);

            try
            {

                return _fileAdapter.ReadAllText(file.FullName);

            }
            catch (Exception e)
            {

                throw new Exception(MessageCollection.FileManager_NotPossibleToRead.Invoke(file, e), e);

            }

        }
        public void WriteAllLines(IFileInfoAdapter file, IEnumerable<string> content)
        {

            Validator.ValidateObject(file, nameof(file));

            try
            {

                _fileAdapter.WriteAllLines(file.FullName, content);

            }
            catch (Exception e)
            {

                throw new Exception(MessageCollection.FileManager_NotPossibleToWrite.Invoke(file, e), e);

            }

        }
        public void WriteAllText(IFileInfoAdapter file, string content)
        {

            Validator.ValidateObject(file, nameof(file));

            try
            {

                _fileAdapter.WriteAllText(file.FullName, content);

            }
            catch(Exception e)
            {

                throw new Exception(MessageCollection.FileManager_NotPossibleToWrite.Invoke(file, e), e);

            }

        }
        public FileInfoAdapter Create(string filePath)
            => Create(new FileInfo(filePath));
        public FileInfoAdapter Create(FileInfo fileInfo)
            => new FileInfoAdapter(fileInfo);

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2021

*/
