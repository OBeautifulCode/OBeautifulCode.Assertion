// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.ElementInSet.cs" company="OBeautifulCode">
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
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void BeElementIn___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeElementIn<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeElementIn(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void BeElementIn___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.BeElementIn(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeElementIn);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty comparisonValues always fails
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeElementInExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustFailingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<decimal>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeElementInExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachPassingValues = new[]
                {
                    new decimal[0],
                    new[] { comparisonValue4.First(), comparisonValue4.Last() },
                },
                MustFailingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4.First(), A.Dummy<decimal>() },
                    new[] { A.Dummy<decimal>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void BeElementIn___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            var comparisonValues1 = new int?[] { 10 };
            var expected1 = "Provided value (name: 'subject1') is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is <null>.  Specified 'comparisonValues' is ['10'].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected2 = "Provided value (name: 'subject2') is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            var subject3 = new int?[] { null };
            var comparisonValues3 = new int?[] { 10 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is <null>.  Specified 'comparisonValues' is ['10'].";

            var subject4 = new[] { 30, 130 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '130'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeElementIn(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeElementIn(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeElementIn(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeElementIn(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeElementIn___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeElementIn<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeElementIn(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotBeElementIn___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.NotBeElementIn(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeElementIn);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty collection always succeeds
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeElementInExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<decimal>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeElementInExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new[]
                {
                    new decimal[0],
                    new[] { A.Dummy<decimal>(), A.Dummy<decimal>() },
                },
                MustFailingValues = new[]
                {
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new[] { comparisonValue4.First() },
                    new[] { A.Dummy<decimal>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void NotBeElementIn___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            var comparisonValues1 = new int?[] { 5, null };
            var expected1 = "Provided value (name: 'subject1') is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is <null>.  Specified 'comparisonValues' is ['5', <null>].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 10, 120 };
            var expected2 = "Provided value (name: 'subject2') is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '10', ...].";

            var subject3 = new int?[] { 20, null };
            var comparisonValues3 = new int?[] { 10, null };
            var expected3 = "Provided value (name: 'subject3') contains an element that is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is <null>.  Specified 'comparisonValues' is ['10', <null>].";

            var subject4 = new[] { 130, 30 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '30'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeElementIn(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeElementIn(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeElementIn(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeElementIn(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeElementInWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeElementInWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeElementInWhenNotNull(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void BeElementInWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.BeElementInWhenNotNull(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeElementInWhenNotNull);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty comparisonValues always fails
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeElementInWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustFailingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<string>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeElementInWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<string>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachPassingValues = new[]
                {
                    new string[0],
                    new[] { comparisonValue4.First(), null, comparisonValue4.Last() },
                },
                MustFailingValues = new[]
                {
                    A.Dummy<string>(),
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new[] { comparisonValue4.First(), A.Dummy<string>() },
                    new[] { A.Dummy<string>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void BeElementInWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 5;
            var comparisonValues1 = new int?[] { 10 };
            var expected1 = "Provided value (name: 'subject1') is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '5'.  Specified 'comparisonValues' is ['10'].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected2 = "Provided value (name: 'subject2') is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            var subject3 = new int?[] { null, 8 };
            var comparisonValues3 = new int?[] { 10 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '8'.  Specified 'comparisonValues' is ['10'].";

            var subject4 = new[] { 30, 130 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not equal to any of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '130'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeElementInWhenNotNull(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeElementInWhenNotNull(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeElementInWhenNotNull(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeElementInWhenNotNull(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeElementInWhenNotNull___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_comparisonValues_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeElementInWhenNotNull<string>(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeElementInWhenNotNull(comparisonValues:) where parameter 'comparisonValues' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        public static void NotBeElementInWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(IReadOnlyCollection<T> comparisonValues)
            {
                return (subject, because, applyBecause, data) => subject.NotBeElementInWhenNotNull(comparisonValues, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeElementInWhenNotNull);

            // comparisonValues is wrong type
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<decimal>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<string>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    null,
                    string.Empty,
                    A.Dummy<string>(),
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<string>[]
                {
                    new string[] { },
                    new string[] { null },
                    new string[] { A.Dummy<string>() },
                },
            };

            verificationTest1.Run(stringTestValues1);

            // comparisonValues is wrong type
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<IReadOnlyCollection<int>>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "IReadOnlyCollection<decimal>",
                VerificationParameterInvalidTypeName = "comparisonValues",
            };

            var decimalTestValues2 = new TestValues<decimal>
            {
                MustVerificationParameterInvalidTypeValues = new[]
                {
                    A.Dummy<decimal>(),
                    decimal.MaxValue,
                },
                MustEachVerificationParameterInvalidTypeValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { A.Dummy<decimal>() },
                },
            };

            verificationTest2.Run(decimalTestValues2);

            // empty collection always succeeds
            var comparisonValue3 = new decimal[0];
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeElementInWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    A.Dummy<decimal>(),
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    Some.ReadOnlyDummies<decimal>(),
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // various passing and failing cases
            var comparisonValue4 = Some.ReadOnlyDummies<string>(5);
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeElementInWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues4 = new TestValues<string>
            {
                MustPassingValues = new[]
                {
                    null,
                    A.Dummy<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new string[0],
                    new[] { A.Dummy<string>(), null, A.Dummy<string>() },
                },
                MustFailingValues = new[]
                {
                    comparisonValue4.First(),
                    comparisonValue4.Last(),
                },
                MustEachFailingValues = new IEnumerable<string>[]
                {
                    new[] { comparisonValue4.First() },
                    new[] { A.Dummy<string>(), comparisonValue4.Last() },
                },
            };

            verificationTest4.Run(decimalTestValues4);
        }

        [Fact]
        public static void NotBeElementInWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 5;
            var comparisonValues1 = new int?[] { null, 5 };
            var expected1 = "Provided value (name: 'subject1') is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '5'.  Specified 'comparisonValues' is [<null>, '5'].";

            var subject2 = 10;
            var comparisonValues2 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 10, 120 };
            var expected2 = "Provided value (name: 'subject2') is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '10', ...].";

            var subject3 = new int?[] { null, 10 };
            var comparisonValues3 = new int?[] { null, 10 };
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '10'.  Specified 'comparisonValues' is [<null>, '10'].";

            var subject4 = new[] { 130, 30 };
            var comparisonValues4 = new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is equal to one or more of the comparison values using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '30'.  Specified 'comparisonValues' is ['20', '30', '40', '50', '60', '70', '80', '90', '100', '110', ...].";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeElementInWhenNotNull(comparisonValues1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeElementInWhenNotNull(comparisonValues2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeElementInWhenNotNull(comparisonValues3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeElementInWhenNotNull(comparisonValues4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}