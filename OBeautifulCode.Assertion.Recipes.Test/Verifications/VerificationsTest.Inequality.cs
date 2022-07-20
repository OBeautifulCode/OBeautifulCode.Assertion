// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Inequality.cs" company="OBeautifulCode">
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
        public static void BeLessThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeLessThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeLessThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2, comparisonValue2 - .0000001m },
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2 + .00000001m, comparisonValue2 - .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 + .00000001m, comparisonValue5 - .0000001m },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeLessThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 10;
            int comparisonValue3 = 5;
            var expected3 = "Provided value (name: 'subject3') is not less than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '5'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = null;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 5;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not less than the comparison value using Comparer<T>.Default, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '5'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeLessThan(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeLessThan(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeLessThan(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeLessThan(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeLessThan(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeLessThan(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeLessThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeLessThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeLessThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2, comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, null, comparisonValue2 },
                    new decimal?[] { comparisonValue2, comparisonValue2 - .0000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 - .0000001m, comparisonValue5 },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeLessThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int subject3 = 10;
            int comparisonValue3 = 20;
            var expected3 = "Provided value (name: 'subject3') is less than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 20;
            var expected6 = "Provided value (name: 'subject6') contains an element that is less than the comparison value using Comparer<T>.Default, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeLessThan(comparisonValue1));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeLessThan(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeLessThan(comparisonValue4));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeLessThan(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeGreaterThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeGreaterThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeGreaterThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            decimal? comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 + .0000001m, null, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2 - .00000001m, comparisonValue2 + .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 - .00000001m, comparisonValue5 + .0000001m },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>(), null, A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeGreaterThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int? subject2 = null;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int subject3 = 5;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not greater than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { null };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 5 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not greater than the comparison value using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeGreaterThan(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeGreaterThan(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeGreaterThan(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeGreaterThan(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeGreaterThan(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeGreaterThan(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeGreaterThan___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeGreaterThan(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeGreaterThan);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, comparisonValue2 + .0000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 + .0000001m, comparisonValue5 },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, A.Dummy<decimal>(), null },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeGreaterThan___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 10;
            int comparisonValue3 = 5;
            var expected3 = "Provided value (name: 'subject3') is greater than the comparison value using Comparer<T>.Default, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '5'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 5;
            var expected6 = "Provided value (name: 'subject6') contains an element that is greater than the comparison value using Comparer<T>.Default, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '5'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeGreaterThan(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeGreaterThan(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeGreaterThan(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeGreaterThan(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeLessThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeLessThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeLessThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, comparisonValue2 + .00000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 + .00000001m, comparisonValue5 },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, A.Dummy<decimal>(), null },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeLessThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 20;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '20'.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 20 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '20'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeLessThanOrEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeLessThanOrEqualTo(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeLessThanOrEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeLessThanOrEqualTo(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeLessThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeLessThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeLessThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 + .0000001m, null, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2, comparisonValue2 + .0000001m },
                    new decimal?[] { comparisonValue2 + .0000001m, comparisonValue2 - .0000001m, comparisonValue2 + .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5, comparisonValue5 + .0000001m },
                    new decimal[] { comparisonValue5 + .0000001m, comparisonValue5 - .0000001m, comparisonValue5 + .0000001m },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>(), null,  A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeLessThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int? subject2 = null;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int subject3 = 5;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { null };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 5 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is less than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeLessThanOrEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeLessThanOrEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeLessThanOrEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeLessThanOrEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeLessThanOrEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeLessThanOrEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeGreaterThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeGreaterThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeGreaterThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue2, comparisonValue2 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2, null, comparisonValue2 },
                    new decimal?[] { comparisonValue2, comparisonValue2 - .00000001m, comparisonValue2 },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                    comparisonValue5 + Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5, comparisonValue5 + .0000001m, comparisonValue5 + Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5, comparisonValue5 - .00000001m, comparisonValue5 },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void BeGreaterThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int subject3 = 5;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject6 = new int[] { 5 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeGreaterThanOrEqualTo(comparisonValue1));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeGreaterThanOrEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeGreaterThanOrEqualTo(comparisonValue4));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeGreaterThanOrEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeGreaterThanOrEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeGreaterThanOrEqualTo);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues1 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            var comparisonValue2 = A.Dummy<decimal?>();
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue2 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    comparisonValue2 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2, comparisonValue2 - .0000001m },
                    new decimal?[] { comparisonValue2 - .0000001m, comparisonValue2 + .0000001m, comparisonValue2 - .0000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest3.Run(stringTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest4.Run(decimalTestValues4);

            var comparisonValue5 = A.Dummy<decimal>();
            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue5),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var decimalTestValues5 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue5 - .0000001m,
                    comparisonValue5 - Math.Abs(comparisonValue5),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 - Math.Abs(comparisonValue5) },
                },
                MustFailingValues = new[]
                {
                    comparisonValue5,
                    comparisonValue5 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5, comparisonValue5 - .0000001m },
                    new decimal[] { comparisonValue5 - .0000001m, comparisonValue5 + .0000001m, comparisonValue5 - .0000001m },
                },
            };

            verificationTest5.Run(decimalTestValues5);

            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToExceptionMessageSuffix,
            };

            var nullableDecimalTestValues6 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null },
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest6.Run(nullableDecimalTestValues6);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 20;
            int comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Provided value is '20'.  Specified 'comparisonValue' is '10'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = null;
            var expected4 = "Provided value (name: 'subject4') contains an element that is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 20 };
            int comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is greater than or equal to the comparison value using Comparer<T>.Default, where T: int.  Element value is '20'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeGreaterThanOrEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeGreaterThanOrEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeGreaterThanOrEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeGreaterThanOrEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeGreaterThanOrEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeGreaterThanOrEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeLessThanWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeLessThanWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeLessThanWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference nor nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    5m,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { 5 },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue3 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3 - .0000001m, comparisonValue3, comparisonValue3 - .0000001m },
                    new decimal?[] { comparisonValue3 - .0000001m, comparisonValue3 + .00000001m, comparisonValue3 - .0000001m },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal?>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void BeLessThanWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 10;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and is not less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int? subject2 = 10;
            int? comparisonValue2 = 5;
            var expected2 = "Provided value (name: 'subject2') is not null and is not less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is '5'.";

            var subject3 = new int?[] { 10 };
            int? comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject4 = new int?[] { 10 };
            int? comparisonValue4 = 5;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is '5'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeLessThanWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeLessThanWhenNotNull(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeLessThanWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeLessThanWhenNotNull(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeLessThanWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeLessThanWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeLessThanWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being reference type nor nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { comparisonValue3, comparisonValue3 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3, comparisonValue3 - .0000001m, comparisonValue3 },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void NotBeLessThanWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 10;
            int? comparisonValue1 = 20;
            var expected1 = "Provided value (name: 'subject1') is not null and is less than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject2 = new int?[] { 10 };
            int? comparisonValue2 = 20;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is less than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeLessThanWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeLessThanWhenNotNull(comparisonValue2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void BeGreaterThanWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeGreaterThanWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeGreaterThanWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference type nor a nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new decimal[]
                {
                    10m,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { 10m },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            decimal? comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                    new decimal?[] { comparisonValue3 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                    comparisonValue3 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3 + .0000001m, comparisonValue3, comparisonValue3 + .0000001m },
                    new decimal?[] { comparisonValue3 + .0000001m, comparisonValue3 - .00000001m, comparisonValue3 + .0000001m },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { A.Dummy<decimal>() },
                    new decimal?[] { A.Dummy<decimal>(), null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void BeGreaterThanWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 5;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not null and is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject2 = new int?[] { 5 };
            int? comparisonValue2 = 10;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is not greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeGreaterThanWhenNotNull(comparisonValue1));

            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeGreaterThanWhenNotNull(comparisonValue2));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeGreaterThanWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeGreaterThanWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeGreaterThanWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference type nor a nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3,
                    comparisonValue3 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue3, comparisonValue3 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3, comparisonValue3 + .0000001m, comparisonValue3 },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void NotBeGreaterThanWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not null and is greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int? subject3 = 10;
            int? comparisonValue3 = 5;
            var expected3 = "Provided value (name: 'subject3') is not null and is greater than the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is '5'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not null and is greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int?[] { 10 };
            int? comparisonValue6 = 5;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not null and is greater than the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is '5'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeGreaterThanWhenNotNull(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeGreaterThanWhenNotNull(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeGreaterThanWhenNotNull(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeGreaterThanWhenNotNull(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeLessThanOrEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeLessThanOrEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeLessThanOrEqualToWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference type nor a nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>(), },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3,
                    comparisonValue3 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue3, comparisonValue3 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3, comparisonValue3 + .00000001m, comparisonValue3 },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeLessThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void BeLessThanOrEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not null and is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int? subject3 = 20;
            int? comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not null and is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '20'.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not null and is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int?[] { 20 };
            int? comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not null and is not less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '20'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeLessThanOrEqualToWhenNotNull(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeLessThanOrEqualToWhenNotNull(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeLessThanOrEqualToWhenNotNull(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeLessThanOrEqualToWhenNotNull(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeLessThanOrEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeLessThanOrEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeLessThanOrEqualToWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference type nor nullable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue3 + .0000001m },
                    new decimal?[] { comparisonValue3 + .0000001m, null, comparisonValue3 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                    comparisonValue3 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3 + .0000001m, comparisonValue3, comparisonValue3 + .0000001m },
                    new decimal?[] { comparisonValue3 + .0000001m, comparisonValue3 - .0000001m, comparisonValue3 + .0000001m },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeLessThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { A.Dummy<decimal>() },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void NotBeLessThanOrEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject3 = 5;
            int? comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not null and is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject6 = new int?[] { 5 };
            int? comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not null and is less than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeLessThanOrEqualToWhenNotNull(comparisonValue3));

            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeLessThanOrEqualToWhenNotNull(comparisonValue6));

            // Assert
            actual3.Message.Should().Be(expected3);

            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeGreaterThanOrEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeGreaterThanOrEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeGreaterThanOrEqualToWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference type nor a nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>(), },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { comparisonValue3, comparisonValue3 + .0000001m },
                    new decimal?[] { comparisonValue3, null, comparisonValue3 },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3, comparisonValue3 - .00000001m, comparisonValue3 },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeGreaterThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, A.Dummy<decimal>() },
                },
                MustFailingValues = new decimal?[]
                {
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void BeGreaterThanOrEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject3 = 5;
            int? comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not null and is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '5'.  Specified 'comparisonValue' is '10'.";

            var subject6 = new int?[] { 5 };
            int? comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not null and is not greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '5'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeGreaterThanOrEqualToWhenNotNull(comparisonValue3));

            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeGreaterThanOrEqualToWhenNotNull(comparisonValue6));

            // Assert
            actual3.Message.Should().Be(expected3);

            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeGreaterThanOrEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeGreaterThanOrEqualToWhenNotNull);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on decimal not being a reference type nor a nullable type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type, Nullable<T>",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var customClassTestValues1 = new TestValues<decimal>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>(), },
                },
            };

            verificationTest1.Run(customClassTestValues1);

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Type With A Working (Non-Throwing) Default Comparer",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Type With A Working (Non-Throwing) Default Comparer>",
            };

            var customClassTestValues2 = new TestValues<TestClass>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    null,
                    new TestClass(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<TestClass>[]
                {
                    new TestClass[] { },
                    new TestClass[] { null },
                    new TestClass[] { new TestClass() },
                },
            };

            verificationTest2.Run(customClassTestValues2);

            var comparisonValue3 = A.Dummy<decimal?>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues3 = new TestValues<decimal?>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue3 - .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, comparisonValue3 - .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { comparisonValue3 - .0000001m, comparisonValue3, comparisonValue3 - .0000001m },
                    new decimal?[] { comparisonValue3 - .0000001m, comparisonValue3 + .0000001m, comparisonValue3 - .0000001m },
                },
            };

            verificationTest3.Run(nullableDecimalTestValues3);

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var stringTestValues4 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest4.Run(stringTestValues4);

            var verificationTest5 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler((decimal?)null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeGreaterThanOrEqualToWhenNotNullExceptionMessageSuffix,
            };

            var nullableDecimalTestValues5 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null },
                },
                MustFailingValues = new decimal?[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { A.Dummy<decimal>() },
                },
            };

            verificationTest5.Run(nullableDecimalTestValues5);
        }

        [Fact]
        public static void NotBeGreaterThanOrEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not null and is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int? subject3 = 20;
            int? comparisonValue3 = 10;
            var expected3 = "Provided value (name: 'subject3') is not null and is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Provided value is '20'.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not null and is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int?[] { 20 };
            int? comparisonValue6 = 10;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not null and is greater than or equal to the comparison value using Comparer<T>.Default, where T: int?.  Element value is '20'.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeGreaterThanOrEqualToWhenNotNull(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeGreaterThanOrEqualToWhenNotNull(comparisonValue3));

            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeGreaterThanOrEqualToWhenNotNull(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeGreaterThanOrEqualToWhenNotNull(comparisonValue6));

            // Assert
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }
    }
}