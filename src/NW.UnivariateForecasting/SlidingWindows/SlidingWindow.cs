using System;
using System.Collections.Generic;
using NW.Shared.Validation;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <summary>A data structure representing a collection of values taken at successive equally spaced points in time.</summary>
    public class SlidingWindow
    {

        #region Fields
        #endregion

        #region Properties

        public List<SlidingWindowItem> Items { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes an <see cref="SlidingWindow"/> instance.</summary>
        public SlidingWindow(List<SlidingWindowItem> items) 
        {

            Validator.ValidateList(items, nameof(items));

            Items = items;
        
        }

        #endregion

        #region Methods_public

        public override string ToString()
            => ToString(true);

        /// <inheritdoc cref="object.ToString"/>
        public string ToString(bool rolloutItems)
        {

            // [ Items: '6' ]
            // ...

            string content = $"[ {nameof(Items)}: '{Items?.Count.ToString() ?? "null"}' ]";

            if (rolloutItems == false)
                return content;

            List<string> strings = new List<string>();
            strings.Add(content);

            foreach (SlidingWindowItem item in Items)
                strings.Add(item.ToString());

            return string.Join(
                Environment.NewLine,
                strings
                );

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 16.02.2023
*/
