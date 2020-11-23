using System;
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
                )

        };
        private static TestCaseData[] readAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate( 
                        () => new FileManager().ReadAllLines(null) 
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllLines(ObjectMother.FileInfoAdapter_DoesntExist)
                    ),
                typeof(ArgumentException),
                MessageCollection.ProvidedPathDoesntExist.Invoke(ObjectMother.FileInfoAdapter_DoesntExist)
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_ReadAllMethodsThrowIOException)
                                    .ReadAllLines(ObjectMother.FileInfoAdapter_Exists)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToRead.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                )

        };
        private static TestCaseData[] readAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllText(null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllText(ObjectMother.FileInfoAdapter_DoesntExist)
                    ),
                typeof(ArgumentException),
                MessageCollection.ProvidedPathDoesntExist.Invoke(ObjectMother.FileInfoAdapter_DoesntExist)
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_ReadAllMethodsThrowIOException)
                                    .ReadAllText(ObjectMother.FileInfoAdapter_Exists)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToRead.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                )

        };
        private static TestCaseData[] writeAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllLines(null, ObjectMother.Content_MultipleLines)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllLines(
                                    ObjectMother.FileInfoAdapter_DoesntExist, 
                                    ObjectMother.Content_MultipleLines)
                    ),
                typeof(ArgumentException),
                MessageCollection.ProvidedPathDoesntExist.Invoke(ObjectMother.FileInfoAdapter_DoesntExist)
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_WriteAllMethodsThrowIOException)
                                    .WriteAllLines(
                                        ObjectMother.FileInfoAdapter_Exists,
                                        ObjectMother.Content_MultipleLines)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToWrite.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                )

        };
        private static TestCaseData[] writeAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllText(null, ObjectMother.Content_SingleLine)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllText(
                                    ObjectMother.FileInfoAdapter_DoesntExist,
                                    ObjectMother.Content_SingleLine)
                    ),
                typeof(ArgumentException),
                MessageCollection.ProvidedPathDoesntExist.Invoke(ObjectMother.FileInfoAdapter_DoesntExist)
                ),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapter_WriteAllMethodsThrowIOException)
                                    .WriteAllText(
                                        ObjectMother.FileInfoAdapter_Exists,
                                        ObjectMother.Content_SingleLine)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToWrite.Invoke(
                                    ObjectMother.FileInfoAdapter_Exists,
                                    ObjectMother.FileAdapter_IOException)
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(fileManagerExceptionTestCases))]
        public void FileManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expected, string nessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expected, del);
            Assert.AreEqual(nessage, actual.Message);

        }

        [TestCaseSource(nameof(readAllLinesExceptionTestCases))]
        public void ReadAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expected, string message)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expected, del);
            Assert.AreEqual(message, actual.Message);

        }

        [TestCaseSource(nameof(readAllTextExceptionTestCases))]
        public void ReadAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expected, string message)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expected, del);
            Assert.AreEqual(message, actual.Message);

        }

        [TestCaseSource(nameof(writeAllLinesExceptionTestCases))]
        public void WriteAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expected, string message)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expected, del);
            Assert.AreEqual(message, actual.Message);

        }

        [TestCaseSource(nameof(writeAllTextExceptionTestCases))]
        public void WriteAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expected, string message)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expected, del);
            Assert.AreEqual(message, actual.Message);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 23.11.2020

*/
