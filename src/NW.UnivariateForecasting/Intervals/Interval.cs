using System;

namespace NW.UnivariateForecasting.Intervals
{
    public class Interval
    {

        #region Fields
        #endregion

        #region Properties

        public static string Format { get; } = "{0}:{1}:{2}:{3}:{4}:{5}:{6}";
        public static string FormatOnlyDates { get; } = "{0}:{1}:{2}";
        public static string DateFormat { get; } = "yyyyMMdd";

        public uint Size { get; set; }
        public IntervalUnits Unit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TargetDate { get; set; }
        public uint Steps { get; set; }
        public uint SubIntervals { get; set; }

        #endregion

        #region Constructors

        public Interval() { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            // "6:Months:20190131:20190731:20190831:1:6"

            return
                string.Format(
                    Format,
                    Size.ToString(),
                    Unit.ToString(),
                    StartDate.ToString(DateFormat),
                    EndDate.ToString(DateFormat),
                    TargetDate.ToString(DateFormat),
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
                        FormatOnlyDates,
                        StartDate.ToString(DateFormat),
                        EndDate.ToString(DateFormat),
                        TargetDate.ToString(DateFormat)
                        );

            return ToString();

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 11.10.2021
*/