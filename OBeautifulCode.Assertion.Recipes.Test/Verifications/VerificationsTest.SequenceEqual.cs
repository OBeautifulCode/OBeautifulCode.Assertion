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
                    null,
                    new[] { "a", "b", "C" },
                    new[] { "c", "b", "a" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue1, null },
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
                    null,
                    new[] { "c", "b", "a" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue2, null },
                    new[] { comparisonValue2, new[] { "c", "b", "a" } },
                },
            };

            verificationTest2.Run(testValues2);

            IEnumerable<string> comparisonValue3 = null;

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSequenceEqualToExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    (IEnumerable<string>)null,
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null, null },
                },
                MustFailingValues = new[]
                {
                    new[] { "a", "b", "C" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { null, new[] { "a", "b", "C" } },
                },
            };

            verificationTest3.Run(testValues3);
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

        [Fact]
        public static void BeSequenceEqualToWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().BeSequenceEqualToWhenNotNull(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().BeSequenceEqualToWhenNotNull(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called BeSequenceEqualToWhenNotNull() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called BeSequenceEqualToWhenNotNull() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void BeSequenceEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.BeSequenceEqualToWhenNotNull(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeSequenceEqualToWhenNotNull);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSequenceEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue1,
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { comparisonValue1, null },
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
                ExceptionMessageSuffix = Verifications.BeSequenceEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { comparisonValue2, null },
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

            IEnumerable<string> comparisonValue3 = null;

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeSequenceEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    (IEnumerable<string>)null,
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null, null },
                },
                MustFailingValues = new[]
                {
                    new[] { "a", "b", "C" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { null, new[] { "a", "b", "C" } },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        public static void BeSequenceEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { 0 };
            int[] comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { "a", "b", "c" };
            var comparisonValue2 = new[] { "a", "B" };
            var expected2 = "Provided value (name: 'subject2') is not null and is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject3 = new[] { new[] { 0 } };
            int[] comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject4 = new[] { new[] { "a", "b", "c" } };
            var comparisonValue4 = new[] { "a", "B" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeSequenceEqualToWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeSequenceEqualToWhenNotNull(comparisonValue2, StringComparer.OrdinalIgnoreCase));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeSequenceEqualToWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeSequenceEqualToWhenNotNull(comparisonValue4, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeSequenceEqualTo___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().NotBeSequenceEqualTo(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().NotBeSequenceEqualTo(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called NotBeSequenceEqualTo() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called NotBeSequenceEqualTo() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void NotBeSequenceEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.NotBeSequenceEqualTo(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeSequenceEqualTo);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSequenceEqualToExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "a", "b", "C" },
                    new[] { "c", "b", "a" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { new[] { "a", "b", "C" }, null },
                    new[] { null, new[] { "a", "b", "C" } },
                },
                MustFailingValues = new[]
                {
                    comparisonValue1,
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "a", "b", "C" }, comparisonValue1 },
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
                ExceptionMessageSuffix = Verifications.NotBeSequenceEqualToExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "c", "b", "a" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { new[] { "c", "b", "a" }, null },
                    new[] { null, new[] { "c", "b", "a" } },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "c", "b", "a" }, new[] { "A", "B", "C" } },
                },
            };

            verificationTest2.Run(testValues2);

            IEnumerable<string> comparisonValue3 = null;

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSequenceEqualToExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    new[] { "a", "b", "C" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new[] { "a", "b", "C" }, new[] { "a", "b", "C" } },
                },
                MustFailingValues = new[]
                {
                    (IEnumerable<string>)null,
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "a", "b", "C" }, null },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        public static void NotBeSequenceEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int[] subject1 = null;
            int[] comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { 0, 1 };
            var comparisonValue2 = new[] { 0, 1 };
            var expected2 = "Provided value (name: 'subject2') is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject3 = new[] { "a", "b", "c" };
            var comparisonValue3 = new[] { "a", "B", "c" };
            var expected3 = "Provided value (name: 'subject3') is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject4 = new int[][] { null };
            int[] comparisonValue4 = null;
            var expected4 = "Provided value (name: 'subject4') contains an element that is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject5 = new[] { new[] { 0 }, new[] { 1, 2 } };
            var comparisonValue5 = new[] { 1, 2 };
            var expected5 = "Provided value (name: 'subject5') contains an element that is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject6 = new[] { new[] { "a", "b", "c" }, new[] { "A", "B" } };
            var comparisonValue6 = new[] { "a", "B" };
            var expected6 = "Provided value (name: 'subject6') contains an element that is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeSequenceEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeSequenceEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeSequenceEqualTo(comparisonValue3, StringComparer.OrdinalIgnoreCase));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeSequenceEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeSequenceEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeSequenceEqualTo(comparisonValue6, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeSequenceEqualToWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().NotBeSequenceEqualToWhenNotNull(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().NotBeSequenceEqualToWhenNotNull(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called NotBeSequenceEqualToWhenNotNull() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called NotBeSequenceEqualToWhenNotNull() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void NotBeSequenceEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.NotBeSequenceEqualToWhenNotNull(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeSequenceEqualToWhenNotNull);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSequenceEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "a", "b", "C" },
                    new[] { "c", "b", "a" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { new[] { "a", "b", "C" }, null },
                    new[] { null, new[] { "a", "b", "C" } },
                },
                MustFailingValues = new[]
                {
                    comparisonValue1,
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "a", "b", "C" }, comparisonValue1 },
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
                ExceptionMessageSuffix = Verifications.NotBeSequenceEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "c", "b", "a" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { new[] { "c", "b", "a" }, null },
                    new[] { null, new[] { "c", "b", "a" } },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "c", "b", "a" }, new[] { "A", "B", "C" } },
                },
            };

            verificationTest2.Run(testValues2);

            IEnumerable<string> comparisonValue3 = null;

            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeSequenceEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "a", "b", "C" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new IEnumerable<string>[] { new[] { "a", "b", "C" }, new[] { "a", "b", "C" } },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        public static void NotBeSequenceEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { 0, 1 };
            var comparisonValue1 = new[] { 0, 1 };
            var expected1 = "Provided value (name: 'subject1') is not null and is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { "a", "b", "c" };
            var comparisonValue2 = new[] { "a", "B", "c" };
            var expected2 = "Provided value (name: 'subject2') is not null and is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject3 = new[] { new[] { 0 }, new[] { 1, 2 } };
            var comparisonValue3 = new[] { 1, 2 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject4 = new[] { new[] { "a", "b", "c" }, new[] { "A", "B" } };
            var comparisonValue4 = new[] { "a", "B" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is sequence equal to the comparison value using EqualityExtensions.IsSequenceEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeSequenceEqualToWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeSequenceEqualToWhenNotNull(comparisonValue2, StringComparer.OrdinalIgnoreCase));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeSequenceEqualToWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeSequenceEqualToWhenNotNull(comparisonValue4, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}