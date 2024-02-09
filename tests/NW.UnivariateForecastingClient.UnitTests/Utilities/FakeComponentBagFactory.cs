using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecastingClient.Shared;

namespace NW.UnivariateForecastingClient.UnitTests.Utilities
{

    public class FakeComponentBagFactory : IComponentBagFactory
    {

        #region Fields

        #endregion

        #region Properties

        private ComponentBag _fakeComponentBag;

        #endregion

        #region Constructors

        public FakeComponentBagFactory(ComponentBag fakeComponentBag)
        {

            _fakeComponentBag = fakeComponentBag;

        }

        #endregion

        #region Methods_public

        public ComponentBag Create()
                => _fakeComponentBag;

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.02.2024
*/