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
        public void Serialize_ShouldReturnExpectedString_WhenForecastingInitWithCE()
        {

            // Arrange
            ForecastingInit obj = Forecasts.ObjectMother.ForecastingInit_SingleWithCE;
            string expected = Forecasts.ObjectMother.ForecastingInitSingleWithCEAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingInit>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingInitWithoutCE()
        {

            // Arrange
            ForecastingInit obj = Forecasts.ObjectMother.ForecastingInit_SingleWithoutCE;
            string expected = Forecasts.ObjectMother.ForecastingInitSingleWithoutCEAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingInit>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingInitBareMinimum()
        {

            // Arrange
            ForecastingInit obj = Forecasts.ObjectMother.ForecastingInit_BareMinimum;
            string expected = Forecasts.ObjectMother.ForecastingInitBareMinimumAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingInit>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionSingleWithCE()
        {

            // Arrange
            ForecastingSession obj = Forecasts.ObjectMother.ForecastingSession_SingleWithCE;
            string expected = Forecasts.ObjectMother.ForecastingSessionSingleWithCEAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionSingleWithoutCE()
        {

            // Arrange
            ForecastingSession obj = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_SingleWithoutCE,
                    observations: Observations.ObjectMother.Observations_Containing01_WithoutInitCE,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            string expected = Properties.Resources.ForecastingSessionSingleWithoutCEAsJson;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionDoubleWithCE()
        {

            // Arrange
            ForecastingSession obj = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_DoubleWithCE,
                    observations: Observations.ObjectMother.Observations_Containing0102_WithInitCE,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            string expected = Properties.Resources.ForecastingSessionDoubleWithCEAsJson;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        
        [TestCaseSource(nameof(deserializeOrDefaultWhenUnproperArgumentTestCases))]
        public void DeserializeOrDefault_ShouldReturnDefault_WhenWhenUnproperArgument(string json)
        {

            // Arrange
            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json); // The type doesn't matter.

            // Assert
            Assert.AreEqual(default(ForecastingInit), actual);

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingInitWithCE()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingInitSingleWithCEAsJson_Content;
            ForecastingInit expected = Forecasts.ObjectMother.ForecastingInit_SingleWithCE;

            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingInitWithoutCE()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingInitSingleWithoutCEAsJson_Content;
            ForecastingInit expected = Forecasts.ObjectMother.ForecastingInit_SingleWithoutCE;

            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingInitBareMinimum()
        {

            // Arrange
            string json = Forecasts.ObjectMother.ForecastingInitBareMinimumAsJson_Content;
            ForecastingInit expected = Forecasts.ObjectMother.ForecastingInit_BareMinimum;

            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingSessionSingleWithCE()
        {

            // Arrange
            string json = Properties.Resources.ForecastingSessionSingleWithCEAsJson;

            ForecastingSession expected = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_SingleWithCE,
                    observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            // Act
            ForecastingSession actual = new Serializer<ForecastingSession>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingSessionSingleWithoutCE()
        {

            // Arrange
            string json = Properties.Resources.ForecastingSessionSingleWithoutCEAsJson;

            ForecastingSession expected = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_SingleWithoutCE,
                    observations: Observations.ObjectMother.Observations_Containing01_WithoutInitCE,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            // Act
            ForecastingSession actual = new Serializer<ForecastingSession>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenForecastingSessionDoubleWithCE()
        {

            // Arrange
            string json = Properties.Resources.ForecastingSessionDoubleWithCEAsJson;

            ForecastingSession expected = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_DoubleWithCE,
                    observations: Observations.ObjectMother.Observations_Containing0102_WithInitCE,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            // Act
            ForecastingSession actual = new Serializer<ForecastingSession>().DeserializeOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    Forecasts.ObjectMother.AreEqual(expected, actual)
                );

        }


        [Test]
        public void Serializer_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Serializer<ForecastingInit> actual = new Serializer<ForecastingInit>();

            // Assert
            Assert.IsInstanceOf<Serializer<ForecastingInit>>(actual);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.03.2023
*/
