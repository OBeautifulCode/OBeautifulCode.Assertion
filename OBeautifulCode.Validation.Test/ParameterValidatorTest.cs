// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidatorTest.cs" company="OBeautifulCode">
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
        public static void Named___Should_create_Parameter_and_set_Value_to_value_and_ValueType_to_TParameterValue_and_Name_to_name_and_HasBeenNamed_to_true___When_value_not_of_type_Parameter()
        {
            // Arrange
            string nullValue = null;
            var value = A.Dummy<decimal?>();
            var name = A.Dummy<string>();

            var expected1 = new Parameter
            {
                Value = nullValue,
                ValueType = typeof(string),
                Name = null,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected2 = new Parameter
            {
                Value = nullValue,
                ValueType = typeof(string),
                Name = name,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected3 = new Parameter
            {
                Value = value,
                ValueType = typeof(decimal?),
                Name = null,
                HasBeenNamed = true,
                HasBeenMusted = false,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected4 = new Parameter
            {
                Value = value,
                ValueType = typeof(decimal?),
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
        public static void Must___Should_throw_InvalidOperationException___When_value_is_a_Parameter_with_null_ValueType()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenNamed).Where(_ => !_.HasBeenMusted).Where(_ => !_.HasBeenEached).Where(_ => !_.HasBeenValidated).ToList();
            foreach (var parameter in parameters)
            {
                parameter.ValueType = null;
            }

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
        public static void Must___Should_throw_InvalidOperationException___When_value_is_a_Parameter_that_has_not_been_named()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.HasBeenNamed).ToList();

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
        public static void Must___Should_return_same_Parameter_but_with_HasBeenMusted_set_to_true___When_value_is_a_Parameter_with_ValueType_that_is_named_and_not_musted_and_not_eached_and_not_validated()
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
        public static void Must___Should_return_Parameter_with_Value_and_ValueType_and_Name_pulled_out_of_anonymous_object_property_and_HasBeenMusted_set_to_true___When_value_is_an_anonymous_object_with_one_property()
        {
            // Arrange
            var value1 = A.Dummy<object>();
            string value2 = null;

            var expected1 = new Parameter
            {
                Value = value1,
                ValueType = typeof(object),
                Name = "someParameter",
                HasBeenNamed = false,
                HasBeenMusted = true,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected2 = new Parameter
            {
                Value = value2,
                ValueType = typeof(string),
                Name = "someParameter",
                HasBeenNamed = false,
                HasBeenMusted = true,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            // Act
            var actual1 = new { someParameter = value1 }.Must();
            var actual2 = new { someParameter = value2 }.Must();

            // Assert
            ParameterComparer.Equals(expected1, actual1).Should().BeTrue();
            ParameterComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_return_Parameter_with_null_Value_and_ValueType_set_to_TParameterValue_and_null_Name_and_HasBeenMusted_set_to_true___When_value_is_null()
        {
            // Arrange
            var value1 = new { someParameter = A.Dummy<object>() };
            var value1Type = value1.GetType();
            value1 = null;

            string value2 = null;

            var expected1 = new Parameter
            {
                Value = null,
                ValueType = value1Type,
                Name = null,
                HasBeenNamed = false,
                HasBeenMusted = true,
                HasBeenEached = false,
                HasBeenValidated = false,
            };

            var expected2 = new Parameter
            {
                Value = null,
                ValueType = typeof(string),
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
            ParameterComparer.Equals(expected1, actual1).Should().BeTrue();
            ParameterComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_return_Parameter_with_Value_set_to_value_and_ValueType_set_to_TParameterValue_and_null_Name_and_HasBeenMusted_set_to_true___When_value_is_not_an_anonymous_object_and_not_null()
        {
            // Arrange
            var value = A.Dummy<string>();

            var expected = new Parameter
            {
                Value = value,
                ValueType = typeof(string),
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

        [Fact]
        public static void Named_and_Must___Should_return_expected_Parameter___When_all_supported_chaining_combinations_are_executed()
        {
            // Arrange
            var testParameter = A.Dummy<string>();
            var expected1 = new Parameter
            {
                Value = testParameter,
                ValueType = typeof(string),
                HasBeenMusted = true,
            };

            var expected2 = new Parameter
            {
                Value = testParameter,
                ValueType = typeof(string),
                Name = nameof(testParameter),
                HasBeenMusted = true,
                HasBeenNamed = true,
            };

            var expected3 = new Parameter
            {
                Value = testParameter,
                ValueType = typeof(string),
                Name = nameof(testParameter),
                HasBeenMusted = true,
            };

            // Act
            var actual1 = testParameter.Must();
            var actual2 = testParameter.Named(nameof(testParameter)).Must();
            var actual3 = new { testParameter }.Must();

            // Assert
            ParameterComparer.Equals(actual1, expected1).Should().BeTrue();
            ParameterComparer.Equals(actual2, expected2).Should().BeTrue();
            ParameterComparer.Equals(actual3, expected3).Should().BeTrue();
        }

        [Fact]
        public static void Each___Should_throw_InvalidOperationException___When_parameter_ValueType_is_null()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenMusted).Where(_ => !_.HasBeenEached).ToList();
            foreach (var parameter in parameters)
            {
                parameter.ValueType = null;
            }

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Each___Should_throw_InvalidOperationException___When_parameter_has_not_been_musted()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.HasBeenMusted).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Each___Should_throw_InvalidOperationException___When_parameter_has_been_eached()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenEached).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Each___Should_throw_InvalidOperationException___When_parameter_Value_is_null()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags(valueMustBeNull: true).Where(_ => _.HasBeenMusted).Where(_ => !_.HasBeenEached).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Each___Should_throw_InvalidCastException___When_parameter_Value_is_not_an_IEnumerable()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags(valueCanBeNull: false).Where(_ => _.HasBeenMusted).Where(_ => !_.HasBeenEached).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidCastException>();
                actual.Message.Should().Be("called Each() on an object that is not one of the following types: IEnumerable");
            }
        }

        [Fact]
        public static void Each___Should_return_same_parameter_but_with_HasBeenEached_set_to_true___When_parameter_is_musted_and_not_eached_and_Value_is_an_IEnumerable()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags(valueType: typeof(IEnumerable), valueCanBeNull: false).Where(_ => _.HasBeenMusted).Where(_ => !_.HasBeenEached).ToList();

            var expecteds = parameters.Select(_ =>
            {
                var result = _.Clone();
                result.HasBeenEached = true;
                return result;
            }).ToList();

            // Act
            var actuals = parameters.Select(_ => _.Each()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => ParameterComparer.Equals(expected, actual));
        }

        [Fact]
        public static void And___Should_throw_InvalidOperationException___When_parameter_ValueType_is_null()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenMusted).Where(_ => _.HasBeenValidated).ToList();
            foreach (var parameter in parameters)
            {
                parameter.ValueType = null;
            }

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void And___Should_throw_InvalidOperationException___When_parameter_has_not_been_musted()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.HasBeenMusted).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void And___Should_throw_InvalidOperationException___When_parameter_has_not_been_validated()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.HasBeenValidated).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<InvalidOperationException>();
                actual.Message.Should().Be(ParameterValidator.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void And___Should_return_same_parameter___When_parameter_is_musted_and_valudated()
        {
            // Arrange
            var expecteds = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.HasBeenMusted).Where(_ => _.HasBeenValidated).ToList();

            // Act
            var actuals = expecteds.Select(_ => _.And()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => ParameterComparer.Equals(expected, actual));
        }

        private static IReadOnlyCollection<Parameter> BuildParametersWithAllCombinationsOfFlags(
            Type valueType = null,
            bool valueCanBeNull = true,
            bool valueMustBeNull = false)
        {
            if (valueType == null)
            {
                valueType = typeof(object);
            }

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
                                Value = valueMustBeNull ? null : valueCanBeNull ? (ThreadSafeRandom.Next(0, 2) == 0 ? AD.ummy(valueType) : null) : AD.ummy(valueType),
                                ValueType = valueType,
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
