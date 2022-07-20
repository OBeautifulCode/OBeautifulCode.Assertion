// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Boolean.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void BeTrue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeTrue,
                VerificationName = nameof(Verifications.BeTrue),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeTrueExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool, bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool>, IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    A.Dummy<object>(),
                    new List<string> { null },
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeTrue___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not true.  Provided value is <null>.";

            var subject2 = new[] { true, false, true };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not true.  Element value is 'False'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeTrue());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeTrue());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeTrue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeTrue,
                VerificationName = nameof(Verifications.NotBeTrue),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeTrueExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool, bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool>, IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeTrue___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = true;
            var expected1 = "Provided value (name: 'subject1') is true.";

            var subject2 = new[] { false, true, false };
            var expected2 = "Provided value (name: 'subject2') contains an element that is true.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeTrue());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeTrue());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeTrueWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeTrueWhenNotNull,
                VerificationName = nameof(Verifications.BeTrueWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeTrueWhenNotNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    A.Dummy<object>(),
                    new List<string> { null },
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
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
                    new bool?[] { true },
                    new bool?[] { null },
                },
                MustFailingValues = new bool?[]
                {
                    false,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { true, false },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeTrueWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = false;
            var expected1 = "Provided value (name: 'subject1') is not null and is not true.  Provided value is 'False'.";

            var subject2 = new[] { true, false, true };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is not true.  Element value is 'False'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeTrueWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeTrueWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeTrueWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeTrueWhenNotNull,
                VerificationName = nameof(Verifications.NotBeTrueWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeTrueWhenNotNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
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
                    new bool?[] { null },
                    new bool?[] { false },
                },
                MustFailingValues = new bool?[]
                {
                    true,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { false, true },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeTrueWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = true;
            var expected1 = "Provided value (name: 'subject1') is not null and is true.";

            var subject2 = new[] { false, true, false };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is true.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeTrueWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeTrueWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeFalse___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeFalse,
                VerificationName = nameof(Verifications.BeFalse),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeFalseExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool, bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool>, IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeFalse___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not false.  Provided value is <null>.";

            var subject2 = new[] { false, true, false };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not false.  Element value is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeFalse());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeFalse());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeFalse___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeFalse,
                VerificationName = nameof(Verifications.NotBeFalse),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeFalseExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool, bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool>, IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeFalse___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = false;
            var expected1 = "Provided value (name: 'subject1') is false.";

            var subject2 = new[] { true, false, true };
            var expected2 = "Provided value (name: 'subject2') contains an element that is false.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeFalse());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeFalse());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeFalseWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeFalseWhenNotNull,
                VerificationName = nameof(Verifications.BeFalseWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeFalseWhenNotNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
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
                    new bool?[] { false },
                    new bool?[] { null },
                },
                MustFailingValues = new bool?[]
                {
                    true,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { false, true },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeFalseWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = true;
            var expected1 = "Provided value (name: 'subject1') is not null and is not false.  Provided value is 'True'.";

            var subject2 = new[] { false, true, false };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is not false.  Element value is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeFalseWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeFalseWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeFalseWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeFalseWhenNotNull,
                VerificationName = nameof(Verifications.NotBeFalseWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeFalseWhenNotNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "bool?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<bool?>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    string.Empty,
                    " \r\n  ",
                    A.Dummy<string>(),
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    new string[] { string.Empty, null, " \r\n  " },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
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
                    new bool?[] { true },
                    new bool?[] { null },
                },
                MustFailingValues = new bool?[]
                {
                    false,
                },
                MustEachFailingValues = new[]
                {
                    new bool?[] { true, false },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeFalseWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            bool? subject1 = false;
            var expected1 = "Provided value (name: 'subject1') is not null and is false.";

            var subject2 = new[] { true, false, true };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is false.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeFalseWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeFalseWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}