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
        public void Serialize_ShouldReturnExpectedString_WhenForecastingInitWithInitCE()
        {

            // Arrange
            ForecastingInit obj = Forecasts.ObjectMother.ForecastingInit_WithInitCE;
            string expected = Forecasts.ObjectMother.ForecastingInitWithInitCEAsJson_Content;

            // Act
            string actual = new Serializer<ForecastingInit>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        // 

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionSingleWithInitCE()
        {

            // Arrange
            ForecastingSession obj = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_WithInitCE,
                    observations: Observations.ObjectMother.Observations_Containing01_WithInitCE,
                    steps: Forecasts.ObjectMother.ForecastingSession_Single_Steps,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            string expected = Properties.Resources.ForecastingSessionSingleWithInitCEAsJson;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenForecastingSessionSingleWithoutInitCE()
        {

            // Arrange
            ForecastingSession obj = new ForecastingSession(
                    init: Forecasts.ObjectMother.ForecastingInit_WithoutInitCE,
                    observations: Observations.ObjectMother.Observations_Containing01_WithoutInitCE,
                    steps: Forecasts.ObjectMother.ForecastingSession_Single_Steps,
                    version: Forecasts.ObjectMother.ForecastingSession_Version
                );

            string expected = Properties.Resources.ForecastingSessionSingleWithoutInitCEAsJson;

            // Act
            string actual = new Serializer<ForecastingSession>().Serialize(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        //

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
            string json = Forecasts.ObjectMother.ForecastingInitWithInitCEAsJson_Content;
            ForecastingInit expected = Forecasts.ObjectMother.ForecastingInit_WithInitCE;

            // Act
            ForecastingInit actual = new Serializer<ForecastingInit>().DeserializeOrDefault(json: json);

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
    Last Update: 18.02.2023
*/
