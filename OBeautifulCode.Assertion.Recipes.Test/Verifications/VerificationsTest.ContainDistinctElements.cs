// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.ContainDistinctElements.cs" company="OBeautifulCode">
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
        public static void ContainOnlyDistinctElements___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            var verificationName = nameof(Verifications.ContainOnlyDistinctElements);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustFailingValues = new IEnumerable<string>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable<string>[] { new string[] { A.Dummy<string>() }, null, new string[] { A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(enumerableTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[0],
                    new[] { 5m },
                    new[] { 5m, 15m },
                    new[] { 5m, 15m, 20m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new decimal[] { }, new[] { 5m }, new[] { 5m, 15m }, new[] { 5m, 15m, 20m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 1m, 1m },
                    new decimal[] { 1m, 2m, 1m },
                    new decimal[] { 1m, 1m, 2m },
                    new decimal[] { 1m, 2m, 2m },
                    new decimal[] { 1m, 2m, 3m, 1m },
                    new decimal[] { 1m, 2m, 2m, 3m },
                    new decimal[] { 1m, 1m, 2m, 3m },
                    new decimal[] { 1m, 2m, 3m, 3m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 1m }, new[] { 1m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 3m }, new[] { 1m } },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // unordered collection
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElements,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                    },
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                    },
                    new[]
                    {
                        null,
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new[]
                    {
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                            new[] { "a", "c" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "a", "b" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "a" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "c" },
                        new[] { "c", "b" },
                        new[] { "c", "d" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                        null,
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "a", "b" },
                            new[] { "b", "c" },
                            new[] { "c", "b" },
                            new[] { "c", "d" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void ContainOnlyDistinctElements___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3, 1 };
            var expected1 = "Provided value (name: 'subject1') contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject2 = new int?[] { 1, 2, null, null, 3 };
            var expected2 = "Provided value (name: 'subject2') contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3, 1 } };
            var expected3 = "Provided value (name: 'subject3') contains an element that contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject4 = new int?[][] { new int?[] { 1, null, null, 2, 3 } };
            var expected4 = "Provided value (name: 'subject4') contains an element that contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainOnlyDistinctElements());
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainOnlyDistinctElements());

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainOnlyDistinctElements());
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainOnlyDistinctElements());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ContainOnlyDistinctElementsWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            var verificationName = nameof(Verifications.ContainOnlyDistinctElementsWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
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

            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);

            // nulls allowed
            ////var verificationTest2 = new VerificationTest
            ////{
            ////    VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
            ////    VerificationName = verificationName,
            ////    ArgumentExceptionType = typeof(ArgumentNullException),
            ////    EachArgumentExceptionType = typeof(ArgumentException),
            ////    ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            ////};

            ////var enumerableTestValues2 = new TestValues<IEnumerable<string>>
            ////{
            ////    MustFailingValues = new IEnumerable<string>[]
            ////    {
            ////        null,
            ////    },
            ////    MustEachFailingValues = new[]
            ////    {
            ////        new IEnumerable<string>[] { new string[] { A.Dummy<string>() }, null, new string[] { A.Dummy<string>() } },
            ////    },
            ////};

            ////verificationTest2.Run(enumerableTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[0],
                    new[] { 5m },
                    new[] { 5m, 15m },
                    new[] { 5m, 15m, 20m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new decimal[] { }, new[] { 5m }, new[] { 5m, 15m }, new[] { 5m, 15m, 20m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 1m, 1m },
                    new decimal[] { 1m, 2m, 1m },
                    new decimal[] { 1m, 1m, 2m },
                    new decimal[] { 1m, 2m, 2m },
                    new decimal[] { 1m, 2m, 3m, 1m },
                    new decimal[] { 1m, 2m, 2m, 3m },
                    new decimal[] { 1m, 1m, 2m, 3m },
                    new decimal[] { 1m, 2m, 3m, 3m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 1m }, new[] { 1m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 1m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 1m, 2m, 3m }, new[] { 1m } },
                    new[] { new[] { 1m }, new[] { 1m, 2m, 3m, 3m }, new[] { 1m } },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // unordered collection
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = Verifications.ContainOnlyDistinctElementsWhenNotNull,
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainOnlyDistinctElementsWhenNotNullExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                    },
                    new[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                    },
                    new[]
                    {
                        null,
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        null,
                    },
                    new[]
                    {
                        null,
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "a", "b" },
                            new[] { "a", "c" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "a", "b" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "a" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { "a", "b" },
                        new[] { "b", "c" },
                        new[] { "c", "b" },
                        new[] { "c", "d" },
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                        new[] { "a", "b" },
                        new[] { "a", "c" },
                        null,
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "a", "b" },
                            new[] { "b", "c" },
                            new[] { "c", "b" },
                            new[] { "c", "d" },
                        },
                        new IReadOnlyCollection<string>[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void ContainOnlyDistinctElementsWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3, 1 };
            var expected1 = "Provided value (name: 'subject1') is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject2 = new int?[] { 1, 2, null, null, 3 };
            var expected2 = "Provided value (name: 'subject2') is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3, 1 } };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            var subject4 = new int?[][] { new int?[] { 1, null, null, 2, 3 } };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and contains two or more elements that are equal using EqualityExtensions.IsEqualTo<T>, where T: int?.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainOnlyDistinctElementsWhenNotNull());
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainOnlyDistinctElementsWhenNotNull());

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainOnlyDistinctElementsWhenNotNull());
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainOnlyDistinctElementsWhenNotNull());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}