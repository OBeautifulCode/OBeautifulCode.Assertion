// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    using static System.FormattableString;

    public static class ParameterValidationsTest
    {
        private static readonly ParameterEqualityComparer ParameterComparer = new ParameterEqualityComparer();

        private delegate Parameter Validation(Parameter parameter, string because = null);

        [Fact]
        public static void Test()
        {
            // Arrange
            IEnumerable<string> values5 = new List<string>();
            IReadOnlyCollection<string> values6 = new List<string>();
            var values1 = new[] { string.Empty };
            var values2 = new List<string> { string.Empty };
            var values3 = new ArrayList();
            var values4 = new Dictionary<string, string>();

            values6.Must().Each().NotBeNull();
            values5.Must().Each().NotBeNull();
            values1.Must().Each().NotBeNull();
            values2.Must().Each().NotBeNull();
            values3.Must().Each().NotBeNull();
            values4.Must().Each().NotBeNull();
        }

        [Fact]
        public static void BeNull()
        {
            // Arrange
            var validationTest = new ValidationTest
            {
                Validation = ParameterValidation.BeNull,
                ValidationName = nameof(ParameterValidation.BeNull),
                ExceptionType = typeof(ArgumentException),
                EachExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = "is not null",
                InvalidCastExpectedTypes = "Any Reference Type, Nullable<T>",
                InvalidCastExpectedEnumerableTypes = "IEnumerable<Any Reference Type>, IEnumerable<Nullable<T>>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustInvalidTypeValues = new[] { Guid.Empty, Guid.NewGuid() },
                MustEachInvalidTypeValues = new IEnumerable<Guid>[] { new Guid[] { }, new Guid[] { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, new List<Guid>() },
            };

            // Act, Assert
            RunValidationTest(validationTest, guidTestValues);
        }

        private static void RunValidationTest<T>(
            ValidationTest validationTest,
            TestValues<T> testValues)
        {
            var names = new[] { null, "paramName" };
            var becauses = new[] { null, "because" };

            // passing cases
            foreach (var name in names)
            {
                foreach (var because in becauses)
                {
                    // all passing scenarios
                    var passingParameters = testValues.MustPassingValues.Select(_ => _.Named(name).Must()).Concat(testValues.MustEachPassingValues.Select(_ => _.Named(name).Must().Each())).ToList();
                    foreach (var parameter in passingParameters)
                    {
                        // Arrange
                        var expected = parameter.CloneWithHasBeenValidated();

                        // Act
                        var actual = validationTest.Validation(parameter, because);

                        // Assert
                        ParameterComparer.Equals(actual, expected).Should().BeTrue();
                    }

                    // all failing scenarios on the value itself (no Each())
                    foreach (var failingValue in testValues.MustFailingValues)
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

                    // calling Each() on IEnumerable that is null OR a value that's not IEnumerable
                    if (true)
                    {
                        // Arrange
                        IEnumerable<string> nullEnumerable = null;
                        var parameter1 = nullEnumerable.Named(name).Must();
                        parameter1.HasBeenEached = true;

                        object notEnumerable = new object();
                        var parameter2 = notEnumerable.Named(name).Must();
                        parameter2.HasBeenEached = true;

                        // Act
                        var actual1 = Record.Exception(() => validationTest.Validation(parameter1, because));
                        var actual2 = Record.Exception(() => validationTest.Validation(parameter2, because));

                        // Assert
                        actual1.Should().BeOfType<InvalidOperationException>();
                        actual1.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);

                        actual2.Should().BeOfType<InvalidOperationException>();
                        actual2.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
                    }

                    // all failing scenarios on the elements of an IEnumerable value (Each())
                    foreach (var eachFailingValue in testValues.MustEachFailingValues)
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

                    // all invalid cast scenarios on the value itself (no Each())
                    foreach (var invalidTypeValue in testValues.MustInvalidTypeValues)
                    {
                        // Arrange
                        var parameter = invalidTypeValue.Named(name).Must();
                        var expectedMessage = Invariant($"called {validationTest.ValidationName}() on an object that is not one of the following types: {validationTest.InvalidCastExpectedTypes}");

                        // Act
                        var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                        // Assert
                        actual.Should().BeOfType<InvalidCastException>();
                        actual.Message.Should().Be(expectedMessage);
                    }

                    // all invalid cast scenarios on the elements of an IEnumerable value (Each())
                    foreach (var invalidTypeValue in testValues.MustEachInvalidTypeValues)
                    {
                        // Arrange
                        var parameter = invalidTypeValue.Named(name).Must().Each();
                        var expectedMessage = Invariant($"called {validationTest.ValidationName}() on an object that is not one of the following types: {validationTest.InvalidCastExpectedEnumerableTypes}");

                        // Act
                        var actual = Record.Exception(() => validationTest.Validation(parameter, because));

                        // Assert
                        actual.Should().BeOfType<InvalidCastException>();
                        actual.Message.Should().Be(expectedMessage);
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

            public string InvalidCastExpectedTypes { get; set; }

            public string InvalidCastExpectedEnumerableTypes { get; set; }

            public string ValidationName { get; set; }
        }

        private class TestValues<T>
        {
            public IReadOnlyCollection<T> MustInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustPassingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachPassingValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustFailingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachFailingValues { get; set; } = new List<List<T>>();
        }
    }
}