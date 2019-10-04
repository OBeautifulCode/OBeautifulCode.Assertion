// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertionExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    public static class AssertionExtensionsTest
    {
        private static readonly ParameterEqualityComparer ParameterComparer = new ParameterEqualityComparer();

        [Fact]
        public static void Named___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_of_type_Parameter()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Named(A.Dummy<string>())));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Named___Should_create_Parameter_and_set_Value_to_value_and_ValueType_to_TParameterValue_and_Name_to_name_and_HasBeenNamed_to_true___When_value_not_of_type_Parameter()
        {
            // Arrange
            string nullValue = null;
            var value = A.Dummy<decimal?>();
            var name = A.Dummy<string>();

            var expected1 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = null,
                Actions = Actions.Named,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Named,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = null,
                Actions = Actions.Named,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Named,
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
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_a_Parameter_with_null_ValueType()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Named))
                .Where(_ => !_.Actions.HasFlag(Actions.Musted))
                .Where(_ => !_.Actions.HasFlag(Actions.Eached))
                .Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            foreach (var parameter in parameters)
            {
                parameter.SubjectType = null;
            }

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_a_Parameter_that_has_not_been_named()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.Named)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_a_Parameter_that_has_been_musted()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "eached", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_a_Parameter_that_has_been_eached()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Eached)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_a_Parameter_that_has_been_validated()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "eached", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_return_same_Parameter_but_with_HasBeenMusted_set_to_true___When_value_is_a_Parameter_with_ValueType_that_is_named_and_not_musted_and_not_eached_and_not_validated()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Named)).Where(_ => !_.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();
            var expecteds = parameters.Select(_ =>
            {
                var result = _.Clone();
                result.Actions |= Actions.Musted;
                return result;
            }).ToList();

            // Act
            var actuals = parameters.Select(_ => _.Must()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => ParameterComparer.Equals(expected, actual));
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_value_is_an_anonymous_object_with_multiple_properties()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new { someParameter = A.Dummy<object>(), someParameter2 = A.Dummy<object>() }.Must());

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_return_Parameter_with_Value_and_ValueType_and_Name_pulled_out_of_anonymous_object_property_and_HasBeenMusted_set_to_true___When_value_is_an_anonymous_object_with_one_property()
        {
            // Arrange
            var value1 = A.Dummy<object>();
            string value2 = null;

            var expected1 = new AssertionTracker
            {
                SubjectValue = value1,
                SubjectType = typeof(object),
                SubjectName = "someParameter",
                Actions = Actions.Musted,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = value2,
                SubjectType = typeof(string),
                SubjectName = "someParameter",
                Actions = Actions.Musted,
            };

            // Act
            var actual1 = new { someParameter = value1 }.Must();
            var actual2 = new { someParameter = value2 }.Must();

            // Assert
            ParameterComparer.Equals(expected1, actual1).Should().BeTrue();
            ParameterComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Typeset", Justification = "This is cased correctly.")]
        public static void Must___Should_return_Parameter_with_null_Value_and_ValueType_set_to_TParameterValue_and_null_Name_and_HasBeenMusted_set_to_true___When_value_is_null()
        {
            // Arrange
            var value1 = new { someParameter = A.Dummy<object>() };
            var value1Type = value1.GetType();
            value1 = null;

            string value2 = null;

            var expected1 = new AssertionTracker
            {
                SubjectValue = null,
                SubjectType = value1Type,
                SubjectName = null,
                Actions = Actions.Musted,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = null,
                SubjectType = typeof(string),
                SubjectName = null,
                Actions = Actions.Musted,
            };

            // Act
            var actual1 = value1.Must();
            var actual2 = value2.Must();

            // Assert
            ParameterComparer.Equals(expected1, actual1).Should().BeTrue();
            ParameterComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Typeset", Justification = "This is cased correctly.")]
        public static void Must___Should_return_Parameter_with_Value_set_to_value_and_ValueType_set_to_TParameterValue_and_null_Name_and_HasBeenMusted_set_to_true___When_value_is_not_an_anonymous_object_and_not_null()
        {
            // Arrange
            var value = A.Dummy<string>();

            var expected = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(string),
                SubjectName = null,
                Actions = Actions.Musted,
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
            var expected1 = new AssertionTracker
            {
                SubjectValue = testParameter,
                SubjectType = typeof(string),
                Actions = Actions.Musted,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = testParameter,
                SubjectType = typeof(string),
                SubjectName = nameof(testParameter),
                Actions = Actions.Named | Actions.Musted,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = testParameter,
                SubjectType = typeof(string),
                SubjectName = nameof(testParameter),
                Actions = Actions.Musted,
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
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_ValueType_is_null()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).ToList();
            foreach (var parameter in parameters)
            {
                parameter.SubjectType = null;
            }

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_has_not_been_musted()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.Musted)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "eached", Justification = "This is the best wording for this identifier.")]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_has_been_eached()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Eached)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eached", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "eached", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void Each___Should_return_same_parameter_but_with_HasBeenEached_set_to_true___When_parameter_is_musted_and_not_eached()
        {
            // Arrange
            var parameters1 = BuildParametersWithAllCombinationsOfFlags(valueType: typeof(object)).Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).ToList();
            var parameters2 = BuildParametersWithAllCombinationsOfFlags(valueType: typeof(IEnumerable)).Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).ToList();

            var expecteds1 = parameters1.Select(_ =>
            {
                var result = _.Clone();
                result.Actions |= Actions.Eached;
                return result;
            }).ToList();

            var expecteds2 = parameters2.Select(_ =>
            {
                var result = _.Clone();
                result.Actions |= Actions.Eached;
                return result;
            }).ToList();

            // Act
            var actuals1 = parameters1.Select(_ => _.Each()).ToList();
            var actuals2 = parameters2.Select(_ => _.Each()).ToList();

            // Assert
            actuals1.Should().Equal(expecteds1, (expected, actual) => ParameterComparer.Equals(expected, actual));
            actuals2.Should().Equal(expecteds2, (expected, actual) => ParameterComparer.Equals(expected, actual));
        }

        [Fact]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_ValueType_is_null()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();
            foreach (var parameter in parameters)
            {
                parameter.SubjectType = null;
            }

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_has_not_been_musted()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.Musted)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_has_not_been_validated()
        {
            // Arrange
            var parameters = BuildParametersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            // Act
            var actuals = parameters.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void And___Should_return_same_parameter___When_parameter_is_musted_and_validated()
        {
            // Arrange
            var expecteds = BuildParametersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            // Act
            var actuals = expecteds.Select(_ => _.And()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => ParameterComparer.Equals(expected, actual));
        }

        private static IReadOnlyCollection<AssertionTracker> BuildParametersWithAllCombinationsOfFlags(
            Type valueType = null,
            bool valueCanBeNull = true,
            bool valueMustBeNull = false)
        {
            if (valueType == null)
            {
                valueType = typeof(object);
            }

            var result = EnumExtensions.GetAllPossibleEnumValues<Actions>().Select(
                _ =>
                    new AssertionTracker
                    {
                        SubjectValue = valueMustBeNull ? null : valueCanBeNull ? (ThreadSafeRandom.Next(0, 2) == 0 ? AD.ummy(valueType) : null) : AD.ummy(valueType),
                        SubjectType = valueType,
                        SubjectName = ThreadSafeRandom.Next(0, 2) == 0 ? A.Dummy<string>() : null,
                        Actions = _,
                    })
                .ToList();

            return result;
        }
    }
}
