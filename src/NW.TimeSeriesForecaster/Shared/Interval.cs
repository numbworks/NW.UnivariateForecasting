using System;

namespace NW.UnivariateForecasting
{
    public class Interval
    {

        // Fields
        // Properties
        public uint Size { get; }
        public IntervalUnits Unit { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DateTime TargetDate { get; }
        public uint Steps { get; }
        public uint SubIntervals { get; }

        // Constructors
        public Interval(uint size, IntervalUnits unit, DateTime startDate, uint steps)
        {

            if (size < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(size)));
            if (steps < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));
            if (size % steps != 0)
                throw new Exception(MessageCollection.DividingSizeByStepsMustReturnWholeNumber);

            Size = size;
            Unit = unit;
            StartDate = StartDate;
            EndDate = CalculateNext(StartDate, unit, size);
            TargetDate = CalculateNext(EndDate, unit, steps);
            Steps = steps;
            SubIntervals = size / steps;

        }

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
        private DateTime CalculateNext(DateTime date, IntervalUnits unit, uint size)
        {

            if (unit == IntervalUnits.Months)
                return AddMonths(date, size);

            throw new Exception(MessageCollection.NoStrategyToCalculateNextDateUnit.Invoke(unit.ToString()));

        }
        private DateTime AddMonths(DateTime date, uint months)
        {

            /*
                2019-01-31
                2019-02-28
                2019-03-28 < Error, this should be 2019-03-31
             */

            DateTime nextDate = date.AddMonths((int)months);

            if (!IsEndOfTheMonth(date))
                return nextDate;

            return MoveToEndOfTheMonth(nextDate);

        }
        private bool IsEndOfTheMonth(DateTime dt)
            => dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month);
        private DateTime MoveToEndOfTheMonth(DateTime date)
            => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 18.08.2020

*/
