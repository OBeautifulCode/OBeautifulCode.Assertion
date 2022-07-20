// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Count.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static partial class VerificationsTest
    {
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
    }
}