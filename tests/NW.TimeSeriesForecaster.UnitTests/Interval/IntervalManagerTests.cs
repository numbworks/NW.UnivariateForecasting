﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class IntervalManagerTests
    {

        // Fields
        private static TestCaseData[] createExceptionTestCases =
        {

            // Create()
            new TestCaseData(
                new TestDelegate( 
                    () => new IntervalManager()
                            .Create(
                                0, 
                                IntervalUnits.Months, 
                                MemberRepository.SlidingWindow1_StartDate,
                                1)),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("size")).Message
                ).SetDescription(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("size")),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .Create(
                                1,
                                IntervalUnits.Months,
                                MemberRepository.SlidingWindow1_StartDate,
                                0)),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("steps")).Message
                ).SetDescription(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("steps")),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .Create(
                                5,
                                IntervalUnits.Months,
                                MemberRepository.SlidingWindow1_StartDate,
                                2)),
                typeof(Exception),
                new Exception(
                        MessageCollection.DividingSizeByStepsMustReturnWholeNumber).Message
                ).SetDescription(
                        MessageCollection.DividingSizeByStepsMustReturnWholeNumber)

        };
        private static TestCaseData[] calculateNextExceptionTestCases =
        {

            // CalculateNext()
            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .CalculateNext(
                                MemberRepository.SlidingWindow1_StartDate,
                                IntervalUnits.Months,
                                0)),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("steps")).Message
                ).SetDescription(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("steps")),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .CalculateNext(
                                MemberRepository.SlidingWindow1_StartDate,
                                MemberRepository.NonExistantIntervalUnit,
                                1)),
                typeof(Exception),
                new Exception(
                        MessageCollection.NoStrategyToCalculateNextDateUnit.Invoke(
                            MemberRepository.NonExistantIntervalUnit.ToString())).Message
                ).SetDescription(
                        MessageCollection.NoStrategyToCalculateNextDateUnit.Invoke(
                            MemberRepository.NonExistantIntervalUnit.ToString())),

        };
        private static TestCaseData[] calculateSubIntervalsExceptionTestCases =
        {

            // CalculateSubIntervals()
            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                                .CalculateSubIntervals(
                                    MemberRepository.InvalidIntervalDueOfSize)),
                typeof(Exception),
                new Exception(
                        MessageCollection.IntervalNullOrInvalid).Message
                ).SetDescription(
                        MessageCollection.IntervalNullOrInvalid),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                                .CalculateSubIntervals(
                                        MemberRepository.SlidingWindow1_SubInterval1)), // took a random subinterval with steps=1
                typeof(Exception),
                new Exception(
                        MessageCollection.SubIntervalsCantBeLessThanTwo).Message
                ).SetDescription(
                        MessageCollection.SubIntervalsCantBeLessThanTwo)

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                    (uint)6, 
                    IntervalUnits.Months,
                    new DateTime(2019, 01, 31),
                    (uint)1,
                    MemberRepository.SlidingWindow1_Interval
                ).SetDescription("Interval"),

            new TestCaseData(
                    (uint)1,
                    IntervalUnits.Months,
                    new DateTime(2019, 01, 31),
                    (uint)1,
                    MemberRepository.SlidingWindow1_SubInterval1
                ).SetDescription("SubInterval"),

        };
        private static TestCaseData[] calculateNextTestCases =
        {

            new TestCaseData(
                new DateTime(2019, 01, 01), 
                IntervalUnits.Months, 
                (uint)1, 
                new DateTime(2019, 02, 01)
                ),
            new TestCaseData(
                new DateTime(2019, 01, 31), 
                IntervalUnits.Months,
                (uint)1, 
                new DateTime(2019, 02, 28)
                ),
            new TestCaseData(
                new DateTime(2019, 01, 01),
                IntervalUnits.Months,
                (uint)2,
                new DateTime(2019, 03, 01)
                ),
            new TestCaseData(
                new DateTime(2019, 01, 31),
                IntervalUnits.Months,
                (uint)2,
                new DateTime(2019, 03, 31)
                )

        };
        private static TestCaseData[] isEndOfTheMonthTestCases =
        {

            new TestCaseData(new DateTime(2019, 01, 01), false),
            new TestCaseData(new DateTime(2019, 02, 28), true)

        };
        private static TestCaseData[] moveToEndOfTheMonthTestCases =
        {

            new TestCaseData(new DateTime(2019, 01, 01), new DateTime(2019, 01, 31)),
            new TestCaseData(new DateTime(2019, 02, 01), new DateTime(2019, 02, 28))

        };
        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData(MemberRepository.InvalidIntervalDueOfEndDate, false),
            new TestCaseData(MemberRepository.InvalidIntervalDueOfSize, false),
            new TestCaseData(MemberRepository.InvalidIntervalDueOfSizeBySteps, false),
            new TestCaseData(MemberRepository.InvalidIntervalDueOfSteps, false),
            new TestCaseData(MemberRepository.InvalidIntervalDueOfSubIntervals, false),
            new TestCaseData(MemberRepository.InvalidIntervalDueOfTargetDate, false),
            new TestCaseData(MemberRepository.SlidingWindow1_Interval, true),
            new TestCaseData(MemberRepository.SlidingWindow1_SubInterval1, true),
            new TestCaseData(MemberRepository.SlidingWindow1_SubInterval2, true),
            new TestCaseData(MemberRepository.SlidingWindow1_SubInterval3, true),
            new TestCaseData(MemberRepository.SlidingWindow1_SubInterval4, true),
            new TestCaseData(MemberRepository.SlidingWindow1_SubInterval5, true),
            new TestCaseData(MemberRepository.SlidingWindow1_SubInterval6, true)

        };
        private static TestCaseData[] calculateSubIntervalsTestCases =
        {

            new TestCaseData(
                MemberRepository.SlidingWindow1_Interval,
                new List<Interval>() {
                    MemberRepository.SlidingWindow1_SubInterval1,
                    MemberRepository.SlidingWindow1_SubInterval2,
                    MemberRepository.SlidingWindow1_SubInterval3,
                    MemberRepository.SlidingWindow1_SubInterval4,
                    MemberRepository.SlidingWindow1_SubInterval5,
                    MemberRepository.SlidingWindow1_SubInterval6
                    }
                )

        }; 

        // SetUp
        // Tests
        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(calculateNextExceptionTestCases))]
        public void CalculateNext_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(calculateSubIntervalsExceptionTestCases))]
        public void CalculateSubIntervals_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception objActual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, objActual.Message);

        }

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnExpectedInterval_WhenProperArguments
            (uint size, IntervalUnits unit, DateTime startDate, uint steps, Interval expected)
        {

            // Arrange
            // Act
            Interval actual = new IntervalManager().Create(size, unit, startDate, steps);

            // Assert
            Assert.IsTrue(
                    MemberRepository.AreEqual(expected, actual));

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
        public void IsValid_ShouldReturnExpectedDateTime_WhenDateTime
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
                MemberRepository.AreEqual(expected, actual));

        }

        // TearDown

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 09.09.2020

*/
