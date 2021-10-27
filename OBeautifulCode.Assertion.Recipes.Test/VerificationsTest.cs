// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.RegularExpressions;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Type.Recipes;

    using Xunit;

    using static System.FormattableString;

    public static class VerificationsTest
    {
        private static readonly AssertionTrackerEqualityComparer AssertionTrackerComparer = new AssertionTrackerEqualityComparer();

        private delegate AssertionTracker VerificationHandler(AssertionTracker assertionTracker, string because = null, ApplyBecause applyBecause = ApplyBecause.PrefixedToDefaultMessage, IDictionary data = null);

        [Fact]
        public static void Verifications_with_verification_parameter___Should_throw_ImproperUseOfAssertionFrameworkException_with_expected_Exception_message___When_verification_parameter_is_not_of_the_expected_type()
        {
            // Arrange
            var subject1 = A.Dummy<string>();
            var expected1 = "Called BeLessThan(comparisonValue:) where 'comparisonValue' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            var subject2 = Some.ReadOnlyDummies<string>();
            var expected2 = "Called BeLessThan(comparisonValue:) where 'comparisonValue' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            var subject3 = Some.ReadOnlyDummies<string>();
            var expected3 = "Called ContainElement(itemToSearchFor:) where 'itemToSearchFor' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            var subject4 = new[] { Some.ReadOnlyDummies<string>(), Some.ReadOnlyDummies<string>() };
            var expected4 = "Called ContainElement(itemToSearchFor:) where 'itemToSearchFor' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeLessThan(A.Dummy<decimal>()));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeLessThan(A.Dummy<decimal>()));
            var actual3 = Record.Exception(() => new { subject3 }.Must().ContainElement(A.Dummy<decimal>()));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainElement(A.Dummy<decimal>()));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ApplyBecause___Should_not_alter_default_exception_message___When_ApplyBecause_is_PrefixedToDefaultMessage_and_because_is_null_or_white_space()
        {
            // Arrange
            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: null, applyBecause: ApplyBecause.PrefixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: "  \r\n ", applyBecause: ApplyBecause.PrefixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_not_alter_default_exception_message___When_ApplyBecause_is_SuffixedToDefaultMessage_and_because_is_null_or_white_space()
        {
            // Arrange
            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: null, applyBecause: ApplyBecause.SuffixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: "  \r\n ", applyBecause: ApplyBecause.SuffixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_prefix_default_exception_message_with_because___When_ApplyBecause_is_PrefixedToDefaultMessage_and_because_is_not_null_and_not_white_space()
        {
            // Arrange
            var because = A.Dummy<string>();

            Guid? subject1 = null;
            var expected1 = because + "  Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = because + "  Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: because, applyBecause: ApplyBecause.PrefixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: because, applyBecause: ApplyBecause.PrefixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_suffix_default_exception_message_with_because___When_ApplyBecause_is_SuffixedToDefaultMessage_and_because_is_not_null_and_not_white_space()
        {
            // Arrange
            var because = A.Dummy<string>();

            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.  " + because;

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.  " + because;

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: because, applyBecause: ApplyBecause.SuffixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: because, applyBecause: ApplyBecause.SuffixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_replace_default_exception_message_with_empty_string___When_ApplyBecause_is_InLieuOfDefaultMessage_and_because_is_null()
        {
            // Arrange
            Guid? subject1 = null;

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: null, applyBecause: ApplyBecause.InLieuOfDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: null, applyBecause: ApplyBecause.InLieuOfDefaultMessage));

            // Assert
            actual1.Message.Should().Be(string.Empty);
            actual2.Message.Should().Be(string.Empty);
        }

        [Fact]
        public static void ApplyBecause___Should_replace_default_exception_message_with_because___When_ApplyBecause_is_InLieuOfDefaultMessage_and_because_is_not_null()
        {
            // Arrange
            var because = A.Dummy<string>();

            Guid? subject1 = null;

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: string.Empty, applyBecause: ApplyBecause.InLieuOfDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: because, applyBecause: ApplyBecause.InLieuOfDefaultMessage));

            // Assert
            actual1.Message.Should().Be(string.Empty);
            actual2.Message.Should().Be(because);
        }

        [Fact]
        public static void BeNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeNull,
                VerificationName = nameof(Verifications.BeNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
        }

        [Fact]
        public static void BeNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            decimal? subject1 = 5;
            var expected1 = "Provided value (name: 'subject1') is not null.  Provided value is '5'.";

            var subject2 = new decimal?[] { null, -6, null };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null.  Element value is '-6'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNull,
                VerificationName = nameof(Verifications.NotBeNull),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
        }

        [Fact]
        public static void NotBeNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            decimal? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is null.";

            var subject2 = new decimal?[] { -6, null, -5 };
            var expected2 = "Provided value (name: 'subject2') contains an element that is null.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

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

        [Fact]
        public static void NotBeNullNorWhiteSpace___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNullNorWhiteSpace,
                VerificationName = nameof(Verifications.NotBeNullNorWhiteSpace),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNullNorWhiteSpace,
                VerificationName = nameof(Verifications.NotBeNullNorWhiteSpace),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullNorWhiteSpaceExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
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
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void NotBeNullNorWhiteSpace___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "\r\n";
            var expected1 = Invariant($"Provided value (name: 'subject1') is white space.  Provided value is '{Environment.NewLine}'.");

            var subject2 = new[] { A.Dummy<string>(), "    ", A.Dummy<string>() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is white space.  Element value is '    '.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeNullNorWhiteSpace());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeNullNorWhiteSpace());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeNullOrNotWhiteSpace___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeNullOrNotWhiteSpace,
                VerificationName = nameof(Verifications.BeNullOrNotWhiteSpace),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeNullOrNotWhiteSpaceExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
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

            var stringTestValues = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    null,
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "  \r\n  ",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), " \r\n ", A.Dummy<string>() },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(stringTestValues);
        }

        [Fact]
        public static void BeNullOrNotWhiteSpace___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "\r\n";
            var expected1 = Invariant($"Provided value (name: 'subject1') is not null and is white space.  Provided value is '{Environment.NewLine}'.");

            var subject2 = new[] { A.Dummy<string>(), "    ", A.Dummy<string>() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is white space.  Element value is '    '.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeNullOrNotWhiteSpace());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeNullOrNotWhiteSpace());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeEmptyGuid___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeEmptyGuid,
                VerificationName = nameof(Verifications.BeEmptyGuid),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEmptyGuidExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "Guid, Guid?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Guid>, IEnumerable<Guid?>",
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
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    "   ",
                    "   \r\n ",
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new[] { string.Empty },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(enumerableTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeEmptyGuid___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeEmptyGuid___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeEmptyGuid,
                VerificationName = nameof(Verifications.NotBeEmptyGuid),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyGuidExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "Guid, Guid?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Guid>, IEnumerable<Guid?>",
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
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    "   ",
                    "   \r\n ",
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new[] { string.Empty },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(enumerableTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeEmptyGuid___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            Guid? subject1 = Guid.Empty;
            var expected1 = "Provided value (name: 'subject1') is an empty guid.";

            var subject2 = new Guid[] { Guid.NewGuid(), Guid.Empty, Guid.NewGuid() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty guid.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEmptyGuid());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEmptyGuid());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeEmptyString___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeEmptyString,
                VerificationName = nameof(Verifications.BeEmptyString),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEmptyStringExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(enumerableTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void BeEmptyString___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty string.  Provided value is <null>.";

            var subject2 = new[] { string.Empty, "abcd", string.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty string.  Element value is 'abcd'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyString());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyString());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeEmptyString___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeEmptyString,
                VerificationName = nameof(Verifications.NotBeEmptyString),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyStringExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string>(),
                    new List<string> { string.Empty },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string>(), new string[] { } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(enumerableTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(boolTestValues);
            verificationTest.Run(nullableBoolTestValues);
        }

        [Fact]
        public static void NotBeEmptyString___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = string.Empty;
            var expected1 = "Provided value (name: 'subject1') is an empty string.";

            var subject2 = new[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty string.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEmptyString());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEmptyString());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeEmptyEnumerable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.BeEmptyEnumerable,
                VerificationName = nameof(Verifications.BeEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.BeEmptyEnumerable,
                VerificationName = nameof(Verifications.BeEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEmptyEnumerableExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues1);

            verificationTest2.Run(stringTestValues2);
            verificationTest2.Run(enumerableTestValues2A);
            verificationTest2.Run(enumerableTestValues2B);
            verificationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void BeEmptyEnumerable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') is not an empty enumerable.  Enumerable has 1 element(s).";

            var subject2 = new[] { new object[] { }, new[] { A.Dummy<object>(), A.Dummy<object>() }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty enumerable.  Enumerable has 2 element(s).";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyEnumerable());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyEnumerable());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeEmptyEnumerable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeEmptyEnumerable,
                VerificationName = nameof(Verifications.NotBeEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeEmptyEnumerable,
                VerificationName = nameof(Verifications.NotBeEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyEnumerableExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues1);

            verificationTest2.Run(stringTestValues2);
            verificationTest2.Run(enumerableTestValues2A);
            verificationTest2.Run(enumerableTestValues2B);
            verificationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void NotBeEmptyEnumerable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new object[] { };
            var expected1 = "Provided value (name: 'subject1') is an empty enumerable.";

            var subject2 = new[] { new[] { A.Dummy<object>() }, new object[] { }, new[] { A.Dummy<object>() } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty enumerable.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEmptyEnumerable());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEmptyEnumerable());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeEmptyDictionary___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.BeEmptyDictionary;
            var verificationName = nameof(Verifications.BeEmptyDictionary);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>(), null, new Dictionary<string, string>() },
                },
            };

            verificationTest2.Run(dictionaryTest);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEmptyDictionaryExceptionMessageSuffix,
            };

            var dictionaryTest3A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary(),
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { new ListDictionary(), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()) },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary(),
                    },
                },
            };

            var dictionaryTest3B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { new Dictionary<string, string>(), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()) },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                    },
                },
            };

            var dictionaryTest3C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { new Dictionary<string, string>(), new Dictionary<string, string>(new Dictionary<string, string>()) },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    new Dictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                    },
                },
            };

            var dictionaryTest3D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>(), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()) },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                    },
                },
            };

            var dictionaryTest3E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()) },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    },
                },
            };

            verificationTest3.Run(dictionaryTest3A);
            verificationTest3.Run(dictionaryTest3B);
            verificationTest3.Run(dictionaryTest3C);
            verificationTest3.Run(dictionaryTest3D);
            verificationTest3.Run(dictionaryTest3E);
        }

        [Fact]
        public static void BeEmptyDictionary___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } };
            var expected1 = "Provided value (name: 'subject1') is not an empty dictionary.  Dictionary contains 2 key/value pair(s).";

            var subject2 = new IReadOnlyDictionary<string, string>[]
            {
                new Dictionary<string, string>(),
                new Dictionary<string, string>(), new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
            };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty dictionary.  Dictionary contains 1 key/value pair(s).";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyDictionary());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyDictionary());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeEmptyDictionary___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeEmptyDictionary;
            var verificationName = nameof(Verifications.NotBeEmptyDictionary);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
            };

            verificationTest2.Run(dictionaryTest);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyDictionaryExceptionMessageSuffix,
            };

            var dictionaryTest3A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest3.Run(dictionaryTest3A);
            verificationTest3.Run(dictionaryTest3B);
            verificationTest3.Run(dictionaryTest3C);
            verificationTest3.Run(dictionaryTest3D);
            verificationTest3.Run(dictionaryTest3E);
        }

        [Fact]
        public static void NotBeEmptyDictionary___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>();
            var expected1 = "Provided value (name: 'subject1') is an empty dictionary.";

            var subject2 = new IReadOnlyDictionary<string, string>[]
            {
                new Dictionary<string, string>(), new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                new Dictionary<string, string>(),
            };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty dictionary.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEmptyDictionary());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEmptyDictionary());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ContainSomeNullElements___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.ContainSomeNullElements;
            var verificationName = nameof(Verifications.ContainSomeNullElements);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
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

            verificationTest3.Run(enumerableTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainSomeNullElementsExceptionMessageSuffix,
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

            verificationTest4.Run(enumerableTestValues4A);
            verificationTest4.Run(enumerableTestValues4B);
            verificationTest4.Run(enumerableTestValues4C);
        }

        [Fact]
        public static void ContainSomeNullElements___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') contains no null elements.";

            var subject2 = new[] { new object[] { }, new object[] { }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains no null elements.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainSomeNullElements());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainSomeNullElements());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyNullElements___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyNullElements;
            var verificationName = nameof(Verifications.NotContainAnyNullElements);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
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

            verificationTest3.Run(enumerableTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyNullElementsExceptionMessageSuffix,
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

            verificationTest4.Run(enumerableTestValues4A);
            verificationTest4.Run(enumerableTestValues4B);
            verificationTest4.Run(enumerableTestValues4C);
        }

        [Fact]
        public static void NotContainAnyNullElements___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>(), null, A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') contains at least one null element.";

            var subject2 = new[] { new object[] { }, new object[] { A.Dummy<object>(), null, A.Dummy<object>() }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains at least one null element.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyNullElements());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyNullElements());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyNullElementsWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyNullElementsWhenNotNull;
            var verificationName = nameof(Verifications.NotContainAnyNullElementsWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyNullElementsWhenNotNullExceptionMessageSuffix,
            };

            var enumerableTestValues3A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    null,
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { null },
                    new IEnumerable[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() }, null },
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

            var enumerableTestValues3B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    null,
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { null },
                    new IList[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() }, null },
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

            var enumerableTestValues3C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    null,
                    new List<string> { },
                    new List<string> { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { null },
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, null },
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

            verificationTest3.Run(enumerableTestValues3A);
            verificationTest3.Run(enumerableTestValues3B);
            verificationTest3.Run(enumerableTestValues3C);
        }

        [Fact]
        public static void NotContainAnyNullElementsWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>(), null, A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') is not null and contains at least one null element.";

            var subject2 = new[] { new object[] { }, new object[] { A.Dummy<object>(), null, A.Dummy<object>() }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and contains at least one null element.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyNullElementsWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyNullElementsWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ContainSomeKeyValuePairsWithNullValue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.ContainSomeKeyValuePairsWithNullValue;
            var verificationName = nameof(Verifications.ContainSomeKeyValuePairsWithNullValue);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey,Any Reference Type>, IDictionary<TKey,Nullable<T>>, IReadOnlyDictionary<TKey,Any Reference Type>, IReadOnlyDictionary<TKey,Nullable<T>>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey,Any Reference Type>>, IEnumerable<IDictionary<TKey,Nullable<T>>>, IEnumerable<IReadOnlyDictionary<TKey,Any Reference Type>>, IEnumerable<IReadOnlyDictionary<TKey,Nullable<T>>>",
            };

            var dictionaryTest2A = new TestValues<IDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2B = new TestValues<Dictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new Dictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2C = new TestValues<IReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IReadOnlyDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2D = new TestValues<ReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new ReadOnlyDictionary<string, bool>[]
                {
                    new ReadOnlyDictionary<string, bool>(new Dictionary<string, bool>()),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new ReadOnlyDictionary<string, bool>[] { },
                },
            };

            verificationTest2.Run(dictionaryTest2A);
            verificationTest2.Run(dictionaryTest2B);
            verificationTest2.Run(dictionaryTest2C);
            verificationTest2.Run(dictionaryTest2D);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest3 = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), null } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), null } } },
                },
            };

            verificationTest3.Run(dictionaryTest3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainSomeKeyValuePairsWithNullValueExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), null } },
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary() { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                    },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                    },
                },
            };

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);
        }

        [Fact]
        public static void ContainSomeKeyValuePairsWithNullValue___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>();
            var expected1 = "Provided value (name: 'subject1') contains no key-value pairs with a null value.";

            var subject2 = new[] { new Dictionary<string, string>() };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains no key-value pairs with a null value.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainSomeKeyValuePairsWithNullValue());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainSomeKeyValuePairsWithNullValue());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyKeyValuePairsWithNullValue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyKeyValuePairsWithNullValue;
            var verificationName = nameof(Verifications.NotContainAnyKeyValuePairsWithNullValue);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey,Any Reference Type>, IDictionary<TKey,Nullable<T>>, IReadOnlyDictionary<TKey,Any Reference Type>, IReadOnlyDictionary<TKey,Nullable<T>>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey,Any Reference Type>>, IEnumerable<IDictionary<TKey,Nullable<T>>>, IEnumerable<IReadOnlyDictionary<TKey,Any Reference Type>>, IEnumerable<IReadOnlyDictionary<TKey,Nullable<T>>>",
            };

            var dictionaryTest2A = new TestValues<IDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2B = new TestValues<Dictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new Dictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2C = new TestValues<IReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IReadOnlyDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2D = new TestValues<ReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new ReadOnlyDictionary<string, bool>[]
                {
                    new ReadOnlyDictionary<string, bool>(new Dictionary<string, bool>()),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new ReadOnlyDictionary<string, bool>[] { },
                },
            };

            verificationTest2.Run(dictionaryTest2A);
            verificationTest2.Run(dictionaryTest2B);
            verificationTest2.Run(dictionaryTest2C);
            verificationTest2.Run(dictionaryTest2D);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest3 = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
            };

            verificationTest3.Run(dictionaryTest3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyKeyValuePairsWithNullValueExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[]
                    {
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), null } },
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);
        }

        [Fact]
        public static void NotContainAnyKeyValuePairsWithNullValue___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>() { { "abc", A.Dummy<string>() }, { "def", null } };
            var expected1 = "Provided value (name: 'subject1') contains at least one key-value pair with a null value.  For example, see this key: 'def'.";

            var subject2 = new[] { new Dictionary<string, string>() { { "abc", A.Dummy<string>() }, { "def", null } } };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains at least one key-value pair with a null value.  For example, see this key: 'def'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyKeyValuePairsWithNullValue());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyKeyValuePairsWithNullValue());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ContainKey___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().ContainKey<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called ContainKey(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void ContainKey___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainKey(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainKey);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryStringKeyTestValues3 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustFailingValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, decimal>[]
                    {
                        new Dictionary<string, decimal>
                        {
                            { comparisonValue3, 10 },
                        },
                        null,
                        new Dictionary<string, decimal>
                        {
                            { comparisonValue3, 10 },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryStringKeyTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainKeyExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues4 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue4, A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { comparisonValue4, A.Dummy<string>() },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest4.Run(dictionaryDecimalKeyTestValues4);
        }

        [Fact]
        public static void ContainKey___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 3, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 5, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainKey(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainKey(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainKey___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().NotContainKey<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotContainKey(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotContainKey___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainKey(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainKey);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryStringKeyTestValues3 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustFailingValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, decimal>[]
                    {
                        new Dictionary<string, decimal>
                        {
                            { A.Dummy<string>(), 10 },
                        },
                        null,
                        new Dictionary<string, decimal>
                        {
                            { A.Dummy<string>(), 10 },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryStringKeyTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainKeyExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues4 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>(),
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>()
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue4, A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest4.Run(dictionaryDecimalKeyTestValues4);
        }

        [Fact]
        public static void NotContainKey___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 4, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 4, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainKey(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainKey(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ContainKeyWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().ContainKeyWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called ContainKeyWhenNotNull(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void ContainKeyWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainKeyWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainKeyWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = 10m;
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainKeyWhenNotNullExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues3 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue3, A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new IReadOnlyDictionary<decimal, string>[]
                    {
                        null,
                    },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { comparisonValue3, A.Dummy<string>() },
                        },
                        null,
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryDecimalKeyTestValues3);
        }

        [Fact]
        public static void ContainKeyWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 3, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') is not null and does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 5, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainKeyWhenNotNull(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainKeyWhenNotNull(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainKeyWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().NotContainKeyWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotContainKeyWhenNotNull(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotContainKeyWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainKeyWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainKeyWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = 10m;
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainKeyWhenNotNullExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues3 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new IReadOnlyDictionary<decimal, string>[] { null },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>(),
                        null,
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>()
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue3, A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryDecimalKeyTestValues3);
        }

        [Fact]
        public static void NotContainKeyWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 4, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') is not null and contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 4, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainKeyWhenNotNull(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainKeyWhenNotNull(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeNullNorEmptyEnumerable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNullNorEmptyEnumerable,
                VerificationName = nameof(Verifications.NotBeNullNorEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNullNorEmptyEnumerable,
                VerificationName = nameof(Verifications.NotBeNullNorEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyEnumerableExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues1);

            verificationTest2.Run(stringTestValues2);
            verificationTest2.Run(enumerableTestValues2A);
            verificationTest2.Run(enumerableTestValues2B);
            verificationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void NotBeNullNorEmptyEnumerable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new object[] { };
            var expected1 = "Provided value (name: 'subject1') is an empty enumerable.";

            var subject2 = new[] { new[] { A.Dummy<object>() }, new object[] { }, new[] { A.Dummy<object>() } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty enumerable.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeNullNorEmptyEnumerable());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeNullNorEmptyEnumerable());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeNullNorEmptyDictionary___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeNullNorEmptyDictionary;
            var verificationName = nameof(Verifications.NotBeNullNorEmptyDictionary);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
            };

            verificationTest2.Run(dictionaryTest);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyDictionaryExceptionMessageSuffix,
            };

            var dictionaryTest3A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest3.Run(dictionaryTest3A);
            verificationTest3.Run(dictionaryTest3B);
            verificationTest3.Run(dictionaryTest3C);
            verificationTest3.Run(dictionaryTest3D);
            verificationTest3.Run(dictionaryTest3E);
        }

        [Fact]
        public static void NotBeNullNorEmptyDictionary___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>();
            var expected1 = "Provided value (name: 'subject1') is an empty dictionary.";

            var subject2 = new IReadOnlyDictionary<string, string>[]
            {
                new Dictionary<string, string>(), new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                new Dictionary<string, string>(),
            };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty dictionary.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeNullNorEmptyDictionary());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeNullNorEmptyDictionary());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeNullNorEmptyEnumerableNorContainAnyNulls___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeNullNorEmptyEnumerableNorContainAnyNulls;
            var verificationName = nameof(Verifications.NotBeNullNorEmptyEnumerableNorContainAnyNulls);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
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

            verificationTest3.Run(enumerableTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyEnumerableExceptionMessageSuffix,
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

            verificationTest4.Run(enumerableTestValues4A);
            verificationTest4.Run(enumerableTestValues4B);
            verificationTest4.Run(enumerableTestValues4C);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyNullElementsExceptionMessageSuffix,
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

            verificationTest5.Run(enumerableTestValues5A);
            verificationTest5.Run(enumerableTestValues5B);
            verificationTest5.Run(enumerableTestValues5C);
        }

        [Fact]
        public static void NotBeNullNorEmptyDictionaryNorContainAnyNullValues___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeNullNorEmptyDictionaryNorContainAnyNullValues;
            var verificationName = nameof(Verifications.NotBeNullNorEmptyDictionaryNorContainAnyNullValues);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey,Any Reference Type>, IDictionary<TKey,Nullable<T>>, IReadOnlyDictionary<TKey,Any Reference Type>, IReadOnlyDictionary<TKey,Nullable<T>>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey,Any Reference Type>>, IEnumerable<IDictionary<TKey,Nullable<T>>>, IEnumerable<IReadOnlyDictionary<TKey,Any Reference Type>>, IEnumerable<IReadOnlyDictionary<TKey,Nullable<T>>>",
            };

            var dictionaryTest2A = new TestValues<IDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2B = new TestValues<Dictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new Dictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2C = new TestValues<IReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IReadOnlyDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2D = new TestValues<ReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new ReadOnlyDictionary<string, bool>[]
                {
                    new ReadOnlyDictionary<string, bool>(new Dictionary<string, bool>()),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new ReadOnlyDictionary<string, bool>[] { },
                },
            };

            verificationTest2.Run(dictionaryTest2A);
            verificationTest2.Run(dictionaryTest2B);
            verificationTest2.Run(dictionaryTest2C);
            verificationTest2.Run(dictionaryTest2D);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest3 = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
            };

            verificationTest3.Run(dictionaryTest3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyDictionaryExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyKeyValuePairsWithNullValueExceptionMessageSuffix,
            };

            var dictionaryTest5A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), null } },
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest5.Run(dictionaryTest5A);
            verificationTest5.Run(dictionaryTest5B);
            verificationTest5.Run(dictionaryTest5C);
            verificationTest5.Run(dictionaryTest5D);
            verificationTest5.Run(dictionaryTest5E);
        }

        [Fact]
        public static void BeDefault___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeDefault,
                VerificationName = nameof(Verifications.BeDefault),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeDefaultExceptionMessageSuffix,
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(dateTimeTestValues);
            verificationTest.Run(decimalTestValues);
            verificationTest.Run(objectTestValues);
        }

        [Fact]
        public static void BeDefault___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int subject1 = 5;
            var expected1 = "Provided value (name: 'subject1') is not equal to default(T) using EqualityExtensions.IsEqualTo<T>, where T: int.  default(T) is '0'.  Provided value is '5'.";

            var subject2 = new[] { null, "abc", null };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not equal to default(T) using EqualityExtensions.IsEqualTo<T>, where T: string.  default(T) is <null>.  Element value is 'abc'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeDefault());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeDefault());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeDefault___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeDefault,
                VerificationName = nameof(Verifications.NotBeDefault),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeDefaultExceptionMessageSuffix,
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
            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(dateTimeTestValues);
            verificationTest.Run(decimalTestValues);
            verificationTest.Run(objectTestValues);
        }

        [Fact]
        public static void NotBeDefault___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is equal to default(T) using EqualityExtensions.IsEqualTo<T>, where T: int?.  default(T) is <null>.";

            var subject2 = new[] { 1, 0, 1 };
            var expected2 = "Provided value (name: 'subject2') contains an element that is equal to default(T) using EqualityExtensions.IsEqualTo<T>, where T: int.  default(T) is '0'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeDefault());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeDefault());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeLessThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeLessThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeLessThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2, comparisonValue2 - .0000001m },
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2 + .00000001m, comparisonValue2 - .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeLessThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 10;
            int comparisonValue3 = 5;
            var expected3 = "Provided value (name: 'subject3') is not less than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '5'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = null;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 5;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not less than the comparison value using Comparer<T>.Default, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '5'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeLessThan(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeLessThan(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeLessThan(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeLessThan(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeLessThan(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeLessThan(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeLessThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeLessThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeLessThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2, comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, null, comparisonValue2 },
                    new decimal?[] { comparisonValue2, comparisonValue2 - .0000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeLessThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int subject3 = 10;
            int comparisonValue3 = 20;
            var expected3 = "Provided value (name: 'subject3') is less than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 20;
            var expected6 = "Provided value (name: 'subject6') contains an element that is less than the comparison value using Comparer<T>.Default, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeLessThan(comparisonValue1));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeLessThan(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeLessThan(comparisonValue4));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeLessThan(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeGreaterThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeGreaterThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeGreaterThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            decimal? comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 + .0000001m, null, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2 - .00000001m, comparisonValue2 + .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>(), null, A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeGreaterThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int? subject2 = null;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int subject3 = 5;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not greater than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { null };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 5 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not greater than the comparison value using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeGreaterThan(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeGreaterThan(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeGreaterThan(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeGreaterThan(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeGreaterThan(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeGreaterThan(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeGreaterThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeGreaterThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeGreaterThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, comparisonValue2 + .0000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, A.Dummy<decimal>(), null },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeGreaterThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 10;
            int comparisonValue3 = 5;
            var expected3 = "Provided value (name: 'subject3') is greater than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '5'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 5;
            var expected6 = "Provided value (name: 'subject6') contains an element that is greater than the comparison value using Comparer<T>.Default, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '5'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeGreaterThan(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeGreaterThan(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeGreaterThan(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeGreaterThan(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeLessThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeLessThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeLessThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, comparisonValue2 + .00000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, A.Dummy<decimal>(), null },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeLessThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 20;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '20'.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 20 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '20'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeLessThanOrEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeLessThanOrEqualTo(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeLessThanOrEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeLessThanOrEqualTo(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeLessThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeLessThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeLessThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 + .0000001m, null, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2 - .0000001m, comparisonValue2 + .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>(), null,  A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeLessThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int? subject2 = null;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int subject3 = 5;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { null };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 5 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeLessThanOrEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeLessThanOrEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeLessThanOrEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeLessThanOrEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeLessThanOrEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeLessThanOrEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeGreaterThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeGreaterThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeGreaterThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2, comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, null, comparisonValue2 },
                    new decimal?[] { comparisonValue2, comparisonValue2 - .00000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeGreaterThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int subject3 = 5;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject6 = new int[] { 5 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeGreaterThanOrEqualTo(comparisonValue1));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeGreaterThanOrEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeGreaterThanOrEqualTo(comparisonValue4));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeGreaterThanOrEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeGreaterThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeGreaterThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2, comparisonValue2 - .0000001m },
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2 + .0000001m, comparisonValue2 - .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
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

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 20;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '20'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = null;
            var expected4 = "Provided value (name: 'subject4') contains an element that is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 20 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '20'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeGreaterThanOrEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeGreaterThanOrEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeGreaterThanOrEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeGreaterThanOrEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeGreaterThanOrEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeGreaterThanOrEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeEqualTo);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToExceptionMessageSuffix,
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

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { "4", "5", null }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { "4", "5", null }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        comparisonValue4,
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void BeEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 10;
            int comparisonValue3 = 20;
            var expected3 = "Provided value (name: 'subject3') is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 20;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeEqualTo);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToExceptionMessageSuffix,
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

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "ghi",
                                new List<string> { "4", null, "5", }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", "5" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { null, "5", "4" }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { null, "5", "4" }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void NotBeEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'comparisonValue' is <null>.";

            int subject2 = 10;
            int comparisonValue2 = 10;
            var expected2 = "Provided value (name: 'subject2') is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            var subject3 = new int?[] { null };
            int? comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'comparisonValue' is <null>.";

            var subject4 = new int[] { 10 };
            int comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeEqualTo(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeEqualTo(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeEqualTo(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeEqualToWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToWhenNotNullExceptionMessageSuffix,
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

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { "4", "5", null }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                    },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { "4", "5", null }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        comparisonValue4,
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void BeEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 10;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject2 = 10;
            int comparisonValue2 = 20;
            var expected2 = "Provided value (name: 'subject2') is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject3 = new int?[] { 10 };
            int? comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject4 = new int[] { 10 };
            int comparisonValue4 = 20;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEqualToWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeEqualToWhenNotNull(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeEqualToWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeEqualToWhenNotNull(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeEqualToWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToWhenNotNullExceptionMessageSuffix,
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

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                    },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "ghi",
                                new List<string> { "4", null, "5", }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", "5" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { null, "5", "4" }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { null, "5", "4" }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void NotBeEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int subject1 = 10;
            int comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not null and is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            var subject2 = new int[] { 10 };
            int comparisonValue2 = 10;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEqualToWhenNotNull(comparisonValue1));

            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEqualToWhenNotNull(comparisonValue2));

            // Assert
            actual1.Message.Should().Be(expected1);

            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeEqualToAnyOf___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeEqualToAnyOf<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeEqualToAnyOf(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void BeEqualToAnyOf___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.BeEqualToAnyOf(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeEqualToAnyOf);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty comparisonValues always fails
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToAnyOfExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustFailingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<decimal>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToAnyOfExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachPassingValues = new[]
                {
                    new decimal[0],
                    new[] { comparisonValue4.First(), comparisonValue4.Last() },
                },
                MustFailingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4.First(), A.Dummy<decimal>() },
                    new[] { A.Dummy<decimal>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void BeEqualToAnyOf___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            var comparisonValues1 = new int?[] { 10 };
            var expected1 = "Provided value (name: 'subject1') is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is <null>.  Specified 'comparisonValues' is ['10'].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected2 = "Provided value (name: 'subject2') is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            var subject3 = new int?[] { null };
            var comparisonValues3 = new int?[] { 10 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is <null>.  Specified 'comparisonValues' is ['10'].";

            var subject4 = new[] { 30, 130 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '130'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEqualToAnyOf(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeEqualToAnyOf(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeEqualToAnyOf(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeEqualToAnyOf(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeEqualToAnyOf___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeEqualToAnyOf<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeEqualToAnyOf(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotBeEqualToAnyOf___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.NotBeEqualToAnyOf(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeEqualToAnyOf);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty collection always succeeds
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToAnyOfExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<decimal>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToAnyOfExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new[]
                {
                    new decimal[0],
                    new[] { A.Dummy<decimal>(), A.Dummy<decimal>() },
                },
                MustFailingValues = new[]
                {
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4.First() },
                    new[] { A.Dummy<decimal>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void NotBeEqualToAnyOf___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            var comparisonValues1 = new int?[] { 5, null };
            var expected1 = "Provided value (name: 'subject1') is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is <null>.  Specified 'comparisonValues' is ['5', <null>].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 10, 120 };
            var expected2 = "Provided value (name: 'subject2') is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '10', ...].";

            var subject3 = new int?[] { 20, null };
            var comparisonValues3 = new int?[] { 10, null };
            var expected3 = "Provided value (name: 'subject3') contains an element that is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is <null>.  Specified 'comparisonValues' is ['10', <null>].";

            var subject4 = new[] { 130, 30 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '30'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEqualToAnyOf(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeEqualToAnyOf(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeEqualToAnyOf(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeEqualToAnyOf(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeEqualToAnyOfWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeEqualToAnyOfWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeEqualToAnyOfWhenNotNull(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void BeEqualToAnyOfWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.BeEqualToAnyOfWhenNotNull(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeEqualToAnyOfWhenNotNull);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty comparisonValues always fails
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToAnyOfWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustFailingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<string>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToAnyOfWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<string>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachPassingValues = new[]
                {
                    new string[0],
                    new[] { comparisonValue4.First(), null, comparisonValue4.Last() },
                },
                MustFailingValues = new[]
                {
                    A.Dummy<string>(),
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new[] { comparisonValue4.First(), A.Dummy<string>() },
                    new[] { A.Dummy<string>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void BeEqualToAnyOfWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 5;
            var comparisonValues1 = new int?[] { 10 };
            var expected1 = "Provided value (name: 'subject1') is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '5'.  Specified 'comparisonValues' is ['10'].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected2 = "Provided value (name: 'subject2') is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            var subject3 = new int?[] { null, 8 };
            var comparisonValues3 = new int?[] { 10 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '8'.  Specified 'comparisonValues' is ['10'].";

            var subject4 = new[] { 30, 130 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '130'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEqualToAnyOfWhenNotNull(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeEqualToAnyOfWhenNotNull(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeEqualToAnyOfWhenNotNull(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeEqualToAnyOfWhenNotNull(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeEqualToAnyOfWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeEqualToAnyOfWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeEqualToAnyOfWhenNotNull(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotBeEqualToAnyOfWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.NotBeEqualToAnyOfWhenNotNull(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeEqualToAnyOfWhenNotNull);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty collection always succeeds
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToAnyOfWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<string>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToAnyOfWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<string>
            {
                MustPassingValues = new[]
                {
                    null,
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[0],
                    new[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
                MustFailingValues = new[]
                {
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new[] { comparisonValue4.First() },
                    new[] { A.Dummy<string>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void NotBeEqualToAnyOfWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 5;
            var comparisonValues1 = new int?[] { null, 5 };
            var expected1 = "Provided value (name: 'subject1') is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '5'.  Specified 'comparisonValues' is [<null>, '5'].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 10, 120 };
            var expected2 = "Provided value (name: 'subject2') is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '10', ...].";

            var subject3 = new int?[] { null, 10 };
            var comparisonValues3 = new int?[] { null, 10 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '10'.  Specified 'comparisonValues' is [<null>, '10'].";

            var subject4 = new[] { 130, 30 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '30'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEqualToAnyOfWhenNotNull(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeEqualToAnyOfWhenNotNull(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeEqualToAnyOfWhenNotNull(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeEqualToAnyOfWhenNotNull(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeInRange___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T minimum, T maximum)
            {
                return (subject, because, applyBecause, data) => subject.BeInRange(minimum, maximum, because: because, applyBecause: applyBecause, data: data);
            }

            var verificationName = nameof(Verifications.BeInRange);

            var ex1 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.IncludesMinimumAndExcludesMaximum));
            var ex2 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndIncludesMaximum));
            var ex3 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndMaximum));
            ex1.Should().BeOfType<NotImplementedException>();
            ex2.Should().BeOfType<NotImplementedException>();
            ex3.Should().BeOfType<NotImplementedException>();

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>(), A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            decimal? minimum2 = 10m;
            decimal? maximum2 = 20m;
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum2, maximum2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    10m,
                    16m,
                    20m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { 10m, 16m, 20m },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    decimal.MinValue,
                    decimal.MaxValue,
                    9.999999999m,
                    20.000000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { 16m, null, 16m },
                    new decimal?[] { 16m, decimal.MinValue, 16m },
                    new decimal?[] { 16m, decimal.MaxValue, 16m },
                    new decimal?[] { 16m, 9.999999999m, 16m },
                    new decimal?[] { 16m, 20.000000001m, 16m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>(), A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "minimum",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>(), A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "minimum",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var minimum5 = 5m;
            var maximum5 = 4.5m;
            var verificationTest5Actual = Record.Exception(() => A.Dummy<decimal>().Must().BeInRange(minimum5, maximum5, because: A.Dummy<string>()));
            verificationTest5Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest5Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '5'.  Specified 'maximum' is '4.5'.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            var minimum6 = 10m;
            var maximum6 = 20m;
            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum6, maximum6),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
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

            verificationTest6.Run(decimalTestValues6);

            var comparisonValue7 = A.Dummy<decimal>();
            var verificationTest7 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue7, comparisonValue7),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
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

            verificationTest7.Run(decimalTestValues7);

            var verificationTest8Actual = Record.Exception(() => A.Dummy<decimal?>().Must().BeInRange(10m, (decimal?)null, because: A.Dummy<string>()));
            verificationTest8Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest8Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '10'.  Specified 'maximum' is <null>.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            decimal? maximum9 = 20m;
            var verificationTest9 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(null, maximum9),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues9 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    decimal.MinValue,
                    20m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, decimal.MinValue, 20m },
                },
                MustFailingValues = new decimal?[]
                {
                    20.000000001m,
                    decimal.MaxValue,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, 20.000000001m, null },
                    new decimal?[] { null, decimal.MaxValue, null },
                },
            };

            verificationTest9.Run(nullableDecimalTestValues9);

            var verificationTest10 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<decimal?>(null, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues10 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, decimal.MinValue, null },
                    new decimal?[] { null, decimal.MaxValue, null },
                },
            };

            verificationTest10.Run(nullableDecimalTestValues10);
        }

        [Fact]
        public static void BeInRange___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? minimum1 = 10;
            int? maximum1 = 20;
            var expected1 = "Provided value (name: 'subject1') is not within the specified range using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            int? subject2 = 5;
            int? minimum2 = null;
            int? maximum2 = null;
            var expected2 = "Provided value (name: 'subject2') is not within the specified range using Comparer<T>.Default, where T: int?.  Provided value is '5'.  Specified 'minimum' is <null>.  Specified 'maximum' is <null>.";

            int subject3 = 5;
            int minimum3 = 10;
            int maximum3 = 20;
            var expected3 = "Provided value (name: 'subject3') is not within the specified range using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            var subject4 = new int?[] { null };
            int? minimum4 = 10;
            int? maximum4 = 20;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not within the specified range using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            var subject5 = new int?[] { 5 };
            int? minimum5 = null;
            int? maximum5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not within the specified range using Comparer<T>.Default, where T: int?.  Element value is '5'.  Specified 'minimum' is <null>.  Specified 'maximum' is <null>.";

            var subject6 = new int[] { 5 };
            int minimum6 = 10;
            int maximum6 = 20;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not within the specified range using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeInRange(minimum1, maximum1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeInRange(minimum2, maximum2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeInRange(minimum3, maximum3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeInRange(minimum4, maximum4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeInRange(minimum5, maximum5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeInRange(minimum6, maximum6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeInRange___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T minimum, T maximum)
            {
                return (subject, because, applyBecause, data) => subject.NotBeInRange(minimum, maximum, because: because, applyBecause: applyBecause, data: data);
            }

            var verificationName = nameof(Verifications.NotBeInRange);

            var ex1 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.IncludesMinimumAndExcludesMaximum));
            var ex2 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndIncludesMaximum));
            var ex3 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndMaximum));
            ex1.Should().BeOfType<NotImplementedException>();
            ex2.Should().BeOfType<NotImplementedException>();
            ex3.Should().BeOfType<NotImplementedException>();

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>(), A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            decimal? minimum2 = 10m;
            decimal? maximum2 = 20m;
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum2, maximum2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    9.9999999999m,
                    20.00000001m,
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, decimal.MinValue, 9.9999999999m, 20.00000001m, decimal.MaxValue },
                },
                MustFailingValues = new decimal?[]
                {
                    10m,
                    15m,
                    20m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { 9.9999999999m, 10m, 20.00000001m },
                    new decimal?[] { 9.9999999999m, 15m, 20.00000001m },
                    new decimal?[] { 9.9999999999m, 20m, 20.00000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>(), A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "minimum",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>(), A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "minimum",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var minimum5 = 5m;
            var maximum5 = 4.5m;
            var verificationTest5Actual = Record.Exception(() => A.Dummy<decimal>().Must().NotBeInRange(minimum5, maximum5, because: A.Dummy<string>()));
            verificationTest5Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest5Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '5'.  Specified 'maximum' is '4.5'.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            var minimum6 = 10m;
            var maximum6 = 20m;
            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum6, maximum6),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
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

            verificationTest6.Run(decimalTestValues6);

            var comparisonValue7 = A.Dummy<decimal>();
            var verificationTest7 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue7, comparisonValue7),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
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

            verificationTest7.Run(decimalTestValues7);

            var verificationTest8Actual = Record.Exception(() => A.Dummy<decimal?>().Must().NotBeInRange(10m, (decimal?)null, because: A.Dummy<string>()));
            verificationTest8Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest8Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '10'.  Specified 'maximum' is <null>.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            decimal? maximum9 = 20m;
            var verificationTest9 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(null, maximum9),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues9 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    20.00000000001m,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { 20.00000000001m, decimal.MaxValue },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    20m,
                    decimal.MinValue,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { decimal.MaxValue, null, decimal.MaxValue },
                    new decimal?[] { decimal.MaxValue, 20m, decimal.MaxValue },
                    new decimal?[] { decimal.MaxValue, decimal.MinValue, decimal.MaxValue },
                },
            };

            verificationTest9.Run(nullableDecimalTestValues9);

            var verificationTest10 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<decimal?>(null, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues10 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { decimal.MinValue },
                    new decimal?[] { decimal.MaxValue },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { decimal.MinValue, null, decimal.MinValue },
                },
            };

            verificationTest10.Run(nullableDecimalTestValues10);
        }

        [Fact]
        public static void ContainElement___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainElement(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainElement);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
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

            verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementExceptionMessageSuffix,
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

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { null, "a", "b" },
                        },
                        new[]
                        {
                            new[] { "b", "a", null },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void ContainElement___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 2, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 2, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainElement(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainElement(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainElement(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainElement(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotContainElement___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainElement(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainElement);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
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

            verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementExceptionMessageSuffix,
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

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void NotContainElement___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, null, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 10, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, null, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 10, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainElement(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotContainElement(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotContainElement(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotContainElement(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ContainElementWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainElementWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainElementWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            // null is allowed for this verification
            ////var comparisonValue3 = A.Dummy<string>();
            ////var verificationTest3 = new VerificationTest
            ////{
            ////    VerificationHandler = GetVerificationHandler(comparisonValue3),
            ////    VerificationName = verificationName,
            ////    ArgumentExceptionType = typeof(ArgumentNullException),
            ////    EachArgumentExceptionType = typeof(ArgumentException),
            ////    ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            ////};

            ////var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            ////{
            ////    MustFailingValues = new IEnumerable<string>[]
            ////    {
            ////        null,
            ////    },
            ////    MustEachFailingValues = new[]
            ////    {
            ////        new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, comparisonValue3 }, null, new string[] { A.Dummy<string>(), null, comparisonValue3 } },
            ////    },
            ////};

            ////verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementWhenNotNullExceptionMessageSuffix,
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

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementWhenNotNullExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new[]
                    {
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { null, "a", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "b", "a", null },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void ContainElementWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 2, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 2, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainElementWhenNotNull(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainElementWhenNotNull(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainElementWhenNotNull(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainElementWhenNotNull(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotContainElementWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainElementWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainElementWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            // nulls are allowed
            ////var verificationTest3 = new VerificationTest
            ////{
            ////    VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
            ////    VerificationName = verificationName,
            ////    ArgumentExceptionType = typeof(ArgumentNullException),
            ////    EachArgumentExceptionType = typeof(ArgumentException),
            ////    ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            ////};

            ////var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            ////{
            ////    MustFailingValues = new IEnumerable<string>[]
            ////    {
            ////        null,
            ////    },
            ////    MustEachFailingValues = new[]
            ////    {
            ////        new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
            ////    },
            ////};

            ////verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementWhenNotNullExceptionMessageSuffix,
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

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementWhenNotNullExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            null,
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void NotContainElementWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, null, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 10, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, null, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 10, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainElementWhenNotNull(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotContainElementWhenNotNull(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotContainElementWhenNotNull(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotContainElementWhenNotNull(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ContainOnlyDistinctElements___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            var verificationName = nameof(Verifications.ContainOnlyDistinctElements);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustFailingValues = new IEnumerable<string>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable<string>[] { new string[] { A.Dummy<string>() }, null, new string[] { A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(enumerableTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[0],
                    new[] { 5m },
                    new[] { 5m, 15m },
                    new[] { 5m, 15m, 20m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new decimal[] { }, new[] { 5m }, new[] { 5m, 15m }, new[] { 5m, 15m, 20m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 1m, 1m },
                    new decimal[] { 1m, 2m, 1m },
                    new decimal[] { 1m, 1m, 2m },
                    new decimal[] { 1m, 2m, 2m },
                    new decimal[] { 1m, 2m, 3m, 1m },
                    new decimal[] { 1m, 2m, 2m, 3m },
                    new decimal[] { 1m, 1m, 2m, 3m },
                    new decimal[] { 1m, 2m, 3m, 3m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 1m }, new[] { 1m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 3m }, new[] { 1m } },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // unordered collection
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                    },
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                    },
                    new[]
                    {
                        null,
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new[]
                    {
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                            new[] { "a", "c" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "a", "b" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "a" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "c" },
                        new[] { "c", "b" },
                        new[] { "c", "d" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                        null,
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "a", "b" },
                            new[] { "b", "c" },
                            new[] { "c", "b" },
                            new[] { "c", "d" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void ContainOnlyDistinctElements___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3, 1 };
            var expected1 = "Provided value (name: 'subject1') contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject2 = new int?[] { 1, 2, null, null, 3 };
            var expected2 = "Provided value (name: 'subject2') contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3, 1 } };
            var expected3 = "Provided value (name: 'subject3') contains an element that contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject4 = new int?[][] { new int?[] { 1, null, null, 2, 3 } };
            var expected4 = "Provided value (name: 'subject4') contains an element that contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainOnlyDistinctElements());
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainOnlyDistinctElements());

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainOnlyDistinctElements());
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainOnlyDistinctElements());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ContainOnlyDistinctElementsWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            var verificationName = nameof(Verifications.ContainOnlyDistinctElementsWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            // nulls allowed
            ////var verificationTest2 = new VerificationTest
            ////{
            ////    VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
            ////    VerificationName = verificationName,
            ////    ArgumentExceptionType = typeof(ArgumentNullException),
            ////    EachArgumentExceptionType = typeof(ArgumentException),
            ////    ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            ////};

            ////var enumerableTestValues2 = new TestValues<IEnumerable<string>>
            ////{
            ////    MustFailingValues = new IEnumerable<string>[]
            ////    {
            ////        null,
            ////    },
            ////    MustEachFailingValues = new[]
            ////    {
            ////        new IEnumerable<string>[] { new string[] { A.Dummy<string>() }, null, new string[] { A.Dummy<string>() } },
            ////    },
            ////};

            ////verificationTest2.Run(enumerableTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[0],
                    new[] { 5m },
                    new[] { 5m, 15m },
                    new[] { 5m, 15m, 20m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new decimal[] { }, new[] { 5m }, new[] { 5m, 15m }, new[] { 5m, 15m, 20m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 1m, 1m },
                    new decimal[] { 1m, 2m, 1m },
                    new decimal[] { 1m, 1m, 2m },
                    new decimal[] { 1m, 2m, 2m },
                    new decimal[] { 1m, 2m, 3m, 1m },
                    new decimal[] { 1m, 2m, 2m, 3m },
                    new decimal[] { 1m, 1m, 2m, 3m },
                    new decimal[] { 1m, 2m, 3m, 3m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 1m }, new[] { 1m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 3m }, new[] { 1m } },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // unordered collection
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsWhenNotNullExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                    },
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                    },
                    new[]
                    {
                        null,
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        null,
                    },
                    new[]
                    {
                        null,
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                            new[] { "a", "c" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "a", "b" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "a" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "c" },
                        new[] { "c", "b" },
                        new[] { "c", "d" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                        null,
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "a", "b" },
                            new[] { "b", "c" },
                            new[] { "c", "b" },
                            new[] { "c", "d" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void ContainOnlyDistinctElementsWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3, 1 };
            var expected1 = "Provided value (name: 'subject1') is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject2 = new int?[] { 1, 2, null, null, 3 };
            var expected2 = "Provided value (name: 'subject2') is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3, 1 } };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject4 = new int?[][] { new int?[] { 1, null, null, 2, 3 } };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainOnlyDistinctElementsWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainOnlyDistinctElementsWhenNotNull());

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainOnlyDistinctElementsWhenNotNull());
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainOnlyDistinctElementsWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeAlphabetic___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(char[] otherAllowedCharacters)
            {
                return (subject, because, applyBecause, data) => subject.BeAlphabetic(otherAllowedCharacters, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<char[]>()),
                VerificationName = nameof(Verifications.BeAlphabetic),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "isAlphabetic", null, "isAlphabetic" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { A.Dummy<string>() } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(null),
                VerificationName = nameof(Verifications.BeAlphabetic),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAlphabeticExceptionMessageSuffix,
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" },
                },
                MustFailingValues = new[]
                {
                    " ",
                    "\r\n",
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyz5ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                    "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0", string.Empty },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new char[0]),
                VerificationName = nameof(Verifications.BeAlphabetic),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAlphabeticExceptionMessageSuffix,
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" },
                },
                MustFailingValues = new[]
                {
                    " ",
                    "\r\n",
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyz5ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                    "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0", string.Empty },
                },
            };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new[] { 'b', '-', '_', '^', '\\', '/', '(', 'b', ' ' }),
                VerificationName = nameof(Verifications.BeAlphabetic),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAlphabeticExceptionMessageSuffix,
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    @"abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-_^\/(",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, @"abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-_^\/(" },
                },
                MustFailingValues = new[]
                {
                    "\r\n",
                    "9abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                    "&abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ)",
                    "abcdefghijklmnopqrstuvwxyz$ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ)", string.Empty },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);

            verificationTest4.Run(stringTestValues4);
        }

        [Fact]
        public static void BeAlphabetic___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "abc-def";
            var expected1 = "Provided value (name: 'subject1') is not alphabetic.  Provided value is 'abc-def'.  Specified 'otherAllowedCharacters' is <null>.";

            var subject2 = "abc-def";
            var expected2 = "Provided value (name: 'subject2') is not alphabetic.  Provided value is 'abc-def'.  Specified 'otherAllowedCharacters' is [<empty>].";

            var subject3 = "abc4def";
            var expected3 = "Provided value (name: 'subject3') is not alphabetic.  Provided value is 'abc4def'.  Specified 'otherAllowedCharacters' is ['-'].";

            var subject4 = new[] { "a-c", "d7f", "g*i" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not alphabetic.  Element value is 'd7f'.  Specified 'otherAllowedCharacters' is ['-', '*'].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAlphabetic());
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeAlphabetic(otherAllowedCharacters: new char[0]));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeAlphabetic(otherAllowedCharacters: new[] { '-' }));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeAlphabetic(otherAllowedCharacters: new[] { '-', '*' }));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeAlphanumeric___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(char[] otherAllowedCharacters)
            {
                return (subject, because, applyBecause, data) => subject.BeAlphanumeric(otherAllowedCharacters, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<char[]>()),
                VerificationName = nameof(Verifications.BeAlphanumeric),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "isalphanumeric1", null, "isalphanumeric2" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { A.Dummy<string>() } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(null),
                VerificationName = nameof(Verifications.BeAlphanumeric),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAlphanumericExceptionMessageSuffix,
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" },
                },
                MustFailingValues = new[]
                {
                    " ",
                    "\r\n",
                    "-abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*",
                    "abcdefghijklmnopqrstuvwxyz%ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*", string.Empty },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new char[0]),
                VerificationName = nameof(Verifications.BeAlphanumeric),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAlphanumericExceptionMessageSuffix,
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" },
                },
                MustFailingValues = new[]
                {
                    " ",
                    "\r\n",
                    "-abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*",
                    "abcdefghijklmnopqrstuvwxyz%ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*", string.Empty },
                },
            };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new[] { '0', '-', '_', '^', '\\', '/', '(', '0', ' ' }),
                VerificationName = nameof(Verifications.BeAlphanumeric),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAlphanumericExceptionMessageSuffix,
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    @"abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-_^\/(1234567890",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, @"abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-_^\/(1234567890" },
                },
                MustFailingValues = new[]
                {
                    "\r\n",
                    "&abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890)",
                    "abcdefghijklmnopqrstuvwxyz$ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890)", string.Empty },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);

            verificationTest4.Run(stringTestValues4);
        }

        [Fact]
        public static void BeAlphanumeric___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "abc-def";
            var expected1 = "Provided value (name: 'subject1') is not alphanumeric.  Provided value is 'abc-def'.  Specified 'otherAllowedCharacters' is <null>.";

            var subject2 = "abc-def";
            var expected2 = "Provided value (name: 'subject2') is not alphanumeric.  Provided value is 'abc-def'.  Specified 'otherAllowedCharacters' is [<empty>].";

            var subject3 = "abc*def";
            var expected3 = "Provided value (name: 'subject3') is not alphanumeric.  Provided value is 'abc*def'.  Specified 'otherAllowedCharacters' is ['-'].";

            var subject4 = new[] { "a-c", "d f", "g*i" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not alphanumeric.  Element value is 'd f'.  Specified 'otherAllowedCharacters' is ['-', '*'].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAlphanumeric());
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeAlphanumeric(otherAllowedCharacters: new char[0]));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeAlphanumeric(otherAllowedCharacters: new[] { '-' }));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeAlphanumeric(otherAllowedCharacters: new[] { '-', '*' }));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeAsciiPrintable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(bool treatNewlineAsPrintable)
            {
                return (subject, because, applyBecause, data) => subject.BeAsciiPrintable(treatNewlineAsPrintable, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(false),
                VerificationName = nameof(Verifications.BeAsciiPrintable),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "isalphanumeric1", null, "isalphanumeric2" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { A.Dummy<string>() } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(false),
                VerificationName = nameof(Verifications.BeAsciiPrintable),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAsciiPrintableExceptionMessageSuffix,
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ !""#$%&'()*+,-./0123456789:;<=>?@[\]^_`{|}~",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ !""#$%&'()*+,-./0123456789:;<=>?@[\]^_`{|}~" },
                },
                MustFailingValues = new[]
                {
                    "\r\n",
                    $@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ !""#$%&'()*+{Environment.NewLine},-./0123456789:;<=>?@[\]^_`{{|}}~",
                    Convert.ToChar(31).ToString(),
                    Convert.ToChar(127).ToString(),
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "\r\n", string.Empty },
                    new string[] { string.Empty, $@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ !""#$%&'()*+{Environment.NewLine},-./0123456789:;<=>?@[\]^_`{{|}}~", string.Empty },
                    new string[] { string.Empty, Convert.ToChar(31).ToString(), string.Empty },
                    new string[] { string.Empty, Convert.ToChar(127).ToString(), string.Empty },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(true),
                VerificationName = nameof(Verifications.BeAsciiPrintable),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAsciiPrintableExceptionMessageSuffix,
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    $@"abcdefghijklmnopqrstuvwxyz{Environment.NewLine}ABCDEFGHIJKLMNOPQRSTUVWXYZ !""#$%&'()*+{Environment.NewLine},-./0123456789:;<=>?@[\]^_`{{|}}~",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, $@"abcdefghijklmnopqrstuvwxyz{Environment.NewLine}ABCDEFGHIJKLMNOPQRSTUVWXYZ !""#$%&'()*+{Environment.NewLine},-./0123456789:;<=>?@[\]^_`{{|}}~" },
                },
                MustFailingValues = new[]
                {
                    Convert.ToChar(31).ToString(),
                    Convert.ToChar(127).ToString(),
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, Convert.ToChar(31).ToString(), string.Empty },
                    new string[] { string.Empty, Convert.ToChar(127).ToString(), string.Empty },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void BeAsciiPrintable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = $"abc{Environment.NewLine}def";
            var expected1 = $"Provided value (name: 'subject1') is not ASCII Printable.  Provided value is 'abc{Environment.NewLine}def'.  Specified 'treatNewLineAsPrintable' is 'False'.";

            var subject2 = $"abc{Environment.NewLine}def" + Convert.ToChar(30);
            var expected2 = $"Provided value (name: 'subject2') is not ASCII Printable.  Provided value is 'abc{Environment.NewLine}def{Convert.ToChar(30)}'.  Specified 'treatNewLineAsPrintable' is 'True'.";

            var subject3 = new[] { "a-c", $"d{Environment.NewLine}f", "g*i" };
            var expected3 = $"Provided value (name: 'subject3') contains an element that is not ASCII Printable.  Element value is 'd{Environment.NewLine}f'.  Specified 'treatNewLineAsPrintable' is 'False'.";

            var subject4 = new[] { "a-c", $"d{Environment.NewLine}f" + Convert.ToChar(30), "g*i" };
            var expected4 = $"Provided value (name: 'subject4') contains an element that is not ASCII Printable.  Element value is 'd{Environment.NewLine}f{Convert.ToChar(30)}'.  Specified 'treatNewLineAsPrintable' is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAsciiPrintable());
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeAsciiPrintable(true));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeAsciiPrintable());
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeAsciiPrintable(true));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeMatchedByRegex___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_regex_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeMatchedByRegex(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeMatchedByRegex(regex:) where parameter 'regex' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void BeMatchedByRegex___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Regex regex)
            {
                return (subject, because, applyBecause, data) => subject.BeMatchedByRegex(regex, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new Regex("abc")),
                VerificationName = nameof(Verifications.BeMatchedByRegex),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "abc", null, "abc" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { A.Dummy<string>() } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new Regex("abc")),
                VerificationName = nameof(Verifications.BeMatchedByRegex),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeMatchedByRegexExceptionMessageSuffix,
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "abc",
                    "def-abc-def",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "abc", "def-abc-def" },
                },
                MustFailingValues = new[]
                {
                    string.Empty,
                    "\r\n",
                    "a-b-c",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abc", "a-b-c", "abc" },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void BeMatchedByRegex___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "abc-def";
            var regex1 = new Regex("^abc$");
            var expected1 = "Provided value (name: 'subject1') is not matched by the specified regex.  Provided value is 'abc-def'.  Specified 'regex' is ^abc$.";

            var subject2 = new[] { "abc", "abc-def", "abc" };
            var regex2 = new Regex("^abc$");
            var expected2 = "Provided value (name: 'subject2') contains an element that is not matched by the specified regex.  Element value is 'abc-def'.  Specified 'regex' is ^abc$.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeMatchedByRegex(regex1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeMatchedByRegex(regex2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeMatchedByRegex___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_regex_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeMatchedByRegex(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeMatchedByRegex(regex:) where parameter 'regex' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotBeMatchedByRegex___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Regex regex)
            {
                return (subject, because, applyBecause, data) => subject.NotBeMatchedByRegex(regex, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new Regex("abc")),
                VerificationName = nameof(Verifications.NotBeMatchedByRegex),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "def", null, "def" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { A.Dummy<string>() } },
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
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(new Regex("abc")),
                VerificationName = nameof(Verifications.NotBeMatchedByRegex),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeMatchedByRegexExceptionMessageSuffix,
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "def",
                    "a-b-c",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, "def", "a-b-c" },
                },
                MustFailingValues = new[]
                {
                    "abc",
                    "def-abc-def",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "def", "abc", "def" },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void NotBeMatchedByRegex___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "def-abc-def";
            var regex1 = new Regex("abc");
            var expected1 = "Provided value (name: 'subject1') is matched by the specified regex.  Provided value is 'def-abc-def'.  Specified 'regex' is abc.";

            var subject2 = new[] { "def", "def-abc-def", "def" };
            var regex2 = new Regex("abc");
            var expected2 = "Provided value (name: 'subject2') contains an element that is matched by the specified regex.  Element value is 'def-abc-def'.  Specified 'regex' is abc.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeMatchedByRegex(regex1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeMatchedByRegex(regex2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void StartWith___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().StartWith(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called StartWith(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void StartWith___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue, StringComparison? comparisonType)
            {
                return (subject, because, applyBecause, data) => subject.StartWith(comparisonValue, comparisonType, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", A.Dummy<StringComparison>()),
                VerificationName = nameof(Verifications.StartWith),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", null),
                VerificationName = nameof(Verifications.StartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.StartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
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

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "starter",
                    "starter" + A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "starter", "starter" + A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "Astarter",
                    "Somestarter",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", string.Empty, "starter" + A.Dummy<string>() },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("staRTer", StringComparison.OrdinalIgnoreCase),
                VerificationName = nameof(Verifications.StartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.StartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "starter",
                    "starteR" + A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "stArter", "STarter" + A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "AstaRTer",
                    "SomestaRTer",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "staRTer", string.Empty, "staRTer" + A.Dummy<string>() },
                },
            };

            // Act, Assert
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void StartWith___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "some-string";
            var expected1 = Invariant($"Provided value (name: 'subject1') does not start with the specified comparison value.  Provided value is 'some-string'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is <null>.");

            var subject2 = new[] { "starter", "some-string", "starter" };
            var expected2 = "Provided value (name: 'subject2') contains an element that does not start with the specified comparison value.  Element value is 'some-string'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is 'OrdinalIgnoreCase'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().StartWith("starter"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().StartWith("starter", StringComparison.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotStartWith___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotStartWith(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotStartWith(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotStartWith___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue, StringComparison? comparisonType)
            {
                return (subject, because, applyBecause, data) => subject.NotStartWith(comparisonValue, comparisonType, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", A.Dummy<StringComparison>()),
                VerificationName = nameof(Verifications.NotStartWith),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", null),
                VerificationName = nameof(Verifications.NotStartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotStartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
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

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", null, "something" },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "something",
                    "something-starter",
                    "Starter",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "something", "something-starter", "Starter" },
                },
                MustFailingValues = new string[]
                {
                    "starter",
                    "starter-something",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", "starter", "something" },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("STarter", StringComparison.OrdinalIgnoreCase),
                VerificationName = nameof(Verifications.NotStartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotStartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "something",
                    "something-STarter",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "something", "something-STarter" },
                },
                MustFailingValues = new string[]
                {
                    "STarter",
                    "STarter-something",
                    "starter",
                    "starter-something",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", "starter", "something" },
                },
            };

            // Act, Assert
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void NotStartWith___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "starter-something";
            var expected1 = Invariant($"Provided value (name: 'subject1') starts with the specified comparison value.  Provided value is 'starter-something'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is <null>.");

            var subject2 = new[] { "something", "STARTER-something", "something" };
            var expected2 = "Provided value (name: 'subject2') contains an element that starts with the specified comparison value.  Element value is 'STARTER-something'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is 'OrdinalIgnoreCase'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotStartWith("starter"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotStartWith("starter", StringComparison.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeSameReferenceAs___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeSameReferenceAs(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeSameReferenceAs);

            // subject type is not a value type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>",
            };

            var testValues1a = new TestValues<int>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    1,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new int[] { },
                    new int[] { 1, 2 },
                },
            };

            var testValues1b = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1c = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);

            // comparisonValue is not the same type as the subject
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "VerificationsTest.TestClass",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var testValues2 = new TestValues<TestClass>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass(),
                    null,
                },
                MustEachVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass[] { },
                    new TestClass[] { new TestClass(), null, new TestClass() },
                },
            };

            verificationTest2.Run(testValues2);

            // same or not same reference
            var comparisonValue3 = A.Dummy<EquatableTestClass>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { comparisonValue3, comparisonValue3 },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    null,
                    new EquatableTestClass(),
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { comparisonValue3, null, comparisonValue3 },
                    new EquatableTestClass[] { comparisonValue3, new EquatableTestClass(), comparisonValue3 },
                },
            };

            verificationTest3.Run(testValues3);

            // null comparison value
            EquatableTestClass comparisonValue4 = null;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new EquatableTestClass[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { comparisonValue4, null, comparisonValue4 },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    new EquatableTestClass(),
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { null, new EquatableTestClass(), null },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void BeSameReferenceAs___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            EquatableTestClass subject1 = null;
            EquatableTestClass comparisonValue1 = new EquatableTestClass();
            var expected1 = "Provided value (name: 'subject1') is not the same reference as the comparison value.  Provided value is <null>.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            EquatableTestClass subject2 = new EquatableTestClass();
            EquatableTestClass comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not the same reference as the comparison value.  Provided value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is <null>.";

            EquatableTestClass subject3 = new EquatableTestClass();
            EquatableTestClass comparisonValue3 = new EquatableTestClass();
            var expected3 = "Provided value (name: 'subject3') is not the same reference as the comparison value.  Provided value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            var subject4 = new EquatableTestClass[] { null };
            var comparisonValue4 = new EquatableTestClass();
            var expected4 = "Provided value (name: 'subject4') contains an element that is not the same reference as the comparison value.  Element value is <null>.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            var subject5 = new EquatableTestClass[] { new EquatableTestClass() };
            EquatableTestClass comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not the same reference as the comparison value.  Element value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new EquatableTestClass[] { new EquatableTestClass() };
            var comparisonValue6 = new EquatableTestClass();
            var expected6 = "Provided value (name: 'subject6') contains an element that is not the same reference as the comparison value.  Element value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeSameReferenceAs(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeSameReferenceAs(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeSameReferenceAs(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeSameReferenceAs(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeSameReferenceAs(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeSameReferenceAs(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeSameReferenceAs___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeSameReferenceAs(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeSameReferenceAs);

            // subject type is not a value type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>",
            };

            var testValues1a = new TestValues<int>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    1,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new int[] { },
                    new int[] { 1, 2 },
                },
            };

            var testValues1b = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1c = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);

            // comparisonValue is not the same type as the subject
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "VerificationsTest.TestClass",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var testValues2 = new TestValues<TestClass>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass(),
                    null,
                },
                MustEachVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass[] { },
                    new TestClass[] { new TestClass(), null, new TestClass() },
                },
            };

            verificationTest2.Run(testValues2);

            // same or not same reference
            var comparisonValue3 = A.Dummy<EquatableTestClass>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new[]
                {
                    null,
                    new EquatableTestClass(),
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { new EquatableTestClass(), null, new EquatableTestClass() },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    comparisonValue3,
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { new EquatableTestClass(), comparisonValue3, new EquatableTestClass() },
                },
            };

            verificationTest3.Run(testValues3);

            // null comparison value
            EquatableTestClass comparisonValue4 = null;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new EquatableTestClass[]
                {
                    new EquatableTestClass(),
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { new EquatableTestClass(), new EquatableTestClass() },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { new EquatableTestClass(), null, new EquatableTestClass() },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void NotBeSameReferenceAs___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            EquatableTestClass subject1 = null;
            EquatableTestClass comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is the same reference as the comparison value.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            EquatableTestClass subject2 = new EquatableTestClass();
            EquatableTestClass comparisonValue2 = subject2;
            var expected2 = "Provided value (name: 'subject2') is the same reference as the comparison value.  Provided value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            var subject3 = new EquatableTestClass[] { new EquatableTestClass(), null, new EquatableTestClass() };
            EquatableTestClass comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is the same reference as the comparison value.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject4 = new EquatableTestClass[] { null, new EquatableTestClass(), null };
            EquatableTestClass comparisonValue4 = subject4.Skip(1).First();
            var expected4 = "Provided value (name: 'subject4') contains an element that is the same reference as the comparison value.  Element value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeSameReferenceAs(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeSameReferenceAs(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeSameReferenceAs(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeSameReferenceAs(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ContainString___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().ContainString(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called ContainString(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void ContainString___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.ContainString(comparisonValue, because, applyBecause, data);
            }

            // invalid subject type and null strings
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter"),
                VerificationName = nameof(Verifications.ContainString),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var testValues1a = new TestValues<Guid>
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

            var testValues1b = new TestValues<Guid?>
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

            var testValues1c = new TestValues<object>
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

            var testValues1d = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);
            verificationTest1.Run(testValues1d);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("to-find"),
                VerificationName = nameof(Verifications.ContainString),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainStringExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "to-find",
                    "to-find" + A.Dummy<string>(),
                    A.Dummy<string>() + "to-find",
                    "abcdto-findefgh",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "to-find", "to-find" + A.Dummy<string>(), A.Dummy<string>() + "to-find", "abcdto-findefgh" },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "to find",
                    "tofind",
                    "TO-FIND",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "to-find", string.Empty, "to-find" },
                    new string[] { "to-find", "to find", "to-find" },
                    new string[] { "to-find", "tofind", "to-find" },
                    new string[] { "to-find", "TO-FIND", "to-find" },
                },
            };

            verificationTest2.Run(testValues2);
        }

        [Fact]
        public static void ContainString___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "some-string";
            var expected1 = Invariant($"Provided value (name: 'subject1') does not contain the specified comparison value.  Provided value is 'some-string'.  Specified 'comparisonValue' is 'to-find'.");

            var subject2 = new[] { "to-find", "some-string", "to-find" };
            var expected2 = "Provided value (name: 'subject2') contains an element that does not contain the specified comparison value.  Element value is 'some-string'.  Specified 'comparisonValue' is 'to-find'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainString("to-find"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainString("to-find"));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainString___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotContainString(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotContainString(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotContainString___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotContainString(comparisonValue, because, applyBecause, data);
            }

            // invalid type and null subject
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("to-find"),
                VerificationName = nameof(Verifications.NotContainString),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var testValues1a = new TestValues<Guid>
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

            var testValues1b = new TestValues<Guid?>
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

            var testValues1c = new TestValues<object>
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

            var testValues1d = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "some-string", null, "some-string" },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);
            verificationTest1.Run(testValues1d);

            // passing and failing values
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("to-find"),
                VerificationName = nameof(Verifications.NotContainString),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainStringExceptionMessageSuffix,
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "TO-FIND",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, "TO-FIND" },
                },
                MustFailingValues = new string[]
                {
                    "to-find",
                    A.Dummy<string>() + "to-find",
                    "to-find" + A.Dummy<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "to-find", string.Empty },
                    new string[] { string.Empty, A.Dummy<string>() + "to-find", string.Empty },
                    new string[] { string.Empty, "to-find" + A.Dummy<string>(), string.Empty },
                },
            };

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void NotContainString___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "Ato-findB";
            var expected1 = Invariant($"Provided value (name: 'subject1') contains the specified comparison value.  Provided value is 'Ato-findB'.  Specified 'comparisonValue' is 'to-find'.");

            var subject2 = new[] { "some-string", "Ato-findB", "some-string" };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains the specified comparison value.  Element value is 'Ato-findB'.  Specified 'comparisonValue' is 'to-find'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainString("to-find"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainString("to-find"));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void HaveCount___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedCount_is_negative()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();

            var expectedCount = A.Dummy<NegativeInteger>();

            var actual = Record.Exception(() => new { subject }.Must().HaveCount(expectedCount));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called HaveCount(expectedCount:) where parameter 'expectedCount' is < 0.  Specified value for 'expectedCount' is " + (int)expectedCount + ".  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void HaveCount___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler(int expectedCount)
            {
                return (subject, because, applyBecause, data) => subject.HaveCount(expectedCount, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.HaveCount);

            // invalid types and null subject value
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var testValues1a = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1b = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1c = new TestValues<object>
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

            var testValues1d = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "ab", null, "cd" },
                },
            };

            var testValues1e = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { "a", "b" }, null, new string[] { "c", "d" } },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);
            verificationTest1.Run(testValues1d);
            verificationTest1.Run(testValues1e);

            // passing and failing values with expected count = 0
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(0),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.HaveCountExceptionMessageSuffix,
            };

            var testValues2a = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                },
                MustEachPassingValues = new[]
                {
                    new string[] {  },
                    new string[] { string.Empty, string.Empty },
                },
                MustFailingValues = new[]
                {
                    "a",
                    "ab",
                    "abc",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { string.Empty, "a", string.Empty },
                    new string[] { string.Empty, "ab", string.Empty },
                    new string[] { string.Empty, "abc", string.Empty },
                },
            };

            var testValues2b = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[]
                    {
                        new List<string> { },
                        new List<string> { },
                    },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>() { "a" },
                    new List<string>() { "a", "b" },
                    new List<string>() { "a", "b", "c" },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[]
                    {
                        new List<string> { },
                        new List<string> { "a" },
                        new List<string> { },
                    },
                    new List<string>[]
                    {
                        new List<string> { },
                        new List<string> { "a", "b" },
                        new List<string> { },
                    },
                    new List<string>[]
                    {
                        new List<string> { },
                        new List<string> { "a", "b", "c" },
                        new List<string> { },
                    },
                },
            };

            verificationTest2.Run(testValues2a);
            verificationTest2.Run(testValues2b);

            // passing and failing values with positive expected count
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.HaveCountExceptionMessageSuffix,
            };

            var testValues3a = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "abc",
                },
                MustEachPassingValues = new[]
                {
                    new string[] {  },
                    new string[] { "abc", "def" },
                },
                MustFailingValues = new[]
                {
                    string.Empty,
                    "a",
                    "ab",
                    "abcd",
                    "abcdefghijkoaldjf;lsjdfl;jsd;fljs;dlfjal;sdfj;kls;jdfklsdf",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abc", string.Empty, "abc" },
                    new string[] { "abc", "a", "abc" },
                    new string[] { "abc", "ab", "abc" },
                    new string[] { "abc", "abcd", "abc" },
                    new string[] { "abc", "abcdefghijkoaldjf;lsjdfl;jsd;fljs;dlfjal;sdfj;kls;jdfklsdf", "abc" },
                },
            };

            var testValues3b = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { "a", "b", "c" },
                    new List<string> { "a", null, "c" },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[]
                    {
                        new List<string> { "a", "b", "c" },
                        new List<string> { "a", null, "c" },
                        new List<string> { "a", "b", null },
                    },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>() { },
                    new List<string>() { "a" },
                    new List<string>() { "a", "b" },
                    new List<string>() { "a", "b", "c", "d" },
                    Some.ReadOnlyDummies<string>(10).ToList(),
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[]
                    {
                        new List<string> { "a", "b", "c" },
                        new List<string> {  },
                        new List<string> { "a", "b", null },
                    },
                    new List<string>[]
                    {
                        new List<string> { "a", "b", "c" },
                        new List<string> { "a" },
                        new List<string> { "a", "b", null },
                    },
                    new List<string>[]
                    {
                        new List<string> { "a", "b", "c" },
                        new List<string> { "a", "b" },
                        new List<string> { "a", "b", null },
                    },
                    new List<string>[]
                    {
                        new List<string> { "a", "b", "c" },
                        new List<string> { "a", "b", null, "d" },
                        new List<string> { "a", "b", null },
                    },
                },
            };

            verificationTest3.Run(testValues3a);
            verificationTest3.Run(testValues3b);
        }

        [Fact]
        public static void HaveCount___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>(), A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') is an enumerable whose element count does not equal the expected count.  Enumerable has 2 element(s).  Specified 'expectedCount' is '3'.";

            var subject2 = new[] { new object[] { new object() }, new object[] { }, new object[] { new object() } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an enumerable whose element count does not equal the expected count.  Enumerable has 0 element(s).  Specified 'expectedCount' is '1'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().HaveCount(3));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().HaveCount(1));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotHaveCount___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedCount_is_negative()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();

            var unexpectedCount = A.Dummy<NegativeInteger>();

            var actual = Record.Exception(() => new { subject }.Must().NotHaveCount(unexpectedCount));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotHaveCount(unexpectedCount:) where parameter 'unexpectedCount' is < 0.  Specified value for 'unexpectedCount' is " + (int)unexpectedCount + ".  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotHaveCount___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler(int unexpectedCount)
            {
                return (subject, because, applyBecause, data) => subject.NotHaveCount(unexpectedCount, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotHaveCount);

            // invalid types and null subject value
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var testValues1a = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1b = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1c = new TestValues<object>
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

            var testValues1d = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abc", null, "cde" },
                },
            };

            var testValues1e = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { "a", "b", "c" }, null, new string[] { "c", "d", "e" } },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);
            verificationTest1.Run(testValues1d);
            verificationTest1.Run(testValues1e);

            // passing and failing values with unexpected count = 0
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(0),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotHaveCountExceptionMessageSuffix,
            };

            var testValues2a = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "a",
                    "ab",
                },
                MustEachPassingValues = new[]
                {
                    new string[] {  },
                    new string[] { "a", "ab", "abc" },
                },
                MustFailingValues = new[]
                {
                    string.Empty,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "a", string.Empty, "b" },
                    new string[] { "ab", string.Empty, "cd" },
                },
            };

            var testValues2b = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { null },
                    new List<string> { string.Empty },
                    new List<string> { "a" },
                    new List<string> { "a", "b" },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[]
                    {
                        new List<string> { null },
                        new List<string> { string.Empty },
                        new List<string> { "a" },
                        new List<string> { "a", "b" },
                    },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>() { },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[]
                    {
                        new List<string> { "a", "b" },
                        new List<string> { },
                        new List<string> { "a", "b" },
                    },
                },
            };

            verificationTest2.Run(testValues2a);
            verificationTest2.Run(testValues2b);

            // passing and failing values with positive expected count
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotHaveCountExceptionMessageSuffix,
            };

            var testValues3a = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    string.Empty,
                    "a",
                    "ab",
                    "abcd",
                },
                MustEachPassingValues = new[]
                {
                    new string[] {  },
                    new string[] { string.Empty, "a", "ab", "abcd" },
                },
                MustFailingValues = new[]
                {
                    "abc",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abcd", "abc", "abcd" },
                },
            };

            var testValues3b = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { },
                    new List<string> { "a" },
                    new List<string> { "a", "b" },
                    new List<string> { "a", "b", null, "d" },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[]
                    {
                        new List<string> { },
                        new List<string> { "a" },
                        new List<string> { "a", "b" },
                        new List<string> { "a", "b", null, "d" },
                    },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>() { string.Empty, "b", "c" },
                    new List<string>() { "a", null, "c" },
                    new List<string>() { "a", "b", "c" },
                    new List<string>() { null, null, null },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[]
                    {
                        new List<string> {  },
                        new List<string> { string.Empty, "b", "c" },
                        new List<string> { },
                    },
                    new List<string>[]
                    {
                        new List<string> {  },
                        new List<string>() { "a", null, "c" },
                        new List<string> { },
                    },
                    new List<string>[]
                    {
                        new List<string> {  },
                        new List<string>() { "a", "b", "c" },
                        new List<string> { },
                    },
                    new List<string>[]
                    {
                        new List<string> {  },
                        new List<string>() { null, null, null },
                        new List<string> { },
                    },
                },
            };

            verificationTest3.Run(testValues3a);
            verificationTest3.Run(testValues3b);
        }

        [Fact]
        public static void NotHaveCount___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>(), A.Dummy<object>(), A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') is an enumerable whose element count equals the unexpected count.  Specified 'unexpectedCount' is '3'.";

            var subject2 = new[] { new object[] { new object() }, new object[] { }, new object[] { new object() } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an enumerable whose element count equals the unexpected count.  Specified 'unexpectedCount' is '0'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotHaveCount(3));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotHaveCount(0));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_TExpected___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.BeOfType<string>,
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1);

            // the failing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.BeOfType<ArgumentException>,
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the failing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.BeOfType<ArgumentOutOfRangeException>,
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_TExpected___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not of the expected type.  The type of the provided value is 'ArgumentException'.  Specified 'TExpected' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not of the expected type.  The type of the element is 'ArgumentException'.  Specified 'TExpected' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeOfType<ArgumentOutOfRangeException>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeOfType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_expectedType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeOfType(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeOfType(expectedType:) where parameter 'expectedType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_expectedType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type expectedType)
            {
                return (subject, because, applyBecause, data) => subject.BeOfType(expectedType, because: because, applyBecause: applyBecause, data: data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(string)),
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1);

            // the failing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException)),
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the failing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentOutOfRangeException)),
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_expectedType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not of the expected type.  The type of the provided value is 'ArgumentException'.  Specified 'expectedType' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not of the expected type.  The type of the element is 'ArgumentException'.  Specified 'expectedType' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeOfType(typeof(ArgumentOutOfRangeException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeOfType(typeof(ArgumentOutOfRangeException)));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_TUnexpected___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeOfType<int?>,
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1);

            // the passing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeOfType<ArgumentException>,
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new InvalidOperationException(),
                    new ArgumentOutOfRangeException(string.Empty),
                    new ArgumentNullException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new ArgumentNullException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(), new ArgumentException(string.Empty), new InvalidOperationException() },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentNullException(string.Empty), new ArgumentException(string.Empty), new ArgumentNullException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the passing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeOfType<ArgumentOutOfRangeException>,
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new Exception(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_TUnexpected___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentOutOfRangeException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is of the unexpected type.  Specified 'TUnexpected' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is of the unexpected type.  Specified 'TUnexpected' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeOfType<ArgumentOutOfRangeException>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeOfType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_unexpectedType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_unexpectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeOfType(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeOfType(unexpectedType:) where parameter 'unexpectedType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_unexpectedType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type unexpectedType)
            {
                return (subject, because, applyBecause, data) => subject.NotBeOfType(unexpectedType, because: because, applyBecause: applyBecause, data: data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(int?)),
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1);

            // the passing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException)),
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new InvalidOperationException(),
                    new ArgumentOutOfRangeException(string.Empty),
                    new ArgumentNullException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new ArgumentNullException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(), new ArgumentException(string.Empty), new InvalidOperationException() },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentNullException(string.Empty), new ArgumentException(string.Empty), new ArgumentNullException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the passing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentOutOfRangeException)),
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new Exception(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_unexpectedType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentOutOfRangeException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is of the unexpected type.  Specified 'unexpectedType' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is of the unexpected type.  Specified 'unexpectedType' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeOfType(typeof(ArgumentOutOfRangeException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeOfType(typeof(ArgumentOutOfRangeException)));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_TAssignable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler<T>()
            {
                return (subject, because, applyBecause, data) => subject.BeAssignableToType<T>(because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<IEnumerable>(),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<ArgumentException>(),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new Exception(), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_TAssignable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'TAssignable' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not assignable to the specified type.  The type of the element is 'ArgumentException'.  Specified 'TAssignable' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAssignableToType<ArgumentOutOfRangeException>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeAssignableToType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_assignableType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();

            var actual1 = Record.Exception(() => new { subject }.Must().BeAssignableToType(null, treatUnboundGenericAsAssignableTo: false));
            var actual2 = Record.Exception(() => new { subject }.Must().BeAssignableToType(null, treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be("Called BeAssignableToType(assignableType:) where parameter 'assignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().Be("Called BeAssignableToType(assignableType:) where parameter 'assignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_assignableType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type assignableType, bool treatUnboundGenericAsAssignableTo)
            {
                return (subject, because, applyBecause, data) => subject.BeAssignableToType(assignableType, treatUnboundGenericAsAssignableTo, because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new Exception(), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // do not treat unbound generic as assignable to
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<object>
            {
                MustFailingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new object[] { "test" },
                    new object[] { new List<string>() },
                },
            };

            verificationTest3.Run(testValues3);

            // treat unbound generic as assignable to
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { "test", new List<string>() },
                },
                MustFailingValues = new object[]
                {
                    new Exception(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new object[] { "test", new Exception(string.Empty), "test" },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_assignableType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'assignableType' is 'ArgumentOutOfRangeException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not assignable to the specified type.  The type of the element is 'ArgumentException'.  Specified 'assignableType' is 'ArgumentOutOfRangeException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.";

            var subject3 = new ArgumentException(string.Empty);
            var expected3 = Invariant($"Provided value (name: 'subject3') is not assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'assignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.");

            var subject4 = new object[] { "test", new ArgumentException(string.Empty), "test" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not assignable to the specified type.  The type of the element is 'ArgumentException'.  Specified 'assignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAssignableToType(typeof(ArgumentOutOfRangeException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeAssignableToType(typeof(ArgumentOutOfRangeException)));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_TAssignable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler<T>()
            {
                return (subject, because, applyBecause, data) => subject.NotBeAssignableToType<T>(because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<IEnumerable>(),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<object>
            {
                MustFailingValues = new object[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new object[] { new Exception(), null, new Exception() },
                },
            };

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<ArgumentException>(),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                    "testing",
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { new InvalidOperationException(), "testing", new Exception() },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentNullException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_TAssignable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = "Provided value (name: 'subject1') is assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'TUnassignable' is 'Exception'.";

            var subject2 = new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is assignable to the specified type.  The type of the element is 'ArgumentOutOfRangeException'.  Specified 'TUnassignable' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeAssignableToType<Exception>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeAssignableToType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_assignableType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();

            var actual1 = Record.Exception(() => new { subject }.Must().NotBeAssignableToType(null, treatUnboundGenericAsAssignableTo: false));
            var actual2 = Record.Exception(() => new { subject }.Must().NotBeAssignableToType(null, treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be("Called NotBeAssignableToType(unassignableType:) where parameter 'unassignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().Be("Called NotBeAssignableToType(unassignableType:) where parameter 'unassignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_assignableType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type assignableType, bool treatUnboundGenericAsAssignableTo)
            {
                return (subject, because, applyBecause, data) => subject.NotBeAssignableToType(assignableType, treatUnboundGenericAsAssignableTo, because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<object>
            {
                MustFailingValues = new object[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new object[] { new Exception(), null, new Exception() },
                },
            };

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                    "testing",
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { new InvalidOperationException(), "testing", new Exception() },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentNullException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // do not treat unbound generic as assignable to
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new object[] { "test", new List<string>() },
                },
            };

            verificationTest3.Run(testValues3);

            // treat unbound generic as assignable to
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    new object(),
                    new Exception(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { new object(), new Exception(string.Empty) },
                },
                MustFailingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new object[] { new object(), "test", new object() },
                    new object[] { new object(), new List<string>(), new object() },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_assignableType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentOutOfRangeException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is assignable to the specified type.  The type of the provided value is 'ArgumentOutOfRangeException'.  Specified 'unassignableType' is 'ArgumentException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.");

            var subject2 = new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is assignable to the specified type.  The type of the element is 'ArgumentOutOfRangeException'.  Specified 'unassignableType' is 'ArgumentOutOfRangeException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.";

            var subject3 = "testing";
            var expected3 = Invariant($"Provided value (name: 'subject3') is assignable to the specified type.  The type of the provided value is 'string'.  Specified 'unassignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.");

            var subject4 = new object[] { new ArgumentException(string.Empty), "testing", new ArgumentException(string.Empty) };
            var expected4 = "Provided value (name: 'subject4') contains an element that is assignable to the specified type.  The type of the element is 'string'.  Specified 'unassignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeAssignableToType(typeof(ArgumentException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeAssignableToType(typeof(ArgumentOutOfRangeException)));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeValidEmailAddress___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.BeValidEmailAddress,
                VerificationName = nameof(Verifications.BeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "test@domain.com", null, "test@domain.com" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    "test@domain.com",
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { "test@domain.com" } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    "test@domain.com",
                    A.Dummy<object>(),
                    new List<string>() { "test@domain.com" },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), "test@domain.com", A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.BeValidEmailAddress,
                VerificationName = nameof(Verifications.BeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeValidEmailAddressExceptionMessageSuffix,
            };

            // from: https://gist.github.com/cjaoude/fd9910626629b53c4d25
            // and: https://stackoverflow.com/questions/2049502/what-characters-are-allowed-in-an-email-address
            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    // should be valid but our code doesn't allow:
                    // "email@123.123.123.123",
                    // "email@[123.123.123.123]",
                    // "much.\"more\\ unusual\"@example.com",
                    // "very.unusual.\"@\".unusual.com@example.com",
                    // "very.\"(),:;<>[]\".VERY.\"very@\\\\ \"very\".unusual@strange.example.com",
                    // "admin@mailserver1",
                    "email@example.com",
                    "firstname.lastname@example.com",
                    "email@subdomain.example.com",
                    "firstname+lastname@example.com",
                    "\"email\"@example.com",
                    "1234567890@example.com",
                    "email@example-one.com",
                    "_______@example.com",
                    "email@example.name",
                    "email@example.museum",
                    "email@example.co.jp",
                    "firstname-lastname@example.com",
                    "simple@example.com",
                    "very.common@example.com",
                    "disposable.style.email.with+symbol@example.com",
                    "other.email-with-hyphen@example.com",
                    "fully-qualified-domain@example.com",
                    "user.name+tag+sorting@example.com",
                    "x@example.com",
                    "example-indeed@strange-example.com",
                    "example@s.example",
                    "\" \"@example.org",
                    "\"john..doe\"@example.org",
                    "mailhost!username@example.org",
                    "user%example.com@example.org",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "email@example.com", "firstname.lastname@example.com", "email@subdomain.example.com", "firstname+lastname@example.com", "\"email\"@example.com", "1234567890@example.com", "email@example-one.com", "_______@example.com", "email@example.name", "email@example.museum", "email@example.co.jp", "firstname-lastname@example.com", "simple@example.com", "very.common@example.com", "disposable.style.email.with+symbol@example.com", "other.email-with-hyphen@example.com", "fully-qualified-domain@example.com", "user.name+tag+sorting@example.com", "x@example.com", "example-indeed@strange-example.com", "example@s.example", "\" \"@example.org", "\"john..doe\"@example.org", "mailhost!username@example.org", "user%example.com@example.org" },
                },
                MustFailingValues = new[]
                {
                    // should fail but our code allows:
                    // "email@example.web",
                    // "1234567890123456789012345678901234567890123456789012345678901234+x@example.com",
                    // "i_like_underscore@but_its_not_allow_in_this_part.example.com",
                    string.Empty,
                    " ",
                    "\r\n",
                    "plainaddress",
                    "#@%^%#$@#$@#.com",
                    "@example.com",
                    "Joe Smith <email@example.com>",
                    "email.example.com",
                    "email@example@example.com",
                    ".email@example.com",
                    "email.@example.com",
                    "email..email@example.com",
                    "email@example.com (Joe Smith)",
                    "email@example",
                    "email@-example.com",
                    "email@111.222.333.44444",
                    "email@example..com",
                    "Abc..123@example.com",
                    "\"(),:;<>[\\]@example.com",
                    "just\"not\"right@example.com",
                    "this\\ is\"really\"not\\allowed@example.com",
                    "Abc.example.com",
                    "A@b@c@example.com",
                    "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com",
                    "just\"not\"right@example.com",
                    "this is\"not\\allowed@example.com",
                    "this\\ still\\\"not\\\\allowed@example.com",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "email@example.com", string.Empty, "email@example.com" },
                    new string[] { "email@example.com", " ", "email@example.com" },
                    new string[] { "email@example.com", "\r\n", "email@example.com" },
                    new string[] { "email@example.com", "plainaddress", "email@example.com" },
                    new string[] { "email@example.com", "#@%^%#$@#$@#.com", "email@example.com" },
                    new string[] { "email@example.com", "@example.com", "email@example.com" },
                    new string[] { "email@example.com", "Joe Smith <email@example.com>", "email@example.com" },
                    new string[] { "email@example.com", "email.example.com", "email@example.com" },
                    new string[] { "email@example.com", "email@example@example.com", "email@example.com" },
                    new string[] { "email@example.com", ".email@example.com", "email@example.com" },
                    new string[] { "email@example.com", "email.@example.com", "email@example.com" },
                    new string[] { "email@example.com", "email..email@example.com", "email@example.com" },
                    new string[] { "email@example.com", "email@example.com (Joe Smith)", "email@example.com" },
                    new string[] { "email@example.com", "email@example", "email@example.com" },
                    new string[] { "email@example.com", "email@-example.com", "email@example.com" },
                    new string[] { "email@example.com", "email@111.222.333.44444", "email@example.com" },
                    new string[] { "email@example.com", "email@example..com", "email@example.com" },
                    new string[] { "email@example.com", "Abc..123@example.com", "email@example.com" },
                    new string[] { "email@example.com", "\"(),:;<>[\\]@example.com", "email@example.com" },
                    new string[] { "email@example.com", "just\"not\"right@example.com", "email@example.com" },
                    new string[] { "email@example.com", "this\\ is\"really\"not\\allowed@example.com", "email@example.com" },
                    new string[] { "email@example.com", "Abc.example.com", "email@example.com" },
                    new string[] { "email@example.com", "A@b@c@example.com", "email@example.com" },
                    new string[] { "email@example.com", "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com", "email@example.com" },
                    new string[] { "email@example.com", "just\"not\"right@example.com", "email@example.com" },
                    new string[] { "email@example.com", "this is\"not\\allowed@example.com", "email@example.com" },
                    new string[] { "email@example.com", "this\\ still\\\"not\\\\allowed@example.com", "email@example.com" },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void BeValidEmailAddress___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "abc-def";
            var expected1 = "Provided value (name: 'subject1') is not a valid email address.  Provided value is 'abc-def'.";

            var subject2 = new[] { "test@example.com", "d f", "test@example.com" };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not a valid email address.  Element value is 'd f'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeValidEmailAddress());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeValidEmailAddress());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeValidEmailAddress___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeValidEmailAddress,
                VerificationName = nameof(Verifications.NotBeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
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
                    new string[] { "abc-def", null, "abc-def" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    "abc-def",
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { "abc-def" } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    "abc-def",
                    A.Dummy<object>(),
                    new List<string>() { "abc-def" },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), "abc-def", A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeValidEmailAddress,
                VerificationName = nameof(Verifications.NotBeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeValidEmailAddressExceptionMessageSuffix,
            };

            // from: https://gist.github.com/cjaoude/fd9910626629b53c4d25
            // and: https://stackoverflow.com/questions/2049502/what-characters-are-allowed-in-an-email-address
            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    // should fail but our code allows:
                    // "email@example.web",
                    // "1234567890123456789012345678901234567890123456789012345678901234+x@example.com",
                    // "i_like_underscore@but_its_not_allow_in_this_part.example.com",
                    string.Empty,
                    " ",
                    "\r\n",
                    "plainaddress",
                    "#@%^%#$@#$@#.com",
                    "@example.com",
                    "Joe Smith <email@example.com>",
                    "email.example.com",
                    "email@example@example.com",
                    ".email@example.com",
                    "email.@example.com",
                    "email..email@example.com",
                    "email@example.com (Joe Smith)",
                    "email@example",
                    "email@-example.com",
                    "email@111.222.333.44444",
                    "email@example..com",
                    "Abc..123@example.com",
                    "\"(),:;<>[\\]@example.com",
                    "just\"not\"right@example.com",
                    "this\\ is\"really\"not\\allowed@example.com",
                    "Abc.example.com",
                    "A@b@c@example.com",
                    "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com",
                    "just\"not\"right@example.com",
                    "this is\"not\\allowed@example.com",
                    "this\\ still\\\"not\\\\allowed@example.com",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, " ", "\r\n", "plainaddress", "#@%^%#$@#$@#.com", "@example.com", "Joe Smith <email@example.com>", "email.example.com", "email@example@example.com", ".email@example.com", "email.@example.com", "email..email@example.com", "email@example.com (Joe Smith)", "email@example", "email@-example.com", "email@111.222.333.44444", "email@example..com", "Abc..123@example.com", "\"(),:;<>[\\]@example.com", "just\"not\"right@example.com", "this\\ is\"really\"not\\allowed@example.com", "Abc.example.com", "A@b@c@example.com", "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com", "just\"not\"right@example.com", "this is\"not\\allowed@example.com", "this\\ still\\\"not\\\\allowed@example.com", },
                },
                MustFailingValues = new[]
                {
                    // should be valid but our code doesn't allow:
                    // "email@123.123.123.123",
                    // "email@[123.123.123.123]",
                    // "much.\"more\\ unusual\"@example.com",
                    // "very.unusual.\"@\".unusual.com@example.com",
                    // "very.\"(),:;<>[]\".VERY.\"very@\\\\ \"very\".unusual@strange.example.com",
                    // "admin@mailserver1",
                    "email@example.com",
                    "firstname.lastname@example.com",
                    "email@subdomain.example.com",
                    "firstname+lastname@example.com",
                    "\"email\"@example.com",
                    "1234567890@example.com",
                    "email@example-one.com",
                    "_______@example.com",
                    "email@example.name",
                    "email@example.museum",
                    "email@example.co.jp",
                    "firstname-lastname@example.com",
                    "simple@example.com",
                    "very.common@example.com",
                    "disposable.style.email.with+symbol@example.com",
                    "other.email-with-hyphen@example.com",
                    "fully-qualified-domain@example.com",
                    "user.name+tag+sorting@example.com",
                    "x@example.com",
                    "example-indeed@strange-example.com",
                    "example@s.example",
                    "\" \"@example.org",
                    "\"john..doe\"@example.org",
                    "mailhost!username@example.org",
                    "user%example.com@example.org",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abc-def", "email@example.com", "abc-def" },
                    new string[] { "abc-def", "firstname.lastname@example.com", "abc-def" },
                    new string[] { "abc-def", "email@subdomain.example.com", "abc-def" },
                    new string[] { "abc-def", "firstname+lastname@example.com", "abc-def" },
                    new string[] { "abc-def", "\"email\"@example.com", "abc-def" },
                    new string[] { "abc-def", "1234567890@example.com", "abc-def" },
                    new string[] { "abc-def", "email@example-one.com", "abc-def" },
                    new string[] { "abc-def", "_______@example.com", "abc-def" },
                    new string[] { "abc-def", "email@example.name", "abc-def" },
                    new string[] { "abc-def", "email@example.museum", "abc-def" },
                    new string[] { "abc-def", "email@example.co.jp", "abc-def" },
                    new string[] { "abc-def", "firstname-lastname@example.com", "abc-def" },
                    new string[] { "abc-def", "simple@example.com", "abc-def" },
                    new string[] { "abc-def", "very.common@example.com", "abc-def" },
                    new string[] { "abc-def", "disposable.style.email.with+symbol@example.com", "abc-def" },
                    new string[] { "abc-def", "other.email-with-hyphen@example.com", "abc-def" },
                    new string[] { "abc-def", "fully-qualified-domain@example.com", "abc-def" },
                    new string[] { "abc-def", "user.name+tag+sorting@example.com", "abc-def" },
                    new string[] { "abc-def", "x@example.com", "abc-def" },
                    new string[] { "abc-def", "example-indeed@strange-example.com", "abc-def" },
                    new string[] { "abc-def", "example@s.example", "abc-def" },
                    new string[] { "abc-def", "\" \"@example.org", "abc-def" },
                    new string[] { "abc-def", "\"john..doe\"@example.org", "abc-def" },
                    new string[] { "abc-def", "mailhost!username@example.org", "abc-def" },
                    new string[] { "abc-def", "user%example.com@example.org", "abc-def" },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void NotBeValidEmailAddress___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "test@example.com";
            var expected1 = "Provided value (name: 'subject1') is a valid email address.  Provided value is 'test@example.com'.";

            var subject2 = new[] { "d f", "test@example.com", "d f" };
            var expected2 = "Provided value (name: 'subject2') contains an element that is a valid email address.  Element value is 'test@example.com'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeValidEmailAddress());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeValidEmailAddress());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeUtcDateTime___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.BeUtcDateTime,
                VerificationName = nameof(Verifications.BeUtcDateTime),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "DateTime, DateTime?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<DateTime>, IEnumerable<DateTime?>",
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

            var nullableDateTimeTestValues1 = new TestValues<DateTime?>
            {
                MustFailingValues = new DateTime?[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new DateTime?[] { DateTime.UtcNow, null, DateTime.UtcNow },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(nullableDateTimeTestValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.BeUtcDateTime,
                VerificationName = nameof(Verifications.BeUtcDateTime),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeUtcDateTimeExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "DateTime, DateTime?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<DateTime>, IEnumerable<DateTime?>",
            };

            var dateTimeTestValues2 = new TestValues<DateTime>
            {
                MustPassingValues = new[]
                {
                    DateTime.UtcNow,
                },
                MustEachPassingValues = new[]
                {
                    new DateTime[] { },
                    new DateTime[] { DateTime.UtcNow },
                    new DateTime[] { DateTime.UtcNow, DateTime.UtcNow },
                },
                MustFailingValues = new[]
                {
                    DateTime.Now,
                },
                MustEachFailingValues = new[]
                {
                    new DateTime[] { DateTime.UtcNow, DateTime.Now, DateTime.UtcNow },
                },
            };

            var nullableDateTimeTestValues2 = new TestValues<DateTime?>
            {
                MustPassingValues = new DateTime?[]
                {
                    DateTime.UtcNow,
                },
                MustEachPassingValues = new[]
                {
                    new DateTime?[] { },
                    new DateTime?[] { DateTime.UtcNow },
                    new DateTime?[] { DateTime.UtcNow, DateTime.UtcNow },
                },
                MustFailingValues = new DateTime?[]
                {
                    DateTime.Now,
                },
                MustEachFailingValues = new[]
                {
                    new DateTime?[] { DateTime.UtcNow, DateTime.Now, DateTime.UtcNow },
                },
            };

            verificationTest2.Run(dateTimeTestValues2);
            verificationTest2.Run(nullableDateTimeTestValues2);
        }

        [Fact]
        public static void BeUtcDateTime___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = DateTime.Now;
            var expected1 = "Provided value (name: 'subject1') is of a Kind that is not DateTimeKind.Utc.  Kind is DateTimeKind.Local.";

            var subject2 = new[] { DateTime.UtcNow, DateTime.Now, DateTime.UtcNow };
            var expected2 = "Provided value (name: 'subject2') contains an element that is of a Kind that is not DateTimeKind.Utc.  Kind is DateTimeKind.Local.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeUtcDateTime());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeUtcDateTime());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeUtcDateTimeWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest = new VerificationTest
            {
                VerificationHandler = Verifications.BeUtcDateTimeWhenNotNull,
                VerificationName = nameof(Verifications.BeUtcDateTimeWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeUtcDateTimeWhenNotNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "DateTime?",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<DateTime?>",
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

            var dateTimeTestValues = new TestValues<DateTime>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    DateTime.UtcNow,
                    DateTime.Now,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new DateTime[] { },
                    new DateTime[] { DateTime.UtcNow },
                    new DateTime[] { DateTime.UtcNow, DateTime.UtcNow },
                    new DateTime[] { DateTime.UtcNow, DateTime.Now, DateTime.UtcNow },
                },
            };

            var nullableDateTimeTestValues = new TestValues<DateTime?>
            {
                MustPassingValues = new DateTime?[]
                {
                    DateTime.UtcNow,
                    null,
                },
                MustEachPassingValues = new[]
                {
                    new DateTime?[] { },
                    new DateTime?[] { DateTime.UtcNow },
                    new DateTime?[] { null },
                    new DateTime?[] { DateTime.UtcNow, DateTime.UtcNow },
                    new DateTime?[] { DateTime.UtcNow, null },
                },
                MustFailingValues = new DateTime?[]
                {
                    DateTime.Now,
                },
                MustEachFailingValues = new[]
                {
                    new DateTime?[] { DateTime.UtcNow, DateTime.Now, DateTime.UtcNow },
                },
            };

            verificationTest.Run(guidTestValues);
            verificationTest.Run(nullableGuidTestValues);
            verificationTest.Run(stringTestValues);
            verificationTest.Run(objectTestValues);
            verificationTest.Run(dateTimeTestValues);
            verificationTest.Run(nullableDateTimeTestValues);
        }

        [Fact]
        public static void BeUtcDateTimeWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            DateTime? subject1 = DateTime.Now;
            var expected1 = "Provided value (name: 'subject1') is not null and is of a Kind that is not DateTimeKind.Utc.  Kind is DateTimeKind.Local.";

            var subject2 = new DateTime?[] { DateTime.UtcNow, DateTime.Now, DateTime.UtcNow };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is of a Kind that is not DateTimeKind.Utc.  Kind is DateTimeKind.Local.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeUtcDateTimeWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeUtcDateTimeWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        private static void Run<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues)
        {
            var assertionKinds = EnumExtensions.GetDefinedEnumValues<AssertionKind>();

            var subjectNames = new[] { null, A.Dummy<string>() };

            var userData = new[] { null, A.Dummy<Dictionary<string, string>>() };

            foreach (var assertionKind in assertionKinds)
            {
                foreach (var subjectName in subjectNames)
                {
                    foreach (var data in userData)
                    {
                        // the only way to specify a subject name in an uncategorized assertion is to use
                        // an anonymous object and none of these tests do that...
                        var subjectNameToUse = assertionKind == AssertionKind.Unknown ? null : subjectName;

                        verificationTest.RunPassingScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustFailingScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustEachFailingScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustEachImproperUseOfFrameworkScenarios(assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustInvalidSubjectTypeScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustEachInvalidSubjectTypeScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunInvalidVerificationParameterTypeScenarios(testValues, assertionKind, subjectNameToUse, data);
                    }
                }
            }
        }

        private static void RunPassingScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            var mustAssertionTrackers = testValues.MustPassingValues.Select(_ => _.RunMust(assertionKind, subjectName, each: false));
            var muchEachAssertionTrackers = testValues.MustEachPassingValues.Select(_ => _.RunMust(assertionKind, subjectName, each: true));

            var assertionTrackers = new AssertionTracker[0]
                .Concat(mustAssertionTrackers)
                .Concat(muchEachAssertionTrackers)
                .ToList();

            foreach (var assertionTracker in assertionTrackers)
            {
                // Arrange
                var expected = assertionTracker.CloneWithActionVerifiedAtLeastOnce();

                // Act
                var actual = verificationTest.VerificationHandler(assertionTracker, data: data);

                // Assert
                AssertionTrackerComparer.Equals(actual, expected).Should().BeTrue();
            }
        }

        private static void RunMustFailingScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var failingValue in testValues.MustFailingValues)
            {
                // Arrange
                var assertionTracker = failingValue.RunMust(assertionKind, subjectName, each: false);

                var expectedExceptionMessage = subjectName == null
                    ? "Provided value " + verificationTest.ExceptionMessageSuffix
                    : "Provided value (name: '" + subjectName + "') " + verificationTest.ExceptionMessageSuffix;

                var expectedData = data ?? new Hashtable();

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType(assertionKind.GetExpectedExceptionType(verificationTest.ArgumentExceptionType));
                actual.Message.Should().StartWith(expectedExceptionMessage);
                actual.Data.Keys.Should().BeEquivalentTo(expectedData.Keys);
                foreach (var dataKey in actual.Data.Keys)
                {
                    actual.Data[dataKey].Should().Be(expectedData[dataKey]);
                }
            }
        }

        private static void RunMustEachFailingScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var eachFailingValue in testValues.MustEachFailingValues)
            {
                // Arrange
                var assertionTracker = eachFailingValue.RunMust(assertionKind, subjectName, each: true);

                var expectedExceptionMessage = subjectName == null
                    ? "Provided value contains an element that " + verificationTest.ExceptionMessageSuffix
                    : "Provided value (name: '" + subjectName + "') contains an element that " + verificationTest.ExceptionMessageSuffix;

                var expectedData = data ?? new Hashtable();

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType(assertionKind.GetExpectedExceptionType(verificationTest.EachArgumentExceptionType));
                actual.Message.Should().StartWith(expectedExceptionMessage);
                actual.Data.Keys.Should().BeEquivalentTo(expectedData.Keys);
                foreach (var dataKey in actual.Data.Keys)
                {
                    actual.Data[dataKey].Should().Be(expectedData[dataKey]);
                }
            }
        }

        private static void RunMustEachImproperUseOfFrameworkScenarios(
            this VerificationTest verificationTest,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            // Arrange
            // calling Each() on object that is not IEnumerable
            var notEnumerable = new object();
            var assertionTracker1 = notEnumerable.RunMust(assertionKind, subjectName, each: true);
            var expectedExceptionMessage1 = Invariant($"Called Each() on a value of type object, which is not one of the following expected type(s): IEnumerable.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

            // calling Each() on object that is IEnumerable, but null
            IEnumerable<string> nullEnumerable = null;
            var assertionTracker2 = nullEnumerable.RunMust(assertionKind, subjectName, each: true);
            var expectedExceptionMessage2 = subjectName == null
                ? "Provided value " + Verifications.NotBeNullExceptionMessageSuffix + "."
                : "Provided value (name: '" + subjectName + "') " + Verifications.NotBeNullExceptionMessageSuffix + ".";

            // Act
            var actual1 = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker1, data: data));
            var actual2 = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker2, data: data));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be(expectedExceptionMessage1);

            actual2.Should().BeOfType(assertionKind.GetExpectedExceptionType(typeof(ArgumentNullException)));
            actual2.Message.Should().Be(expectedExceptionMessage2);
        }

        private static void RunMustInvalidSubjectTypeScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var invalidTypeValue in testValues.MustSubjectInvalidTypeValues)
            {
                // Arrange
                var valueTypeName = testValues.MustSubjectInvalidTypeValues.GetType().GetClosedEnumerableElementType().ToStringReadable();
                var assertionTracker = invalidTypeValue.RunMust(assertionKind, subjectName, each: false);
                var expectedMessage = Invariant($"Called {verificationTest.VerificationName}() on a value of type {valueTypeName}, which is not one of the following expected type(s): {verificationTest.SubjectInvalidTypeExpectedTypes}.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private static void RunMustEachInvalidSubjectTypeScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var invalidTypeValue in testValues.MustEachSubjectInvalidTypeValues)
            {
                // Arrange
                var valueTypeName = testValues.MustSubjectInvalidTypeValues.GetType().GetClosedEnumerableElementType().ToStringReadable();
                var assertionTracker = invalidTypeValue.RunMust(assertionKind, subjectName, each: true);
                var expectedMessage = Invariant($"Called {verificationTest.VerificationName}() on a value of type IEnumerable<{valueTypeName}>, which is not one of the following expected type(s): {verificationTest.SubjectInvalidTypeExpectedEnumerableTypes}.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private static void RunInvalidVerificationParameterTypeScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            var mustAssertionTrackers = testValues.MustVerificationParameterInvalidTypeValues.Select(_ => _.RunMust(assertionKind, subjectName, each: false));
            var mustEachAssertionTrackers = testValues.MustEachVerificationParameterInvalidTypeValues.Select(_ => _.RunMust(assertionKind, subjectName, each: true));
            var assertionTrackers = mustAssertionTrackers.Concat(mustEachAssertionTrackers).ToList();

            foreach (var assertionTracker in assertionTrackers)
            {
                // Arrange
                testValues.GetType().GetGenericArguments().First().ToStringReadable();
                var expectedStartOfMessage = Invariant($"Called {verificationTest.VerificationName}({verificationTest.VerificationParameterInvalidTypeName}:) where '{verificationTest.VerificationParameterInvalidTypeName}' is of type");
                var expectedEndOfMessage = Invariant($"which is not one of the following expected type(s): {verificationTest.VerificationParameterInvalidTypeExpectedTypes}.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().StartWith(expectedStartOfMessage);
                actual.Message.Should().EndWith(expectedEndOfMessage);
            }
        }

        private static AssertionTracker RunMust<T>(
            this T value,
            AssertionKind assertionKind,
            string subjectName,
            bool each)
        {
            AssertionTracker result;

            switch (assertionKind)
            {
                case AssertionKind.Unknown:
                    result = value.Must();
                    break;
                case AssertionKind.Argument:
                    result = value.AsArg(subjectName).Must();
                    break;
                case AssertionKind.Operation:
                    result = value.AsOp(subjectName).Must();
                    break;
                case AssertionKind.Test:
                    result = value.AsTest(subjectName).Must();
                    break;
                default:
                    throw new NotSupportedException("this assertion kind is not supported: " + assertionKind);
            }

            if (each)
            {
                result.Each();
            }

            return result;
        }

        private static Type GetExpectedExceptionType(
            this AssertionKind assertionKind,
            Type argumentExceptionType)
        {
            Type result;

            switch (assertionKind)
            {
                case AssertionKind.Unknown:
                    result = typeof(AssertionVerificationFailedException);
                    break;
                case AssertionKind.Argument:
                    result = argumentExceptionType;
                    break;
                case AssertionKind.Operation:
                    result = typeof(InvalidOperationException);
                    break;
                case AssertionKind.Test:
                    result = typeof(TestAssertionVerificationFailedException);
                    break;
                default:
                    throw new NotSupportedException("this assertion kind is not supported: " + assertionKind);
            }

            return result;
        }

        private class VerificationTest
        {
            public VerificationHandler VerificationHandler { get; set; }

            public Type ArgumentExceptionType { get; set; }

            public Type EachArgumentExceptionType { get; set; }

            public string ExceptionMessageSuffix { get; set; }

            public string SubjectInvalidTypeExpectedTypes { get; set; }

            public string SubjectInvalidTypeExpectedEnumerableTypes { get; set; }

            public string VerificationParameterInvalidTypeExpectedTypes { get; set; }

            public string VerificationParameterInvalidTypeName { get; set; }

            public string VerificationName { get; set; }
        }

        private class TestValues<T>
        {
            public IReadOnlyCollection<T> MustSubjectInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachSubjectInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustVerificationParameterInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachVerificationParameterInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustPassingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachPassingValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustFailingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachFailingValues { get; set; } = new List<List<T>>();
        }

        private class TestClass
        {
        }

        private class EquatableTestClass : IEquatable<EquatableTestClass>
        {
            public bool Equals(EquatableTestClass other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (ReferenceEquals(this, null) || ReferenceEquals(other, null))
                {
                    return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }
    }
}