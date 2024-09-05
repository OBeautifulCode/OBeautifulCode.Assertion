// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.KeyValuePairWithNullValue.cs" company="OBeautifulCode">
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
        public static void ContainSomeKeyValuePairsWithNullValue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.ContainSomeKeyValuePairsWithNullValue;
            var verificationName = nameof(Verifications.ContainSomeKeyValuePairsWithNullValue);

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
                    new IDictionary[] { new Dictionary<string, string>() { { A.Dummy<string>(), null } }, null, new Dictionary<string, string>() { { A.Dummy<string>(), null } } },
                },
            };

            verificationTest3.Run(dictionaryTest3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainSomeKeyValuePairsWithNullValueExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary() { { A.Dummy<string>(), null } },
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary[]
                    {
                        new ListDictionary() { { A.Dummy<string>(), null } },
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() } },
                        new ListDictionary() { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>() { { A.Dummy<string>(), null } },
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                        new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } },
                        new Dictionary<string, string> { { A.Dummy<string>(), null } },
                    },
                },
            };

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), null } }),
                    },
                },
                MustFailingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachFailingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), A.Dummy<string>() } }),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { A.Dummy<string>(), null } }),
                    },
                },
            };

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);
        }

        [Fact]
        public static void ContainSomeKeyValuePairsWithNullValue___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>();
            var expected1 = "Provided value (name: 'subject1') contains no key-value pairs with a null value.";

            var subject2 = new[] { new Dictionary<string, string>() };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains no key-value pairs with a null value.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainSomeKeyValuePairsWithNullValue());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainSomeKeyValuePairsWithNullValue());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyKeyValuePairsWithNullValue___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyKeyValuePairsWithNullValue;
            var verificationName = nameof(Verifications.NotContainAnyKeyValuePairsWithNullValue);

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
                ExceptionMessageSuffix = Verifications.NotContainAnyKeyValuePairsWithNullValueExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    new ListDictionary(),
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[]
                    {
                        new ListDictionary(),
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

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
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

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
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

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
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

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
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

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);
        }

        [Fact]
        public static void NotContainAnyKeyValuePairsWithNullValue___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>() { { "abc", A.Dummy<string>() }, { "def", null } };
            var expected1 = "Provided value (name: 'subject1') contains at least one key-value pair with a null value.  For example, see this key: 'def'.";

            var subject2 = new[] { new Dictionary<string, string>() { { "abc", A.Dummy<string>() }, { "def", null } } };
            var expected2 = "Provided value (name: 'subject2') contains an element that contains at least one key-value pair with a null value.  For example, see this key: 'def'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyKeyValuePairsWithNullValue());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyKeyValuePairsWithNullValue());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainAnyKeyValuePairsWithNullValueWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler verificationHandler = Verifications.NotContainAnyKeyValuePairsWithNullValueWhenNotNull;
            var verificationName = nameof(Verifications.NotContainAnyKeyValuePairsWithNullValueWhenNotNull);

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

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = verificationHandler,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainAnyKeyValuePairsWithNullValueWhenNotNullExceptionMessageSuffix,
            };

            var dictionaryTest4A = new TestValues<IDictionary>
            {
                MustPassingValues = new IDictionary[]
                {
                    null,
                    new ListDictionary(),
                    new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new ListDictionary[] { },
                    new ListDictionary[] { null, null, },
                    new ListDictionary[]
                    {
                        new ListDictionary(),
                        new ListDictionary() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                        null,
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

            var dictionaryTest4B = new TestValues<IDictionary<string, string>>
            {
                MustPassingValues = new IDictionary<string, string>[]
                {
                    null,
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IDictionary<string, string>[] { },
                    new IDictionary<string, string>[] { null, null, },
                    new IDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                        null,
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

            var dictionaryTest4C = new TestValues<Dictionary<string, string>>
            {
                MustPassingValues = new Dictionary<string, string>[]
                {
                    null,
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new Dictionary<string, string>[] { },
                    new Dictionary<string, string>[] { null, null },
                    new Dictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                        null,
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

            var dictionaryTest4D = new TestValues<IReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new IReadOnlyDictionary<string, string>[]
                {
                    null,
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                },
                MustEachPassingValues = new[]
                {
                    new IReadOnlyDictionary<string, string>[] { },
                    new IReadOnlyDictionary<string, string>[] { null, null },
                    new IReadOnlyDictionary<string, string>[]
                    {
                        new Dictionary<string, string>(),
                        new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } },
                        null,
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

            var dictionaryTest4E = new TestValues<ReadOnlyDictionary<string, string>>
            {
                MustPassingValues = new ReadOnlyDictionary<string, string>[]
                {
                    null,
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                    new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                },
                MustEachPassingValues = new[]
                {
                    new ReadOnlyDictionary<string, string>[] { },
                    new ReadOnlyDictionary<string, string>[] { null, null },
                    new ReadOnlyDictionary<string, string>[]
                    {
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()),
                        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() { { A.Dummy<string>(), A.Dummy<string>() }, { A.Dummy<string>(), A.Dummy<string>() } }),
                        null,
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

            verificationTest4.Run(dictionaryTest4A);
            verificationTest4.Run(dictionaryTest4B);
            verificationTest4.Run(dictionaryTest4C);
            verificationTest4.Run(dictionaryTest4D);
            verificationTest4.Run(dictionaryTest4E);
        }

        [Fact]
        public static void NotContainAnyKeyValuePairsWithNullValueWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<string, string>() { { "abc", A.Dummy<string>() }, { "def", null } };
            var expected1 = "Provided value (name: 'subject1') is not null and contains at least one key-value pair with a null value.  For example, see this key: 'def'.";

            var subject2 = new[] { new Dictionary<string, string>() { { "abc", A.Dummy<string>() }, { "def", null } } };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and contains at least one key-value pair with a null value.  For example, see this key: 'def'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainAnyKeyValuePairsWithNullValueWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainAnyKeyValuePairsWithNullValueWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}