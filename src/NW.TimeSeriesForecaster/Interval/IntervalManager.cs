using System;

namespace NW.UnivariateForecasting
{
    public class IntervalManager : IIntervalManager
    {

        // Fields
        // Properties
        // Constructors
        public IntervalManager() { }

        // Methods (public)
        public Interval Create(uint size, IntervalUnits unit, DateTime startDate, uint steps)
        {

            if (size < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(size)));
            if (steps < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));
            if (size % steps != 0)
                throw new Exception(MessageCollection.DividingSizeByStepsMustReturnWholeNumber);

            Interval interval = new Interval();
            interval.Size = size;
            interval.Unit = unit;
            interval.StartDate = startDate;
            interval.EndDate = CalculateNext(startDate, unit, size);
            interval.TargetDate = CalculateNext(interval.EndDate, unit, steps);
            interval.Steps = steps;
            interval.SubIntervals = size / steps;

            return interval;

        }
        public DateTime CalculateNext(DateTime date, IntervalUnits unit, uint size)
        {

            if (size < 1)
                throw new Exception(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(size)));
            if (unit != IntervalUnits.Months)
                throw new Exception(MessageCollection.NoStrategyToCalculateNextDateUnit.Invoke(unit.ToString()));

            return AddMonths(date, size);           

        }

        // Methods (private)
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
    Last Update: 20.08.2020

*/