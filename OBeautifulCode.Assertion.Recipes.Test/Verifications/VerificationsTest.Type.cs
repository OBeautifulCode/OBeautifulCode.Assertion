// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Type.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using Xunit;

    using static System.FormattableString;

    public static partial class VerificationsTest
    {
        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_TExpected___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.BeOfType<string>,
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
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

            verificationTest1.Run(testValues1);

            // the failing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.BeOfType<ArgumentException>,
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the failing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.BeOfType<ArgumentOutOfRangeException>,
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_TExpected___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not of the expected type.  The type of the provided value is 'ArgumentException'.  Specified 'TExpected' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not of the expected type.  The type of the element is 'ArgumentException'.  Specified 'TExpected' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeOfType<ArgumentOutOfRangeException>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeOfType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_expectedType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().BeOfType(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called BeOfType(expectedType:) where parameter 'expectedType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_expectedType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type expectedType)
            {
                return (subject, because, applyBecause, data) => subject.BeOfType(expectedType, because: because, applyBecause: applyBecause, data: data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(string)),
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
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

            verificationTest1.Run(testValues1);

            // the failing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException)),
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the failing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentOutOfRangeException)),
                VerificationName = nameof(Verifications.BeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeOfType_expectedType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not of the expected type.  The type of the provided value is 'ArgumentException'.  Specified 'expectedType' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not of the expected type.  The type of the element is 'ArgumentException'.  Specified 'expectedType' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeOfType(typeof(ArgumentOutOfRangeException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeOfType(typeof(ArgumentOutOfRangeException)));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_TUnexpected___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeOfType<int?>,
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
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

            verificationTest1.Run(testValues1);

            // the passing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeOfType<ArgumentException>,
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new InvalidOperationException(),
                    new ArgumentOutOfRangeException(string.Empty),
                    new ArgumentNullException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new ArgumentNullException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(), new ArgumentException(string.Empty), new InvalidOperationException() },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentNullException(string.Empty), new ArgumentException(string.Empty), new ArgumentNullException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the passing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeOfType<ArgumentOutOfRangeException>,
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new Exception(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_TUnexpected___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentOutOfRangeException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is of the unexpected type.  Specified 'TUnexpected' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is of the unexpected type.  Specified 'TUnexpected' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeOfType<ArgumentOutOfRangeException>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeOfType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_unexpectedType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_unexpectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();
            var actual = Record.Exception(() => new { subject }.Must().NotBeOfType(null));

            // Assert
            actual.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual.Message.Should().Be("Called NotBeOfType(unexpectedType:) where parameter 'unexpectedType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_unexpectedType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type unexpectedType)
            {
                return (subject, because, applyBecause, data) => subject.NotBeOfType(unexpectedType, because: because, applyBecause: applyBecause, data: data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(int?)),
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
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

            verificationTest1.Run(testValues1);

            // the passing values are of a different type or more derived
            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException)),
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new InvalidOperationException(),
                    new ArgumentOutOfRangeException(string.Empty),
                    new ArgumentNullException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new ArgumentNullException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(), new ArgumentException(string.Empty), new InvalidOperationException() },
                    new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                    new Exception[] { new ArgumentNullException(string.Empty), new ArgumentException(string.Empty), new ArgumentNullException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // the passing values are less derived
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentOutOfRangeException)),
                VerificationName = nameof(Verifications.NotBeOfType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeOfTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new Exception(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new Exception(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) },
                    new Exception[] { new Exception(string.Empty), new ArgumentOutOfRangeException(string.Empty), new Exception(string.Empty) },
                },
            };

            verificationTest3.Run(testValues3);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeOfType_unexpectedType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentOutOfRangeException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is of the unexpected type.  Specified 'unexpectedType' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new InvalidOperationException(), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException() };
            var expected2 = "Provided value (name: 'subject2') contains an element that is of the unexpected type.  Specified 'unexpectedType' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeOfType(typeof(ArgumentOutOfRangeException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeOfType(typeof(ArgumentOutOfRangeException)));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_TAssignable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler<T>()
            {
                return (subject, because, applyBecause, data) => subject.BeAssignableToType<T>(because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<IEnumerable>(),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
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

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<ArgumentException>(),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new Exception(), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_TAssignable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'TAssignable' is 'ArgumentOutOfRangeException'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not assignable to the specified type.  The type of the element is 'ArgumentException'.  Specified 'TAssignable' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAssignableToType<ArgumentOutOfRangeException>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeAssignableToType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_assignableType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();

            var actual1 = Record.Exception(() => new { subject }.Must().BeAssignableToType(null, treatUnboundGenericAsAssignableTo: false));
            var actual2 = Record.Exception(() => new { subject }.Must().BeAssignableToType(null, treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be("Called BeAssignableToType(assignableType:) where parameter 'assignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().Be("Called BeAssignableToType(assignableType:) where parameter 'assignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_assignableType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type assignableType, bool treatUnboundGenericAsAssignableTo)
            {
                return (subject, because, applyBecause, data) => subject.BeAssignableToType(assignableType, treatUnboundGenericAsAssignableTo, because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<string>
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

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<Exception>
            {
                MustPassingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new Exception[] { },
                    new Exception[] { new ArgumentException(string.Empty), new ArgumentNullException(string.Empty), new ArgumentOutOfRangeException(string.Empty) },
                },
                MustFailingValues = new Exception[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new ArgumentException(string.Empty), new Exception(), new ArgumentException(string.Empty) },
                    new Exception[] { new ArgumentException(string.Empty), new InvalidOperationException(), new ArgumentException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // do not treat unbound generic as assignable to
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<object>
            {
                MustFailingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new object[] { "test" },
                    new object[] { new List<string>() },
                },
            };

            verificationTest3.Run(testValues3);

            // treat unbound generic as assignable to
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true),
                VerificationName = nameof(Verifications.BeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { "test", new List<string>() },
                },
                MustFailingValues = new object[]
                {
                    new Exception(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new object[] { "test", new Exception(string.Empty), "test" },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void BeAssignableToType_assignableType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is not assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'assignableType' is 'ArgumentOutOfRangeException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.");

            var subject2 = new Exception[] { new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not assignable to the specified type.  The type of the element is 'ArgumentException'.  Specified 'assignableType' is 'ArgumentOutOfRangeException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.";

            var subject3 = new ArgumentException(string.Empty);
            var expected3 = Invariant($"Provided value (name: 'subject3') is not assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'assignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.");

            var subject4 = new object[] { "test", new ArgumentException(string.Empty), "test" };
            var expected4 = "Provided value (name: 'subject4') contains an element that is not assignable to the specified type.  The type of the element is 'ArgumentException'.  Specified 'assignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeAssignableToType(typeof(ArgumentOutOfRangeException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeAssignableToType(typeof(ArgumentOutOfRangeException)));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_TAssignable___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler<T>()
            {
                return (subject, because, applyBecause, data) => subject.NotBeAssignableToType<T>(because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<IEnumerable>(),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<object>
            {
                MustFailingValues = new object[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new object[] { new Exception(), null, new Exception() },
                },
            };

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler<ArgumentException>(),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                    "testing",
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { new InvalidOperationException(), "testing", new Exception() },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentNullException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_TAssignable___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentException(string.Empty);
            var expected1 = "Provided value (name: 'subject1') is assignable to the specified type.  The type of the provided value is 'ArgumentException'.  Specified 'TUnassignable' is 'Exception'.";

            var subject2 = new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is assignable to the specified type.  The type of the element is 'ArgumentOutOfRangeException'.  Specified 'TUnassignable' is 'ArgumentOutOfRangeException'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeAssignableToType<Exception>());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeAssignableToType<ArgumentOutOfRangeException>());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_assignableType___Should_throw_ImproperUseOfAssertionFrameworkException___When_parameter_expectedType_is_null()
        {
            // Arrange, Act
            var subject = A.Dummy<string>();

            var actual1 = Record.Exception(() => new { subject }.Must().NotBeAssignableToType(null, treatUnboundGenericAsAssignableTo: false));
            var actual2 = Record.Exception(() => new { subject }.Must().NotBeAssignableToType(null, treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual1.Message.Should().Be("Called NotBeAssignableToType(unassignableType:) where parameter 'unassignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);

            actual2.Should().BeOfType<ImproperUseOfAssertionFrameworkException>();
            actual2.Message.Should().Be("Called NotBeAssignableToType(unassignableType:) where parameter 'unassignableType' is null.  " + Verifications.ImproperUseOfFrameworkErrorMessage);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_assignableType___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            VerificationHandler GetVerificationHandler(Type assignableType, bool treatUnboundGenericAsAssignableTo)
            {
                return (subject, because, applyBecause, data) => subject.NotBeAssignableToType(assignableType, treatUnboundGenericAsAssignableTo, because, applyBecause, data);
            }

            // null subjects
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
            };

            var testValues1 = new TestValues<object>
            {
                MustFailingValues = new object[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new object[] { new Exception(), null, new Exception() },
                },
            };

            verificationTest1.Run(testValues1);

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(ArgumentException), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues2 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    new Exception(),
                    new InvalidOperationException(),
                    "testing",
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { new InvalidOperationException(), "testing", new Exception() },
                },
                MustFailingValues = new Exception[]
                {
                    new ArgumentException(string.Empty),
                    new ArgumentNullException(string.Empty),
                    new ArgumentOutOfRangeException(string.Empty),
                },
                MustEachFailingValues = new[]
                {
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentNullException(string.Empty), new InvalidOperationException(string.Empty) },
                    new Exception[] { new InvalidOperationException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new InvalidOperationException(string.Empty) },
                },
            };

            verificationTest2.Run(testValues2);

            // do not treat unbound generic as assignable to
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: false),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues3 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachPassingValues = new[]
                {
                    new object[] { "test", new List<string>() },
                },
            };

            verificationTest3.Run(testValues3);

            // treat unbound generic as assignable to
            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true),
                VerificationName = nameof(Verifications.NotBeAssignableToType),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeAssignableToTypeExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<object>
            {
                MustPassingValues = new object[]
                {
                    new object(),
                    new Exception(string.Empty),
                },
                MustEachPassingValues = new[]
                {
                    new object[] { },
                    new object[] { new object(), new Exception(string.Empty) },
                },
                MustFailingValues = new object[]
                {
                    "test",
                    new List<string>(),
                },
                MustEachFailingValues = new[]
                {
                    new object[] { new object(), "test", new object() },
                    new object[] { new object(), new List<string>(), new object() },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void NotBeAssignableToType_assignableType___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = new ArgumentOutOfRangeException(string.Empty);
            var expected1 = Invariant($"Provided value (name: 'subject1') is assignable to the specified type.  The type of the provided value is 'ArgumentOutOfRangeException'.  Specified 'unassignableType' is 'ArgumentException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.");

            var subject2 = new Exception[] { new ArgumentException(string.Empty), new ArgumentOutOfRangeException(string.Empty), new ArgumentException(string.Empty) };
            var expected2 = "Provided value (name: 'subject2') contains an element that is assignable to the specified type.  The type of the element is 'ArgumentOutOfRangeException'.  Specified 'unassignableType' is 'ArgumentOutOfRangeException'.  Specified 'treatUnboundGenericAsAssignableTo' is 'False'.";

            var subject3 = "testing";
            var expected3 = Invariant($"Provided value (name: 'subject3') is assignable to the specified type.  The type of the provided value is 'string'.  Specified 'unassignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.");

            var subject4 = new object[] { new ArgumentException(string.Empty), "testing", new ArgumentException(string.Empty) };
            var expected4 = "Provided value (name: 'subject4') contains an element that is assignable to the specified type.  The type of the element is 'string'.  Specified 'unassignableType' is 'IEnumerable<T>'.  Specified 'treatUnboundGenericAsAssignableTo' is 'True'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeAssignableToType(typeof(ArgumentException)));
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeAssignableToType(typeof(ArgumentOutOfRangeException)));
            var actual3 = Record.Exception(() => new { subject3 }.Must().NotBeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeAssignableToType(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }
    }
}