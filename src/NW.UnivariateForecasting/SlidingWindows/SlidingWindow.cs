using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;

namespace NW.UnivariateForecasting.SlidingWindows
{
    /// <summary>
    /// A data structure representing a collection of values taken at successive equally spaced points in time.
    /// </summary>
    public class SlidingWindow
    {

        // Fields
        // Properties
        public string Id { get; set; }
        public string ObservationName { get; set; }
        public Interval Interval { get; set; }
        public List<SlidingWindowItem> Items { get; set; }

        // Constructors
        public SlidingWindow() { }

        // Methods
        public override string ToString()
            => ToString(true);
        public string ToString(bool rolloutItems)
        {

            // [ Id: 'null', ObservationName: 'null', Interval: 'null', Items: 'null' ]
            // [ Id: 'SW20200906090516', ObservationName: 'Total Monthly Sales USD', Interval: '6:Months:20190131:20190731:20190831:1:6', Items: '6' ]
            // ...

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id ?? "null"}'",
                    $"{nameof(ObservationName)}: '{ObservationName ?? "null"}'",
                    $"{nameof(Interval)}: '{Interval?.ToString() ?? "null"}'",
                    $"{nameof(Items)}: '{Items?.Count.ToString() ?? "null"}'"
                    );
            content = $"[ {content} ]";

            if (rolloutItems == false || Items == null || Items?.Count == 0)
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

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.09.2020

*/
