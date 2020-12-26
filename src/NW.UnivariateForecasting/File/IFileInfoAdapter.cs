using System;
using System.IO;
using System.Runtime.Serialization;

namespace NW.UnivariateForecasting
{
    public interface IFileInfoAdapter
    {
        FileAttributes Attributes { get; set; }
        DateTime CreationTime { get; set; }
        DateTime CreationTimeUtc { get; set; }
        DirectoryInfo Directory { get; }
        string DirectoryName { get; }
        bool Exists { get; }
        string Extension { get; }
        string FullName { get; }
        bool IsReadOnly { get; set; }
        DateTime LastAccessTime { get; set; }
        DateTime LastAccessTimeUtc { get; set; }
        DateTime LastWriteTime { get; set; }
        DateTime LastWriteTimeUtc { get; set; }
        long Length { get; }
        string Name { get; }

        StreamWriter AppendText();
        FileInfo CopyTo(string destFileName);
        FileInfo CopyTo(string destFileName, bool overwrite);
        FileStream Create();
        StreamWriter CreateText();
        void Decrypt();
        void Delete();
        void Encrypt();
        void GetObjectData(SerializationInfo info, StreamingContext context);
        void MoveTo(string destFileName);
        FileStream Open(FileMode mode);
        FileStream Open(FileMode mode, FileAccess access);
        FileStream Open(FileMode mode, FileAccess access, FileShare share);
        FileStream OpenRead();
        StreamReader OpenText();
        FileStream OpenWrite();
        void Refresh();
        FileInfo Replace(string destinationFileName, string destinationBackupFileName);
        FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
        string ToString();
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.11.2020

*/