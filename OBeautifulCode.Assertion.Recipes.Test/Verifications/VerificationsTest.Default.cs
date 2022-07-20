// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Default.cs" company="OBeautifulCode">
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
    }
}