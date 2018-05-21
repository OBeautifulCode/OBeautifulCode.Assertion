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
                    PassingValues = new Guid[0],  // guids can't be null
                    FailingValues = new Guid[] { A.Dummy<Guid>(), Guid.Empty },
                    EachPassingValues = new IEnumerable<Guid>[0],
                    EachFailingValues = new IEnumerable<Guid>[] { Some.ReadOnlyDummies<Guid>() },
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
                        actual.Should().BeOfType(validationTest.ExceptionType);
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