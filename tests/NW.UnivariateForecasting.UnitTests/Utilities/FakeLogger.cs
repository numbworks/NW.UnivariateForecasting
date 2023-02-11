using System.Collections.Generic;

namespace NW.UnivariateForecasting.UnitTests.Utilities
{
    public class FakeLogger
    {

        #region Fields

        public List<string> Messages { get; }

        #endregion

        #region Properties
        #endregion

        #region Constructors

        public FakeLogger()
        {

            Messages = new List<string>();

        }

        #endregion

        #region Methods_public

        public void Log(string message)
            => Messages.Add(message);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/