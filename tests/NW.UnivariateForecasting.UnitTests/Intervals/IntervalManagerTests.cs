using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Intervals;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests.Intervals
{
    [TestFixture]
    public class IntervalManagerTests
    {

        #region Fields

        private static TestCaseData[] createExceptionTestCases =
        {

            // Create()
            new TestCaseData(
                new TestDelegate( 
                    () => new IntervalManager()
                            .Create(
                                0,
                                ObjectMother.IntervalUnits_Months,
                                ObjectMother.Interval_SixMonths_StartDate,
                                1)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanOne.Invoke("size")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .Create(
                                1,
                                ObjectMother.IntervalUnits_Months,
                                ObjectMother.Interval_SixMonths_StartDate,
                                0)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanOne.Invoke("steps")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .Create(
                                5,
                                ObjectMother.IntervalUnits_Months,
                                ObjectMother.Interval_SixMonths_StartDate,
                                2)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.DividingMustReturnWholeNumber.Invoke("size", "steps")
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_03")

        };
        private static TestCaseData[] calculateNextExceptionTestCases =
        {

            // CalculateNext()
            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .CalculateNext(
                                ObjectMother.Interval_SixMonths_StartDate,
                                ObjectMother.IntervalUnits_Months,
                                0)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.VariableCantBeLessThanOne.Invoke("steps")
                ).SetArgDisplayNames($"{nameof(calculateNextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .CalculateNext(
                                ObjectMother.Interval_SixMonths_StartDate,
                                ObjectMother.IntervalUnit_NonExistant,
                                1)),
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.ProvidedIntervalUnitNotSupported.Invoke(
                            ObjectMother.IntervalUnit_NonExistant.ToString())
                ).SetArgDisplayNames($"{nameof(calculateNextExceptionTestCases)}_02")

        };
        private static TestCaseData[] calculateSubIntervalsExceptionTestCases =
        {

            // CalculateSubIntervals()
            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                                .CalculateSubIntervals(
                                    ObjectMother.Interval_InvalidDueOfSize)),
                typeof(ArgumentException),
                MessageCollection.IntervalNullOrInvalid
                ).SetArgDisplayNames($"{nameof(calculateSubIntervalsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                                .CalculateSubIntervals(
                                        ObjectMother.Interval_SixMonths_SubInterval01)), // took a random subinterval with steps=1
                typeof(ArgumentException),
                UnivariateForecasting.Validation.MessageCollection.SubIntervalsCantBeLessThanTwo
                ).SetArgDisplayNames($"{nameof(calculateSubIntervalsExceptionTestCases)}_02")

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                    (uint)6,
                    ObjectMother.IntervalUnits_Months,
                    new DateTime(2019, 01, 31),
                    (uint)1,
                    ObjectMother.Interval_SixMonths
                ).SetArgDisplayNames($"{nameof(createTestCases)}_01"),

            new TestCaseData(
                    (uint)1,
                    ObjectMother.IntervalUnits_Months,
                    new DateTime(2019, 01, 31),
                    (uint)1,
                    ObjectMother.Interval_SixMonths_SubInterval01
                ).SetArgDisplayNames($"{nameof(createTestCases)}_02")

        };
        private static TestCaseData[] calculateNextTestCases =
        {

            new TestCaseData(
                new DateTime(2019, 01, 01),
                ObjectMother.IntervalUnits_Months, 
                (uint)1, 
                new DateTime(2019, 02, 01)
                ).SetArgDisplayNames($"{nameof(calculateNextTestCases)}_01"),

            new TestCaseData(
                new DateTime(2019, 01, 31),
                ObjectMother.IntervalUnits_Months,
                (uint)1, 
                new DateTime(2019, 02, 28)
                ).SetArgDisplayNames($"{nameof(calculateNextTestCases)}_02"),

            new TestCaseData(
                new DateTime(2019, 01, 01),
                ObjectMother.IntervalUnits_Months,
                (uint)2,
                new DateTime(2019, 03, 01)
                ).SetArgDisplayNames($"{nameof(calculateNextTestCases)}_03"),

            new TestCaseData(
                new DateTime(2019, 01, 31),
                ObjectMother.IntervalUnits_Months,
                (uint)2,
                new DateTime(2019, 03, 31)
                ).SetArgDisplayNames($"{nameof(calculateNextTestCases)}_04")

        };
        private static TestCaseData[] isEndOfTheMonthTestCases =
        {

            new TestCaseData(
                new DateTime(2019, 01, 01), 
                false
                ).SetArgDisplayNames($"{nameof(isEndOfTheMonthTestCases)}_01"),

            new TestCaseData(
                new DateTime(2019, 02, 28), 
                true
                ).SetArgDisplayNames($"{nameof(isEndOfTheMonthTestCases)}_02")

        };
        private static TestCaseData[] moveToEndOfTheMonthTestCases =
        {

            new TestCaseData(
                new DateTime(2019, 01, 01), 
                new DateTime(2019, 01, 31)
                ).SetArgDisplayNames($"{nameof(moveToEndOfTheMonthTestCases)}_01"),

            new TestCaseData(
                new DateTime(2019, 02, 01), 
                new DateTime(2019, 02, 28)
                ).SetArgDisplayNames($"{nameof(moveToEndOfTheMonthTestCases)}_02")

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(
                null, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData(
                ObjectMother.Interval_InvalidDueOfEndDate, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(
                ObjectMother.Interval_InvalidDueOfSize, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData(
                ObjectMother.Interval_InvalidDueOfSizeBySteps, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData(
                ObjectMother.Interval_InvalidDueOfSteps, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_05"),

            new TestCaseData(
                ObjectMother.Interval_InvalidDueOfSubIntervals, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_06"),

            new TestCaseData(
                ObjectMother.Interval_InvalidDueOfTargetDate, 
                false
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_07"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_08"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths_SubInterval01, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_09"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths_SubInterval02, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_10"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths_SubInterval03, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_11"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths_SubInterval04, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_12"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths_SubInterval05, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_13"),

            new TestCaseData(
                ObjectMother.Interval_SixMonths_SubInterval06, 
                true
                ).SetArgDisplayNames($"{nameof(isValidTestCases)}_14")

        };
        private static TestCaseData[] calculateSubIntervalsTestCases =
        {

            new TestCaseData(
                ObjectMother.Interval_SixMonths,
                new List<Interval>() {
                    ObjectMother.Interval_SixMonths_SubInterval01,
                    ObjectMother.Interval_SixMonths_SubInterval02,
                    ObjectMother.Interval_SixMonths_SubInterval03,
                    ObjectMother.Interval_SixMonths_SubInterval04,
                    ObjectMother.Interval_SixMonths_SubInterval05,
                    ObjectMother.Interval_SixMonths_SubInterval06
                    }
                ).SetArgDisplayNames($"{nameof(calculateSubIntervalsTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(calculateNextExceptionTestCases))]
        public void CalculateNext_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(calculateSubIntervalsExceptionTestCases))]
        public void CalculateSubIntervals_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedInterval_WhenProperArguments
            (uint size, IntervalUnits unit, DateTime startDate, uint steps, Interval expected)
        {

            // Arrange
            // Act
            Interval actual = new IntervalManager().Create(size, unit, startDate, steps);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual));

        }

        [TestCaseSource(nameof(calculateNextTestCases))]
        public void CalculateNext_ShouldReturnNextDateTime_WhenDateTime
            (DateTime date, IntervalUnits unit, uint steps, DateTime expected)
        {

            // Arrange
            // Act
            DateTime actual = new IntervalManager().CalculateNext(date, unit, steps);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(isEndOfTheMonthTestCases))]
        public void IsEndOfTheMonth_ShouldReturnExpectedBoolean_WhenDateTime
            (DateTime date, bool expected)
        {

            // Arrange
            // Act
            bool actual = new IntervalManager().IsEndOfTheMonth(date);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(moveToEndOfTheMonthTestCases))]
        public void MoveToEndOfTheMonth_ShouldReturnExpectedDateTime_WhenDateTime
            (DateTime date, DateTime expected)
        {

            // Arrange
            // Act
            DateTime actual = new IntervalManager().MoveToEndOfTheMonth(date);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenInvoked
            (Interval interval, bool expected)
        {

            // Arrange
            // Act
            bool actual = new IntervalManager().IsValid(interval);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(calculateSubIntervalsTestCases))]
        public void CalculateSubIntervals_ShouldReturnExpectedSubIntervals_WhenInterval
            (Interval interval, List<Interval> expected)
        {

            // Arrange
            List<Interval> actual = new IntervalManager().CalculateSubIntervals(interval);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual));

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.11.2022
*/