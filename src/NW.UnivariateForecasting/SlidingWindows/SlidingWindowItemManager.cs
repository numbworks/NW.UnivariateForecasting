using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Validation;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <inheritdoc cref="ISlidingWindowItemManager"/>
    public class SlidingWindowItemManager : ISlidingWindowItemManager
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="SlidingWindowItemManager"/>.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public SlidingWindowItemManager() { }

        #endregion

        #region Methods_public

        public SlidingWindowItem CreateItem(uint id, double X_Actual, double? Y_Forecasted)
        {

            return new SlidingWindowItem()
            {
                Id = id,
                X_Actual = X_Actual,
                Y_Forecasted = Y_Forecasted

            };

        }
        public List<SlidingWindowItem> CreateItems(List<double> values)
        {

            Validator.ValidateList(values, nameof(values));

            /*

                 values = [58.50, 615.26, 659.84, 635.69, 612.27, 632.94]

                 List<SlidingWindowItem>

                     - SlidingWindowItem
                         - Id		    1
                         - X_Actual	    58.50
                         - Y_Forecasted	615.26

                     - SlidingWindowItem
                         - Id		    2
                         - X_Actual	    615.26
                         - Y_Forecasted	659.84

                     - SlidingWindowItem
                         - Id		    3
                         - X_Actual	    659.84
                         - Y_Forecasted	635.69

                     - SlidingWindowItem
                         - Id		    4
                         - X_Actual	    635.69
                         - Y_Forecasted	612.27

                     - SlidingWindowItem
                         - Id		    5
                         - X_Actual	    612.27
                         - Y_Forecasted	632.94

                     - SlidingWindowItem
                         - Id		    6
                         - X_Actual	    632.94
                         - Y_Forecasted	null

                 The item.Id should start from '1' and not from '0'.

              */

            List<SlidingWindowItem> items = new List<SlidingWindowItem>();
            for (int i = 0; i < values.Count; i++)
            {

                SlidingWindowItem item = null;
                if (i == (values.Count - 1))
                    item = CreateItem((uint)(i + 1), values[i], null);
                else
                    item = CreateItem((uint)(i + 1), values[i], values[i + 1]);

                items.Add(item);

            }

            return items;

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2023
*/
