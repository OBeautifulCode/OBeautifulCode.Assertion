// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.DateTime.cs" company="OBeautifulCode">
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
    }
}