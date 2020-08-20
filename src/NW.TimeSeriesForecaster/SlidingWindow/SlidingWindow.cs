using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
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
        {

            string content 
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id ?? "null"}'",
                    $"{nameof(ObservationName)}: '{ObservationName ?? "null"}'",
                    $"{nameof(Interval)}: '{Interval.ToString() ?? "null"}'",
                    $"{nameof(Items)}: '{Items.Count.ToString() ?? "null"}'"
                    );

            return $"[ {content} ]";

        }
        public string ToString(bool rolloutItems)
        {

            if (rolloutItems == false)
                return ToString();

            List<string> strings = new List<string>();
            strings.Add(ToString());

            if (Items != null)
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
    Last Update: 28.04.2020

*/
