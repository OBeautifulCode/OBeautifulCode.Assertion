// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.StartOrEndWith.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    using static System.FormattableString;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void StartWith___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().StartWith(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called StartWith(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void StartWith___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue, StringComparison? comparisonType)
            {
                return (subject, because, applyBecause, data) => subject.StartWith(comparisonValue, comparisonType, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", A.Dummy<StringComparison>()),
                VerificationName = nameof(Verifications.StartWith),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", null),
                VerificationName = nameof(Verifications.StartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.StartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", null, "starter" },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "starter",
                    "starter" + A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "starter", "starter" + A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "Astarter",
                    "Somestarter",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "starter", string.Empty, "starter" + A.Dummy<string>() },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("staRTer", StringComparison.OrdinalIgnoreCase),
                VerificationName = nameof(Verifications.StartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.StartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "starter",
                    "starteR" + A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "stArter", "STarter" + A.Dummy<string>() },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "AstaRTer",
                    "SomestaRTer",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "staRTer", string.Empty, "staRTer" + A.Dummy<string>() },
                },
            };

            // Act, Assert
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void StartWith___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "some-string";
            var expected1 = Invariant($"Provided value (name: 'subject1') does not start with the specified comparison value.  Provided value is 'some-string'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is <null>.");

            var subject2 = new[] { "starter", "some-string", "starter" };
            var expected2 = "Provided value (name: 'subject2') contains an element that does not start with the specified comparison value.  Element value is 'some-string'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is 'OrdinalIgnoreCase'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().StartWith("starter"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().StartWith("starter", StringComparison.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotStartWith___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotStartWith(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotStartWith(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotStartWith___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue, StringComparison? comparisonType)
            {
                return (subject, because, applyBecause, data) => subject.NotStartWith(comparisonValue, comparisonType, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", A.Dummy<StringComparison>()),
                VerificationName = nameof(Verifications.NotStartWith),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("starter", null),
                VerificationName = nameof(Verifications.NotStartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotStartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", null, "something" },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "something",
                    "something-starter",
                    "Starter",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "something", "something-starter", "Starter" },
                },
                MustFailingValues = new string[]
                {
                    "starter",
                    "starter-something",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", "starter", "something" },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("STarter", StringComparison.OrdinalIgnoreCase),
                VerificationName = nameof(Verifications.NotStartWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotStartWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "something",
                    "something-STarter",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "something", "something-STarter" },
                },
                MustFailingValues = new string[]
                {
                    "STarter",
                    "STarter-something",
                    "starter",
                    "starter-something",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", "starter", "something" },
                },
            };

            // Act, Assert
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void NotStartWith___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "starter-something";
            var expected1 = Invariant($"Provided value (name: 'subject1') starts with the specified comparison value.  Provided value is 'starter-something'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is <null>.");

            var subject2 = new[] { "something", "STARTER-something", "something" };
            var expected2 = "Provided value (name: 'subject2') contains an element that starts with the specified comparison value.  Element value is 'STARTER-something'.  Specified 'comparisonValue' is 'starter'.  Specified 'comparisonType' is 'OrdinalIgnoreCase'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotStartWith("starter"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotStartWith("starter", StringComparison.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void EndWith___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().EndWith(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called EndWith(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void EndWith___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue, StringComparison? comparisonType)
            {
                return (subject, because, applyBecause, data) => subject.EndWith(comparisonValue, comparisonType, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("ending", A.Dummy<StringComparison>()),
                VerificationName = nameof(Verifications.EndWith),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("ending", null),
                VerificationName = nameof(Verifications.EndWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.EndWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "ending", null, "ending" },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "ending",
                    A.Dummy<string>() + "ending",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "ending", A.Dummy<string>() + "ending" },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "endingA",
                    "endingSome",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "ending", string.Empty, A.Dummy<string>() + "ending" },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("endINg", StringComparison.OrdinalIgnoreCase),
                VerificationName = nameof(Verifications.EndWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.EndWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "ending",
                    A.Dummy<string>() + "endinG",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "ending", A.Dummy<string>() + "endinG" },
                },
                MustFailingValues = new string[]
                {
                    string.Empty,
                    "endINgA",
                    "endINgSome",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "endINg", string.Empty, A.Dummy<string>() + "endINg" },
                },
            };

            // Act, Assert
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void EndWith___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "some-string";
            var expected1 = Invariant($"Provided value (name: 'subject1') does not end with the specified comparison value.  Provided value is 'some-string'.  Specified 'comparisonValue' is 'ending'.  Specified 'comparisonType' is <null>.");

            var subject2 = new[] { "ending", "some-string", "ending" };
            var expected2 = "Provided value (name: 'subject2') contains an element that does not end with the specified comparison value.  Element value is 'some-string'.  Specified 'comparisonValue' is 'ending'.  Specified 'comparisonType' is 'OrdinalIgnoreCase'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().EndWith("ending"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().EndWith("ending", StringComparison.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotEndWith___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValue_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotEndWith(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotEndWith(comparisonValue:) where parameter 'comparisonValue' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotEndWith___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(string comparisonValue, StringComparison? comparisonType)
            {
                return (subject, because, applyBecause, data) => subject.NotEndWith(comparisonValue, comparisonType, because, applyBecause, data);
            }

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("ending", A.Dummy<StringComparison>()),
                VerificationName = nameof(Verifications.NotEndWith),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("ending", null),
                VerificationName = nameof(Verifications.NotEndWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotEndWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid>[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.NewGuid() },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    A.Dummy<Guid>(),
                    Guid.Empty,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { Guid.NewGuid(), Guid.NewGuid() },
                    new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid() },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    null,
                    A.Dummy<object>(),
                    new List<string> { null },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), null, A.Dummy<object>() },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", null, "something" },
                },
            };

            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "something",
                    "ending-something",
                    "Ending",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "something", "ending-something", "Ending" },
                },
                MustFailingValues = new string[]
                {
                    "ending",
                    "something=ending",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", "ending", "something" },
                },
            };

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler("EndINg", StringComparison.OrdinalIgnoreCase),
                VerificationName = nameof(Verifications.NotEndWith),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotEndWithExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var stringTestValues3 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    "something",
                    "EndINg-something",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "something", "EndINg-something" },
                },
                MustFailingValues = new string[]
                {
                    "EndINg",
                    "something-EndINg",
                    "ending",
                    "something-ending",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "something", "ending", "something" },
                },
            };

            // Act, Assert
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(stringTestValues1);

            verificationTest2.Run(guidTestValues);
            verificationTest2.Run(nullableGuidTestValues);
            verificationTest2.Run(objectTestValues);
            verificationTest2.Run(stringTestValues2);

            verificationTest3.Run(stringTestValues3);
        }

        [Fact]
        public static void NotEndWith___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            string subject1 = "something-ending";
            var expected1 = Invariant($"Provided value (name: 'subject1') ends with the specified comparison value.  Provided value is 'something-ending'.  Specified 'comparisonValue' is 'ending'.  Specified 'comparisonType' is <null>.");

            var subject2 = new[] { "something", "something-ENDING", "something" };
            var expected2 = "Provided value (name: 'subject2') contains an element that ends with the specified comparison value.  Element value is 'something-ENDING'.  Specified 'comparisonValue' is 'ending'.  Specified 'comparisonType' is 'OrdinalIgnoreCase'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotEndWith("ending"));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotEndWith("ending", StringComparison.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}