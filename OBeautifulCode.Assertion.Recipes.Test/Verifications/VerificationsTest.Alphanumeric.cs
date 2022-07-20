// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Alphanumeric.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
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
    }
}