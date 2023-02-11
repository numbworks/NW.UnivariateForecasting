using System;
using NW.UnivariateForecasting.Filenames;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Filenames
{
    [TestFixture]
    public class FilenameFactoryTests
    {

        #region Fields

        private static TestCaseData[] createMethodExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new FilenameFactory().CreateForSessionJson(folderPath: null, now: ObjectMother.FakeNow)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
            ).SetArgDisplayNames($"{nameof(createMethodExceptionTestCases)}_01")

        };
        private static TestCaseData[] createMethodTestCases =
        {

            new TestCaseData(
                    new Func<string>(
                            () => new FilenameFactory()
                                        .CreateForSessionJson(
                                            folderPath: ObjectMother.FakeFilePath,
                                            now: ObjectMother.FakeNow)
                        ),
                    ObjectMother.Filename_SessionJson
                ).SetArgDisplayNames($"{nameof(createMethodTestCases)}_01")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(createMethodExceptionTestCases))]
        public void CreateMethod_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createMethodTestCases))]
        public void CreateMethod_ShouldReturnExpectedString_WhenInvoked(Func<string> func, string expected)
        {

            // Arrange
            // Act
            string actual = func();

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void FilenameFactory_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            FilenameFactory actual = new FilenameFactory();

            // Assert
            Assert.IsInstanceOf<FilenameFactory>(actual);

            Assert.IsInstanceOf<string>(FilenameFactory.DefaultFileNameTemplate);
            Assert.IsInstanceOf<string>(FilenameFactory.DefaultFormatNow);
            Assert.IsInstanceOf<string>(FilenameFactory.DefaultJsonExtension);
            Assert.IsInstanceOf<string>(FilenameFactory.DefaultMainToken);
            Assert.IsInstanceOf<string>(FilenameFactory.DefaultSessionToken);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/
