// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.ContainString.cs" company="OBeautifulCode">
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
    }
}