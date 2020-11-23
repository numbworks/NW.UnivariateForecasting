using System.IO;

namespace NW.UnivariateForecasting
{
    public interface IFileInfoAdapter
    {
        DirectoryInfo Directory { get; }
        string DirectoryName { get; }
        bool Exists { get; }
        bool IsReadOnly { get; set; }
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
        void MoveTo(string destFileName);
        FileStream Open(FileMode mode);
        FileStream Open(FileMode mode, FileAccess access);
        FileStream Open(FileMode mode, FileAccess access, FileShare share);
        FileStream OpenRead();
        StreamReader OpenText();
        FileStream OpenWrite();
        FileInfo Replace(string destinationFileName, string destinationBackupFileName);
        FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
        string ToString();
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.11.2020

*/