using System;
using System.Collections.Generic;

namespace NW.UnivariateForecasting
{
    /// <summary>
    /// Collects all the methods useful to manipulate an <see cref="Interval"/>.
    /// </summary>
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
                throw new ArgumentException(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(size)));
            if (steps < 1)
                throw new ArgumentException(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));
            if (size % steps != 0)
                throw new ArgumentException(MessageCollection.DividingSizeByStepsMustReturnWholeNumber);

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
        public DateTime CalculateNext(DateTime date, IntervalUnits unit, uint steps)
        {

            if (steps < 1)
                throw new ArgumentException(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(steps)));
            if (unit != IntervalUnits.Months)
                throw new ArgumentException(MessageCollection.ProvidedIntervalUnitNotSupported.Invoke(unit.ToString()));

            return AddMonths(date, steps);           

        }
        public bool IsEndOfTheMonth(DateTime datetime)
            => datetime.Day == DateTime.DaysInMonth(datetime.Year, datetime.Month);
        public DateTime MoveToEndOfTheMonth(DateTime datetime)
            => new DateTime(datetime.Year, datetime.Month, DateTime.DaysInMonth(datetime.Year, datetime.Month));
        public bool IsValid(Interval interval)
        {

            if (interval == null)
                return false;
            if (interval.Size < 1)
                return false;
            if (interval.Steps < 1)
                return false;
            if (interval.Size % interval.Steps != 0)
                return false;
            if (interval.EndDate != CalculateNext(interval.StartDate, interval.Unit, interval.Size))
                return false;
            if (interval.TargetDate != CalculateNext(interval.EndDate, interval.Unit, interval.Steps))
                return false;
            if (interval.SubIntervals != (interval.Size / interval.Steps))
                return false;

            return true;

        }
        public List<Interval> CalculateSubIntervals(Interval interval)
        {

            if (!IsValid(interval))
                throw new ArgumentException(MessageCollection.IntervalNullOrInvalid);
            if (interval.SubIntervals < 2)
                throw new ArgumentException(MessageCollection.SubIntervalsCantBeLessThanTwo);

            List<Interval> subIntervals = new List<Interval>();
            for (int i = 0; i < interval.Size; i++)
            {

                Interval subInterval = new Interval();

                subInterval.Size = 1;
                subInterval.Steps = 1;
                subInterval.SubIntervals = 1;
                subInterval.Unit = interval.Unit;

                if (i == 0)
                    subInterval.StartDate = interval.StartDate;
                else
                    subInterval.StartDate = CalculateNext(interval.StartDate, interval.Unit, (uint)i);

                subInterval.EndDate = CalculateNext(subInterval.StartDate, subInterval.Unit, subInterval.Steps);
                subInterval.TargetDate = CalculateNext(subInterval.EndDate, subInterval.Unit, subInterval.Steps);

                subIntervals.Add(subInterval);

            }

            return subIntervals;

        }

        // Methods (private)
        private DateTime AddMonths(DateTime datetime, uint months)
        {

            /*
                2019-01-31
                2019-02-28
                2019-03-28 < Error, this should be 2019-03-31
             */

            DateTime nextDate = datetime.AddMonths((int)months);

            if (!IsEndOfTheMonth(datetime))
                return nextDate;

            return MoveToEndOfTheMonth(nextDate);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 25.04.2021

*/
