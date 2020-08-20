using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public class SlidingWindow
    {

        // Fields
        // Properties
        public string Id { get; }
        public string ObservationName { get; }
        public Interval Interval { get; }
        public List<SlidingWindowItem> Items { get; }

        // Constructors
        public SlidingWindow(
            string id,
            string observationName,
            Interval interval,
            List<SlidingWindowItem> items
            )
        {

            if (string.IsNullOrWhiteSpace(id))
                throw new Exception(MessageCollection.VariableCantBeEmptyOrNull.Invoke(nameof(id)));
            if (string.IsNullOrWhiteSpace(observationName))
                throw new Exception(MessageCollection.VariableCantBeEmptyOrNull.Invoke(nameof(ObservationName)));
            if (interval == null)
                throw new ArgumentNullException(nameof(interval));
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items.Count == 0)
                throw new Exception(MessageCollection.VariableContainsZeroItems.Invoke(nameof(items)));
            if (items.Count != interval.SubIntervals)
                throw new Exception(MessageCollection.ItemsDontMatchSubintervals.Invoke(items.Count, interval));

            Id = id;
            ObservationName = observationName;
            Interval = interval;
            Items = items;

        }

        // Methods
        public override string ToString()
        {

            string content 
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id}'",
                    $"{nameof(ObservationName)}: '{ObservationName}'",
                    $"{nameof(Interval)}: '{Interval.ToString()}'",
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
