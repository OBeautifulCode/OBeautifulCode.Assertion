// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidatorTest.cs" company="OBeautifulCode">
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

    using OBeautifulCode.Math.Recipes;

    using Xunit;

    public static class ParameterValidatorTest
    {
        private static readonly ParameterEqualityComparer ParameterComparer = new ParameterEqualityComparer();

        [Fact]
        public static void Named___Should_throw_InvalidOperationException___When_value_is_of_type_Parameter()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Named(A.Dummy<string>())));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Named___Should_set_create_Parameter_and_set_Value_to_value_and_Name_to_name_and_HasBeenNamed_to_true___When_value_not_of_type_Parameter()
        {
            // Arrange
            string nullValue = null;
            var value = A.Dummy<object>();
            var name = A.Dummy<string>();

            var expected1 = new Parameter
            {
                Value = null,
                Name = null,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected2 = new Parameter
            {
                Value = null,
                Name = name,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected3 = new Parameter
            {
                Value = value,
                Name = null,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected4 = new Parameter
            {
                Value = value,
                Name = name,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            // Act
            var actual1 = nullValue.Named(null);
            var actual2 = nullValue.Named(name);
            var actual3 = value.Named(null);
            var actual4 = value.Named(name);

            // Assert
            ParameterComparer.Equals(actual1, expected1).Should().BeTrue();
            ParameterComparer.Equals(actual2, expected2).Should().BeTrue();
            ParameterComparer.Equals(actual3, expected3).Should().BeTrue();
            ParameterComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_throw_InvalidOperationException___When_value_is_a_Parameter_that_has_not_been_named()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenNamed = false).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_InvalidOperationException___When_value_is_a_Parameter_that_has_been_musted()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenMusted).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_InvalidOperationException___When_value_is_a_Parameter_that_has_been_eached()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenEached).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_InvalidOperationException___When_value_is_a_Parameter_that_has_been_validated()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenValidated).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_return_same_Parameter_but_with_HasBeenMusted_set_to_true___When_value_is_a_Parameter_that_is_named_and_not_musted_and_not_eached_and_not_validated()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenNamed).Where(_ => !_.HasBeenMusted).Where(_ => !_.HasBeenEached).Where(_ => !_.HasBeenValidated).ToList();
            var expecteds = parameters.Select(_ =>
            {
                var result = _.Clone();
                result.HasBeenMusted = true;
                return result;
            }).ToList();

            // Act
            var actuals = parameters.Select(_ => _.Must()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => ParameterComparer.Equals(expected, actual));
        }

        [Fact]
        public static void Must___Should_throw_InvalidOperationException___When_value_is_an_anonymous_object_with_multiple_properties()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new { someParameter = A.Dummy<object>(), someParameter2 = A.Dummy<object>() }.Must());

            // Assert
            actual.Should().BeOfType<InvalidOperationException>();
            actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
        }

        [Fact]
        public static void Must___Should_return_Parameter_with_Value_and_Name_pulled_out_of_anonymous_object_property_and_HasBeenMusted_set_to_true___When_value_is_an_anonymous_object_with_one_property()
        {
            // Arrange
            var value = A.Dummy<object>();
            var expected = new Parameter
            {
                Value = value,
                Name = "someParameter",
                HasBeenNamed = false,
                HasBeenMusted = true,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            // Act
            var actual = new { someParameter = value }.Must();

            // Assert
            ParameterComparer.Equals(expected, actual).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_return_Parameter_with_null_Value_and_null_Name_and_HasBeenMusted_set_to_true___When_value_is_null()
        {
            // Arrange
            var value1 = new { someParameter = A.Dummy<object>() };
            value1 = null;

            string value2 = null;

            var expected = new Parameter
            {
                Value = null,
                Name = null,
                HasBeenNamed = false,
                HasBeenMusted = true,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            // Act
            var actual1 = value1.Must();
            var actual2 = value2.Must();

            // Assert
            ParameterComparer.Equals(expected, actual1).Should().BeTrue();
            ParameterComparer.Equals(expected, actual2).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_return_Parameter_with_Value_set_to_value_and_null_Name_and_HasBeenMusted_set_to_true___When_value_is_null()
        {
            // Arrange
            var value = A.Dummy<object>();

            var expected = new Parameter
            {
                Value = value,
                Name = null,
                HasBeenNamed = false,
                HasBeenMusted = true,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            // Act
            var actual = value.Must();

            // Assert
            ParameterComparer.Equals(expected, actual).Should().BeTrue();
        }

        private static IReadOnlyCollection<Parameter> BuildParametersWithAllCombinationsOfFlags()
        {
            var flags = new[] { true, false };
            var result = new List<Parameter>();

            foreach (var nameFlag in flags)
            {
                foreach (var mustFlag in flags)
                {
                    foreach (var eachFlag in flags)
                    {
                        foreach (var validatedFlag in flags)
                        {
                            var parameter = new Parameter
                            {
                                Value = ThreadSafeRandom.Next(0, 2) == 0 ? A.Dummy<object>() : null,
                                Name = ThreadSafeRandom.Next(0, 2) == 0 ? A.Dummy<string>() : null,
                                HasBeenNamed = nameFlag,
                                HasBeenMusted = mustFlag,
                                HasBeenEached = eachFlag,
                                HasBeenValidated = validatedFlag,
                            };

                            result.Add(parameter);
                        }
                    }
                }
            }

            return result;
        }
    }
}
