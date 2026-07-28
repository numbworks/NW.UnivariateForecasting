using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.CLI.ArgumentParsing;

namespace NW.UnivariateForecasting.CLI.UnitTests.Utilities
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