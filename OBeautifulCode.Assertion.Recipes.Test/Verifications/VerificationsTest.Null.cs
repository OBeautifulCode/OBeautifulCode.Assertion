// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Null.cs" company="OBeautifulCode">
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
    }
}