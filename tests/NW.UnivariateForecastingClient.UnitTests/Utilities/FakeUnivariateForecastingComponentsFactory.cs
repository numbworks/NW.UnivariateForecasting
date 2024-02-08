using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecastingClient.Shared;

namespace NW.UnivariateForecastingClient.UnitTests.Utilities
{

    public class FakeUnivariateForecastingComponentsFactory : IUnivariateForecastingComponentsFactory
    {

        #region Fields

        #endregion

        #region Properties

        private UnivariateForecastingComponents _fakeComponents;

        #endregion

        #region Constructors

        public FakeUnivariateForecastingComponentsFactory(UnivariateForecastingComponents fakeComponents)
        {

            _fakeComponents = fakeComponents;

        }

        #endregion

        #region Methods_public

        public UnivariateForecastingComponents Create()
                => _fakeComponents;

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.03.2023
*/