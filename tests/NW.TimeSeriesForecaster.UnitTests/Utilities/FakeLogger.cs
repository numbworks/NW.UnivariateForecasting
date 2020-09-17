using System.Collections.Generic;

namespace NW.UnivariateForecasting.UnitTests
{
    public class FakeLogger
    {

        // Fields
        // Properties
        public List<string> Messages { get; }

        // Constructors
        public FakeLogger()
        {

            Messages = new List<string>();

        }

        // Methods (public)
        public void Log(string message)
            => Messages.Add(message);

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 17.09.2020

*/
