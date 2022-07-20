// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Range.cs" company="OBeautifulCode">
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
        public static void BeInRange___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T minimum, T maximum)
            {
                return (subject, because, applyBecause, data) => subject.BeInRange(minimum, maximum, because: because, applyBecause: applyBecause, data: data);
            }

            var verificationName = nameof(Verifications.BeInRange);

            var ex1 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.IncludesMinimumAndExcludesMaximum));
            var ex2 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndIncludesMaximum));
            var ex3 = Record.Exception(() => A.Dummy<object>().Must().BeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndMaximum));
            ex1.Should().BeOfType<NotImplementedException>();
            ex2.Should().BeOfType<NotImplementedException>();
            ex3.Should().BeOfType<NotImplementedException>();

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>(), A.Dummy<object>()),
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

            decimal? minimum2 = 10m;
            decimal? maximum2 = 20m;
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum2, maximum2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    10m,
                    16m,
                    20m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { 10m, 16m, 20m },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    decimal.MinValue,
                    decimal.MaxValue,
                    9.999999999m,
                    20.000000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { 16m, null, 16m },
                    new decimal?[] { 16m, decimal.MinValue, 16m },
                    new decimal?[] { 16m, decimal.MaxValue, 16m },
                    new decimal?[] { 16m, 9.999999999m, 16m },
                    new decimal?[] { 16m, 20.000000001m, 16m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>(), A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "minimum",
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
                VerificationHandler = GetVerificationHandler(A.Dummy<int>(), A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "minimum",
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

            var minimum5 = 5m;
            var maximum5 = 4.5m;
            var verificationTest5Actual = Record.Exception(() => A.Dummy<decimal>().Must().BeInRange(minimum5, maximum5, because: A.Dummy<string>()));
            verificationTest5Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest5Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '5'.  Specified 'maximum' is '4.5'.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            var minimum6 = 10m;
            var maximum6 = 20m;
            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum6, maximum6),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues6 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    10m,
                    16m,
                    20m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { 10m, 16m, 20m },
                },
                MustFailingValues = new[]
                {
                    decimal.MinValue,
                    decimal.MaxValue,
                    9.999999999m,
                    20.000000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 16m, decimal.MinValue, 16m },
                    new decimal[] { 16m, decimal.MaxValue, 16m },
                    new decimal[] { 16m, 9.999999999m, 16m },
                    new decimal[] { 16m, 20.000000001m, 16m },
                },
            };

            verificationTest6.Run(decimalTestValues6);

            var comparisonValue7 = A.Dummy<decimal>();
            var verificationTest7 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue7, comparisonValue7),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues7 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue7,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue7, comparisonValue7 },
                },
                MustFailingValues = new decimal[]
                {
                    comparisonValue7 + .000000001m,
                    comparisonValue7 - .000000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue7, comparisonValue7 + .000000001m, comparisonValue7 },
                    new decimal[] { comparisonValue7, comparisonValue7 - .000000001m, comparisonValue7 },
                },
            };

            verificationTest7.Run(decimalTestValues7);

            var verificationTest8Actual = Record.Exception(() => A.Dummy<decimal?>().Must().BeInRange(10m, (decimal?)null, because: A.Dummy<string>()));
            verificationTest8Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest8Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '10'.  Specified 'maximum' is <null>.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            decimal? maximum9 = 20m;
            var verificationTest9 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(null, maximum9),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues9 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    decimal.MinValue,
                    20m,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, decimal.MinValue, 20m },
                },
                MustFailingValues = new decimal?[]
                {
                    20.000000001m,
                    decimal.MaxValue,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, 20.000000001m, null },
                    new decimal?[] { null, decimal.MaxValue, null },
                },
            };

            verificationTest9.Run(nullableDecimalTestValues9);

            var verificationTest10 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<decimal?>(null, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues10 = new TestValues<decimal?>
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
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { null, decimal.MinValue, null },
                    new decimal?[] { null, decimal.MaxValue, null },
                },
            };

            verificationTest10.Run(nullableDecimalTestValues10);
        }

        [Fact]
        public static void BeInRange___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? minimum1 = 10;
            int? maximum1 = 20;
            var expected1 = "Provided value (name: 'subject1') is not within the specified range using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            int? subject2 = 5;
            int? minimum2 = null;
            int? maximum2 = null;
            var expected2 = "Provided value (name: 'subject2') is not within the specified range using Comparer<T>.Default, where T: int?.  Provided value is '5'.  Specified 'minimum' is <null>.  Specified 'maximum' is <null>.";

            int subject3 = 5;
            int minimum3 = 10;
            int maximum3 = 20;
            var expected3 = "Provided value (name: 'subject3') is not within the specified range using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            var subject4 = new int?[] { null };
            int? minimum4 = 10;
            int? maximum4 = 20;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not within the specified range using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            var subject5 = new int?[] { 5 };
            int? minimum5 = null;
            int? maximum5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not within the specified range using Comparer<T>.Default, where T: int?.  Element value is '5'.  Specified 'minimum' is <null>.  Specified 'maximum' is <null>.";

            var subject6 = new int[] { 5 };
            int minimum6 = 10;
            int maximum6 = 20;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not within the specified range using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'minimum' is '10'.  Specified 'maximum' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeInRange(minimum1, maximum1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeInRange(minimum2, maximum2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeInRange(minimum3, maximum3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeInRange(minimum4, maximum4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeInRange(minimum5, maximum5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeInRange(minimum6, maximum6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeInRange___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T minimum, T maximum)
            {
                return (subject, because, applyBecause, data) => subject.NotBeInRange(minimum, maximum, because: because, applyBecause: applyBecause, data: data);
            }

            var verificationName = nameof(Verifications.NotBeInRange);

            var ex1 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.IncludesMinimumAndExcludesMaximum));
            var ex2 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndIncludesMaximum));
            var ex3 = Record.Exception(() => A.Dummy<object>().Must().NotBeInRange(A.Dummy<object>(), A.Dummy<object>(), Range.ExcludesMinimumAndMaximum));
            ex1.Should().BeOfType<NotImplementedException>();
            ex2.Should().BeOfType<NotImplementedException>();
            ex3.Should().BeOfType<NotImplementedException>();

            // here the comparisonValue type doesn't match the subject type, but
            // that shouldn't matter because it first fails on TestClass not being comparable
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<object>(), A.Dummy<object>()),
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

            decimal? minimum2 = 10m;
            decimal? maximum2 = 20m;
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum2, maximum2),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues2 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    null,
                    9.9999999999m,
                    20.00000001m,
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { null, decimal.MinValue, 9.9999999999m, 20.00000001m, decimal.MaxValue },
                },
                MustFailingValues = new decimal?[]
                {
                    10m,
                    15m,
                    20m,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { 9.9999999999m, 10m, 20.00000001m },
                    new decimal?[] { 9.9999999999m, 15m, 20.00000001m },
                    new decimal?[] { 9.9999999999m, 20m, 20.00000001m },
                },
            };

            verificationTest2.Run(nullableDecimalTestValues2);

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>(), A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "minimum",
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
                VerificationHandler = GetVerificationHandler(A.Dummy<int>(), A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "minimum",
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

            var minimum5 = 5m;
            var maximum5 = 4.5m;
            var verificationTest5Actual = Record.Exception(() => A.Dummy<decimal>().Must().NotBeInRange(minimum5, maximum5, because: A.Dummy<string>()));
            verificationTest5Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest5Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '5'.  Specified 'maximum' is '4.5'.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            var minimum6 = 10m;
            var maximum6 = 20m;
            var verificationTest6 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(minimum6, maximum6),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues6 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    9.9999999999m,
                    20.00000001m,
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { decimal.MinValue, 9.9999999999m, 20.00000001m, decimal.MaxValue },
                },
                MustFailingValues = new[]
                {
                    10m,
                    15m,
                    20m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { 9.9999999999m, 10m, 20.00000001m },
                    new decimal[] { 9.9999999999m, 15m, 20.00000001m },
                    new decimal[] { 9.9999999999m, 20m, 20.00000001m },
                },
            };

            verificationTest6.Run(decimalTestValues6);

            var comparisonValue7 = A.Dummy<decimal>();
            var verificationTest7 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue7, comparisonValue7),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var decimalTestValues7 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue7 - .000000001m,
                    comparisonValue7 + .000000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue7 - .000000001m, comparisonValue7 + .000000001m },
                },
                MustFailingValues = new decimal[]
                {
                    comparisonValue7,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue7 - .000000001m, comparisonValue7, comparisonValue7 + .000000001m },
                },
            };

            verificationTest7.Run(decimalTestValues7);

            var verificationTest8Actual = Record.Exception(() => A.Dummy<decimal?>().Must().NotBeInRange(10m, (decimal?)null, because: A.Dummy<string>()));
            verificationTest8Actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            verificationTest8Actual.Message.Should().Be("The specified range is invalid because 'maximum' is less than 'minimum'.  Specified 'minimum' is '10'.  Specified 'maximum' is <null>.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            decimal? maximum9 = 20m;
            var verificationTest9 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(null, maximum9),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues9 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    20.00000000001m,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { },
                    new decimal?[] { 20.00000000001m, decimal.MaxValue },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                    20m,
                    decimal.MinValue,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { decimal.MaxValue, null, decimal.MaxValue },
                    new decimal?[] { decimal.MaxValue, 20m, decimal.MaxValue },
                    new decimal?[] { decimal.MaxValue, decimal.MinValue, decimal.MaxValue },
                },
            };

            verificationTest9.Run(nullableDecimalTestValues9);

            var verificationTest10 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<decimal?>(null, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeInRangeExceptionMessageSuffix,
            };

            var nullableDecimalTestValues10 = new TestValues<decimal?>
            {
                MustPassingValues = new decimal?[]
                {
                    decimal.MinValue,
                    decimal.MaxValue,
                },
                MustEachPassingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { decimal.MinValue },
                    new decimal?[] { decimal.MaxValue },
                },
                MustFailingValues = new decimal?[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<decimal?>[]
                {
                    new decimal?[] { decimal.MinValue, null, decimal.MinValue },
                },
            };

            verificationTest10.Run(nullableDecimalTestValues10);
        }

        [Fact]
        public static void NotBeInRange___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? minimum1 = null;
            int? maximum1 = 20;
            var expected1 = "Provided value (name: 'subject1') is within the specified range using Comparer<T>.Default, where T: int?.  Provided value is <null>.  Specified 'minimum' is <null>.  Specified 'maximum' is '20'.";

            int subject2 = 5;
            int minimum2 = 4;
            int maximum2 = 20;
            var expected2 = "Provided value (name: 'subject2') is within the specified range using Comparer<T>.Default, where T: int.  Provided value is '5'.  Specified 'minimum' is '4'.  Specified 'maximum' is '20'.";

            var subject3 = new int?[] { null };
            int? minimum3 = null;
            int? maximum3 = 20;
            var expected3 = "Provided value (name: 'subject3') contains an element that is within the specified range using Comparer<T>.Default, where T: int?.  Element value is <null>.  Specified 'minimum' is <null>.  Specified 'maximum' is '20'.";

            var subject4 = new int[] { 5 };
            int minimum4 = 4;
            int maximum4 = 20;
            var expected4 = "Provided value (name: 'subject4') contains an element that is within the specified range using Comparer<T>.Default, where T: int.  Element value is '5'.  Specified 'minimum' is '4'.  Specified 'maximum' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeInRange(minimum1, maximum1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeInRange(minimum2, maximum2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeInRange(minimum3, maximum3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeInRange(minimum4, maximum4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}