// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.ContainKey.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void ContainKey___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().ContainKey<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called ContainKey(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void ContainKey___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainKey(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainKey);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryStringKeyTestValues3 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustFailingValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, decimal>[]
                    {
                        new Dictionary<string, decimal>
                        {
                            { comparisonValue3, 10 },
                        },
                        null,
                        new Dictionary<string, decimal>
                        {
                            { comparisonValue3, 10 },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryStringKeyTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainKeyExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues4 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue4, A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { comparisonValue4, A.Dummy<string>() },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest4.Run(dictionaryDecimalKeyTestValues4);
        }

        [Fact]
        public static void ContainKey___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 3, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 5, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainKey(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainKey(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainKey___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().NotContainKey<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotContainKey(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotContainKey___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainKey(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainKey);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var dictionaryStringKeyTestValues3 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustFailingValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IReadOnlyDictionary<string, decimal>[]
                    {
                        new Dictionary<string, decimal>
                        {
                            { A.Dummy<string>(), 10 },
                        },
                        null,
                        new Dictionary<string, decimal>
                        {
                            { A.Dummy<string>(), 10 },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryStringKeyTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainKeyExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues4 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>(),
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>()
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue4, A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue4, A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest4.Run(dictionaryDecimalKeyTestValues4);
        }

        [Fact]
        public static void NotContainKey___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 4, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 4, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainKey(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainKey(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ContainKeyWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().ContainKeyWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called ContainKeyWhenNotNull(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void ContainKeyWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainKeyWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainKeyWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = 10m;
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainKeyWhenNotNullExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues3 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue3, A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new IReadOnlyDictionary<decimal, string>[]
                    {
                        null,
                    },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { comparisonValue3, A.Dummy<string>() },
                        },
                        null,
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryDecimalKeyTestValues3);
        }

        [Fact]
        public static void ContainKeyWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 3, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') is not null and does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 5, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and does not contain the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainKeyWhenNotNull(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().ContainKeyWhenNotNull(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotContainKeyWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_keyToSearchFor_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<Dictionary<string, string>>();
            var actual = Record.Exception(() => new { subject }.Must().NotContainKeyWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotContainKeyWhenNotNull(keyToSearchFor:) where parameter 'keyToSearchFor' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotContainKeyWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainKeyWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainKeyWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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

            var enumerableTestValues = new TestValues<IEnumerable<string>>
            {
                MustSubjectInvalidTypeValues = new IEnumerable<string>[]
                {
                    null,
                    A.Dummy<List<string>>(),
                    new List<string>() { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { A.Dummy<List<string>>(), A.Dummy<List<string>>() },
                },
            };

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(enumerableTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "keyToSearchFor",
            };

            var dictionaryWithStringKeyTestValues2 = new TestValues<IReadOnlyDictionary<string, decimal>>
            {
                MustVerificationParameterInvalidTypeValues = new IReadOnlyDictionary<string, decimal>[]
                {
                    A.Dummy<Dictionary<string, decimal>>(),
                    A.Dummy<ReadOnlyDictionary<string, decimal>>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IReadOnlyDictionary<string, decimal>>[]
                {
                    new IReadOnlyDictionary<string, decimal>[] { },
                    new IReadOnlyDictionary<string, decimal>[] { A.Dummy<Dictionary<string, decimal>>() },
                },
            };

            verificationTest2.Run(dictionaryWithStringKeyTestValues2);

            var comparisonValue3 = 10m;
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainKeyWhenNotNullExceptionMessageSuffix,
            };

            var dictionaryDecimalKeyTestValues3 = new TestValues<IReadOnlyDictionary<decimal, string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new Dictionary<decimal, string>(),
                    new Dictionary<decimal, string>
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new IReadOnlyDictionary<decimal, string>[] { },
                    new IReadOnlyDictionary<decimal, string>[] { null },
                    new[]
                    {
                        new Dictionary<decimal, string>
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>(),
                        null,
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<decimal, string>[]
                {
                    new Dictionary<decimal, string>()
                    {
                        { A.Dummy<decimal>(), A.Dummy<string>() },
                        { comparisonValue3, A.Dummy<string>() },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<decimal, string>>[]
                {
                    new[]
                    {
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { comparisonValue3, A.Dummy<string>() },
                        },
                        new Dictionary<decimal, string>()
                        {
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                            { A.Dummy<decimal>(), A.Dummy<string>() },
                        },
                    },
                },
            };

            verificationTest3.Run(dictionaryDecimalKeyTestValues3);
        }

        [Fact]
        public static void NotContainKeyWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new Dictionary<int, string> { { 4, "some-string" } };
            int keyToSearchFor1 = 4;
            var expected1 = "Provided value (name: 'subject1') is not null and contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            var subject2 = new IReadOnlyDictionary<int, string>[]
            {
                new Dictionary<int, string> { { 3, "some-string-1" } },
                new Dictionary<int, string> { { 4, "some-string-2" } },
            };

            int keyToSearchFor2 = 4;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and contains the key to search for.  Specified 'keyToSearchFor' is '4'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainKeyWhenNotNull(keyToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotContainKeyWhenNotNull(keyToSearchFor2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}