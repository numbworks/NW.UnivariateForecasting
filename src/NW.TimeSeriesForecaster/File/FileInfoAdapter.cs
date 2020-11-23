﻿using System;
using System.IO;

namespace NW.UnivariateForecasting
{
    public class FileInfoAdapter : IFileInfoAdapter
    {

        // Fields
        private FileInfo _fileInfo;

        // Properties
        public bool IsReadOnly {
            get { return _fileInfo.IsReadOnly; }
            set { _fileInfo.IsReadOnly = value; }
        }
        public bool Exists {
            get { return _fileInfo.Exists; }
        }
        public string DirectoryName {
            get { return _fileInfo.DirectoryName; }
        }
        public DirectoryInfo Directory {
            get { return _fileInfo.Directory; }
        }
        public long Length {
            get { return _fileInfo.Length; }
        }
        public string Name {
            get { return _fileInfo.Name; }
        }

        // Constructors
        public FileInfoAdapter(FileInfo fileInfo)
        {

            if (fileInfo == null)
                throw new ArgumentNullException(nameof(fileInfo));

            _fileInfo = fileInfo;

        }
        public FileInfoAdapter(string fileName)
        {

            _fileInfo = new FileInfo(fileName);

        }

        // Methods (public)
        public StreamWriter AppendText()
            => _fileInfo.AppendText();
        public FileInfo CopyTo(string destFileName)
            => _fileInfo.CopyTo(destFileName);
        public FileInfo CopyTo(string destFileName, bool overwrite)
            => _fileInfo.CopyTo(destFileName, overwrite);
        public FileStream Create()
            => _fileInfo.Create();
        public StreamWriter CreateText()
            => _fileInfo.CreateText();
        public void Decrypt()
            => _fileInfo.Decrypt();
        public void Delete()
            => _fileInfo.Delete();
        public void Encrypt()
            => _fileInfo.Encrypt();
        public void MoveTo(string destFileName)
            => _fileInfo.MoveTo(destFileName);
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
            => _fileInfo.Open(mode, access, share);
        public FileStream Open(FileMode mode, FileAccess access)
            => _fileInfo.Open(mode, access);
        public FileStream Open(FileMode mode)
            => _fileInfo.Open(mode);
        public FileStream OpenRead()
            => _fileInfo.OpenRead();
        public StreamReader OpenText()
            => _fileInfo.OpenText();
        public FileStream OpenWrite()
            => _fileInfo.OpenWrite();
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
            => _fileInfo.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
            => _fileInfo.Replace(destinationFileName, destinationBackupFileName);
        public override string ToString()
            => _fileInfo.ToString();

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.11.2020

*/