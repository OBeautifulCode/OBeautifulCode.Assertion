// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Empty.cs" company="OBeautifulCode">
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
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
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
        public static void NotBeEmptyEnumerableWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeEmptyEnumerableWhenNotNull,
                VerificationName = nameof(Verifications.NotBeEmptyEnumerableWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeEmptyEnumerableWhenNotNull,
                VerificationName = nameof(Verifications.NotBeEmptyEnumerableWhenNotNull),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyEnumerableWhenNotNullExceptionMessageSuffix,
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

            var stringTestValues2 = new TestValues<string>
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
                    new string[] { null },
                    new string[] { "    ", "    ", "  \r\n ", A.Dummy<string>(), null },
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

            var enumerableTestValues2A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    null,
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { null },
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, new List<string> { null }, new string[] { string.Empty }, null },
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
                    null,
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { null },
                    new IList[] { new string[] { A.Dummy<string>() }, new List<string> { null }, new string[] { string.Empty }, null },
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
                    null,
                    new List<string>() { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { null },
                    new List<string>[] { new List<string> { A.Dummy<string>() }, new List<string> { null }, new List<string> { string.Empty }, null },
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
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            verificationTest2.Run(stringTestValues2);
            verificationTest2.Run(enumerableTestValues2A);
            verificationTest2.Run(enumerableTestValues2B);
            verificationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void NotBeEmptyEnumerableWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new object[] { };
            var expected1 = "Provided value (name: 'subject1') is not null and is an empty enumerable.";

            var subject2 = new[] { new[] { A.Dummy<object>() }, new object[] { }, new[] { A.Dummy<object>() } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is an empty enumerable.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEmptyEnumerableWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEmptyEnumerableWhenNotNull());

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
        public static void NotBeEmptyDictionaryWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeEmptyDictionaryWhenNotNull;
            var verificationName = nameof(Verifications.NotBeEmptyDictionaryWhenNotNull);

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

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyDictionaryWhenNotNullExceptionMessageSuffix,
            };

            var dictionaryTest3A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    null,
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { null },
                    new IDictionary[] { new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), null },
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
                    null,
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { null },
                    new IDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), null },
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
                    null,
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { null },
                    new Dictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null },
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
                    null,
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { null },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), null },
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
                    null,
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { null },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), null },
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
        public static void NotBeEmptyDictionaryWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>();
            var expected1 = "Provided value (name: 'subject1') is not null and is an empty dictionary.";

            var subject2 = new IReadOnlyDictionary<string, string>[]
            {
                new Dictionary<string, string>(), new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                new Dictionary<string, string>(),
            };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is an empty dictionary.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEmptyDictionaryWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEmptyDictionaryWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}