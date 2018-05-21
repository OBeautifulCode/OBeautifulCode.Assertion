// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class ParameterValidationsTest
    {
        private static readonly ParameterEqualityComparer ParameterComparer = new ParameterEqualityComparer();

        private delegate Parameter Validation(Parameter parameter, string because = null);

        [Fact]
        public static void BeNull()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeNull,
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = "is not null",
                GuidTestValues = new TestValues<Guid>
                {
                    PassingValues = new Guid[0],
                    FailingValues = new Guid[] { A.Dummy<Guid>(), Guid.Empty },
                    EachPassingValues = new IEnumerable<Guid>[0],
                    EachFailingValues = new IEnumerable<Guid>[] { Some.ReadOnlyDummies<Guid>() },
                },
                NullableGuidTestValues = new TestValues<Guid?>
                {
                    PassingValues = new Guid?[] { null },
                    FailingValues = new Guid?[] { A.Dummy<Guid>(), Guid.Empty },
                    EachPassingValues = new IEnumerable<Guid?>[] { new Guid?[] { null, null } },
                    EachFailingValues = new IEnumerable<Guid?>[] { new Guid?[] { null, Guid.NewGuid(), null } },
                },
                DateTimeTestValues = new TestValues<DateTime>
                {
                    PassingValues = new DateTime[0],
                    FailingValues = new DateTime[] { A.Dummy<DateTime>() },
                    EachPassingValues = new IEnumerable<DateTime>[0],
                    EachFailingValues = new IEnumerable<DateTime>[] { Some.ReadOnlyDummies<DateTime>() },
                },
                NullableDateTimeTestValues = new TestValues<DateTime?>
                {
                    PassingValues = new DateTime?[] { null },
                    FailingValues = new DateTime?[] { A.Dummy<DateTime>() },
                    EachPassingValues = new IEnumerable<DateTime?>[] { new DateTime?[] { null, null } },
                    EachFailingValues = new IEnumerable<DateTime?>[] { new DateTime?[] { null, A.Dummy<DateTime>(), null } },
                },
                DecimalTestValues = new TestValues<decimal>
                {
                    PassingValues = new decimal[0],
                    FailingValues = new decimal[] { A.Dummy<decimal>() },
                    EachPassingValues = new IEnumerable<decimal>[0],
                    EachFailingValues = new IEnumerable<decimal>[] { Some.ReadOnlyDummies<decimal>() },
                },
                NullableDecimalTestValues = new TestValues<decimal?>
                {
                    PassingValues = new decimal?[] { null },
                    FailingValues = new decimal?[] { A.Dummy<decimal>() },
                    EachPassingValues = new IEnumerable<decimal?>[] { new decimal?[] { null, null } },
                    EachFailingValues = new IEnumerable<decimal?>[] { new decimal?[] { null, A.Dummy<decimal>(), null } },
                },
                StringTestValues = new TestValues<string>
                {
                    PassingValues = new string[] { null },
                    FailingValues = new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    EachPassingValues = new IEnumerable<string>[] { new string[] { null, null } },
                    EachFailingValues = new IEnumerable<string>[] { new string[] { null, string.Empty, " \r\n  ", A.Dummy<string>(), null } },
                },
                ObjectTestValues = new TestValues<object>
                {
                    PassingValues = new object[] { null },
                    FailingValues = new object[] { A.Dummy<object>() },
                    EachPassingValues = new IEnumerable<object>[] { new object[] { null, null } },
                    EachFailingValues = new IEnumerable<object>[] { new object[] { null, A.Dummy<object>(), null } },
                },
            };

            // Act, Assert
            RunValidationTest(validationTest);
        }

        [Fact]
        public static void NotBeNull()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.NotBeNull,
                ExceptionType = typeof(ArgumentNullException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = "is null",
                GuidTestValues = new TestValues<Guid>
                {
                    PassingValues = new Guid[] { A.Dummy<Guid>(), Guid.Empty },
                    FailingValues = new Guid[0],
                    EachPassingValues = new IEnumerable<Guid>[] { Some.ReadOnlyDummies<Guid>() },
                    EachFailingValues = new IEnumerable<Guid>[0],
                },
                NullableGuidTestValues = new TestValues<Guid?>
                {
                    PassingValues = new Guid?[] { A.Dummy<Guid>(), Guid.Empty },
                    FailingValues = new Guid?[] { null },
                    EachPassingValues = new IEnumerable<Guid?>[] { new Guid?[] { Guid.NewGuid(), Guid.NewGuid() } },
                    EachFailingValues = new IEnumerable<Guid?>[] { new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() } },
                },
                DateTimeTestValues = new TestValues<DateTime>
                {
                    PassingValues = new DateTime[] { A.Dummy<DateTime>() },
                    FailingValues = new DateTime[0],
                    EachPassingValues = new IEnumerable<DateTime>[] { Some.ReadOnlyDummies<DateTime>() },
                    EachFailingValues = new IEnumerable<DateTime>[0],
                },
                NullableDateTimeTestValues = new TestValues<DateTime?>
                {
                    PassingValues = new DateTime?[] { A.Dummy<DateTime>() },
                    FailingValues = new DateTime?[] { null },
                    EachPassingValues = new IEnumerable<DateTime?>[] { new DateTime?[] { A.Dummy<DateTime>(), A.Dummy<DateTime>() } },
                    EachFailingValues = new IEnumerable<DateTime?>[] { new DateTime?[] { A.Dummy<DateTime>(), null, A.Dummy<DateTime>() } },
                },
                DecimalTestValues = new TestValues<decimal>
                {
                    PassingValues = new decimal[] { A.Dummy<decimal>() },
                    FailingValues = new decimal[0],
                    EachPassingValues = new IEnumerable<decimal>[] { Some.ReadOnlyDummies<decimal>() },
                    EachFailingValues = new IEnumerable<decimal>[0],
                },
                NullableDecimalTestValues = new TestValues<decimal?>
                {
                    PassingValues = new decimal?[] { A.Dummy<decimal>() },
                    FailingValues = new decimal?[] { null },
                    EachPassingValues = new IEnumerable<decimal?>[] { new decimal?[] { A.Dummy<decimal>(), A.Dummy<decimal>() } },
                    EachFailingValues = new IEnumerable<decimal?>[] { new decimal?[] { A.Dummy<decimal>(), null, A.Dummy<decimal>() } },
                },
                StringTestValues = new TestValues<string>
                {
                    PassingValues = new string[] { string.Empty, " \r\n  ", A.Dummy<string>() },
                    FailingValues = new string[] { null },
                    EachPassingValues = new IEnumerable<string>[] { new string[] { string.Empty, " \r\n  ", A.Dummy<string>() } },
                    EachFailingValues = new IEnumerable<string>[] { new string[] { string.Empty, null, " \r\n  " } },
                },
                ObjectTestValues = new TestValues<object>
                {
                    PassingValues = new object[] { A.Dummy<object>() },
                    FailingValues = new object[] { null },
                    EachPassingValues = new IEnumerable<object>[] { new object[] { A.Dummy<object>(), A.Dummy<object>() } },
                    EachFailingValues = new IEnumerable<object>[] { new object[] { A.Dummy<object>(), null, A.Dummy<object>() } },
                },
            };

            // Act, Assert
            RunValidationTest(validationTest);
        }

        private static void RunValidationTest(
            ValidationTest validationTest)
        {
            RunValidationTest(validationTest, validationTest.GuidTestValues);
            RunValidationTest(validationTest, validationTest.NullableGuidTestValues);
            RunValidationTest(validationTest, validationTest.DateTimeTestValues);
            RunValidationTest(validationTest, validationTest.NullableDateTimeTestValues);
            RunValidationTest(validationTest, validationTest.DecimalTestValues);
            RunValidationTest(validationTest, validationTest.NullableDecimalTestValues);
            RunValidationTest(validationTest, validationTest.StringTestValues);
            RunValidationTest(validationTest, validationTest.ObjectTestValues);
        }

        private static void RunValidationTest<T>(
            ValidationTest validationTest,
            TestValues<T> testValues)
        {
            if (testValues == null)
            {
                return;
            }

            var names = new[] { null, "paramName" };
            var becauses = new[] { null, "because" };

            // passing cases
            foreach (var name in names)
            {
                foreach (var because in becauses)
                {
                    var passingParameters = testValues.PassingValues.Select(_ => _.Named(name).Must()).Concat(testValues.EachPassingValues.Select(_ => _.Named(name).Must().Each())).ToList();
                    foreach (var parameter in passingParameters)
                    {
                        // Arrange
                        var expected = parameter.CloneWithHasBeenValidated();

                        // Act
                        var actual = validationTest.Validation(parameter, because);

                        // Assert
                        ParameterComparer.Equals(actual, expected).Should().BeTrue();
                    }

                    foreach (var failingValue in testValues.FailingValues)
                    {
                        // Arrange
                        var parameter = failingValue.Named(name).Must();
                        var expectedExceptionMessage = because;
                        if (expectedExceptionMessage == null)
                        {
                            if (name == null)
                            {
                                expectedExceptionMessage = "parameter " + validationTest.ExceptionMessageSuffix;
                            }
                            else
                            {
                                expectedExceptionMessage = "parameter '" + name + "' " + validationTest.ExceptionMessageSuffix;
                            }
                        }

                        // Act
                        var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                        // Assert
                        actual.Should().BeOfType(validationTest.ExceptionType);
                        actual.Message.Should().Be(expectedExceptionMessage);
                    }

                    foreach (var eachFailingValue in testValues.EachFailingValues)
                    {
                        // Arrange
                        var parameter = eachFailingValue.Named(name).Must().Each();
                        var expectedExceptionMessage = because;
                        if (expectedExceptionMessage == null)
                        {
                            if (name == null)
                            {
                                expectedExceptionMessage = "parameter contains an element that " + validationTest.ExceptionMessageSuffix;
                            }
                            else
                            {
                                expectedExceptionMessage = "parameter '" + name + "' contains an element that " + validationTest.ExceptionMessageSuffix;
                            }
                        }

                        // Act
                        var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                        // Assert
                        actual.Should().BeOfType(validationTest.EachExceptionType);
                        actual.Message.Should().Be(expectedExceptionMessage);
                    }
                }
            }
        }

        private class ValidationTest
        {
            public Validation Validation { get; set; }

            public Type ExceptionType { get; set; }

            public Type EachExceptionType { get; set; }

            public string ExceptionMessageSuffix { get; set; }

            public TestValues<Guid> GuidTestValues { get; set; }

            public TestValues<Guid?> NullableGuidTestValues { get; set; }

            public TestValues<DateTime> DateTimeTestValues { get; set; }

            public TestValues<DateTime?> NullableDateTimeTestValues { get; set; }

            public TestValues<decimal> DecimalTestValues { get; set; }

            public TestValues<decimal?> NullableDecimalTestValues { get; set; }

            public TestValues<string> StringTestValues { get; set; }

            public TestValues<object> ObjectTestValues { get; set; }
        }

        private class TestValues<T>
        {
            public IReadOnlyCollection<T> PassingValues { get; set; }

            public IReadOnlyCollection<T> FailingValues { get; set; }

            public IReadOnlyCollection<IEnumerable<T>> EachPassingValues { get; set; }

            public IReadOnlyCollection<IEnumerable<T>> EachFailingValues { get; set; }
        }
    }
}