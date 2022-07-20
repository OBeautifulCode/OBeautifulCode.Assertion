// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.AsciiPrintable.cs" company="OBeautifulCode">
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
    }
}