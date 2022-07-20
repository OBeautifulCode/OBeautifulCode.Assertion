// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.NullElement.cs" company="OBeautifulCode">
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
        public static void ContainSomeNullElements___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.ContainSomeNullElements;
            var verificationName = nameof(Verifications.ContainSomeNullElements);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
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
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>(), null }, null, new string[] { A.Dummy<string>(), null } },
                },
            };

            verificationTest3.Run(enumerableTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainSomeNullElementsExceptionMessageSuffix,
            };

            var enumerableTestValues4A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable[] { new List<string> { A.Dummy<string>(), null } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IList[] { new List<string> { A.Dummy<string>(), null } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { null }, new List<string> { A.Dummy<string>(), null } },
                    new List<string>[] { new List<string> { A.Dummy<string>(), null } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { null }, new List<string> { A.Dummy<string>(), null }, new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest4.Run(enumerableTestValues4A);
            verificationTest4.Run(enumerableTestValues4B);
            verificationTest4.Run(enumerableTestValues4C);
        }

        [Fact]
        public static void ContainSomeNullElements___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') contains no null elements.";

            var subject2 = new[] { new object[] { }, new object[] { }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains no null elements.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainSomeNullElements());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainSomeNullElements());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyNullElements___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyNullElements;
            var verificationName = nameof(Verifications.NotContainAnyNullElements);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
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
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>(), A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest3.Run(enumerableTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyNullElementsExceptionMessageSuffix,
            };

            var enumerableTestValues4A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                    new IEnumerable[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                    new IList[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues4C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { },
                    new List<string> { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                    new List<string>[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new List<string> { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new List<string> { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            verificationTest4.Run(enumerableTestValues4A);
            verificationTest4.Run(enumerableTestValues4B);
            verificationTest4.Run(enumerableTestValues4C);
        }

        [Fact]
        public static void NotContainAnyNullElements___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>(), null, A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') contains at least one null element.";

            var subject2 = new[] { new object[] { }, new object[] { A.Dummy<object>(), null, A.Dummy<object>() }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains at least one null element.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyNullElements());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyNullElements());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyNullElementsWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyNullElementsWhenNotNull;
            var verificationName = nameof(Verifications.NotContainAnyNullElementsWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
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
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>, IEnumerable when not IEnumerable<Any Value Type>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable<Any Reference Type>>, IEnumerable<IEnumerable<Nullable<T>>>, IEnumerable<IEnumerable when not IEnumerable<Any Value Type>>",
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { null, A.Dummy<string>() },
                    new string[] { string.Empty, null },
                    new string[] { A.Dummy<string>() },
                },
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<bool>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<bool>[]
                {
                    new bool[] { },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable<bool>[] { new bool[] { }, },
                },
            };

            verificationTest2.Run(enumerableTestValues2);
            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyNullElementsWhenNotNullExceptionMessageSuffix,
            };

            var enumerableTestValues3A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    null,
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { null },
                    new IEnumerable[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() }, null },
                    new IEnumerable[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues3B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    null,
                    new List<string> { },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { null },
                    new IList[] { new List<string> { }, new string[] { A.Dummy<string>(), A.Dummy<string>() }, null },
                    new IList[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues3C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    null,
                    new List<string> { },
                    new List<string> { A.Dummy<string>(), A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { null },
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, null },
                    new List<string>[] { new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new List<string> { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new List<string> { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            verificationTest3.Run(enumerableTestValues3A);
            verificationTest3.Run(enumerableTestValues3B);
            verificationTest3.Run(enumerableTestValues3C);
        }

        [Fact]
        public static void NotContainAnyNullElementsWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { A.Dummy<object>(), null, A.Dummy<object>() };
            var expected1 = "Provided value (name: 'subject1') is not null and contains at least one null element.";

            var subject2 = new[] { new object[] { }, new object[] { A.Dummy<object>(), null, A.Dummy<object>() }, new object[] { } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and contains at least one null element.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyNullElementsWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyNullElementsWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}