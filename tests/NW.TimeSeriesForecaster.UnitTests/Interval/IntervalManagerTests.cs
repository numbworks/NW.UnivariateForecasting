using System;
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

        // TearDown

        // Support methods
        /*

        private static TestCaseData[] arrTestCases =
        {

            new TestCaseData( "Something", false )

        };

        [TestCaseSource(nameof(arrTestCases))]
        public void Method_Should_When()
        {

            // Arrange
            // Act
            // Assert

        }
        [Test]
        public void SomeOtherMethod_Should_When()
        {

            // Arrange
            // Act
            // Assert

        }

        private bool AreEqual(SketchCollection obj1, SketchCollection obj2)
        {

            return string.Equals(obj1.Id, obj2.Id, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(obj1.Url, obj2.Url, StringComparison.InvariantCultureIgnoreCase)
                   && (obj1.Type == obj2.Type);

        }		
		*/

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 06.09.2020

*/
