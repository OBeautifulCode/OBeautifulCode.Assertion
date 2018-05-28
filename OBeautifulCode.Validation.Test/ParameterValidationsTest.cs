// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    using static System.FormattableString;

    public static class ParameterValidationsTest
    {
        private static readonly ParameterEqualityComparer ParameterComparer = new ParameterEqualityComparer();

        private delegate Parameter Validation(Parameter parameter, string because = null);

        [Fact]
        public static void GetEnumerableGenericType___Gets_the_correct_generic_type___When_called_with_various_flavors_of_IEnumerable()
        {
            // Arrange
            var values1 = new[] { string.Empty };
            var values2 = new List<string> { string.Empty };
            var values3 = new ArrayList();
            var values4 = new Dictionary<string, object>();
            IEnumerable<string> values5 = new List<string>();
            IReadOnlyCollection<string> values6 = new List<string>();

            var expectedStringMessage = "validationName: BeOfTypeThatDoesNotExist, isElementInEnumerable: True, parameterValueTypeName: String";
            var expectedObjectMessage = "validationName: BeOfTypeThatDoesNotExist, isElementInEnumerable: True, parameterValueTypeName: Object";
            var expectedKvpMessage = "validationName: BeOfTypeThatDoesNotExist, isElementInEnumerable: True, parameterValueTypeName: KeyValuePair<string, object>";

            // Act
            // note: GetEnumerableGenericType is not public, so we're using BeOfNonExistentType which
            // always throws and checking that parameterValueTypeName is the expected type
            var actual1 = Record.Exception(() => values1.Must().Each().BeOfTypeThatDoesNotExist());
            var actual2 = Record.Exception(() => values2.Must().Each().BeOfTypeThatDoesNotExist());
            var actual3 = Record.Exception(() => values3.Must().Each().BeOfTypeThatDoesNotExist());
            var actual4 = Record.Exception(() => values4.Must().Each().BeOfTypeThatDoesNotExist());
            var actual5 = Record.Exception(() => values5.Must().Each().BeOfTypeThatDoesNotExist());
            var actual6 = Record.Exception(() => values6.Must().Each().BeOfTypeThatDoesNotExist());

            // Assert
            actual1.Should().BeOfType<InvalidCastException>();
            actual1.Message.Should().Be(expectedStringMessage);

            actual2.Should().BeOfType<InvalidCastException>();
            actual2.Message.Should().Be(expectedStringMessage);

            actual3.Should().BeOfType<InvalidCastException>();
            actual3.Message.Should().Be(expectedObjectMessage);

            actual4.Should().BeOfType<InvalidCastException>();
            actual4.Message.Should().Be(expectedKvpMessage);

            actual5.Should().BeOfType<InvalidCastException>();
            actual5.Message.Should().Be(expectedStringMessage);

            actual6.Should().BeOfType<InvalidCastException>();
            actual6.Message.Should().Be(expectedStringMessage);
        }

        [Fact]
        public static void BeNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeNull,
                ValidationName = nameof(ParameterValidation.BeNull),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeNullExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Any Reference Type, Nullable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustPassingValues = new Guid?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { null, null },
                },
                MustFailingValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                },
                MustEachFailingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { null, Guid.NewGuid(), null },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null, null },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new string[] { null, string.Empty, null },
                    new string[] { null, " \r\n ", null },
                    new string[] { null, A.Dummy<string>(), null },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { null, null },
                },
                MustFailingValues = new object[]
                {
                    A.Dummy<object>(),
                },
                MustEachFailingValues = new IEnumerable<object>[]
                {
                    new object[] { null, A.Dummy<object>(), null },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(objectTestValues);
        }

        [Fact]
        public static void NotBeNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeNull,
                ValidationName = nameof(ParameterValidation.NotBeNull),
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Any Reference Type, Nullable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustPassingValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                },
                MustEachPassingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                },
                MustFailingValues = new Guid?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachPassingValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                },
                MustFailingValues = new object[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<object>[]
                {
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(objectTestValues);
        }

        [Fact]
        public static void BeTrue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeTrue,
                ValidationName = nameof(ParameterValidation.BeTrue),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeTrueExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Boolean, Nullable<Boolean>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Boolean>, IEnumerable<Nullable<Boolean>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    A.Dummy<object>(),
                    new List<string> { null },
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustPassingValues = new[]
                {
                    true,
                },
                MustEachPassingValues = new[]
                {
                    new bool[] { },
                    new bool[] { true, true },
                },
                MustFailingValues = new[]
                {
                    false,
                },
                MustEachFailingValues = new[]
                {
                    new bool[] { false, false },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustPassingValues = new bool?[]
                {
                    true,
                },
                MustEachPassingValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true, true },
                },
                MustFailingValues = new bool?[]
                {
                    false,
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { false, null },
                    new bool?[] { null, false },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeTrue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeTrue,
                ValidationName = nameof(ParameterValidation.NotBeTrue),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeTrueExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Boolean, Nullable<Boolean>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Boolean>, IEnumerable<Nullable<Boolean>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustPassingValues = new[]
                {
                    false,
                },
                MustEachPassingValues = new[]
                {
                    new bool[] { },
                    new bool[] { false, false },
                },
                MustFailingValues = new[]
                {
                    true,
                },
                MustEachFailingValues = new[]
                {
                    new[] { true, true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustPassingValues = new bool?[]
                {
                    false,
                    null,
                },
                MustEachPassingValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { false, null },
                    new bool?[] { null, false },
                },
                MustFailingValues = new bool?[]
                {
                    true,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { true, true },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeFalse___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeFalse,
                ValidationName = nameof(ParameterValidation.BeFalse),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeFalseExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Boolean, Nullable<Boolean>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Boolean>, IEnumerable<Nullable<Boolean>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustPassingValues = new[]
                {
                    false,
                },
                MustEachPassingValues = new[]
                {
                    new bool[] { },
                    new bool[] { false, false },
                },
                MustFailingValues = new[]
                {
                    true,
                },
                MustEachFailingValues = new[]
                {
                    new bool[] { true, true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustPassingValues = new bool?[]
                {
                    false,
                },
                MustEachPassingValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { false },
                },
                MustFailingValues = new bool?[]
                {
                    true,
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { true, null },
                    new bool?[] { null, true },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeFalse___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeFalse,
                ValidationName = nameof(ParameterValidation.NotBeFalse),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeFalseExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Boolean, Nullable<Boolean>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Boolean>, IEnumerable<Nullable<Boolean>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustPassingValues = new[]
                {
                    true,
                },
                MustEachPassingValues = new[]
                {
                    new bool[] { },
                    new bool[] { true, true },
                },
                MustFailingValues = new[]
                {
                    false,
                },
                MustEachFailingValues = new[]
                {
                    new bool[] { false, false },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustPassingValues = new bool?[]
                {
                    true,
                    null,
                },
                MustEachPassingValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true, null },
                    new bool?[] { null, true },
                },
                MustFailingValues = new bool?[]
                {
                    false,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { false, false },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeNullNorWhiteSpace___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest1 = new ValidationTest
            {
                Validation = ParameterValidation.NotBeNullNorWhiteSpace,
                ValidationName = nameof(ParameterValidation.NotBeNullNorWhiteSpace),
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "String",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<String>",
            };

            var validationTest2 = new ValidationTest
            {
                Validation = ParameterValidation.NotBeNullNorWhiteSpace,
                ValidationName = nameof(ParameterValidation.NotBeNullNorWhiteSpace),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullNorWhiteSpaceExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "String",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<String>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "    ",
                    " \r\n  ",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), "    ", A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), " \r\n  ", A.Dummy<string>() },
                },
            };

            // Act, Assert
            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);
            validationTest1.Run(stringTestValues1);

            validationTest2.Run(guidTestValues);
            validationTest2.Run(nullableGuidTestValues);
            validationTest2.Run(objectTestValues);
            validationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void BeEmptyGuid___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeEmptyGuid,
                ValidationName = nameof(ParameterValidation.BeEmptyGuid),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeEmptyGuidExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Guid, Nullable<Guid>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Guid>, IEnumerable<Nullable<Guid>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustPassingValues = new[]
                {
                    Guid.Empty,
                },
                MustEachPassingValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                },
                MustFailingValues = new[]
                {
                    Guid.NewGuid(),
                },
                MustEachFailingValues = new[]
                {
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustPassingValues = new Guid?[]
                {
                    Guid.Empty,
                },
                MustEachPassingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                },
                MustFailingValues = new Guid?[]
                {
                    null,
                    Guid.NewGuid(),
                },
                MustEachFailingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    "   ",
                    "   \r\n ",
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new string[] { },
                    new[] { string.Empty },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustParameterInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(enumerableTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeEmptyGuid___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeEmptyGuid,
                ValidationName = nameof(ParameterValidation.NotBeEmptyGuid),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeEmptyGuidExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "Guid, Nullable<Guid>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<Guid>, IEnumerable<Nullable<Guid>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustPassingValues = new[]
                {
                    Guid.NewGuid(),
                },
                MustEachPassingValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid(), Guid.NewGuid() },
                },
                MustFailingValues = new[]
                {
                    Guid.Empty,
                },
                MustEachFailingValues = new[]
                {
                    new Guid[] { Guid.NewGuid(), Guid.Empty, Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustPassingValues = new Guid?[]
                {
                    null,
                    Guid.NewGuid(),
                },
                MustEachPassingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid() },
                    new Guid?[] { null },
                },
                MustFailingValues = new Guid?[]
                {
                    Guid.Empty,
                },
                MustEachFailingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { Guid.NewGuid(), Guid.Empty, Guid.NewGuid() },
                    new Guid?[] { null, Guid.Empty, null },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    "   ",
                    "   \r\n ",
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new string[] { },
                    new[] { string.Empty },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustParameterInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(enumerableTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeEmptyString___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeEmptyString,
                ValidationName = nameof(ParameterValidation.BeEmptyString),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeEmptyStringExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "String",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<String>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty },
                },
                MustFailingValues = new[]
                {
                    null,
                    "   ",
                    "   \r\n ",
                    A.Dummy<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { "    ", A.Dummy<string>() },
                    new string[] { "  \r\n  ", A.Dummy<string>() },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustParameterInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(enumerableTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeEmptyString___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeEmptyString,
                ValidationName = nameof(ParameterValidation.NotBeEmptyString),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeEmptyStringExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "String",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<String>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    null,
                    "   ",
                    "   \r\n ",
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { null, A.Dummy<string>() },
                    new string[] { "    ", A.Dummy<string>() },
                    new string[] { "  \r\n  ", A.Dummy<string>() },
                },
                MustFailingValues = new[]
                {
                    string.Empty,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustParameterInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(enumerableTestValues);
            validationTest.Run(objectTestValues);
            validationTest.Run(boolTestValues);
            validationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeEmptyEnumerable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest1 = new ValidationTest
            {
                Validation = ParameterValidation.BeEmptyEnumerable,
                ValidationName = nameof(ParameterValidation.BeEmptyEnumerable),
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var validationTest2 = new ValidationTest
            {
                Validation = ParameterValidation.BeEmptyEnumerable,
                ValidationName = nameof(ParameterValidation.BeEmptyEnumerable),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeEmptyEnumerableExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, null, string.Empty },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty },
                },
                MustFailingValues = new[]
                {
                    "   ",
                    "   \r\n ",
                    A.Dummy<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "    ", string.Empty },
                    new string[] { string.Empty, "  \r\n  ", string.Empty },
                    new string[] { string.Empty, A.Dummy<string>(), string.Empty },
                },
            };

            var enumerableTestValues1 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { }, null, new string[] { } },
                },
            };

            var enumerableTestValues2A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { }, new List<string> { null }, new string[] { } },
                },
            };

            var enumerableTestValues2B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string>(), new string[] { } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { }, new List<string> { null }, new string[] { } },
                },
            };

            var enumerableTestValues2C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string>(), new List<string>() },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>() { string.Empty },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { }, new List<string> { null }, new List<string> { } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            validationTest1.Run(stringTestValues1);
            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);
            validationTest1.Run(boolTestValues);
            validationTest1.Run(nullableBoolTestValues);
            validationTest1.Run(enumerableTestValues1);

            validationTest2.Run(stringTestValues2);
            validationTest2.Run(enumerableTestValues2A);
            validationTest2.Run(enumerableTestValues2B);
            validationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void NotBeEmptyEnumerable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest1 = new ValidationTest
            {
                Validation = ParameterValidation.NotBeEmptyEnumerable,
                ValidationName = nameof(ParameterValidation.NotBeEmptyEnumerable),
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var validationTest2 = new ValidationTest
            {
                Validation = ParameterValidation.NotBeEmptyEnumerable,
                ValidationName = nameof(ParameterValidation.NotBeEmptyEnumerable),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeEmptyEnumerableExceptionMessageSuffix,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "   ",
                    "   \r\n ",
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "    ", "    ", "  \r\n ", A.Dummy<string>() },
                },
                MustFailingValues = new[]
                {
                    string.Empty,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
            };

            var enumerableTestValues1 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { A.Dummy<string>() } },
                },
            };

            var enumerableTestValues2A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, new List<string> { null }, new string[] { string.Empty } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues2B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new string[] { A.Dummy<string>() }, new List<string> { null }, new string[] { string.Empty } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues2C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string>() { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { A.Dummy<string>() }, new List<string> { null }, new List<string> { string.Empty } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string>(), new List<string>() },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            validationTest1.Run(stringTestValues1);
            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);
            validationTest1.Run(boolTestValues);
            validationTest1.Run(nullableBoolTestValues);
            validationTest1.Run(enumerableTestValues1);

            validationTest2.Run(stringTestValues2);
            validationTest2.Run(enumerableTestValues2A);
            validationTest2.Run(enumerableTestValues2B);
            validationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void ContainSomeNulls___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation validation = ParameterValidation.ContainSomeNulls;
            var validationName = nameof(ParameterValidation.ContainSomeNulls);

            var validationTest1 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);
            validationTest1.Run(boolTestValues);
            validationTest1.Run(nullableBoolTestValues);

            var validationTest2 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable, IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>, IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustParameterInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            validationTest2.Run(enumerableTestValues2);
            validationTest2.Run(stringTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>(), null }, null, new string[] { A.Dummy<string>(), null } },
                },
            };

            validationTest3.Run(enumerableTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.ContainSomeNullsExceptionMessageSuffix,
            };

            var enumerableTestValues4A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable[] { new List<string> { A.Dummy<string>(), null } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IList[] { new List<string> { A.Dummy<string>(), null } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { null }, new List<string> { A.Dummy<string>(), null } },
                    new List<string>[] { new List<string> { A.Dummy<string>(), null } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { null }, new List<string> { A.Dummy<string>(), null }, new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            validationTest4.Run(enumerableTestValues4A);
            validationTest4.Run(enumerableTestValues4B);
            validationTest4.Run(enumerableTestValues4C);
        }

        [Fact]
        public static void NotContainAnyNulls___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation validation = ParameterValidation.NotContainAnyNulls;
            var validationName = nameof(ParameterValidation.NotContainAnyNulls);

            var validationTest1 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);
            validationTest1.Run(boolTestValues);
            validationTest1.Run(nullableBoolTestValues);

            var validationTest2 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable, IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>, IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustParameterInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            validationTest2.Run(enumerableTestValues2);
            validationTest2.Run(stringTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>(), A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            validationTest3.Run(enumerableTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotContainAnyNullsExceptionMessageSuffix,
            };

            var enumerableTestValues4A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                    new IEnumerable[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                    new IList[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { },
                    new List<string> { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                    new List<string>[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new List<string> { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new List<string> { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            validationTest4.Run(enumerableTestValues4A);
            validationTest4.Run(enumerableTestValues4B);
            validationTest4.Run(enumerableTestValues4C);
        }

        [Fact]
        public static void NotBeNullNorEmptyNorContainAnyNulls___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation validation = ParameterValidation.NotBeNullNorEmptyNorContainAnyNulls;
            var validationName = nameof(ParameterValidation.NotBeNullNorEmptyNorContainAnyNulls);

            var validationTest1 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustParameterInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustParameterInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);
            validationTest1.Run(boolTestValues);
            validationTest1.Run(nullableBoolTestValues);

            var validationTest2 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable, IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>, IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustParameterInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustParameterInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            validationTest2.Run(enumerableTestValues2);
            validationTest2.Run(stringTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>(), A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            validationTest3.Run(enumerableTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeEmptyEnumerableExceptionMessageSuffix,
            };

            var enumerableTestValues4A = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues4B = new TestValues<IList>
            {
                MustFailingValues = new IList[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues4C = new TestValues<List<string>>
            {
                MustFailingValues = new List<string>[]
                {
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string>(), new List<string>() },
                },
            };

            validationTest4.Run(enumerableTestValues4A);
            validationTest4.Run(enumerableTestValues4B);
            validationTest4.Run(enumerableTestValues4C);

            var validationTest5 = new ValidationTest
            {
                Validation = validation,
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotContainAnyNullsExceptionMessageSuffix,
            };

            var enumerableTestValues5A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), },
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string> { string.Empty }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues5B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), },
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string> { string.Empty }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues5C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), },
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new List<string> { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new List<string> { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            validationTest5.Run(enumerableTestValues5A);
            validationTest5.Run(enumerableTestValues5B);
            validationTest5.Run(enumerableTestValues5C);
        }

        [Fact]
        public static void BeDefault___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeDefault,
                ValidationName = nameof(ParameterValidation.BeDefault),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeDefaultExceptionMessageSuffix,
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustPassingValues = new[]
                {
                    Guid.Empty,
                },
                MustEachPassingValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                },
                MustFailingValues = new[]
                {
                    Guid.NewGuid(),
                },
                MustEachFailingValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustPassingValues = new Guid?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { null, null },
                },
                MustFailingValues = new Guid?[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachFailingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { null, Guid.Empty, null },
                    new Guid?[] { null, Guid.NewGuid(), null },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "  \r\n  ",
                    A.Dummy<string>(),
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new string[] { null, string.Empty, null },
                },
            };

            var dateTimeTestValues = new TestValues<DateTime>
            {
                MustPassingValues = new[]
                {
                    DateTime.MinValue,
                },
                MustEachPassingValues = new IEnumerable<DateTime>[]
                {
                    new DateTime[] { },
                    new DateTime[] { DateTime.MinValue, DateTime.MinValue },
                },
                MustFailingValues = new[]
                {
                    DateTime.MaxValue,
                    DateTime.Now,
                },
                MustEachFailingValues = new IEnumerable<DateTime>[]
                {
                    new DateTime[] { DateTime.MinValue, DateTime.Now, DateTime.MinValue },
                    new DateTime[] { DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue },
                },
            };

            var decimalTestValues = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    0m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { 0m, 0m },
                },
                MustFailingValues = new[]
                {
                    decimal.MaxValue,
                    decimal.MinValue,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 0m, decimal.MaxValue, 0m },
                    new decimal[] { 0m, decimal.MinValue, 0m },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { null },
                },
                MustFailingValues = new object[]
                {
                    A.Dummy<object>(),
                },
                MustEachFailingValues = new IEnumerable<object>[]
                {
                    new object[] { null, A.Dummy<object>(), null },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(dateTimeTestValues);
            validationTest.Run(decimalTestValues);
            validationTest.Run(objectTestValues);
        }

        [Fact]
        public static void NotBeDefault___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeDefault,
                ValidationName = nameof(ParameterValidation.NotBeDefault),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeDefaultExceptionMessageSuffix,
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustPassingValues = new[]
                {
                    Guid.NewGuid(),
                },
                MustEachPassingValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid(), Guid.NewGuid() },
                },
                MustFailingValues = new[]
                {
                    Guid.Empty,
                },
                MustEachFailingValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { Guid.NewGuid(), Guid.Empty, Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustPassingValues = new Guid?[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachPassingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.NewGuid() },
                },
                MustFailingValues = new Guid?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { Guid.Empty, null, Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "  \r\n  ",
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, "  \r\n ", A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new string[] { string.Empty, null, string.Empty },
                    new string[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
            };

            var dateTimeTestValues = new TestValues<DateTime>
            {
                MustPassingValues = new[]
                {
                    DateTime.MaxValue,
                    DateTime.Now,
                },
                MustEachPassingValues = new IEnumerable<DateTime>[]
                {
                    new DateTime[] { },
                    new DateTime[] { DateTime.Now, DateTime.MaxValue },
                },
                MustFailingValues = new[]
                {
                    DateTime.MinValue,
                },
                MustEachFailingValues = new IEnumerable<DateTime>[]
                {
                    new DateTime[] { DateTime.Now, DateTime.MinValue, DateTime.Now },
                    new DateTime[] { DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue },
                },
            };

            var decimalTestValues = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    decimal.MaxValue,
                    decimal.MinValue,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { decimal.MaxValue, decimal.MinValue },
                },
                MustFailingValues = new[]
                {
                    0m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { decimal.MinValue, 0m, decimal.MinValue },
                    new decimal[] { decimal.MaxValue, 0m, decimal.MaxValue },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    A.Dummy<object>(),
                },
                MustEachPassingValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                },
                MustFailingValues = new object[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<object>[]
                {
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            // Act, Assert
            validationTest.Run(guidTestValues);
            validationTest.Run(nullableGuidTestValues);
            validationTest.Run(stringTestValues);
            validationTest.Run(dateTimeTestValues);
            validationTest.Run(decimalTestValues);
            validationTest.Run(objectTestValues);
        }

        [Fact]
        public static void BeLessThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.BeLessThan(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.BeLessThan);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeLessThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 + .00000001m, comparisonValue5 - .0000001m },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void NotBeLessThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.NotBeLessThan(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.NotBeLessThan);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeLessThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 - .0000001m, comparisonValue5 },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void BeGreaterThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.BeGreaterThan(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.BeGreaterThan);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeGreaterThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 - .00000001m, comparisonValue5 + .0000001m },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void NotBeGreaterThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.NotBeGreaterThan(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.NotBeGreaterThan);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeGreaterThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 + .0000001m, comparisonValue5 },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void BeLessThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.BeLessThanOrEqualTo(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.BeLessThanOrEqualTo);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeLessThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 + .00000001m, comparisonValue5 },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void NotBeLessThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.NotBeLessThanOrEqualTo(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.NotBeLessThanOrEqualTo);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeLessThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5, comparisonValue5 + .0000001m },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 - .0000001m, comparisonValue5 + .0000001m },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void BeGreaterThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.BeGreaterThanOrEqualTo(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.BeGreaterThanOrEqualTo);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 - .00000001m, comparisonValue5 },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.NotBeGreaterThanOrEqualTo(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.NotBeGreaterThanOrEqualTo);

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue2),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var validationTest5 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue5),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5, comparisonValue5 - .0000001m },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 + .0000001m, comparisonValue5 - .0000001m },
                },
            };

            validationTest5.Run(decimalTestValues5);
        }

        [Fact]
        public static void BeEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.BeEqualTo(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.BeEqualTo);

            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest1.Run(stringTestValues1);

            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest2.Run(decimalTestValues2);

            var comparisonValue3 = A.Dummy<decimal>();
            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue3),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeEqualToExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue3, comparisonValue3 },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue3, comparisonValue3 - .0000001m, comparisonValue3 },
                    new decimal[] { comparisonValue3, comparisonValue3 + .0000001m, comparisonValue3 },
                },
            };

            validationTest3.Run(decimalTestValues3);
        }

        [Fact]
        public static void NotBeEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T otherValue)
            {
                return (parameter, because) => parameter.NotBeEqualTo(otherValue, because);
            }

            var validationName = nameof(ParameterValidation.NotBeEqualTo);

            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest1.Run(stringTestValues1);

            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "otherValue",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest2.Run(decimalTestValues2);

            var comparisonValue3 = A.Dummy<decimal>();
            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue3),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeEqualToExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue3 - .0000001m, comparisonValue3 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue3 - .0000001m, comparisonValue3, comparisonValue3 + .0000001m },
                },
            };

            validationTest3.Run(decimalTestValues3);
        }

        [Fact]
        public static void BeInRange___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T minimum, T maximum)
            {
                return (parameter, because) => parameter.BeInRange(minimum, maximum, because: because);
            }

            var validationName = nameof(ParameterValidation.BeInRange);

            var ex1 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.IncludesMinimumAndExcludesMaximum));
            var ex2 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndIncludesMaximum));
            var ex3 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndMaximum));
            ex1.Should().BeOfType<NotImplementedException>();
            ex2.Should().BeOfType<NotImplementedException>();
            ex3.Should().BeOfType<NotImplementedException>();

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>(), A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>(), A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>(), A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "minimum",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>(), A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "minimum",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var minimum5 = A.Dummy<decimal>();
            var maximum5 = minimum5 - .00000001m;
            var validationTest5Actual = Record.Exception(() => A.Dummy<decimal>().Must().BeInRange(minimum5, maximum5, because: A.Dummy<string>()));
            validationTest5Actual.Should().BeOfType<InvalidOperationException>();
            validationTest5Actual.Message.Should().Be("The specified range is invalid because 'minimum' is less than 'maximum'.  " + ParameterValidator.ImproperUseOfFrameworkExceptionMessage);

            var minimum6 = 10m;
            var maximum6 = 20m;
            var validationTest6 = new ValidationTest
            {
                Validation = GetValidation(minimum6, maximum6),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues6 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    10m,
                    16m,
                    20m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { 10m, 16m, 20m },
                },
                MustFailingValues = new[]
                {
                    decimal.MinValue,
                    decimal.MaxValue,
                    9.999999999m,
                    20.000000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 16m, decimal.MinValue, 16m },
                    new decimal[] { 16m, decimal.MaxValue, 16m },
                    new decimal[] { 16m, 9.999999999m, 16m },
                    new decimal[] { 16m, 20.000000001m, 16m },
                },
            };

            validationTest6.Run(decimalTestValues6);

            var comparisonValue7 = A.Dummy<decimal>();
            var validationTest7 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue7, comparisonValue7),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.BeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues7 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue7,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue7, comparisonValue7 },
                },
                MustFailingValues = new decimal[]
                {
                    comparisonValue7 + .000000001m,
                    comparisonValue7 - .000000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue7, comparisonValue7 + .000000001m, comparisonValue7 },
                    new decimal[] { comparisonValue7, comparisonValue7 - .000000001m, comparisonValue7 },
                },
            };

            validationTest7.Run(decimalTestValues7);
        }

        [Fact]
        public static void NotBeInRange___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T minimum, T maximum)
            {
                return (parameter, because) => parameter.NotBeInRange(minimum, maximum, because: because);
            }

            var validationName = nameof(ParameterValidation.NotBeInRange);

            var ex1 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.IncludesMinimumAndExcludesMaximum));
            var ex2 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndIncludesMaximum));
            var ex3 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndMaximum));
            ex1.Should().BeOfType<NotImplementedException>();
            ex2.Should().BeOfType<NotImplementedException>();
            ex3.Should().BeOfType<NotImplementedException>();

            // here the otherValue type doesn't match the parameter type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>(), A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            validationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>(), A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IComparable, IComparable<T>",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IComparable>, IEnumerable<IComparable<T>>",
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustParameterInvalidTypeValues = new decimal?[]
                {
                    null,
                    comparisonValue2,
                    A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2),
                    A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ < comparisonValue2) },
                    new decimal?[] { comparisonValue2 },
                    new decimal?[] { A.Dummy<decimal>().ThatIs(_ => _ > comparisonValue2) },
                },
            };

            validationTest2.Run(nullableDecimalTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>(), A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "minimum",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            validationTest3.Run(stringTestValues3);

            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<int>(), A.Dummy<int>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "Decimal",
                ValidationParameterInvalidCastParameterName = "minimum",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustValidationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            validationTest4.Run(decimalTestValues4);

            var minimum5 = A.Dummy<decimal>();
            var maximum5 = minimum5 - .00000001m;
            var validationTest5Actual = Record.Exception(() => A.Dummy<decimal>().Must().BeInRange(minimum5, maximum5, because: A.Dummy<string>()));
            validationTest5Actual.Should().BeOfType<InvalidOperationException>();
            validationTest5Actual.Message.Should().Be("The specified range is invalid because 'minimum' is less than 'maximum'.  " + ParameterValidator.ImproperUseOfFrameworkExceptionMessage);

            var minimum6 = 10m;
            var maximum6 = 20m;
            var validationTest6 = new ValidationTest
            {
                Validation = GetValidation(minimum6, maximum6),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues6 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    9.9999999999m,
                    20.00000001m,
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { decimal.MinValue, 9.9999999999m, 20.00000001m, decimal.MaxValue },
                },
                MustFailingValues = new[]
                {
                    10m,
                    15m,
                    20m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 9.9999999999m, 10m, 20.00000001m },
                    new decimal[] { 9.9999999999m, 15m, 20.00000001m },
                    new decimal[] { 9.9999999999m, 20m, 20.00000001m },
                },
            };

            validationTest6.Run(decimalTestValues6);

            var comparisonValue7 = A.Dummy<decimal>();
            var validationTest7 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue7, comparisonValue7),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentOutOfRangeException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues7 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue7 - .000000001m,
                    comparisonValue7 + .000000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue7 - .000000001m, comparisonValue7 + .000000001m },
                },
                MustFailingValues = new decimal[]
                {
                    comparisonValue7,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue7 - .000000001m, comparisonValue7, comparisonValue7 + .000000001m },
                },
            };

            validationTest7.Run(decimalTestValues7);
        }

        [Fact]
        public static void Contain___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T item)
            {
                return (parameter, because) => parameter.Contain(item, because);
            }

            var validationName = nameof(ParameterValidation.Contain);

            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);

            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "item",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            validationTest2.Run(stringTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue3),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            {
                MustFailingValues = new IEnumerable<string>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, comparisonValue3 }, null, new string[] { A.Dummy<string>(), null, comparisonValue3 } },
                },
            };

            validationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue4),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.ContainExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new[] { comparisonValue4 },
                    new[] { 5m, comparisonValue4, 15m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new[] { comparisonValue4 }, new[] { 5m, comparisonValue4, 15m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new[] { 5m, 9.9999999m, 10.000001m, 15m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { comparisonValue4 }, new[] { 5m, 9.9999999m, 10.000001m, 15m }, new[] { comparisonValue4 } },
                },
            };

            validationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void NotContain___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            Validation GetValidation<T>(T item)
            {
                return (parameter, because) => parameter.NotContain(item, because);
            }

            var validationName = nameof(ParameterValidation.NotContain);

            var validationTest1 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<object>()),
                ValidationName = validationName,
                ParameterInvalidCastExpectedTypes = "IEnumerable",
                ParameterInvalidCastExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustParameterInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustParameterInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustParameterInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachParameterInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            validationTest1.Run(guidTestValues);
            validationTest1.Run(nullableGuidTestValues);
            validationTest1.Run(objectTestValues);

            var validationTest2 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<decimal>()),
                ValidationName = validationName,
                ValidationParameterInvalidCastExpectedTypes = "String",
                ValidationParameterInvalidCastParameterName = "item",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustValidationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachValidationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            validationTest2.Run(stringTestValues2);

            var validationTest3 = new ValidationTest
            {
                Validation = GetValidation(A.Dummy<string>()),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            {
                MustFailingValues = new IEnumerable<string>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            validationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var validationTest4 = new ValidationTest
            {
                Validation = GetValidation(comparisonValue4),
                ValidationName = validationName,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = ParameterValidation.NotContainExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[] { },
                    new[] { 5m, 9.9999999m, 10.000001m, 15m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new[] { A.Dummy<decimal>() }, new[] { 5m, 9.9999999m, 10.000001m, 15m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4 },
                    new[] { 5m, comparisonValue4, 15m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 5m, comparisonValue4, 15m }, new[] { A.Dummy<decimal>() } },
                    new[] { new[] { A.Dummy<decimal>() }, new[] { 5m, comparisonValue4, 15m } },
                },
            };

            validationTest4.Run(decimalTestValues4);
        }

        private static void Run<T>(
            this ValidationTest validationTest,
            TestValues<T> testValues)
        {
            var parameterNames = new[] { null, A.Dummy<string>() };
            var becauses = new[] { null, A.Dummy<string>() };

            foreach (var parameterName in parameterNames)
            {
                foreach (var because in becauses)
                {
                    RunPassingScenarios(validationTest, testValues, parameterName, because);

                    RunMustFailingScenarios(validationTest, testValues, parameterName, because);

                    RunMustEachImproperUseOfFrameworkScenarios<T>(validationTest, parameterName, because);

                    RunMustEachFailingScenarios(validationTest, testValues, parameterName, because);

                    RunMustInvalidParameterTypeScenarios(validationTest, testValues, parameterName, because);

                    RunMustEachInvalidParameterTypeScenarios(validationTest, testValues, parameterName, because);

                    RunInvalidValidationParameterTypeScenarios(validationTest, testValues, parameterName, because);
                }
            }
        }

        private static void RunPassingScenarios<T>(
            ValidationTest validationTest,
            TestValues<T> testValues,
            string parameterName,
            string because)
        {
            var mustParameters = testValues.MustPassingValues.Select(_ => _.Named(parameterName).Must());
            var mustEachParameters = testValues.MustEachPassingValues.Select(_ => _.Named(parameterName).Must().Each());
            var parameters = mustParameters.Concat(mustEachParameters).ToList();

            foreach (var parameter in parameters)
            {
                // Arrange
                var expected = parameter.CloneWithHasBeenValidated();

                // Act
                var actual = validationTest.Validation(parameter, because);

                // Assert
                ParameterComparer.Equals(actual, expected).Should().BeTrue();
            }
        }

        private static void RunMustFailingScenarios<T>(
            ValidationTest validationTest,
            TestValues<T> testValues,
            string parameterName,
            string because)
        {
            foreach (var failingValue in testValues.MustFailingValues)
            {
                // Arrange
                var parameter = failingValue.Named(parameterName).Must();
                var expectedExceptionMessage = because;
                if (expectedExceptionMessage == null)
                {
                    if (parameterName == null)
                    {
                        expectedExceptionMessage = "parameter " + validationTest.ExceptionMessageSuffix;
                    }
                    else
                    {
                        expectedExceptionMessage = "parameter '" + parameterName + "' " + validationTest.ExceptionMessageSuffix;
                    }
                }

                // Act
                var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                // Assert
                actual.Should().BeOfType(validationTest.ExceptionType);
                actual.Message.Should().Be(expectedExceptionMessage);
            }
        }

        private static void RunMustEachFailingScenarios<T>(
            ValidationTest validationTest,
            TestValues<T> testValues,
            string parameterName,
            string because)
        {
            foreach (var eachFailingValue in testValues.MustEachFailingValues)
            {
                // Arrange
                var parameter = eachFailingValue.Named(parameterName).Must().Each();
                var expectedExceptionMessage = because;
                if (expectedExceptionMessage == null)
                {
                    if (parameterName == null)
                    {
                        expectedExceptionMessage = "parameter contains an element that " + validationTest.ExceptionMessageSuffix;
                    }
                    else
                    {
                        expectedExceptionMessage = "parameter '" + parameterName + "' contains an element that " + validationTest.ExceptionMessageSuffix;
                    }
                }

                // Act
                var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                // Assert
                actual.Should().BeOfType(validationTest.EachExceptionType);
                actual.Message.Should().Be(expectedExceptionMessage);
            }
        }

        private static void RunMustInvalidParameterTypeScenarios<T>(
            ValidationTest validationTest,
            TestValues<T> testValues,
            string parameterName,
            string because)
        {
            foreach (var invalidTypeValue in testValues.MustParameterInvalidTypeValues)
            {
                // Arrange
                var parameter = invalidTypeValue.Named(parameterName).Must();
                var expectedMessage = Invariant($"called {validationTest.ValidationName}() on an object that is not one of the following types: {validationTest.ParameterInvalidCastExpectedTypes}");

                // Act
                var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                // Assert
                actual.Should().BeOfType<InvalidCastException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private static void RunMustEachInvalidParameterTypeScenarios<T>(
            ValidationTest validationTest,
            TestValues<T> testValues,
            string parameterName,
            string because)
        {
            foreach (var invalidTypeValue in testValues.MustEachParameterInvalidTypeValues)
            {
                // Arrange
                var parameter = invalidTypeValue.Named(parameterName).Must().Each();
                var expectedMessage = Invariant($"called {validationTest.ValidationName}() on an object that is not one of the following types: {validationTest.ParameterInvalidCastExpectedEnumerableTypes}");

                // Act
                var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                // Assert
                actual.Should().BeOfType<InvalidCastException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private static void RunMustEachImproperUseOfFrameworkScenarios<T>(
            ValidationTest validationTest,
            string parameterName,
            string because)
        {
            // Arrange
            // calling Each() on IEnumerable that is null OR a value that's not IEnumerable
            IEnumerable<string> nullEnumerable = null;
            var parameter1 = nullEnumerable.Named(parameterName).Must();
            parameter1.HasBeenEached = true;

            object notEnumerable = new object();
            var parameter2 = notEnumerable.Named(parameterName).Must();
            parameter2.HasBeenEached = true;

            // Act
            var actual1 = Record.Exception(() => validationTest.Validation(parameter1, because));
            var actual2 = Record.Exception(() => validationTest.Validation(parameter2, because));

            // Assert
            actual1.Should().BeOfType<InvalidOperationException>();
            actual1.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);

            actual2.Should().BeOfType<InvalidOperationException>();
            actual2.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
        }

        private static void RunInvalidValidationParameterTypeScenarios<T>(
            ValidationTest validationTest,
            TestValues<T> testValues,
            string parameterName,
            string because)
        {
            var mustParameters = testValues.MustValidationParameterInvalidTypeValues.Select(_ => _.Named(parameterName).Must());
            var mustEachParameters = testValues.MustEachValidationParameterInvalidTypeValues.Select(_ => _.Named(parameterName).Must().Each());
            var parameters = mustParameters.Concat(mustEachParameters).ToList();

            foreach (var parameter in parameters)
            {
                // Arrange
                var expectedMessage = Invariant($"called {validationTest.ValidationName}({validationTest.ValidationParameterInvalidCastParameterName}:) where '{validationTest.ValidationParameterInvalidCastParameterName}' is not one of the following types: {validationTest.ValidationParameterInvalidCastExpectedTypes}");

                // Act
                var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                // Assert
                actual.Should().BeOfType<InvalidCastException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private class ValidationTest
        {
            public Validation Validation { get; set; }

            public Type ExceptionType { get; set; }

            public Type EachExceptionType { get; set; }

            public string ExceptionMessageSuffix { get; set; }

            public string ParameterInvalidCastExpectedTypes { get; set; }

            public string ParameterInvalidCastExpectedEnumerableTypes { get; set; }

            public string ValidationParameterInvalidCastExpectedTypes { get; set; }

            public string ValidationParameterInvalidCastParameterName { get; set; }

            public string ValidationName { get; set; }
        }

        private class TestValues<T>
        {
            public IReadOnlyCollection<T> MustParameterInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachParameterInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustValidationParameterInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachValidationParameterInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustPassingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachPassingValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustFailingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachFailingValues { get; set; } = new List<List<T>>();
        }

        private class TestClass
        {
        }
    }
}