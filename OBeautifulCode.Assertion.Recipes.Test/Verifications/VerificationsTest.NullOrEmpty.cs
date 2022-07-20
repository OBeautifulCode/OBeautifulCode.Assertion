// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.NullOrEmpty.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void NotBeNullNorEmptyEnumerable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNullNorEmptyEnumerable,
                VerificationName = nameof(Verifications.NotBeNullNorEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "IEnumerable",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IEnumerable>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeNullNorEmptyEnumerable,
                VerificationName = nameof(Verifications.NotBeNullNorEmptyEnumerable),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyEnumerableExceptionMessageSuffix,
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
                    new string[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "   ",
                    "   \r\n ",
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "    ", "    ", "  \r\n ", A.Dummy<string>() },
                },
                MustFailingValues = new[]
                {
                    string.Empty,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
            };

            var enumerableTestValues1 = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { A.Dummy<string>() } },
                },
            };

            var enumerableTestValues2A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, new List<string> { null }, new string[] { string.Empty } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues2B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { string.Empty },
                    new string[] { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new string[] { A.Dummy<string>() }, new List<string> { null }, new string[] { string.Empty } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues2C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string>() { string.Empty },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { A.Dummy<string>() }, new List<string> { null }, new List<string> { string.Empty } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string>(), new List<string>() },
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

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues1);

            verificationTest2.Run(stringTestValues2);
            verificationTest2.Run(enumerableTestValues2A);
            verificationTest2.Run(enumerableTestValues2B);
            verificationTest2.Run(enumerableTestValues2C);
        }

        [Fact]
        public static void NotBeNullNorEmptyEnumerable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new object[] { };
            var expected1 = "Provided value (name: 'subject1') is an empty enumerable.";

            var subject2 = new[] { new[] { A.Dummy<object>() }, new object[] { }, new[] { A.Dummy<object>() } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty enumerable.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeNullNorEmptyEnumerable());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeNullNorEmptyEnumerable());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeNullNorEmptyDictionary___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeNullNorEmptyDictionary;
            var verificationName = nameof(Verifications.NotBeNullNorEmptyDictionary);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
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

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
            };

            verificationTest2.Run(dictionaryTest);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyDictionaryExceptionMessageSuffix,
            };

            var dictionaryTest3A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest3E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest3.Run(dictionaryTest3A);
            verificationTest3.Run(dictionaryTest3B);
            verificationTest3.Run(dictionaryTest3C);
            verificationTest3.Run(dictionaryTest3D);
            verificationTest3.Run(dictionaryTest3E);
        }

        [Fact]
        public static void NotBeNullNorEmptyDictionary___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>();
            var expected1 = "Provided value (name: 'subject1') is an empty dictionary.";

            var subject2 = new IReadOnlyDictionary<string, string>[]
            {
                new Dictionary<string, string>(), new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                new Dictionary<string, string>(),
            };
            var expected2 = "Provided value (name: 'subject2') contains an element that is an empty dictionary.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeNullNorEmptyDictionary());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeNullNorEmptyDictionary());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeNullNorEmptyEnumerableNorContainAnyNulls___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeNullNorEmptyEnumerableNorContainAnyNulls;
            var verificationName = nameof(Verifications.NotBeNullNorEmptyEnumerableNorContainAnyNulls);

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
                ExceptionMessageSuffix = Verifications.NotBeEmptyEnumerableExceptionMessageSuffix,
            };

            var enumerableTestValues4A = new TestValues<IEnumerable>
            {
                MustFailingValues = new IEnumerable[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues4B = new TestValues<IList>
            {
                MustFailingValues = new IList[]
                {
                    new List<string>(),
                    new string[] { },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new List<string>(), new string[] { } },
                },
            };

            var enumerableTestValues4C = new TestValues<List<string>>
            {
                MustFailingValues = new List<string>[]
                {
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string>(), new List<string>() },
                },
            };

            verificationTest4.Run(enumerableTestValues4A);
            verificationTest4.Run(enumerableTestValues4B);
            verificationTest4.Run(enumerableTestValues4C);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyNullElementsExceptionMessageSuffix,
            };

            var enumerableTestValues5A = new TestValues<IEnumerable>
            {
                MustPassingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), },
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable[] { },
                    new IEnumerable[] { new List<string> { string.Empty }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IEnumerable[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable[] { new string[] { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues5B = new TestValues<IList>
            {
                MustPassingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), },
                    new string[] { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new IList[] { },
                    new IList[] { new List<string> { string.Empty }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new IList[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new string[] { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new IList[] { new string[] { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            var enumerableTestValues5C = new TestValues<List<string>>
            {
                MustPassingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), },
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                },
                MustEachPassingValues = new[]
                {
                    new List<string>[] { },
                    new List<string>[] { new List<string> { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustFailingValues = new List<string>[]
                {
                    new List<string> { A.Dummy<string>(), null, A.Dummy<string>() },
                    new List<string> { null, A.Dummy<string>() },
                },
                MustEachFailingValues = new[]
                {
                    new List<string>[] { new List<string> { string.Empty }, new List<string> { A.Dummy<string>(), A.Dummy<string>() }, new List<string> { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            verificationTest5.Run(enumerableTestValues5A);
            verificationTest5.Run(enumerableTestValues5B);
            verificationTest5.Run(enumerableTestValues5C);
        }

        [Fact]
        public static void NotBeNullNorEmptyDictionaryNorContainAnyNullValues___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotBeNullNorEmptyDictionaryNorContainAnyNullValues;
            var verificationName = nameof(Verifications.NotBeNullNorEmptyDictionaryNorContainAnyNullValues);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey, TValue>>, IEnumerable<IReadOnlyDictionary<TKey, TValue>>",
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

            var stringTestValues = new TestValues<string>
            {
                MustSubjectInvalidTypeValues = new string[]
                {
                    A.Dummy<string>(),
                    string.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new string[] { },
                    new string[] { A.Dummy<string>(), string.Empty, null },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    null,
                    new List<string> { A.Dummy<string>() },
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new string[] { A.Dummy<string>() }, null, new string[] { } },
                },
            };

            verificationTest1.Run(stringTestValues);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "IDictionary, IDictionary<TKey,Any Reference Type>, IDictionary<TKey,Nullable<T>>, IReadOnlyDictionary<TKey,Any Reference Type>, IReadOnlyDictionary<TKey,Nullable<T>>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<IDictionary>, IEnumerable<IDictionary<TKey,Any Reference Type>>, IEnumerable<IDictionary<TKey,Nullable<T>>>, IEnumerable<IReadOnlyDictionary<TKey,Any Reference Type>>, IEnumerable<IReadOnlyDictionary<TKey,Nullable<T>>>",
            };

            var dictionaryTest2A = new TestValues<IDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2B = new TestValues<Dictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new Dictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2C = new TestValues<IReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new IReadOnlyDictionary<string, bool>[]
                {
                    new Dictionary<string, bool>(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Dictionary<string, bool>[] { },
                },
            };

            var dictionaryTest2D = new TestValues<ReadOnlyDictionary<string, bool>>
            {
                MustSubjectInvalidTypeValues = new ReadOnlyDictionary<string, bool>[]
                {
                    new ReadOnlyDictionary<string, bool>(new Dictionary<string, bool>()),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new ReadOnlyDictionary<string, bool>[] { },
                },
            };

            verificationTest2.Run(dictionaryTest2A);
            verificationTest2.Run(dictionaryTest2B);
            verificationTest2.Run(dictionaryTest2C);
            verificationTest2.Run(dictionaryTest2D);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryTest3 = new TestValues<IDictionary>
            {
                MustFailingValues = new IDictionary[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
            };

            verificationTest3.Run(dictionaryTest3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEmptyDictionaryExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary[] { },
                    new IDictionary[] { new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } } },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>(),
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string>(),
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }), new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }) },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyKeyValuePairsWithNullValueExceptionMessageSuffix,
            };

            var dictionaryTest5A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), null } },
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                    },
                },
            };

            var dictionaryTest5E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                    },
                },
            };

            verificationTest5.Run(dictionaryTest5A);
            verificationTest5.Run(dictionaryTest5B);
            verificationTest5.Run(dictionaryTest5C);
            verificationTest5.Run(dictionaryTest5D);
            verificationTest5.Run(dictionaryTest5E);
        }
    }
}