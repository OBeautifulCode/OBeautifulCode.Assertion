// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.UnorderedEqual.cs" company="OBeautifulCode">
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
        public static void BeUnorderedEqualTo___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().BeUnorderedEqualTo(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().BeUnorderedEqualTo(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called BeUnorderedEqualTo() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called BeUnorderedEqualTo() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void BeUnorderedEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.BeUnorderedEqualTo(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeUnorderedEqualTo);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeUnorderedEqualToExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    comparisonValue1,
                    new[] { "b", "a", "c" },
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
                    new[] { "c", "b", "a", "b" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue1, null },
                    new[] { comparisonValue1, new[] { "a", "b", "C" } },
                    new[] { comparisonValue1, new[] { "c", "b", "a", "b" } },
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
                ExceptionMessageSuffix = Verifications.BeUnorderedEqualToExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                    new[] { "b", "a", "c" },
                    new[] { "b", "A", "c" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { comparisonValue2, new[] { "a", "b", "C" }, new[] { "A", "B", "C" }, new[] { "b", "a", "c" }, new[] { "b", "A", "c" }, },
                },
                MustFailingValues = new[]
                {
                    null,
                    new[] { "c", "b", "a", "a" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue2, null },
                    new[] { comparisonValue2, new[] { "c", "b", "a", "a" } },
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
                ExceptionMessageSuffix = Verifications.BeUnorderedEqualToExceptionMessageSuffix,
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
                    new[] { "a", "b", "C", "a" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { null, new[] { "a", "b", "C", "a" } },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        public static void BeUnorderedEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int[] subject1 = null;
            var comparisonValue1 = new int[] { 0 };
            var expected1 = "Provided value (name: 'subject1') is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { 0 };
            int[] comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject3 = new[] { "a", "b", "c" };
            var comparisonValue3 = new[] { "a", "B" };
            var expected3 = "Provided value (name: 'subject3') is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject4 = new int[][] { null };
            var comparisonValue4 = new[] { 1 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject5 = new[] { new[] { 0 } };
            int[] comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject6 = new[] { new[] { "a", "b", "c" } };
            var comparisonValue6 = new[] { "a", "b" };
            var expected6 = "Provided value (name: 'subject6') contains an element that is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeUnorderedEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeUnorderedEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeUnorderedEqualTo(comparisonValue3, StringComparer.OrdinalIgnoreCase));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeUnorderedEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeUnorderedEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeUnorderedEqualTo(comparisonValue6, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void BeUnorderedEqualToWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().BeUnorderedEqualToWhenNotNull(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().BeUnorderedEqualToWhenNotNull(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called BeUnorderedEqualToWhenNotNull() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called BeUnorderedEqualToWhenNotNull() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void BeUnorderedEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.BeUnorderedEqualToWhenNotNull(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeUnorderedEqualToWhenNotNull);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeUnorderedEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue1,
                    new[] { "b", "c", "a" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { comparisonValue1, null, new[] { "b", "c", "a" } },
                    new[] { comparisonValue1, comparisonValue1, new[] { "b", "c", "a" }, },
                },
                MustFailingValues = new[]
                {
                    new[] { "a", "b", "C" },
                    new[] { "c", "b", "a", "c" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue1, new[] { "a", "b", "C" } },
                    new[] { comparisonValue1, new[] { "c", "b", "a", "c" } },
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
                ExceptionMessageSuffix = Verifications.BeUnorderedEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                    new[] { "b", "c", "a" },
                    new[] { "b", "c", "A" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { comparisonValue2, null },
                    new[] { comparisonValue2, new[] { "a", "b", "C" }, new[] { "A", "B", "C" }, new[] { "b", "c", "a" }, new[] { "b", "c", "A" }, },
                },
                MustFailingValues = new[]
                {
                    new[] { "c", "b", "a", "c" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { comparisonValue2, new[] { "c", "b" } },
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
                ExceptionMessageSuffix = Verifications.BeUnorderedEqualToWhenNotNullExceptionMessageSuffix,
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
        public static void BeUnorderedEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { 0 };
            int[] comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { "a", "b", "c" };
            var comparisonValue2 = new[] { "a", "b" };
            var expected2 = "Provided value (name: 'subject2') is not null and is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject3 = new[] { new[] { 0 } };
            int[] comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject4 = new[] { new[] { "a", "b", "c" } };
            var comparisonValue4 = new[] { "a", "b" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeUnorderedEqualToWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeUnorderedEqualToWhenNotNull(comparisonValue2, StringComparer.OrdinalIgnoreCase));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeUnorderedEqualToWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeUnorderedEqualToWhenNotNull(comparisonValue4, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeUnorderedEqualTo___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().NotBeUnorderedEqualTo(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().NotBeUnorderedEqualTo(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called NotBeUnorderedEqualTo() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called NotBeUnorderedEqualTo() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void NotBeUnorderedEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.NotBeUnorderedEqualTo(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeUnorderedEqualTo);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeUnorderedEqualToExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "a", "b", "C" },
                    new[] { "c", "b" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { new[] { "a", "b", "C" }, null },
                    new[] { null, new[] { "a", "b" } },
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
                ExceptionMessageSuffix = Verifications.NotBeUnorderedEqualToExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "c", "b" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new[] { new[] { "c", "b" }, null },
                    new[] { null, new[] { "c", "b" } },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                    new[] { "b", "c", "a" },
                    new[] { "b", "c", "A" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "c", "b", "a", "a" }, new[] { "c", "b", "a" } },
                    new[] { new[] { "c", "b", "a", "a" }, new[] { "A", "B", "C" } },
                    new[] { new[] { "c", "b", "a", "a" }, new[] { "b", "c", "a" } },
                    new[] { new[] { "c", "b", "a", "a" }, new[] { "b", "c", "A" }, },
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
                ExceptionMessageSuffix = Verifications.NotBeUnorderedEqualToExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    new[] { "a", "b", "C" },
                    new[] { "a", "b" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { new[] { "a", "b", "C" }, new[] { "a", "b" } },
                },
                MustFailingValues = new[]
                {
                    (IEnumerable<string>)null,
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "a", "b", "c" }, null },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        public static void NotBeUnorderedEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int[] subject1 = null;
            int[] comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { 0, 1 };
            var comparisonValue2 = new[] { 1, 0 };
            var expected2 = "Provided value (name: 'subject2') is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject3 = new[] { "a", "b", "c" };
            var comparisonValue3 = new[] { "a", "B", "c" };
            var expected3 = "Provided value (name: 'subject3') is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject4 = new int[][] { null };
            int[] comparisonValue4 = null;
            var expected4 = "Provided value (name: 'subject4') contains an element that is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is <null>.  Specified 'elementComparer' is <null>.";

            var subject5 = new[] { new[] { 0 }, new[] { 1, 2 } };
            var comparisonValue5 = new[] { 1, 2 };
            var expected5 = "Provided value (name: 'subject5') contains an element that is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject6 = new[] { new[] { "a", "b", "c" }, new[] { "A", "B" } };
            var comparisonValue6 = new[] { "a", "B" };
            var expected6 = "Provided value (name: 'subject6') contains an element that is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeUnorderedEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeUnorderedEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeUnorderedEqualTo(comparisonValue3, StringComparer.OrdinalIgnoreCase));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeUnorderedEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().NotBeUnorderedEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().NotBeUnorderedEqualTo(comparisonValue6, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeUnorderedEqualToWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_subject_type_is_not_assignable_to_comparisonValue_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => 5.AsArg("myArg").Must().NotBeUnorderedEqualToWhenNotNull(new[] { 1, 2, 3 }));
            var actual2 = Record.Exception(() => new[] { 5 }.AsArg("myArg").Must().Each().NotBeUnorderedEqualToWhenNotNull(new[] { 1, 2, 3 }));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().StartWith("Called NotBeUnorderedEqualToWhenNotNull() on a value of type int, which is not assignable to one of the following type(s): IEnumerable<int>.");

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().StartWith("Called NotBeUnorderedEqualToWhenNotNull() on a value of type IEnumerable<int>, which is not assignable to one of the following type(s): IEnumerable<IEnumerable<int>>.");
        }

        [Fact]
        public static void NotBeUnorderedEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IEnumerable<T> comparisonValue, IEqualityComparer<T> elementComparer)
            {
                return (subject, because, applyBecause, data) => subject.NotBeUnorderedEqualToWhenNotNull(comparisonValue, elementComparer, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeUnorderedEqualToWhenNotNull);

            var comparisonValue1 = new[] { "a", "b", "c" };

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue1, null),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeUnorderedEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "a", "b", "C" },
                    new[] { "c", "b", "a", "a" },
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
                    new[] { "a", "c", "b" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "a", "b", "C" }, comparisonValue1 },
                    new[] { new[] { "a", "b", "C" }, new[] { "a", "c", "b" } },
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
                ExceptionMessageSuffix = Verifications.NotBeUnorderedEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "c", "b" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new[] { new[] { "c", "b" }, null },
                    new[] { null, new[] { "c", "b" } },
                },
                MustFailingValues = new[]
                {
                    comparisonValue2,
                    new[] { "a", "b", "C" },
                    new[] { "A", "B", "C" },
                    new[] { "c", "b", "a" },
                    new[] { "c", "b", "A" },
                },
                MustEachFailingValues = new[]
                {
                    new[] { new[] { "c", "b" }, comparisonValue2 },
                    new[] { new[] { "c", "b" }, new[] { "a", "b", "C" } },
                    new[] { new[] { "c", "b" }, new[] { "A", "B", "C" } },
                    new[] { new[] { "c", "b" }, new[] { "c", "b", "a" } },
                    new[] { new[] { "c", "b" }, new[] { "c", "b", "A" } },
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
                ExceptionMessageSuffix = Verifications.NotBeUnorderedEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<IEnumerable<string>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new[] { "a", "b", "C" },
                    new[] { "a", "b", "c", "c" },
                },
                MustEachPassingValues = new[]
                {
                    new IEnumerable<string>[] { },
                    new IEnumerable<string>[] { null },
                    new IEnumerable<string>[] { new[] { "a", "b", "C" }, new[] { "a", "b", "C" } },
                    new IEnumerable<string>[] { new[] { "a", "b", "C" }, new[] { "a", "b", "c", "c" }, },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        public static void NotBeUnorderedEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new[] { 0, 1 };
            var comparisonValue1 = new[] { 1, 0 };
            var expected1 = "Provided value (name: 'subject1') is not null and is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject2 = new[] { "a", "b", "c" };
            var comparisonValue2 = new[] { "a", "B", "c" };
            var expected2 = "Provided value (name: 'subject2') is not null and is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            var subject3 = new[] { new[] { 0 }, new[] { 1, 2 } };
            var comparisonValue3 = new[] { 2, 1 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: int.  Specified 'comparisonValue' is 'System.Int32[]'.  Specified 'elementComparer' is <null>.";

            var subject4 = new[] { new[] { "a", "b", "c" }, new[] { "A", "B" } };
            var comparisonValue4 = new[] { "a", "B" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is unordered equal to the comparison value using EqualityExtensions.IsUnorderedEqualTo<TElement>, where TElement: string.  Specified 'comparisonValue' is 'System.String[]'.  Specified 'elementComparer' is 'System.OrdinalComparer'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeUnorderedEqualToWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeUnorderedEqualToWhenNotNull(comparisonValue2, StringComparer.OrdinalIgnoreCase));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeUnorderedEqualToWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeUnorderedEqualToWhenNotNull(comparisonValue4, StringComparer.OrdinalIgnoreCase));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}