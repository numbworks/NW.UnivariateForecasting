using System;
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
                                ObjectMother.SlidingWindow1_IntervalUnit, 
                                ObjectMother.SlidingWindow1_StartDate,
                                1)),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThanOne.Invoke("size")
                ).SetDescription(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("size")),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .Create(
                                1,
                                ObjectMother.SlidingWindow1_IntervalUnit,
                                ObjectMother.SlidingWindow1_StartDate,
                                0)),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThanOne.Invoke("steps")
                ).SetDescription(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("steps")),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .Create(
                                5,
                                ObjectMother.SlidingWindow1_IntervalUnit,
                                ObjectMother.SlidingWindow1_StartDate,
                                2)),
                typeof(ArgumentException),
                MessageCollection.DividingSizeByStepsMustReturnWholeNumber
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
                                ObjectMother.SlidingWindow1_StartDate,
                                ObjectMother.SlidingWindow1_IntervalUnit,
                                0)),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThanOne.Invoke("steps")
                ).SetDescription(
                        MessageCollection.VariableCantBeLessThanOne.Invoke("steps")),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                            .CalculateNext(
                                ObjectMother.SlidingWindow1_StartDate,
                                ObjectMother.NonExistantIntervalUnit,
                                1)),
                typeof(ArgumentException),
                MessageCollection.ProvidedIntervalUnitNotSupported.Invoke(
                            ObjectMother.NonExistantIntervalUnit.ToString())
                ).SetDescription(
                        MessageCollection.ProvidedIntervalUnitNotSupported.Invoke(
                            ObjectMother.NonExistantIntervalUnit.ToString())),

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
                ).SetDescription(
                        MessageCollection.IntervalNullOrInvalid),

            new TestCaseData(
                new TestDelegate(
                    () => new IntervalManager()
                                .CalculateSubIntervals(
                                        ObjectMother.SlidingWindow1_SubInterval1)), // took a random subinterval with steps=1
                typeof(ArgumentException),
                MessageCollection.SubIntervalsCantBeLessThanTwo
                ).SetDescription(
                        MessageCollection.SubIntervalsCantBeLessThanTwo)

        };
        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                    (uint)6,
                    ObjectMother.SlidingWindow1_IntervalUnit,
                    new DateTime(2019, 01, 31),
                    (uint)1,
                    ObjectMother.SlidingWindow1_Interval
                ).SetDescription("Interval"),

            new TestCaseData(
                    (uint)1,
                    ObjectMother.SlidingWindow1_IntervalUnit,
                    new DateTime(2019, 01, 31),
                    (uint)1,
                    ObjectMother.SlidingWindow1_SubInterval1
                ).SetDescription("SubInterval"),

        };
        private static TestCaseData[] calculateNextTestCases =
        {

            new TestCaseData(
                new DateTime(2019, 01, 01),
                ObjectMother.SlidingWindow1_IntervalUnit, 
                (uint)1, 
                new DateTime(2019, 02, 01)
                ),
            new TestCaseData(
                new DateTime(2019, 01, 31),
                ObjectMother.SlidingWindow1_IntervalUnit,
                (uint)1, 
                new DateTime(2019, 02, 28)
                ),
            new TestCaseData(
                new DateTime(2019, 01, 01),
                ObjectMother.SlidingWindow1_IntervalUnit,
                (uint)2,
                new DateTime(2019, 03, 01)
                ),
            new TestCaseData(
                new DateTime(2019, 01, 31),
                ObjectMother.SlidingWindow1_IntervalUnit,
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

            new TestCaseData(null, false),
            new TestCaseData(ObjectMother.Interval_InvalidDueOfEndDate, false),
            new TestCaseData(ObjectMother.Interval_InvalidDueOfSize, false),
            new TestCaseData(ObjectMother.Interval_InvalidDueOfSizeBySteps, false),
            new TestCaseData(ObjectMother.Interval_InvalidDueOfSteps, false),
            new TestCaseData(ObjectMother.Interval_InvalidDueOfSubIntervals, false),
            new TestCaseData(ObjectMother.Interval_InvalidDueOfTargetDate, false),
            new TestCaseData(ObjectMother.SlidingWindow1_Interval, true),
            new TestCaseData(ObjectMother.SlidingWindow1_SubInterval1, true),
            new TestCaseData(ObjectMother.SlidingWindow1_SubInterval2, true),
            new TestCaseData(ObjectMother.SlidingWindow1_SubInterval3, true),
            new TestCaseData(ObjectMother.SlidingWindow1_SubInterval4, true),
            new TestCaseData(ObjectMother.SlidingWindow1_SubInterval5, true),
            new TestCaseData(ObjectMother.SlidingWindow1_SubInterval6, true)

        };
        private static TestCaseData[] calculateSubIntervalsTestCases =
        {

            new TestCaseData(
                ObjectMother.SlidingWindow1_Interval,
                new List<Interval>() {
                    ObjectMother.SlidingWindow1_SubInterval1,
                    ObjectMother.SlidingWindow1_SubInterval2,
                    ObjectMother.SlidingWindow1_SubInterval3,
                    ObjectMother.SlidingWindow1_SubInterval4,
                    ObjectMother.SlidingWindow1_SubInterval5,
                    ObjectMother.SlidingWindow1_SubInterval6
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

        // TearDown

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 09.09.2020

*/
