using System;
using System.Collections.Generic;
using NW.UnivariateForecasting.Validation;
using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class ValidatorTests
    {

        #region Fields

        private static TestCaseData[] validateLengthExceptionTestCases =
        {

            // ValidateLength<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength<Exception>(0)
                    ),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThanOne.Invoke(Validation.ObjectMother.VariableName_Length)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_01"),

            // ValidateLength
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength(0)
                    ),
                typeof(ArgumentException),
                new ArgumentException(
                        MessageCollection.VariableCantBeLessThanOne.Invoke(Validation.ObjectMother.VariableName_Length)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateObjectExceptionTestCases =
        {

            // ValidateObject<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject<ArgumentException>(null, Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentException),
                new ArgumentException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject(null, Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateArrayExceptionTestCases =
        {

            // ValidateArrayNull
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<string>(
                                null,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray(
                                Array.Empty<string>(),
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(Validation.ObjectMother.VariableName_Variable)
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateListExceptionTestCases =
        {

            // ValidateListNull
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                (List<string>)null,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_01"),

            // ValidateListEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                new List<string>() { },
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(Validation.ObjectMother.VariableName_Variable)
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_02"),

        };
        private static TestCaseData[] validateStringNullOrWhiteSpaceExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace<Exception>(
                                null,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(Exception),
                new Exception(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_01"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                null,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_02"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                string.Empty,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_03"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                Validation.ObjectMother.StringOnlyWhiteSpaces,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_04")

        };
        private static TestCaseData[] validateStringNullOrEmptyExceptionTestCases =
        {

            // ValidateStringNullOrEmpty<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty<Exception>(
                                null,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(Exception),
                new Exception(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_01"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty(
                                null,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_02"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty(
                                string.Empty,
                                Validation.ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Validation.ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_03")

        };
        private static TestCaseData[] validateStringNullOrEmptyTestCases =
        {

            new TestCaseData(
                    Validation.ObjectMother.String01
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyTestCases)}_01"),

            new TestCaseData(
                    Validation.ObjectMother.StringOnlyWhiteSpaces
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyTestCases)}_02")

        };
        private static TestCaseData[] throwIfLessThanOneExceptionTestCases =
        {

            // ThrowIfLessThanOne<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThanOne<Exception>(0, Validation.ObjectMother.VariableName_N1)
                    ),
                typeof(Exception),
                MessageCollection.VariableCantBeLessThanOne.Invoke(Validation.ObjectMother.VariableName_N1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanOneExceptionTestCases)}_01"),

            // ThrowIfLessThanOne
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThanOne(0, Validation.ObjectMother.VariableName_N1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThanOne.Invoke(Validation.ObjectMother.VariableName_N1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanOneExceptionTestCases)}_02")

        };
        private static TestCaseData[] throwIfFirstIsGreaterExceptionTestCases =
        {

            // ThrowIfFirstIsGreater<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreater<Exception>(
                                4,
                                Validation.ObjectMother.VariableName_N1,
                                1,
                                Validation.ObjectMother.VariableName_N2)
                    ),
                typeof(Exception),
                MessageCollection.FirstValueIsGreaterThanSecondValue.Invoke(
                                        Validation.ObjectMother.VariableName_N1,
                                        Validation.ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterExceptionTestCases)}_01"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreater(
                                4,
                                Validation.ObjectMother.VariableName_N1,
                                1,
                                Validation.ObjectMother.VariableName_N2)
                    ),
                typeof(ArgumentException),
                MessageCollection.FirstValueIsGreaterThanSecondValue.Invoke(
                                        Validation.ObjectMother.VariableName_N1,
                                        Validation.ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterExceptionTestCases)}_02")

        };
        private static TestCaseData[] throwIfFirstIsGreaterOrEqualExceptionTestCases =
                {

            // ThrowIfFirstIsGreater<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreaterOrEqual<Exception>(
                                4,
                                Validation.ObjectMother.VariableName_N1,
                                1,
                                Validation.ObjectMother.VariableName_N2)
                    ),
                typeof(Exception),
                MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue.Invoke(
                                        Validation.ObjectMother.VariableName_N1,
                                        Validation.ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases)}_01"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreaterOrEqual(
                                4,
                                Validation.ObjectMother.VariableName_N1,
                                1,
                                Validation.ObjectMother.VariableName_N2)
                    ),
                typeof(ArgumentException),
                MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue.Invoke(
                                        Validation.ObjectMother.VariableName_N1,
                                        Validation.ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(validateLengthExceptionTestCases))]
        public void ValidateLength_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateObjectExceptionTestCases))]
        public void ValidateObject_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateArrayExceptionTestCases))]
        public void ValidateArray_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateListExceptionTestCases))]
        public void ValidateList_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateStringNullOrWhiteSpaceExceptionTestCases))]
        public void ValidateStringNullOrWhiteSpace_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateStringNullOrEmptyExceptionTestCases))]
        public void ValidateStringNullOrEmpty_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfLessThanOneExceptionTestCases))]
        public void ThrowIfLessThanOne_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfFirstIsGreaterExceptionTestCases))]
        public void ThrowIfFirstIsGreater_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases))]
        public void ThrowIfFirstIsGreaterOrEqual_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ValidateLength_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateLength(Validation.ObjectMother.Length01),
                        () => Validator.ValidateLength<ArgumentException>(Validation.ObjectMother.Length01)
                    });

        [Test]
        public void ValidateObject_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateObject(Validation.ObjectMother.Object01, Validation.ObjectMother.VariableName_Variable),
                        () => Validator.ValidateObject<ArgumentException>(Validation.ObjectMother.Object01, Validation.ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ValidateArray_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateArray(Validation.ObjectMother.Array01, Validation.ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ValidateList_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateList(Validation.ObjectMother.List01, Validation.ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ValidateStringNullOrWhiteSpace_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateStringNullOrWhiteSpace(Validation.ObjectMother.String01, Validation.ObjectMother.VariableName_Variable)
                    });

        [TestCaseSource(nameof(validateStringNullOrEmptyTestCases))]
        public void ValidateStringNullOrEmpty_ShouldDoNothing_WhenProperArgument(string str)
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateStringNullOrEmpty(str, Validation.ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ThrowIfFirstIsGreaterOrEqual_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfFirstIsGreaterOrEqual(4, "n1", 5, "n2")
                    });

        [Test]
        public void ThrowIfFirstIsGreater_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfFirstIsGreater(3, "n1", 4, "n2")
                    });

        [Test]
        public void ThrowIfLessThanOne_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfLessThanOne(Validation.ObjectMother.Value, nameof(Validation.ObjectMother.Value))
                    });

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        public void Method_ShouldDoNothing_WhenProperArgument(Action[] actions)
        {

            try
            {

                // Arrange
                // Act
                foreach (Action action in actions)
                    action.Invoke();

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.11.2022
*/
