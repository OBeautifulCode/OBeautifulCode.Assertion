// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.NullOrWhiteSpace.cs" company="OBeautifulCode">
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
    using static System.FormattableString;

    public static partial class VerificationsTest
    {
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
    }
}