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
        private static readonly AssertionTrackerEqualityComparer AssertionTrackerComparer = new AssertionTrackerEqualityComparer();

        [Fact]
        public static void Named___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_of_type_AssertionTracker()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Named(A.Dummy<string>())));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Named___Should_create_AssertionTracker_and_set_SubjectValue_to_parameter_value_and_SubjectType_to_TSubject_and_Name_to_name_and_HasBeenNamed_to_true___When_parameter_value_not_of_type_AssertionTracker()
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
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual4, expected4).Should().BeTrue();
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_with_null_SubjectType()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags()
                .Where(_ => _.Actions.HasFlag(Actions.Named))
                .Where(_ => !_.Actions.HasFlag(Actions.Musted))
                .Where(_ => !_.Actions.HasFlag(Actions.Eached))
                .Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            foreach (var assertionTracker in assertionTrackers)
            {
                assertionTracker.SubjectType = null;
            }

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_not_been_named()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.Named)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_been_musted()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "eached", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_been_eached()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Eached)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Must()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void Must___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_value_is_an_AssertionTracker_that_has_been_verified_at_least_once()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Must()));

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
        public static void Must___Should_return_same_AssertionTracker_but_with_HasBeenMusted_set_to_true___When_parameter_value_is_an_AssertionTracker_with_SubjectType_that_is_named_and_not_musted_and_not_eached_and_not_verified_at_least_once()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Named)).Where(_ => !_.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();
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
            actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public static void Must___Should_return_AssertionTracker_with_SubjectValue_and_SubjectType_and_Name_pulled_out_of_anonymous_object_property_and_HasBeenMusted_set_to_true___When_parameter_value_is_an_anonymous_object_with_one_property()
        {
            // Arrange
            var value1 = A.Dummy<object>();
            string value2 = null;

            var expected1 = new AssertionTracker
            {
                SubjectValue = value1,
                SubjectType = typeof(object),
                SubjectName = "someSubject",
                Actions = Actions.Musted,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = value2,
                SubjectType = typeof(string),
                SubjectName = "someSubject",
                Actions = Actions.Musted,
            };

            // Act
            var actual1 = new { someSubject = value1 }.Must();
            var actual2 = new { someSubject = value2 }.Must();

            // Assert
            AssertionTrackerComparer.Equals(expected1, actual1).Should().BeTrue();
            AssertionTrackerComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Typeset", Justification = "This is cased correctly.")]
        public static void Must___Should_return_AssertionTracker_with_null_SubjectValue_and_SubjectType_set_to_TSubject_and_null_Name_and_HasBeenMusted_set_to_true___When_parameter_value_is_null()
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
            AssertionTrackerComparer.Equals(expected1, actual1).Should().BeTrue();
            AssertionTrackerComparer.Equals(expected2, actual2).Should().BeTrue();
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Typeset", Justification = "This is cased correctly.")]
        public static void Must___Should_return_AssertionTracker_with_SubjectValue_set_to_parameter_value_and_SubjectType_set_to_TSubject_and_null_Name_and_HasBeenMusted_set_to_true___When_parameter_value_is_not_an_anonymous_object_and_not_null()
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
            AssertionTrackerComparer.Equals(expected, actual).Should().BeTrue();
        }

        [Fact]
        public static void Named_and_Must___Should_return_expected_AssertionTracker___When_all_supported_chaining_combinations_are_executed()
        {
            // Arrange
            var testSubject = A.Dummy<string>();
            var expected1 = new AssertionTracker
            {
                SubjectValue = testSubject,
                SubjectType = typeof(string),
                Actions = Actions.Musted,
            };

            var expected2 = new AssertionTracker
            {
                SubjectValue = testSubject,
                SubjectType = typeof(string),
                SubjectName = nameof(testSubject),
                Actions = Actions.Named | Actions.Musted,
            };

            var expected3 = new AssertionTracker
            {
                SubjectValue = testSubject,
                SubjectType = typeof(string),
                SubjectName = nameof(testSubject),
                Actions = Actions.Musted,
            };

            // Act
            var actual1 = testSubject.Must();
            var actual2 = testSubject.Named(nameof(testSubject)).Must();
            var actual3 = new { testSubject = testSubject }.Must();

            // Assert
            AssertionTrackerComparer.Equals(actual1, expected1).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual2, expected2).Should().BeTrue();
            AssertionTrackerComparer.Equals(actual3, expected3).Should().BeTrue();
        }

        [Fact]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_SubjectType_is_null()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).ToList();
            foreach (var assertionTracker in assertionTrackers)
            {
                assertionTracker.SubjectType = null;
            }

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_not_been_musted()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.Musted)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Each()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "eached", Justification = "This is the best wording for this identifier.")]
        public static void Each___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_been_eached()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Eached)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.Each()));

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
        public static void Each___Should_return_same_AssertionTracker_but_with_HasBeenEached_set_to_true___When_AssertionTracker_is_musted_and_not_eached()
        {
            // Arrange
            var assertionTrackers1 = BuildAssertionTrackersWithAllCombinationsOfFlags(subjectType: typeof(object)).Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).ToList();
            var assertionTrackers2 = BuildAssertionTrackersWithAllCombinationsOfFlags(subjectType: typeof(IEnumerable)).Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => !_.Actions.HasFlag(Actions.Eached)).ToList();

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
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_not_been_musted()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.Musted)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        public static void And___Should_throw_ImproperUseOfAssertionFrameworkException___When_AssertionTracker_has_not_been_verified_at_least_once()
        {
            // Arrange
            var assertionTrackers = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => !_.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            // Act
            var actuals = assertionTrackers.Select(_ => Record.Exception(() => _.And()));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(Verifications.ImproperUseOfFrameworkExceptionMessage);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "musted", Justification = "This is the best wording for this identifier.")]
        public static void And___Should_return_same_AssertionTracker___When_AssertionTracker_is_musted_and_verified_at_least_once()
        {
            // Arrange
            var expecteds = BuildAssertionTrackersWithAllCombinationsOfFlags().Where(_ => _.Actions.HasFlag(Actions.Musted)).Where(_ => _.Actions.HasFlag(Actions.VerifiedAtLeastOnce)).ToList();

            // Act
            var actuals = expecteds.Select(_ => _.And()).ToList();

            // Assert
            actuals.Should().Equal(expecteds, (expected, actual) => AssertionTrackerComparer.Equals(expected, actual));
        }

        private static IReadOnlyCollection<AssertionTracker> BuildAssertionTrackersWithAllCombinationsOfFlags(
            Type subjectType = null,
            bool subjectCanBeNull = true,
            bool subjectMustBeNull = false)
        {
            if (subjectType == null)
            {
                subjectType = typeof(object);
            }

            var result = EnumExtensions.GetAllPossibleEnumValues<Actions>().Select(
                _ =>
                    new AssertionTracker
                    {
                        SubjectValue = subjectMustBeNull ? null : subjectCanBeNull ? (ThreadSafeRandom.Next(0, 2) == 0 ? AD.ummy(subjectType) : null) : AD.ummy(subjectType),
                        SubjectType = subjectType,
                        SubjectName = ThreadSafeRandom.Next(0, 2) == 0 ? A.Dummy<string>() : null,
                        Actions = _,
                    })
                .ToList();

            return result;
        }
    }
}
