using System;
using NW.UnivariateForecasting.Validation;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.UnivariateForecastingClient.ApplicationSession
{
    /// <summary>Collects all the dependencies required by <see cref="SessionManager"/>.</summary>
    public class SessionManagerComponents
    {

        #region Fields
        #endregion

        #region Properties

        public IDoubleManager DoubleManager { get; }
        public IOptionValidator MinimumAccuracyValidator { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManagerComponents"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManagerComponents(IDoubleManager doubleManager) 
        {

            Validator.ValidateObject(doubleManager, nameof(doubleManager));

            DoubleManager = doubleManager;

            MinimumAccuracyValidator = new MinimumAccuracyValidator(doubleManager);

        }

        /// <summary>Initializes a <see cref="SessionManagerComponents"/> instance using default parameters.</summary>
        public SessionManagerComponents()
            : this(new DoubleManager()) { }

        #endregion

        #region Methods_public
        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.01.2023
*/