using System;

namespace NW.UnivariateForecasting
{
    public class Interval
    {

        // Fields
        // Properties
        public uint Size { get; set; }
        public IntervalUnits Unit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TargetDate { get; set; }
        public uint Steps { get; set; }
        public uint SubIntervals { get; set; }

        // Constructors
        public Interval() { }

        // Methods (public)
        public override string ToString()
        {

            // "6:Months:20190131:20190731:20190831:1:6"

            return
                string.Format(
                    "{0}:{1}:{2}:{3}:{4}:{5}:{6}",
                    Size.ToString(),
                    Unit.ToString(),
                    StartDate.ToString("yyyyMMdd"),
                    EndDate.ToString("yyyyMMdd"),
                    TargetDate.ToString("yyyyMMdd"),
                    Steps.ToString(),
                    SubIntervals.ToString()
                    );

        }
        public string ToString(bool onlyDates)
        {

            // "20190131:20190731:20190831"

            if (onlyDates)
                return
                    string.Format(
                        "{0}:{1}:{2}",
                        StartDate.ToString("yyyyMMdd"),
                        EndDate.ToString("yyyyMMdd"),
                        TargetDate.ToString("yyyyMMdd")
                        );

            return ToString();

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 18.08.2020

*/