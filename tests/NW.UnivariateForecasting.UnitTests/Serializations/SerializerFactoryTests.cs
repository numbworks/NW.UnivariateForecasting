using NW.UnivariateForecasting.Forecasts;
using NW.UnivariateForecasting.Serializations;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Serializations
{
    [TestFixture]
    public class SerializerFactoryTests
    {

        #region Fields

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [Test]
        public void Create_ShouldCreateExpectedInstanceOfISerializer_WhenTypeIsLabeledExample()
        {

            // Arrange
            // Act
            ISerializer<ForecastingInit> serializer = new SerializerFactory().Create<ForecastingInit>();

            // Assert
            Assert.IsInstanceOf<ISerializer<ForecastingInit>>(serializer);

        }

        [Test]
        public void SerializerFactory_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SerializerFactory serializerFactory = new SerializerFactory();

            // Assert
            Assert.IsInstanceOf<SerializerFactory>(serializerFactory);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.02.2023
*/