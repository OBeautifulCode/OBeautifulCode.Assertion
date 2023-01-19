// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.SequenceEqual.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void BeSequenceEqualTo___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().BeSequenceEqualTo(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().BeSequenceEqualTo(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called BeSequenceEqualTo() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called BeSequenceEqualTo() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void BeSequenceEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.BeSequenceEqualTo(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeSequenceEqualTo);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSequenceEqualToExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    comparisonValue1,
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { comparisonValue1, comparisonValue1 },
                },
                MustFailingValues = new[]
                {
                    new[] { "a", "b", "C" },
                    new[] { "c", "b", "a" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue1, new[] { "a", "b", "C" } },
                    new[] { comparisonValue1, new[] { "c", "b", "a" } },
                },
            };

            verificationTest1.Run(testValues1);

            var comparisonValue2 = new[] { "a", "b", "c" };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue2, StringComparer.OrdinalIgnoreCase),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSequenceEqualToExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { comparisonValue2, new[] { "a", "b", "C" }, new[] { "A", "B", "C" } },
                },
                MustFailingValues = new[]
                {
                    new[] { "c", "b", "a" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue2, new[] { "c", "b", "a" } },
                },
            };

            verificationTest2.Run(testValues2);
        }

        [Fact]
        public static void BeSequenceEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int[] subject1 = null;
            var comparisonValue1 = new int[] { 0 };
            var expected1 = "Provided value (name: 'subject1') is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { 0 };
            int[] comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject3 = new[] { "a", "b", "c" };
            var comparisonValue3 = new[] { "a", "B" };
            var expected3 = "Provided value (name: 'subject3') is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject4 = new int[][] { null };
            var comparisonValue4 = new[] { 1 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject5 = new[] { new[] { 0 } };
            int[] comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject6 = new[] { new[] { "a", "b", "c" } };
            var comparisonValue6 = new[] { "a", "B" };
            var expected6 = "Provided value (name: 'subject6') contains an element that is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeSequenceEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeSequenceEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeSequenceEqualTo(comparisonValue3, StringComparer.OrdinalIgnoreCase));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeSequenceEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeSequenceEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeSequenceEqualTo(comparisonValue6, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }
    }
}