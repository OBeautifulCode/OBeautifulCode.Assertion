// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertionTrackerExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using FakeItEasy;
    using OBeautifulCode.Type;
    using Xunit;

    public static class AssertionTrackerExtensionsTest
    {
        [Fact]
        public static void ToSelfValidationFailure___Should_throw_ArgumentNullException___When_parameter_assertionTracker_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => AssertionTrackerExtensions.ToSelfValidationFailure(null));

            // Act
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("assertionTracker");
        }

        [Fact]
        public static void ToSelfValidationFailure___Should_throw_ArgumentNullException___When_assertionTracker_SubjectName_is_null()
        {
            // Arrange
            var assertionTracker = new AssertionTracker();

            // Act
            var actual = Record.Exception(() => assertionTracker.ToSelfValidationFailure());

            // Act
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("SubjectName");
        }

        [Fact]
        public static void ToSelfValidationFailure___Should_throw_ArgumentException___When_assertionTracker_SubjectName_is_white_space()
        {
            // Arrange
            var assertionTracker = new AssertionTracker { SubjectName = "  \r\n  " };

            // Act
            var actual = Record.Exception(() => assertionTracker.ToSelfValidationFailure());

            // Act
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("SubjectName");
            actual.Message.AsTest().Must().ContainString("white space");
        }

        [Fact]
        public static void ToSelfValidationFailure___Should_return_null___When_assertionTracker_VerificationException_is_null()
        {
            // Arrange
            var assertionTracker = new AssertionTracker { SubjectName = A.Dummy<string>() };

            // Act
            var actual = assertionTracker.ToSelfValidationFailure();

            // Act
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToSelfValidationFailure___Should_return_expected_SelfValidationFailure___When_assertionTracker_VerificationException_is_not_null()
        {
            // Arrange
            var exceptionMessage = A.Dummy<string>();
            var subjectName = A.Dummy<string>();

            var assertionTracker = new AssertionTracker
            {
                SubjectName = subjectName,
                VerificationException = new InvalidOperationException(exceptionMessage),
            };

            var expected = new SelfValidationFailure(subjectName, exceptionMessage);

            // Act
            var actual = assertionTracker.ToSelfValidationFailure();

            // Act
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ToSelfValidationFailures___Should_throw_ArgumentNullException___When_parameter_assertionTrackers_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => AssertionTrackerExtensions.ToSelfValidationFailures(null));

            // Act
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("assertionTrackers");
        }

        [Fact]
        public static void ToSelfValidationFailures___Should_throw_ArgumentException___When_parameter_assertionTrackers_contains_a_null_element()
        {
            // Arrange
            var assertionsTrackers = new[] { new AssertionTracker(), null, new AssertionTracker() };

            // Act
            var actual = Record.Exception(() => assertionsTrackers.ToSelfValidationFailures());

            // Act
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("assertionTrackers");
            actual.Message.AsTest().Must().ContainString("null element");
        }

        [Fact]
        public static void ToSelfValidationFailures___Should_return_empty_list___When_assertionTrackers_have_no_VerificationExceptions()
        {
            // Arrange
            var assertionsTrackers = new[]
            {
                new AssertionTracker
                {
                    SubjectName = A.Dummy<string>(),
                },
                new AssertionTracker
                {
                    SubjectName = A.Dummy<string>(),
                },
                new AssertionTracker
                {
                    SubjectName = A.Dummy<string>(),
                },
            };

            // Act
            var actual = assertionsTrackers.ToSelfValidationFailures();

            // Act
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void ToSelfValidationFailures___Should_return_SelfValidationFailures_for_all_trackers_with_VerificationException___When_some_trackers_in_assertionTrackers_have_VerificationException()
        {
            // Arrange
            var exceptionMessage1 = A.Dummy<string>();
            var subjectName1 = A.Dummy<string>();

            var exceptionMessage2 = A.Dummy<string>();
            var subjectName2 = A.Dummy<string>();

            var assertionsTrackers = new[]
            {
                new AssertionTracker
                {
                    SubjectName = subjectName1,
                    VerificationException = new InvalidOperationException(exceptionMessage1),
                },
                new AssertionTracker
                {
                    SubjectName = A.Dummy<string>(),
                },
                new AssertionTracker
                {
                    SubjectName = subjectName2,
                    VerificationException = new InvalidOperationException(exceptionMessage2),
                },
            };

            var expected = new[]
            {
                new SelfValidationFailure(subjectName1, exceptionMessage1),
                new SelfValidationFailure(subjectName2, exceptionMessage2),
            };

            // Act
            var actual = assertionsTrackers.ToSelfValidationFailures();

            // Act
            actual.AsTest().Must().BeSequenceEqualTo(expected);
        }
    }
}
