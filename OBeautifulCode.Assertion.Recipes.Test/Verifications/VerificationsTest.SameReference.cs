// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.SameReference.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void BeSameReferenceAs___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeSameReferenceAs(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeSameReferenceAs);

            // subject type is not a value type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>",
            };

            var testValues1a = new TestValues<int>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    1,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new int[] { },
                    new int[] { 1, 2 },
                },
            };

            var testValues1b = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1c = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);

            // comparisonValue is not the same type as the subject
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "VerificationsTest.TestClass",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var testValues2 = new TestValues<TestClass>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass(),
                    null,
                },
                MustEachVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass[] { },
                    new TestClass[] { new TestClass(), null, new TestClass() },
                },
            };

            verificationTest2.Run(testValues2);

            // same or not same reference
            var comparisonValue3 = A.Dummy<EquatableTestClass>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { comparisonValue3, comparisonValue3 },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    null,
                    new EquatableTestClass(),
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { comparisonValue3, null, comparisonValue3 },
                    new EquatableTestClass[] { comparisonValue3, new EquatableTestClass(), comparisonValue3 },
                },
            };

            verificationTest3.Run(testValues3);

            // null comparison value
            EquatableTestClass comparisonValue4 = null;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new EquatableTestClass[]
                {
                    null,
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { comparisonValue4, null, comparisonValue4 },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    new EquatableTestClass(),
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { null, new EquatableTestClass(), null },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void BeSameReferenceAs___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            EquatableTestClass subject1 = null;
            EquatableTestClass comparisonValue1 = new EquatableTestClass();
            var expected1 = "Provided value (name: 'subject1') is not the same reference as the comparison value.  Provided value is <null>.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            EquatableTestClass subject2 = new EquatableTestClass();
            EquatableTestClass comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not the same reference as the comparison value.  Provided value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is <null>.";

            EquatableTestClass subject3 = new EquatableTestClass();
            EquatableTestClass comparisonValue3 = new EquatableTestClass();
            var expected3 = "Provided value (name: 'subject3') is not the same reference as the comparison value.  Provided value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            var subject4 = new EquatableTestClass[] { null };
            var comparisonValue4 = new EquatableTestClass();
            var expected4 = "Provided value (name: 'subject4') contains an element that is not the same reference as the comparison value.  Element value is <null>.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            var subject5 = new EquatableTestClass[] { new EquatableTestClass() };
            EquatableTestClass comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not the same reference as the comparison value.  Element value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new EquatableTestClass[] { new EquatableTestClass() };
            var comparisonValue6 = new EquatableTestClass();
            var expected6 = "Provided value (name: 'subject6') contains an element that is not the same reference as the comparison value.  Element value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeSameReferenceAs(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeSameReferenceAs(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeSameReferenceAs(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeSameReferenceAs(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeSameReferenceAs(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeSameReferenceAs(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeSameReferenceAs___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeSameReferenceAs(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeSameReferenceAs);

            // subject type is not a value type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                SubjectInvalidTypeExpectedTypes = "Any Reference Type",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<Any Reference Type>",
            };

            var testValues1a = new TestValues<int>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    1,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new int[] { },
                    new int[] { 1, 2 },
                },
            };

            var testValues1b = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var testValues1c = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            verificationTest1.Run(testValues1a);
            verificationTest1.Run(testValues1b);
            verificationTest1.Run(testValues1c);

            // comparisonValue is not the same type as the subject
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<string>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "VerificationsTest.TestClass",
                VerificationParameterInvalidTypeName = "comparisonValue",
            };

            var testValues2 = new TestValues<TestClass>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass(),
                    null,
                },
                MustEachVerificationParameterInvalidTypeValues = new[]
                {
                    new TestClass[] { },
                    new TestClass[] { new TestClass(), null, new TestClass() },
                },
            };

            verificationTest2.Run(testValues2);

            // same or not same reference
            var comparisonValue3 = A.Dummy<EquatableTestClass>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new[]
                {
                    null,
                    new EquatableTestClass(),
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { new EquatableTestClass(), null, new EquatableTestClass() },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    comparisonValue3,
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { new EquatableTestClass(), comparisonValue3, new EquatableTestClass() },
                },
            };

            verificationTest3.Run(testValues3);

            // null comparison value
            EquatableTestClass comparisonValue4 = null;
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSameReferenceAsExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<EquatableTestClass>
            {
                MustPassingValues = new EquatableTestClass[]
                {
                    new EquatableTestClass(),
                },
                MustEachPassingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { },
                    new EquatableTestClass[] { new EquatableTestClass(), new EquatableTestClass() },
                },
                MustFailingValues = new EquatableTestClass[]
                {
                    null,
                },
                MustEachFailingValues = new IEnumerable<EquatableTestClass>[]
                {
                    new EquatableTestClass[] { new EquatableTestClass(), null, new EquatableTestClass() },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void NotBeSameReferenceAs___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            EquatableTestClass subject1 = null;
            EquatableTestClass comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is the same reference as the comparison value.  Provided value is <null>.  Specified 'comparisonValue' is <null>.";

            EquatableTestClass subject2 = new EquatableTestClass();
            EquatableTestClass comparisonValue2 = subject2;
            var expected2 = "Provided value (name: 'subject2') is the same reference as the comparison value.  Provided value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            var subject3 = new EquatableTestClass[] { new EquatableTestClass(), null, new EquatableTestClass() };
            EquatableTestClass comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is the same reference as the comparison value.  Element value is <null>.  Specified 'comparisonValue' is <null>.";

            var subject4 = new EquatableTestClass[] { null, new EquatableTestClass(), null };
            EquatableTestClass comparisonValue4 = subject4.Skip(1).First();
            var expected4 = "Provided value (name: 'subject4') contains an element that is the same reference as the comparison value.  Element value is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.  Specified 'comparisonValue' is 'OBeautifulCode.Assertion.Recipes.Test.VerificationsTest+EquatableTestClass'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeSameReferenceAs(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeSameReferenceAs(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeSameReferenceAs(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeSameReferenceAs(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}