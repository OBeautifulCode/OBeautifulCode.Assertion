// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Type.Recipes;
    using Xunit;

    using static System.FormattableString;

    public static partial class VerificationsTest
    {
        private static readonly AssertionTrackerEqualityComparer AssertionTrackerComparer = new AssertionTrackerEqualityComparer();

        private delegate AssertionTracker VerificationHandler(AssertionTracker assertionTracker, string because = null, ApplyBecause applyBecause = ApplyBecause.PrefixedToDefaultMessage, IDictionary data = null);

        [Fact]
        public static void Verifications_with_verification_parameter___Should_throw_ImproperUseOfAssertionFrameworkException_with_expected_Exception_message___When_verification_parameter_is_not_of_the_expected_type()
        {
            // Arrange
            var subject1 = A.Dummy<string>();
            var expected1 = "Called BeLessThan(comparisonValue:) where 'comparisonValue' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            var subject2 = Some.ReadOnlyDummies<string>();
            var expected2 = "Called BeLessThan(comparisonValue:) where 'comparisonValue' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            var subject3 = Some.ReadOnlyDummies<string>();
            var expected3 = "Called ContainElement(itemToSearchFor:) where 'itemToSearchFor' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            var subject4 = new[] { Some.ReadOnlyDummies<string>(), Some.ReadOnlyDummies<string>() };
            var expected4 = "Called ContainElement(itemToSearchFor:) where 'itemToSearchFor' is of type decimal, which is not one of the following expected type(s): string.  " + Verifications.ImproperUseOfFrameworkErrorMessage;

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeLessThan(A.Dummy<decimal>()));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeLessThan(A.Dummy<decimal>()));
            var actual3 = Record.Exception(() => new { subject3 }.Must().ContainElement(A.Dummy<decimal>()));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().ContainElement(A.Dummy<decimal>()));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void ApplyBecause___Should_not_alter_default_exception_message___When_ApplyBecause_is_PrefixedToDefaultMessage_and_because_is_null_or_white_space()
        {
            // Arrange
            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: null, applyBecause: ApplyBecause.PrefixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: "  \r\n ", applyBecause: ApplyBecause.PrefixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_not_alter_default_exception_message___When_ApplyBecause_is_SuffixedToDefaultMessage_and_because_is_null_or_white_space()
        {
            // Arrange
            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: null, applyBecause: ApplyBecause.SuffixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: "  \r\n ", applyBecause: ApplyBecause.SuffixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_prefix_default_exception_message_with_because___When_ApplyBecause_is_PrefixedToDefaultMessage_and_because_is_not_null_and_not_white_space()
        {
            // Arrange
            var because = A.Dummy<string>();

            Guid? subject1 = null;
            var expected1 = because + "  Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.";

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = because + "  Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: because, applyBecause: ApplyBecause.PrefixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: because, applyBecause: ApplyBecause.PrefixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_suffix_default_exception_message_with_because___When_ApplyBecause_is_SuffixedToDefaultMessage_and_because_is_not_null_and_not_white_space()
        {
            // Arrange
            var because = A.Dummy<string>();

            Guid? subject1 = null;
            var expected1 = "Provided value (name: 'subject1') is not an empty guid.  Provided value is <null>.  " + because;

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not an empty guid.  Element value is '6d062b50-03c1-4fa4-af8c-097b711214e7'.  " + because;

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: because, applyBecause: ApplyBecause.SuffixedToDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: because, applyBecause: ApplyBecause.SuffixedToDefaultMessage));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void ApplyBecause___Should_replace_default_exception_message_with_empty_string___When_ApplyBecause_is_InLieuOfDefaultMessage_and_because_is_null()
        {
            // Arrange
            Guid? subject1 = null;

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: null, applyBecause: ApplyBecause.InLieuOfDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: null, applyBecause: ApplyBecause.InLieuOfDefaultMessage));

            // Assert
            actual1.Message.Should().Be(string.Empty);
            actual2.Message.Should().Be(string.Empty);
        }

        [Fact]
        public static void ApplyBecause___Should_replace_default_exception_message_with_because___When_ApplyBecause_is_InLieuOfDefaultMessage_and_because_is_not_null()
        {
            // Arrange
            var because = A.Dummy<string>();

            Guid? subject1 = null;

            var subject2 = new Guid[] { Guid.Empty, Guid.Parse("6d062b50-03c1-4fa4-af8c-097b711214e7"), Guid.Empty };

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEmptyGuid(because: string.Empty, applyBecause: ApplyBecause.InLieuOfDefaultMessage));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeEmptyGuid(because: because, applyBecause: ApplyBecause.InLieuOfDefaultMessage));

            // Assert
            actual1.Message.Should().Be(string.Empty);
            actual2.Message.Should().Be(because);
        }

        private static void Run<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues)
        {
            var assertionKinds = EnumExtensions.GetDefinedEnumValues<AssertionKind>();

            var subjectNames = new[] { null, A.Dummy<string>() };

            var userData = new[] { null, A.Dummy<Dictionary<string, string>>() };

            foreach (var assertionKind in assertionKinds)
            {
                foreach (var subjectName in subjectNames)
                {
                    foreach (var data in userData)
                    {
                        // the only way to specify a subject name in an uncategorized assertion is to use
                        // an anonymous object and none of these tests do that...
                        var subjectNameToUse = assertionKind == AssertionKind.Unknown ? null : subjectName;

                        verificationTest.RunPassingScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustFailingScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustEachFailingScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustEachImproperUseOfFrameworkScenarios(assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustInvalidSubjectTypeScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunMustEachInvalidSubjectTypeScenarios(testValues, assertionKind, subjectNameToUse, data);

                        verificationTest.RunInvalidVerificationParameterTypeScenarios(testValues, assertionKind, subjectNameToUse, data);
                    }
                }
            }
        }

        private static void RunPassingScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            var mustAssertionTrackers = testValues.MustPassingValues.Select(_ => _.RunMust(assertionKind, subjectName, each: false));
            var muchEachAssertionTrackers = testValues.MustEachPassingValues.Select(_ => _.RunMust(assertionKind, subjectName, each: true));

            var assertionTrackers = new AssertionTracker[0]
                .Concat(mustAssertionTrackers)
                .Concat(muchEachAssertionTrackers)
                .ToList();

            foreach (var assertionTracker in assertionTrackers)
            {
                // Arrange
                var expected = assertionTracker.CloneWithActionVerifiedAtLeastOnce();

                // Act
                var actual = verificationTest.VerificationHandler(assertionTracker, data: data);

                // Assert
                AssertionTrackerComparer.Equals(actual, expected).Should().BeTrue();
            }
        }

        private static void RunMustFailingScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var failingValue in testValues.MustFailingValues)
            {
                // Arrange
                var assertionTracker = failingValue.RunMust(assertionKind, subjectName, each: false);

                var expectedExceptionMessage = subjectName == null
                    ? "Provided value " + verificationTest.ExceptionMessageSuffix
                    : "Provided value (name: '" + subjectName + "') " + verificationTest.ExceptionMessageSuffix;

                var expectedData = data ?? new Hashtable();

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType(assertionKind.GetExpectedExceptionType(verificationTest.ArgumentExceptionType));
                actual.Message.Should().StartWith(expectedExceptionMessage);
                actual.Data.Keys.Should().BeEquivalentTo(expectedData.Keys);
                foreach (var dataKey in actual.Data.Keys)
                {
                    actual.Data[dataKey].Should().Be(expectedData[dataKey]);
                }
            }
        }

        private static void RunMustEachFailingScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var eachFailingValue in testValues.MustEachFailingValues)
            {
                // Arrange
                var assertionTracker = eachFailingValue.RunMust(assertionKind, subjectName, each: true);

                var expectedExceptionMessage = subjectName == null
                    ? "Provided value contains an element that " + verificationTest.ExceptionMessageSuffix
                    : "Provided value (name: '" + subjectName + "') contains an element that " + verificationTest.ExceptionMessageSuffix;

                var expectedData = data ?? new Hashtable();

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType(assertionKind.GetExpectedExceptionType(verificationTest.EachArgumentExceptionType));
                actual.Message.Should().StartWith(expectedExceptionMessage);
                actual.Data.Keys.Should().BeEquivalentTo(expectedData.Keys);
                foreach (var dataKey in actual.Data.Keys)
                {
                    actual.Data[dataKey].Should().Be(expectedData[dataKey]);
                }
            }
        }

        private static void RunMustEachImproperUseOfFrameworkScenarios(
            this VerificationTest verificationTest,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            // Arrange
            // calling Each() on object that is not IEnumerable
            var notEnumerable = new object();
            var assertionTracker1 = notEnumerable.RunMust(assertionKind, subjectName, each: true);
            var expectedExceptionMessage1 = Invariant($"Called Each() on a value of type object, which is not one of the following expected type(s): IEnumerable.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

            // calling Each() on object that is IEnumerable, but null
            IEnumerable<string> nullEnumerable = null;
            var assertionTracker2 = nullEnumerable.RunMust(assertionKind, subjectName, each: true);
            var expectedExceptionMessage2 = subjectName == null
                ? "Provided value " + Verifications.NotBeNullExceptionMessageSuffix + "."
                : "Provided value (name: '" + subjectName + "') " + Verifications.NotBeNullExceptionMessageSuffix + ".";

            // Act
            var actual1 = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker1, data: data));
            var actual2 = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker2, data: data));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be(expectedExceptionMessage1);

            actual2.Should().BeOfType(assertionKind.GetExpectedExceptionType(typeof(ArgumentNullException)));
            actual2.Message.Should().Be(expectedExceptionMessage2);
        }

        private static void RunMustInvalidSubjectTypeScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var invalidTypeValue in testValues.MustSubjectInvalidTypeValues)
            {
                // Arrange
                var valueTypeName = testValues.MustSubjectInvalidTypeValues.GetType().GetClosedEnumerableElementType().ToStringReadable();
                var assertionTracker = invalidTypeValue.RunMust(assertionKind, subjectName, each: false);
                var expectedMessage = Invariant($"Called {verificationTest.VerificationName}() on a value of type {valueTypeName}, which is not one of the following expected type(s): {verificationTest.SubjectInvalidTypeExpectedTypes}.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private static void RunMustEachInvalidSubjectTypeScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            foreach (var invalidTypeValue in testValues.MustEachSubjectInvalidTypeValues)
            {
                // Arrange
                var valueTypeName = testValues.MustSubjectInvalidTypeValues.GetType().GetClosedEnumerableElementType().ToStringReadable();
                var assertionTracker = invalidTypeValue.RunMust(assertionKind, subjectName, each: true);
                var expectedMessage = Invariant($"Called {verificationTest.VerificationName}() on a value of type IEnumerable<{valueTypeName}>, which is not one of the following expected type(s): {verificationTest.SubjectInvalidTypeExpectedEnumerableTypes}.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().Be(expectedMessage);
            }
        }

        private static void RunInvalidVerificationParameterTypeScenarios<T>(
            this VerificationTest verificationTest,
            TestValues<T> testValues,
            AssertionKind assertionKind,
            string subjectName,
            IDictionary data)
        {
            var mustAssertionTrackers = testValues.MustVerificationParameterInvalidTypeValues.Select(_ => _.RunMust(assertionKind, subjectName, each: false));
            var mustEachAssertionTrackers = testValues.MustEachVerificationParameterInvalidTypeValues.Select(_ => _.RunMust(assertionKind, subjectName, each: true));
            var assertionTrackers = mustAssertionTrackers.Concat(mustEachAssertionTrackers).ToList();

            foreach (var assertionTracker in assertionTrackers)
            {
                // Arrange
                testValues.GetType().GetGenericArguments().First().ToStringReadable();
                var expectedStartOfMessage = Invariant($"Called {verificationTest.VerificationName}({verificationTest.VerificationParameterInvalidTypeName}:) where '{verificationTest.VerificationParameterInvalidTypeName}' is of type");
                var expectedEndOfMessage = Invariant($"which is not one of the following expected type(s): {verificationTest.VerificationParameterInvalidTypeExpectedTypes}.  {Verifications.ImproperUseOfFrameworkErrorMessage}");

                // Act
                var actual = Record.Exception(() => verificationTest.VerificationHandler(assertionTracker, data: data));

                // Assert
                actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
                actual.Message.Should().StartWith(expectedStartOfMessage);
                actual.Message.Should().EndWith(expectedEndOfMessage);
            }
        }

        private static AssertionTracker RunMust<T>(
            this T value,
            AssertionKind assertionKind,
            string subjectName,
            bool each)
        {
            AssertionTracker result;

            switch (assertionKind)
            {
                case AssertionKind.Unknown:
                    result = value.Must();
                    break;
                case AssertionKind.Argument:
                    result = value.AsArg(subjectName).Must();
                    break;
                case AssertionKind.Operation:
                    result = value.AsOp(subjectName).Must();
                    break;
                case AssertionKind.Test:
                    result = value.AsTest(subjectName).Must();
                    break;
                default:
                    throw new NotSupportedException("this assertion kind is not supported: " + assertionKind);
            }

            if (each)
            {
                result.Each();
            }

            return result;
        }

        private static Type GetExpectedExceptionType(
            this AssertionKind assertionKind,
            Type argumentExceptionType)
        {
            Type result;

            switch (assertionKind)
            {
                case AssertionKind.Unknown:
                    result = typeof(AssertionVerificationFailedException);
                    break;
                case AssertionKind.Argument:
                    result = argumentExceptionType;
                    break;
                case AssertionKind.Operation:
                    result = typeof(InvalidOperationException);
                    break;
                case AssertionKind.Test:
                    result = typeof(TestAssertionVerificationFailedException);
                    break;
                default:
                    throw new NotSupportedException("this assertion kind is not supported: " + assertionKind);
            }

            return result;
        }

        private class VerificationTest
        {
            public VerificationHandler VerificationHandler { get; set; }

            public Type ArgumentExceptionType { get; set; }

            public Type EachArgumentExceptionType { get; set; }

            public string ExceptionMessageSuffix { get; set; }

            public string SubjectInvalidTypeExpectedTypes { get; set; }

            public string SubjectInvalidTypeExpectedEnumerableTypes { get; set; }

            public string VerificationParameterInvalidTypeExpectedTypes { get; set; }

            public string VerificationParameterInvalidTypeName { get; set; }

            public string VerificationName { get; set; }
        }

        private class TestValues<T>
        {
            public IReadOnlyCollection<T> MustSubjectInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachSubjectInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustVerificationParameterInvalidTypeValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachVerificationParameterInvalidTypeValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustPassingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachPassingValues { get; set; } = new List<List<T>>();

            public IReadOnlyCollection<T> MustFailingValues { get; set; } = new List<T>();

            public IReadOnlyCollection<IEnumerable<T>> MustEachFailingValues { get; set; } = new List<List<T>>();
        }

        private class TestClass
        {
        }

        private class EquatableTestClass : IEquatable<EquatableTestClass>
        {
            public bool Equals(EquatableTestClass other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (ReferenceEquals(this, null) || ReferenceEquals(other, null))
                {
                    return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }
    }
}