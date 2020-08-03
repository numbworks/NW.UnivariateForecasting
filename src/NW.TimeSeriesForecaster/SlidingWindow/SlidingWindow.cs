using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    public class SlidingWindow
    {

        // Fields
        // Properties
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TargetDate { get; set; }
        public uint Interval { get; set; }
        public IntervalUnits IntervalUnit { get; set; }
        public List<SlidingWindowItem> Items { get; set; }
        public string ObservationName { get; set; }

        // Constructors
        // Methods
        public override string ToString()
        {

            string content 
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id}'",
                    $"{nameof(StartDate)}: '{StartDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(EndDate)}: '{EndDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(TargetDate)}: '{TargetDate.ToString("yyyy-MM-dd")}'",
                    $"{nameof(Interval)}: '{Interval.ToString()}'",
                    $"{nameof(IntervalUnit)}: '{IntervalUnit}'",
                    $"{nameof(Items)}: '{Items.Count.ToString() ?? "null"}'",
                    $"{nameof(ObservationName)}: '{ObservationName}'"
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 28.04.2020

*/
