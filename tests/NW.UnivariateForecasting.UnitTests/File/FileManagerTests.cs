using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class FileManagerTests
    {

        // Fields
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
                        () => new FileManager().ReadAllLines(ObjectMother.FileInfoAdapter_DoesntExist)
                    ),
                typeof(ArgumentException),
                MessageCollection.Validator_ProvidedPathDoesntExist.Invoke(ObjectMother.FileInfoAdapter_DoesntExist)
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_ReadAllMethodsThrowIOException)
                                    .ReadAllLines(ObjectMother.FileInfoAdapter_Exists)
                    ),
                typeof(Exception),
                MessageCollection.FileManager_NotPossibleToRead.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
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
                        () => new FileManager().ReadAllText(ObjectMother.FileInfoAdapter_DoesntExist)
                    ),
                typeof(ArgumentException),
                MessageCollection.Validator_ProvidedPathDoesntExist.Invoke(ObjectMother.FileInfoAdapter_DoesntExist)
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_ReadAllMethodsThrowIOException)
                                    .ReadAllText(ObjectMother.FileInfoAdapter_Exists)
                    ),
                typeof(Exception),
                MessageCollection.FileManager_NotPossibleToRead.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_03")

        };
        private static TestCaseData[] writeAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllLines(null, ObjectMother.Content_MultipleLines)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(writeAllLinesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_WriteAllMethodsThrowIOException)
                                    .WriteAllLines(
                                        ObjectMother.FileInfoAdapter_Exists,
                                        ObjectMother.Content_MultipleLines)
                    ),
                typeof(Exception),
                MessageCollection.FileManager_NotPossibleToWrite.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                ).SetArgDisplayNames($"{nameof(writeAllLinesExceptionTestCases)}_02")

        };
        private static TestCaseData[] writeAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllText(null, ObjectMother.Content_SingleLine)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(writeAllTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_WriteAllMethodsThrowIOException)
                                    .WriteAllText(
                                        ObjectMother.FileInfoAdapter_Exists,
                                        ObjectMother.Content_SingleLine)
                    ),
                typeof(Exception),
                MessageCollection.FileManager_NotPossibleToWrite.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                ).SetArgDisplayNames($"{nameof(writeAllTextExceptionTestCases)}_02")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(fileManagerExceptionTestCases))]
        public void FileManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(readAllLinesExceptionTestCases))]
        public void ReadAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(readAllTextExceptionTestCases))]
        public void ReadAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(writeAllLinesExceptionTestCases))]
        public void WriteAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(writeAllTextExceptionTestCases))]
        public void WriteAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ReadAllLines_ShouldReturnStrings_WhenFileExists()
        {

            // Arrange
            // Act
            IEnumerable<string> actual
                = new FileManager(ObjectMother.FileAdapter_AllMethodsWork)
                            .ReadAllLines(ObjectMother.FileInfoAdapter_Exists);

            // Assert
            Assert.AreEqual(ObjectMother.Content_MultipleLines, actual);

        }

        [Test]
        public void ReadAllText_ShouldReturnString_WhenFileExists()
        {

            // Arrange
            // Act
            string actual
                = new FileManager(ObjectMother.FileAdapter_AllMethodsWork)
                            .ReadAllText(ObjectMother.FileInfoAdapter_Exists);

            // Assert
            Assert.AreEqual(ObjectMother.Content_SingleLine, actual);

        }

        [Test]
        public void WriteAllLines_ShouldSuccessfullyWriteToFile_WhenNoIOIssuesArise()
        {

            // Arrange
            // Act
            // Assert
            try
            {

                new FileManager(ObjectMother.FileAdapter_AllMethodsWork)
                        .WriteAllLines(ObjectMother.FileInfoAdapter_Exists, ObjectMother.Content_MultipleLines);
                Assert.IsTrue(true);

            }
            catch
            {

                Assert.IsFalse(false);

            }

        }

        [Test]
        public void WriteAllText_ShouldSuccessfullyWriteToFile_WhenNoIOIssuesArise()
        {

            // Arrange
            // Act
            // Assert
            try
            {

                new FileManager(ObjectMother.FileAdapter_AllMethodsWork)
                        .WriteAllText(ObjectMother.FileInfoAdapter_Exists, ObjectMother.Content_SingleLine);
                Assert.IsTrue(true);

            }
            catch
            {

                Assert.IsFalse(false);

            }

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 28.04.2021

*/
