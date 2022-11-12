﻿using System;
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
                        Validation.MessageCollection.VariableCantBeLessThanOne.Invoke(Utilities.ObjectMother.Validator_VariableName_Length)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_01"),

            // ValidateLength
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength(0)
                    ),
                typeof(ArgumentException),
                new ArgumentException(
                        Validation.MessageCollection.VariableCantBeLessThanOne.Invoke(Utilities.ObjectMother.Validator_VariableName_Length)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateObjectExceptionTestCases =
        {

            // ValidateObject<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject<ArgumentException>(null, Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentException),
                new ArgumentException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject(null, Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateArrayExceptionTestCases =
        {

            // ValidateArrayNull
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<string>(
                                null,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray(
                                Array.Empty<string>(),
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentException),
                Validation.MessageCollection.VariableContainsZeroItems.Invoke(Utilities.ObjectMother.Validator_VariableName_Variable)
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateListExceptionTestCases =
        {

            // ValidateListNull
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                (List<string>)null,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_01"),

            // ValidateListEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                new List<string>() { },
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentException),
                Validation.MessageCollection.VariableContainsZeroItems.Invoke(Utilities.ObjectMother.Validator_VariableName_Variable)
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_02"),

        };
        private static TestCaseData[] validateStringNullOrWhiteSpaceExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace<Exception>(
                                null,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(Exception),
                new Exception(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_01"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                null,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_02"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                string.Empty,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_03"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                Utilities.ObjectMother.Validator_StringOnlyWhiteSpaces,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_04")

        };
        private static TestCaseData[] validateStringNullOrEmptyExceptionTestCases =
        {

            // ValidateStringNullOrEmpty<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty<Exception>(
                                null,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(Exception),
                new Exception(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_01"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty(
                                null,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_02"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty(
                                string.Empty,
                                Utilities.ObjectMother.Validator_VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(Utilities.ObjectMother.Validator_VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_03")

        };
        private static TestCaseData[] validateStringNullOrEmptyTestCases =
        {

            new TestCaseData(
                    Utilities.ObjectMother.Validator_String1
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyTestCases)}_01"),

            new TestCaseData(
                    Utilities.ObjectMother.Validator_StringOnlyWhiteSpaces
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyTestCases)}_02")

        };
        private static TestCaseData[] throwIfLessThanOneExceptionTestCases =
        {

            // ThrowIfLessThanOne<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThanOne<Exception>(0, Utilities.ObjectMother.Validator_VariableName_N1)
                    ),
                typeof(Exception),
                Validation.MessageCollection.VariableCantBeLessThanOne.Invoke(Utilities.ObjectMother.Validator_VariableName_N1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanOneExceptionTestCases)}_01"),

            // ThrowIfLessThanOne
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThanOne(0, Utilities.ObjectMother.Validator_VariableName_N1)
                    ),
                typeof(ArgumentException),
                Validation.MessageCollection.VariableCantBeLessThanOne.Invoke(Utilities.ObjectMother.Validator_VariableName_N1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanOneExceptionTestCases)}_02")

        };
        private static TestCaseData[] throwIfFirstIsGreaterExceptionTestCases =
        {

            // ThrowIfFirstIsGreater<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreater<Exception>(
                                4,
                                Utilities.ObjectMother.Validator_VariableName_N1,
                                1,
                                Utilities.ObjectMother.Validator_VariableName_N2)
                    ),
                typeof(Exception),
                Validation.MessageCollection.FirstValueIsGreaterThanSecondValue.Invoke(
                                        Utilities.ObjectMother.Validator_VariableName_N1,
                                        Utilities.ObjectMother.Validator_VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterExceptionTestCases)}_01"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreater(
                                4,
                                Utilities.ObjectMother.Validator_VariableName_N1,
                                1,
                                Utilities.ObjectMother.Validator_VariableName_N2)
                    ),
                typeof(ArgumentException),
                Validation.MessageCollection.FirstValueIsGreaterThanSecondValue.Invoke(
                                        Utilities.ObjectMother.Validator_VariableName_N1,
                                        Utilities.ObjectMother.Validator_VariableName_N2
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
                                Utilities.ObjectMother.Validator_VariableName_N1,
                                1,
                                Utilities.ObjectMother.Validator_VariableName_N2)
                    ),
                typeof(Exception),
                Validation.MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue.Invoke(
                                        Utilities.ObjectMother.Validator_VariableName_N1,
                                        Utilities.ObjectMother.Validator_VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases)}_01"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreaterOrEqual(
                                4,
                                Utilities.ObjectMother.Validator_VariableName_N1,
                                1,
                                Utilities.ObjectMother.Validator_VariableName_N2)
                    ),
                typeof(ArgumentException),
                Validation.MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue.Invoke(
                                        Utilities.ObjectMother.Validator_VariableName_N1,
                                        Utilities.ObjectMother.Validator_VariableName_N2
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
                        () => Validator.ValidateLength(Utilities.ObjectMother.Validator_Length1),
                        () => Validator.ValidateLength<ArgumentException>(Utilities.ObjectMother.Validator_Length1)
                    });

        [Test]
        public void ValidateObject_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateObject(Utilities.ObjectMother.Validator_Object1, Utilities.ObjectMother.Validator_VariableName_Variable),
                        () => Validator.ValidateObject<ArgumentException>(Utilities.ObjectMother.Validator_Object1, Utilities.ObjectMother.Validator_VariableName_Variable)
                    });

        [Test]
        public void ValidateArray_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateArray(Utilities.ObjectMother.Validator_Array1, Utilities.ObjectMother.Validator_VariableName_Variable)
                    });

        [Test]
        public void ValidateList_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateList(Utilities.ObjectMother.List1, Utilities.ObjectMother.Validator_VariableName_Variable)
                    });

        [Test]
        public void ValidateStringNullOrWhiteSpace_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateStringNullOrWhiteSpace(Utilities.ObjectMother.Validator_String1, Utilities.ObjectMother.Validator_VariableName_Variable)
                    });

        [TestCaseSource(nameof(validateStringNullOrEmptyTestCases))]
        public void ValidateStringNullOrEmpty_ShouldDoNothing_WhenProperArgument(string str)
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateStringNullOrEmpty(str, Utilities.ObjectMother.Validator_VariableName_Variable)
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
                        () => Validator.ThrowIfLessThanOne(Utilities.ObjectMother.Validator_Value, nameof(Utilities.ObjectMother.Validator_Value))
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
    Last Update: 12.11.2022
*/
