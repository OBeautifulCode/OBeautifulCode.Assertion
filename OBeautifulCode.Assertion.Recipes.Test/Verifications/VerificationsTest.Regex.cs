// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Regex.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
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
    }
}