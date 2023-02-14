using System;
using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Serializations;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Serializations
{
    [TestFixture]
    public class SerializerTests
    {

        #region Fields

        private static TestCaseData[] serializeExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new Serializer<ForecastingInit>().Serialize(obj: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("obj").Message
                ).SetArgDisplayNames($"{nameof(serializeExceptionTestCases)}_01")

        };
        private static TestCaseData[] deserializeOrDefaultWhenUnproperArgumentTestCases =
        {

            new TestCaseData(
                    "Unproper Json content"
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_01"),

            new TestCaseData(
                    string.Empty
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_02"),

            new TestCaseData(
                    null
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_03"),

            new TestCaseData(
                    "[]"
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_04")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(serializeExceptionTestCases))]
        public void Serialize_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingInit()
        {

            // Arrange
            ForecastingInit obj = Forecasts.ObjectMother.ForecastingInit;
            string expected = Forecasts.ObjectMother.ForecastingInitAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingInit>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingInitMinimal()
        {

            // Arrange
            ForecastingInit obj = Forecasts.ObjectMother.ForecastingInit_Minimal;
            string expected = Forecasts.ObjectMother.ForecastingInitMinimalAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingInit>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Ignore("")]
        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSession()
        {

            // Arrange
            ForecastingSession obj = Forecasts.ObjectMother.ForecastingSession_Single;
            string expected = Forecasts.ObjectMother.ForecastingSessionSingleAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Ignore("")]
        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionMinimal()
        {

            // Arrange
            ForecastingSession obj = Forecasts.ObjectMother.ForecastingSession_SingleMinimal;
            string expected = Forecasts.ObjectMother.ForecastingSessionSingleMinimalAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Ignore("")]
        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionMultiple()
        {

            // Arrange
            ForecastingSession obj = Forecasts.ObjectMother.ForecastingSession_Multiple;
            string expected = Forecasts.ObjectMother.ForecastingSessionMultipleAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(deserializeOrDefaultWhenUnproperArgumentTestCases))]
        public void DeserializeOrDefault_ShouldReturnDefault_WhenTypeIsForecastingInit(string json)
        {

            // Arrange
            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

            // Assert
            Assert.AreEqual(default(ForecastingInit), actual);

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingInit()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingInitAsJson_Content;
            ForecastingInit expected = Forecasts.ObjectMother.ForecastingInit;

            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingInitMinimal()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingInitMinimalAsJson_Content;
            ForecastingInit expected = Forecasts.ObjectMother.ForecastingInit_Minimal;

            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Ignore("")]
        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingSessionSingle()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingSessionSingleAsJson_Content;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_Single;

            // Act
            ForecastingSession actual = new Serializer<ForecastingSession>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Ignore("")]
        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingSessionSingleMinimal()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingSessionSingleMinimalAsJson_Content;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_SingleMinimal;

            // Act
            ForecastingSession actual = new Serializer<ForecastingSession>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Ignore("")]
        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingSessionMultiple()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingSessionMultipleAsJson_Content;
            ForecastingSession expected = Forecasts.ObjectMother.ForecastingSession_Multiple;

            // Act
            ForecastingSession actual = new Serializer<ForecastingSession>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
