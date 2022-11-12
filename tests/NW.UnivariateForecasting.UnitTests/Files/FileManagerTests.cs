using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NW.UnivariateForecasting.Files;
using NW.UnivariateForecasting.Messages;

namespace NW.UnivariateForecasting.UnitTests
{

    [TestFixture]
    public class FileManagerTests
    {

        #region Fields

        private static TestCaseData[] fileManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate( () => new FileManager(null) ),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileAdapter").Message
                ).SetArgDisplayNames($"{nameof(fileManagerExceptionTestCases)}_01"),

        };
        private static TestCaseData[] readAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllLines(null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllLines(ObjectMother.FileManager_FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                Messages.MessageCollection.Validator_ProvidedPathDoesntExist.Invoke(ObjectMother.FileManager_FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileManager_FileAdapterReadAllMethodsThrowIOException)
                                    .ReadAllLines(ObjectMother.FileManager_FileInfoAdapterExists)
                    ),
                typeof(Exception),
                Files.MessageCollection.NotPossibleToRead.Invoke(
                                    ObjectMother.FileManager_FileInfoAdapterExists,
                                    ObjectMother.FileManager_FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_03")

        };
        private static TestCaseData[] readAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllText(null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllText(ObjectMother.FileManager_FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                Messages.MessageCollection.Validator_ProvidedPathDoesntExist.Invoke(ObjectMother.FileManager_FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileManager_FileAdapterReadAllMethodsThrowIOException)
                                    .ReadAllText(ObjectMother.FileManager_FileInfoAdapterExists)
                    ),
                typeof(Exception),
                Files.MessageCollection.NotPossibleToRead.Invoke(
                                    ObjectMother.FileManager_FileInfoAdapterExists,
                                    ObjectMother.FileManager_FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_03")

        };
        private static TestCaseData[] writeAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllLines(null, ObjectMother.FileManager_ContentMultipleLines)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(writeAllLinesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileManager_FileAdapterWriteAllMethodsThrowIOException)
                                    .WriteAllLines(
                                        ObjectMother.FileManager_FileInfoAdapterExists,
                                        ObjectMother.FileManager_ContentMultipleLines)
                    ),
                typeof(Exception),
                Files.MessageCollection.NotPossibleToWrite.Invoke(
                                    ObjectMother.FileManager_FileInfoAdapterExists,
                                    ObjectMother.FileManager_FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(writeAllLinesExceptionTestCases)}_02")

        };
        private static TestCaseData[] writeAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllText(null, ObjectMother.FileManager_ContentSingleLine)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(writeAllTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileManager_FileAdapterWriteAllMethodsThrowIOException)
                                    .WriteAllText(
                                        ObjectMother.FileManager_FileInfoAdapterExists,
                                        ObjectMother.FileManager_ContentSingleLine)
                    ),
                typeof(Exception),
                Files.MessageCollection.NotPossibleToWrite.Invoke(
                                    ObjectMother.FileManager_FileInfoAdapterExists,
                                    ObjectMother.FileManager_FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(writeAllTextExceptionTestCases)}_02")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().Create((string)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("filePath").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().Create((FileInfo)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfo").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(fileManagerExceptionTestCases))]
        public void FileManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(readAllLinesExceptionTestCases))]
        public void ReadAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void ReadAllLines_ShouldReturnStrings_WhenFileExists()
        {

            // Arrange
            // Act
            IEnumerable<string> actual
                = new FileManager(ObjectMother.FileManager_FileAdapterAllMethodsWork)
                            .ReadAllLines(ObjectMother.FileManager_FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(ObjectMother.FileManager_ContentMultipleLines, actual);

        }

        [TestCaseSource(nameof(readAllTextExceptionTestCases))]
        public void ReadAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void ReadAllText_ShouldReturnString_WhenFileExists()
        {

            // Arrange
            // Act
            string actual
                = new FileManager(ObjectMother.FileManager_FileAdapterAllMethodsWork)
                            .ReadAllText(ObjectMother.FileManager_FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(ObjectMother.FileManager_ContentSingleLine, actual);

        }

        [TestCaseSource(nameof(writeAllLinesExceptionTestCases))]
        public void WriteAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void WriteAllLines_ShouldSuccessfullyWriteToFile_WhenNoIOIssuesArise()
        {

            // Arrange
            // Act
            // Assert
            try
            {

                new FileManager(ObjectMother.FileManager_FileAdapterAllMethodsWork)
                        .WriteAllLines(ObjectMother.FileManager_FileInfoAdapterExists, ObjectMother.FileManager_ContentMultipleLines);
                Assert.IsTrue(true);

            }
            catch
            {

                Assert.IsFalse(false);

            }

        }

        [TestCaseSource(nameof(writeAllTextExceptionTestCases))]
        public void WriteAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void WriteAllText_ShouldSuccessfullyWriteToFile_WhenNoIOIssuesArise()
        {

            // Arrange
            // Act
            // Assert
            try
            {

                new FileManager(ObjectMother.FileManager_FileAdapterAllMethodsWork)
                        .WriteAllText(ObjectMother.FileManager_FileInfoAdapterExists, ObjectMother.FileManager_ContentSingleLine);
                Assert.IsTrue(true);

            }
            catch
            {

                Assert.IsFalse(false);

            }

        }

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void Create_ShouldRetunAIFileInfoAdapterObject_WhenInvoked()
        {

            // Arrange
            // Act
            IFileInfoAdapter actual = new FileManager().Create(@"J:\");

            // Assert
            Assert.IsInstanceOf<IFileInfoAdapter>(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }

}
/*
    Author: numbworks@gmail.com
    Last Update: 12.11.2022
*/
