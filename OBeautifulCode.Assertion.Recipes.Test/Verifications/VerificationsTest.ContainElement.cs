// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.ContainElement.cs" company="OBeautifulCode">
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
        public static void ContainElement___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainElement(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainElement);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            var comparisonValue3 = A.Dummy<string>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            {
                MustFailingValues = new IEnumerable<string>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, comparisonValue3 }, null, new string[] { A.Dummy<string>(), null, comparisonValue3 } },
                },
            };

            verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new[] { comparisonValue4 },
                    new[] { 5m, comparisonValue4, 15m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new[] { comparisonValue4 }, new[] { 5m, comparisonValue4, 15m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new[] { 5m, 9.9999999m, 10.000001m, 15m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { comparisonValue4 }, new[] { 5m, 9.9999999m, 10.000001m, 15m }, new[] { comparisonValue4 } },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
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
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { null, "a", "b" },
                        },
                        new[]
                        {
                            new[] { "b", "a", null },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void ContainElement___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 2, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 2, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainElement(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainElement(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainElement(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainElement(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotContainElement___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainElement(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainElement);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            {
                MustFailingValues = new IEnumerable<string>[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
                },
            };

            verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[] { },
                    new[] { 5m, 9.9999999m, 10.000001m, 15m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new[] { A.Dummy<decimal>() }, new[] { 5m, 9.9999999m, 10.000001m, 15m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4 },
                    new[] { 5m, comparisonValue4, 15m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 5m, comparisonValue4, 15m }, new[] { A.Dummy<decimal>() } },
                    new[] { new[] { A.Dummy<decimal>() }, new[] { 5m, comparisonValue4, 15m } },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void NotContainElement___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, null, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 10, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, null, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 10, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainElement(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotContainElement(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotContainElement(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotContainElement(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ContainElementWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.ContainElementWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.ContainElementWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            // null is allowed for this verification
            ////var comparisonValue3 = A.Dummy<string>();
            ////var verificationTest3 = new VerificationTest
            ////{
            ////    VerificationHandler = GetVerificationHandler(comparisonValue3),
            ////    VerificationName = verificationName,
            ////    ArgumentExceptionType = typeof(ArgumentNullException),
            ////    EachArgumentExceptionType = typeof(ArgumentException),
            ////    ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            ////};

            ////var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            ////{
            ////    MustFailingValues = new IEnumerable<string>[]
            ////    {
            ////        null,
            ////    },
            ////    MustEachFailingValues = new[]
            ////    {
            ////        new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, comparisonValue3 }, null, new string[] { A.Dummy<string>(), null, comparisonValue3 } },
            ////    },
            ////};

            ////verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new[] { comparisonValue4 },
                    new[] { 5m, comparisonValue4, 15m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new[] { comparisonValue4 }, new[] { 5m, comparisonValue4, 15m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new[] { 5m, 9.9999999m, 10.000001m, 15m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { comparisonValue4 }, new[] { 5m, 9.9999999m, 10.000001m, 15m }, new[] { comparisonValue4 } },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.ContainElementWhenNotNullExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
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
                            new[] { "d" },
                            new[] { "b", "a", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { null, "a", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "b", "a", null },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", null, "b" },
                            new[] { "e", "f" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void ContainElementWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, 2, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 2, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, 2, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 2, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and does not contain the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().ContainElementWhenNotNull(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().ContainElementWhenNotNull(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().ContainElementWhenNotNull(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainElementWhenNotNull(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotContainElementWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T item)
            {
                return (subject, because, applyBecause, data) => subject.NotContainElementWhenNotNull(item, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotContainElementWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
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
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "itemToSearchFor",
            };

            var stringTestValues2 = new TestValues<IEnumerable<string>>
            {
                MustVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new List<string> { A.Dummy<string>(), string.Empty, A.Dummy<string>() },
                    new string[] { A.Dummy<string>(), A.Dummy<string>() },
                    new List<string> { A.Dummy<string>(), null },
                    new string[] { A.Dummy<string>(), null },
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<IEnumerable<string>>[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new List<string> { null }, new string[] { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new List<string> { A.Dummy<string>(), null } },
                    new IEnumerable<string>[] { new string[] { null }, new List<string> { A.Dummy<string>(), null }, new string[] { A.Dummy<string>(), A.Dummy<string>() } },
                },
            };

            verificationTest2.Run(stringTestValues2);

            // nulls are allowed
            ////var verificationTest3 = new VerificationTest
            ////{
            ////    VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
            ////    VerificationName = verificationName,
            ////    ArgumentExceptionType = typeof(ArgumentNullException),
            ////    EachArgumentExceptionType = typeof(ArgumentException),
            ////    ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            ////};

            ////var enumerableTestValues3 = new TestValues<IEnumerable<string>>
            ////{
            ////    MustFailingValues = new IEnumerable<string>[]
            ////    {
            ////        null,
            ////    },
            ////    MustEachFailingValues = new[]
            ////    {
            ////        new IEnumerable<string>[] { new string[] { A.Dummy<string>(), null, A.Dummy<string>() }, null, new string[] { A.Dummy<string>(), null, A.Dummy<string>() } },
            ////    },
            ////};

            ////verificationTest3.Run(enumerableTestValues3);

            var comparisonValue4 = 10m;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<IEnumerable<decimal>>
            {
                MustPassingValues = new[]
                {
                    new decimal[] { },
                    new[] { 5m, 9.9999999m, 10.000001m, 15m },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new IEnumerable<decimal>[] { },
                    new[] { new[] { A.Dummy<decimal>() }, new[] { 5m, 9.9999999m, 10.000001m, 15m } },
                },
                MustFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4 },
                    new[] { 5m, comparisonValue4, 15m },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<decimal>>[]
                {
                    new[] { new[] { 5m, comparisonValue4, 15m }, new[] { A.Dummy<decimal>() } },
                    new[] { new[] { A.Dummy<decimal>() }, new[] { 5m, comparisonValue4, 15m } },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            // unordered collection
            IReadOnlyCollection<string> comparisonValue5 = new[] { "a", null, "b" };

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotContainElementWhenNotNullExceptionMessageSuffix,
            };

            var testValues5 = new TestValues<IEnumerable<IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new IReadOnlyCollection<string>[]
                    {
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                    },
                    new IReadOnlyCollection<string>[]
                    {
                        null,
                        new[] { null, "b" },
                        new[] { "a", null },
                        new[] { "a", null, "b", null },
                        new[] { "a", null, "b", "b" },
                    },
                },
                MustEachPassingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new IEnumerable<IReadOnlyCollection<string>>[] { },
                    new IEnumerable<IReadOnlyCollection<string>>[]
                    {
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            null,
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            null,
                            new[] { "d" },
                            new[] { "a", null, "b", null },
                            new[] { "e", "f" },
                        },
                    },
                },
                MustFailingValues = new IEnumerable<IReadOnlyCollection<string>>[]
                {
                    new[] { new[] { "a", null, "b" } },
                    new[] { null, comparisonValue5, null },
                    new[]
                    {
                        new[] { "d" },
                        new[] { "b", "a", null },
                        new[] { "e", "f" },
                    },
                },
                MustEachFailingValues = new IEnumerable<IEnumerable<IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                        new[]
                        {
                            new[] { "d" },
                            new[] { "a", "b", null },
                            new[] { "e", "f" },
                        },
                        new[]
                        {
                            new[] { null, "b" },
                            new[] { "a", null },
                            new[] { "a", null, "b", null },
                            new[] { "a", null, "b", "b" },
                        },
                    },
                },
            };

            verificationTest5.Run(testValues5);
        }

        [Fact]
        public static void NotContainElementWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new int?[] { 1, null, 3 };
            int? itemToSearchFor1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject2 = new int[] { 1, 10, 3 };
            int itemToSearchFor2 = 10;
            var expected2 = "Provided value (name: 'subject2') is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            var subject3 = new int?[][] { new int?[] { 1, null, 3 } };
            int? itemToSearchFor3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'itemToSearchFor' is <null>.";

            var subject4 = new int[][] { new int[] { 1, 10, 3 } };
            int itemToSearchFor4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and contains the item to search for using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'itemToSearchFor' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotContainElementWhenNotNull(itemToSearchFor1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotContainElementWhenNotNull(itemToSearchFor2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotContainElementWhenNotNull(itemToSearchFor3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotContainElementWhenNotNull(itemToSearchFor4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}