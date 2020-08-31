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

    using OBeautifulCode.Assertion.Recipes.Test.Internal;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    public static class AssertionExtensionsTest
    {
        private static readonly AssertionTrackerEqualityComparer AssertionTrackerComparer = new AssertionTrackerEqualityComparer();

        private enum SubjectValueNullOption
        {
            CannotBeNull,

            CanBeNull,

            MustBeNull,
        }

        [Fact]
        public static void As___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_of_type_AssertionTracker()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags();

            // Act
            var actuals1 = assertionTrackers.Select(_ => Record.Exception(() => _.AsArg(A.Dummy<string>())));
            var actuals2 = assertionTrackers.Select(_ => Record.Exception(() => _.AsOp(A.Dummy<string>())));
            var actuals3 = assertionTrackers.Select(_ => Record.Exception(() => _.AsTest(A.Dummy<string>())));

            // Assert
            var actuals = new Exception[0].Concat(actuals1).Concat(actuals2).Concat(actuals3);
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        public static void As___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_anonymous_object_with_multiple_properties()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => new { someSubject = A.Dummy<object>(), someSubject2 = A.Dummy<object>() }.AsArg());
            var actual2 = Record.Exception(() => new { someSubject = A.Dummy<object>(), someSubject2 = A.Dummy<object>() }.AsOp());
            var actual3 = Record.Exception(() => new { someSubject = A.Dummy<object>(), someSubject2 = A.Dummy<object>() }.AsTest());

            var expectedExceptionMessage = "Provided value is an anonymous object having 2 properties.  Only single-property anonymous objects are supported.  Found the following properties: someSubject, someSubject2.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be(expectedExceptionMessage);

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().Be(expectedExceptionMessage);

            actual3.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual3.Message.Should().Be(expectedExceptionMessage);
        }

        [Fact]
        public static void AsArg___Should_create_AssertionTracker_in_the_expected_state___When_parameter_value_is_not_of_type_AssertionTracker_and_not_an_anonymous_object()
        {
            // Arrange
            var nullValue = (string)null;
            var value = A.Dummy<decimal?>();
            var name = A.Dummy<string>();

            var expected1 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = null,
                Actions = Actions.Categorized,
                AssertionKind = AssertionKind.Argument,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Argument,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = null,
                Actions = Actions.Categorized,
                AssertionKind = AssertionKind.Argument,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Argument,
            };

            // Act
            var actual1 = nullValue.AsArg();
            var actual2 = nullValue.AsArg(name);
            var actual3 = value.AsArg();
            var actual4 = value.AsArg(name);

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void AsArg___Should_create_AssertionTracker_in_the_expected_state___When_parameter_value_is_not_of_type_AssertionTracker_and_an_anonymous_object()
        {
            // Arrange
            var nullValue = (string)null;
            var value = A.Dummy<decimal?>();
            var name = A.Dummy<string>();

            var expected1 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = nameof(nullValue),
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Argument,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Argument,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = nameof(value),
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Argument,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Argument,
            };

            // Act
            var actual1 = new { nullValue }.AsArg();
            var actual2 = new { nullValue }.AsArg(name);
            var actual3 = new { value }.AsArg();
            var actual4 = new { value }.AsArg(name);

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void AsOp___Should_create_AssertionTracker_in_the_expected_state___When_parameter_value_is_not_of_type_AssertionTracker()
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
                Actions = Actions.Categorized,
                AssertionKind = AssertionKind.Operation,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Operation,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = null,
                Actions = Actions.Categorized,
                AssertionKind = AssertionKind.Operation,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Operation,
            };

            // Act
            var actual1 = nullValue.AsOp();
            var actual2 = nullValue.AsOp(name);
            var actual3 = value.AsOp();
            var actual4 = value.AsOp(name);

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void AsOp___Should_create_AssertionTracker_in_the_expected_state___When_parameter_value_is_not_of_type_AssertionTracker_and_an_anonymous_object()
        {
            // Arrange
            var nullValue = (string)null;
            var value = A.Dummy<decimal?>();
            var name = A.Dummy<string>();

            var expected1 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = nameof(nullValue),
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Operation,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Operation,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = nameof(value),
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Operation,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Operation,
            };

            // Act
            var actual1 = new { nullValue }.AsOp();
            var actual2 = new { nullValue }.AsOp(name);
            var actual3 = new { value }.AsOp();
            var actual4 = new { value }.AsOp(name);

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void AsTest___Should_create_AssertionTracker_in_the_expected_state___When_parameter_value_is_not_of_type_AssertionTracker()
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
                Actions = Actions.Categorized,
                AssertionKind = AssertionKind.Test,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Test,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = null,
                Actions = Actions.Categorized,
                AssertionKind = AssertionKind.Test,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Test,
            };

            // Act
            var actual1 = nullValue.AsTest();
            var actual2 = nullValue.AsTest(name);
            var actual3 = value.AsTest();
            var actual4 = value.AsTest(name);

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void AsTest___Should_create_AssertionTracker_in_the_expected_state___When_parameter_value_is_not_of_type_AssertionTracker_and_an_anonymous_object()
        {
            // Arrange
            var nullValue = (string)null;
            var value = A.Dummy<decimal?>();
            var name = A.Dummy<string>();

            var expected1 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = nameof(nullValue),
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Test,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = nullValue,
                SubjectType = typeof(string),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Test,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = nameof(value),
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Test,
            };

            var expected4 = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(decimal?),
                SubjectName = name,
                Actions = Actions.Categorized | Actions.Named,
                AssertionKind = AssertionKind.Test,
            };

            // Act
            var actual1 = new { nullValue }.AsTest();
            var actual2 = new { nullValue }.AsTest(name);
            var actual3 = new { value }.AsTest();
            var actual4 = new { value }.AsTest(name);

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_with_null_SubjectType()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithActionsInMustableState();

            foreach (var assertionTracker in assertionTrackers)
            {
                assertionTracker.SubjectType = null;
            }

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Must));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_not_been_Categorized()
        {
            // Arrange
            var assertionTrackers1 = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => !_.Actions.HasFlag(Actions.Categorized))
                .ToList();

            var assertionTrackers2 = BuildAssertionTrackersWithActionsInMustableState(assertionKind: AssertionKind.Unknown);

            var assertionTrackers = new AssertionTracker[0].Concat(assertionTrackers1).Concat(assertionTrackers2);

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Must));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_been_Must()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Musted))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Must));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_been_Each()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Eached))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_been_VerifiedAtLeastOnce()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Must));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_return_same_AssertionTracker_but_with_Actions_Musted_bit_set___When_parameter_value_is_an_AssertionTracker_that_has_been_Categorized_and_not_Must_and_not_Each_and_not_VerifiedAtLeastOnce()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithActionsInMustableState();

            var expecteds = assertionTrackers.Select(_ =>
            {
                var result = _.Clone();

                result.Actions |= Actions.Musted;

                return result;
            }).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => _.Must()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => AssertionTrackerComparer.Equals(expected, actual));
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_anonymous_object_with_multiple_properties()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new { someSubject = A.Dummy<object>(), someSubject2 = A.Dummy<object>() }.Must());

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Provided value is an anonymous object having 2 properties.  Only single-property anonymous objects are supported.  Found the following properties: someSubject, someSubject2.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_return_AssertionTracker_with_SubjectValue_and_SubjectType_and_SubjectName_pulled_out_of_anonymous_object_property_and_Actions_Musted_bit_set___When_parameter_value_is_an_anonymous_object_with_one_property()
        {
            // Arrange
            var value1 = A.Dummy<object>();
            string value2 = null;

            var expected1 = new AssertionTracker
            {
                SubjectValue = value1,
                SubjectType = typeof(object),
                SubjectName = "someSubject",
                Actions = Actions.Named | Actions.Musted,
                AssertionKind = AssertionKind.Unknown,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = value2,
                SubjectType = typeof(string),
                SubjectName = "someSubject",
                Actions = Actions.Named | Actions.Musted,
                AssertionKind = AssertionKind.Unknown,
            };

            // Act
            var actual1 = new { someSubject = value1 }.Must();
            var actual2 = new { someSubject = value2 }.Must();

            // Assert
            AssertionTrackerComparer.Equals(expected1, actual1).Should().BeTrue();
            AssertionTrackerComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Typeset", Justification = ObcSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWords)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static void Must___Should_return_AssertionTracker_with_null_SubjectValue_and_SubjectType_set_to_TSubject_and_null_Name_and_Actions_Musted_bit_set___When_parameter_value_is_null()
        {
            // Arrange
            var value1 = new { someSubject = A.Dummy<object>() };
            var value1Type = value1.GetType();
            value1 = null;

            string value2 = null;

            var expected1 = new AssertionTracker
            {
                SubjectValue = null,
                SubjectType = value1Type,
                SubjectName = null,
                Actions = Actions.Musted,
                AssertionKind = AssertionKind.Unknown,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = null,
                SubjectType = typeof(string),
                SubjectName = null,
                Actions = Actions.Musted,
                AssertionKind = AssertionKind.Unknown,
            };

            // Act
            var actual1 = value1.Must();
            var actual2 = value2.Must();

            // Assert
            AssertionTrackerComparer.Equals(expected1, actual1).Should().BeTrue();
            AssertionTrackerComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Typeset", Justification = ObcSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWords)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_return_AssertionTracker_with_SubjectValue_set_to_parameter_value_and_SubjectType_set_to_TSubject_and_null_Name_and_Actions_Musted_bit_set___When_parameter_value_is_not_an_anonymous_object_and_not_null()
        {
            // Arrange
            var value = A.Dummy<string>();

            var expected = new AssertionTracker
            {
                SubjectValue = value,
                SubjectType = typeof(string),
                SubjectName = null,
                Actions = Actions.Musted,
                AssertionKind = AssertionKind.Unknown,
            };

            // Act
            var actual = value.Must();

            // Assert
            AssertionTrackerComparer.Equals(expected, actual).Should().BeTrue();
        }

        [Fact]
        public static void As_and_Must___Should_return_expected_AssertionTracker___When_all_supported_chaining_combinations_are_executed()
        {
            // Arrange
            var testSubject = A.Dummy<string>();

            var expecteds = new[]
            {
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = null,
                    Actions = Actions.Musted,
                    AssertionKind = AssertionKind.Unknown,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = null,
                    Actions = Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Argument,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Categorized | Actions.Named | Actions.Musted,
                    AssertionKind = AssertionKind.Argument,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = null,
                    Actions = Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Operation,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Categorized | Actions.Named | Actions.Musted,
                    AssertionKind = AssertionKind.Operation,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = null,
                    Actions = Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Test,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Categorized | Actions.Named | Actions.Musted,
                    AssertionKind = AssertionKind.Test,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Named | Actions.Musted,
                    AssertionKind = AssertionKind.Unknown,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Named | Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Argument,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = "name",
                    Actions = Actions.Named | Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Argument,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Named | Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Operation,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = "name",
                    Actions = Actions.Named | Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Operation,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = nameof(testSubject),
                    Actions = Actions.Named | Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Test,
                },
                new AssertionTracker
                {
                    SubjectValue = testSubject,
                    SubjectType = typeof(string),
                    SubjectName = "name",
                    Actions = Actions.Named | Actions.Categorized | Actions.Musted,
                    AssertionKind = AssertionKind.Test,
                },
            };

            // Act
            var actuals = new[]
            {
                testSubject.Must(),
                testSubject.AsArg().Must(),
                testSubject.AsArg(nameof(testSubject)).Must(),
                testSubject.AsOp().Must(),
                testSubject.AsOp(nameof(testSubject)).Must(),
                testSubject.AsTest().Must(),
                testSubject.AsTest(nameof(testSubject)).Must(),
                new { testSubject }.Must(),
                new { testSubject }.AsArg().Must(),
                new { testSubject }.AsArg("name").Must(),
                new { testSubject }.AsOp().Must(),
                new { testSubject }.AsOp("name").Must(),
                new { testSubject }.AsTest().Must(),
                new { testSubject }.AsTest("name").Must(),
            };

            // Assert
            for (int x = 0; x < expecteds.Length; x++)
            {
                AssertionTrackerComparer.Equals(actuals[x], expecteds[x]).Should().BeTrue();
            }
        }

        [Fact]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_SubjectType_is_null()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Musted))
                .Where(_ => !_.Actions.HasFlag(Actions.Eached))
                .ToList();
            foreach (var assertionTracker in assertionTrackers)
            {
                assertionTracker.SubjectType = null;
            }

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Each));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_is_not_Musted()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => !_.Actions.HasFlag(Actions.Musted))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Each));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eached", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_been_Eached()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Eached))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.Each));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eached", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static void Each___Should_return_same_AssertionTracker_but_with_the_Actions_Eached_bit_set___When_AssertionTracker_is_Musted_but_Not_Eached()
        {
            // Arrange
            var assertionTrackers1 = BuildAssertionTrackersWithAllCombinationsOfFlags(subjectType: typeof(object))
                .Where(_ => _.Actions.HasFlag(Actions.Musted))
                .Where(_ => !_.Actions.HasFlag(Actions.Eached))
                .ToList();

            var assertionTrackers2 = BuildAssertionTrackersWithAllCombinationsOfFlags(subjectType: typeof(IEnumerable))
                .Where(_ => _.Actions.HasFlag(Actions.Musted))
                .Where(_ => !_.Actions.HasFlag(Actions.Eached))
                .ToList();

            var expecteds1 = assertionTrackers1.Select(_ =>
            {
                var result = _.Clone();
                result.Actions |= Actions.Eached;
                return result;
            }).ToList();

            var expecteds2 = assertionTrackers2.Select(_ =>
            {
                var result = _.Clone();
                result.Actions |= Actions.Eached;
                return result;
            }).ToList();

            // Act
            var actuals1 = assertionTrackers1.Select(_ => _.Each()).ToList();
            var actuals2 = assertionTrackers2.Select(_ => _.Each()).ToList();

            // Assert
            actuals1.Should().Equal(expecteds1, (expected, actual) => AssertionTrackerComparer.Equals(expected, actual));
            actuals2.Should().Equal(expecteds2, (expected, actual) => AssertionTrackerComparer.Equals(expected, actual));
        }

        [Fact]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_SubjectType_is_null()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();
            foreach (var assertionTracker in assertionTrackers)
            {
                assertionTracker.SubjectType = null;
            }

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.And));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_not_been_Musted()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => !_.Actions.HasFlag(Actions.Musted))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.And));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_not_been_VerifiedAtLeastOnce()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce))
                .ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(_.And));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.SubjectAndOperationSequencingErrorMessage + "  " + Verifications.ImproperUseOfFrameworkErrorMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void And___Should_return_same_AssertionTracker___When_AssertionTracker_is_Musted_and_VerifiedAtLeastOnce()
        {
            // Arrange
            var expecteds = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Musted))
                .Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce))
                .ToList();

            // Act
            var actuals = expecteds.Select(_ => _.And()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => AssertionTrackerComparer.Equals(expected, actual));
        }

        private static IReadOnlyCollection<AssertionTracker> BuildAssertionTrackersWithActionsInMustableState(
            Type subjectType = null,
            SubjectValueNullOption subjectValueNullOption = SubjectValueNullOption.CanBeNull,
            AssertionKind? assertionKind = null)
        {
            var result = BuildAssertionTrackersWithAllCombinationsOfFlags(subjectType, subjectValueNullOption, assertionKind)
                .Where(_ => _.Actions.HasFlag(Actions.Categorized))
                .Where(_ => !_.Actions.HasFlag(Actions.Musted))
                .Where(_ => !_.Actions.HasFlag(Actions.Eached))
                .Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce))
                .ToList();

            return result;
        }

        private static IReadOnlyCollection<AssertionTracker> BuildAssertionTrackersWithAllCombinationsOfFlags(
            Type subjectType = null,
            SubjectValueNullOption subjectValueNullOption = SubjectValueNullOption.CanBeNull,
            AssertionKind? assertionKind = null)
        {
            if (subjectType == null)
            {
                subjectType = typeof(object);
            }

            var result = EnumExtensions.GetAllPossibleEnumValues<Actions>().Select(
                _ =>
                {
                    object subjectValue;
                    switch (subjectValueNullOption)
                    {
                        case SubjectValueNullOption.CannotBeNull:
                            subjectValue = AD.ummy(subjectType);
                            break;
                        case SubjectValueNullOption.MustBeNull:
                            subjectValue = null;
                            break;
                        case SubjectValueNullOption.CanBeNull:
                            subjectValue = ThreadSafeRandom.Next(0, 2) == 0 ? AD.ummy(subjectType) : null;
                            break;
                        default:
                            throw new NotSupportedException("this is not supported: " + subjectValueNullOption);
                    }

                    return new AssertionTracker
                    {
                        SubjectValue = subjectValue,
                        SubjectType = subjectType,
                        SubjectName = ThreadSafeRandom.Next(0, 2) == 0 ? A.Dummy<string>() : null,
                        Actions = _,
                        AssertionKind = assertionKind ?? A.Dummy<AssertionKind>(),
                    };
                })
                .ToList();

            return result;
        }
    }
}
