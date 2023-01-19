// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Equality.cs" company="OBeautifulCode">
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

    public static partial class VerificationsTest
    {
        [Fact]
        public static void BeEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeEqualTo);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue3, comparisonValue3 },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue3, comparisonValue3 - .0000001m, comparisonValue3 },
                    new decimal[] { comparisonValue3, comparisonValue3 + .0000001m, comparisonValue3 },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { "4", "5", null }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { "4", "5", null }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        comparisonValue4,
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void BeEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is <null>.  Specified 'comparisonValue' is '10'.";

            int? subject2 = 10;
            int? comparisonValue2 = null;
            var expected2 = "Provided value (name: 'subject2') is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject3 = 10;
            int comparisonValue3 = 20;
            var expected3 = "Provided value (name: 'subject3') is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject4 = new int?[] { null };
            int? comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is <null>.  Specified 'comparisonValue' is '10'.";

            var subject5 = new int?[] { 10 };
            int? comparisonValue5 = null;
            var expected5 = "Provided value (name: 'subject5') contains an element that is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject6 = new int[] { 10 };
            int comparisonValue6 = 20;
            var expected6 = "Provided value (name: 'subject6') contains an element that is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeEqualTo(comparisonValue2));
            var actual3 = Record.Exception(() => new { subject3 }.Must().BeEqualTo(comparisonValue3));

            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeEqualTo(comparisonValue4));
            var actual5 = Record.Exception(() => new { subject5 }.Must().Each().BeEqualTo(comparisonValue5));
            var actual6 = Record.Exception(() => new { subject6 }.Must().Each().BeEqualTo(comparisonValue6));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
            actual3.Message.Should().Be(expected3);

            actual4.Message.Should().Be(expected4);
            actual5.Message.Should().Be(expected5);
            actual6.Message.Should().Be(expected6);
        }

        [Fact]
        public static void NotBeEqualTo___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeEqualTo(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeEqualTo);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue3 - .0000001m, comparisonValue3 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue3 - .0000001m, comparisonValue3, comparisonValue3 + .0000001m },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "ghi",
                                new List<string> { "4", null, "5", }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", "5" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { null, "5", "4" }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { null, "5", "4" }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void NotBeEqualTo___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = null;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'comparisonValue' is <null>.";

            int subject2 = 10;
            int comparisonValue2 = 10;
            var expected2 = "Provided value (name: 'subject2') is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            var subject3 = new int?[] { null };
            int? comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Specified 'comparisonValue' is <null>.";

            var subject4 = new int[] { 10 };
            int comparisonValue4 = 10;
            var expected4 = "Provided value (name: 'subject4') contains an element that is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEqualTo(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().NotBeEqualTo(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().NotBeEqualTo(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().NotBeEqualTo(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void BeEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.BeEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.BeEqualToWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue3, comparisonValue3 },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                    comparisonValue3 + .0000001m,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue3, comparisonValue3 - .0000001m, comparisonValue3 },
                    new decimal[] { comparisonValue3, comparisonValue3 + .0000001m, comparisonValue3 },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { "4", "5", null }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                    },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { "4", "5", null }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        comparisonValue4,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        comparisonValue4,
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void BeEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int? subject1 = 10;
            int? comparisonValue1 = null;
            var expected1 = "Provided value (name: 'subject1') is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Provided value is '10'.  Specified 'comparisonValue' is <null>.";

            int subject2 = 10;
            int comparisonValue2 = 20;
            var expected2 = "Provided value (name: 'subject2') is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Provided value is '10'.  Specified 'comparisonValue' is '20'.";

            var subject3 = new int?[] { 10 };
            int? comparisonValue3 = null;
            var expected3 = "Provided value (name: 'subject3') contains an element that is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int?.  Element value is '10'.  Specified 'comparisonValue' is <null>.";

            var subject4 = new int[] { 10 };
            int comparisonValue4 = 20;
            var expected4 = "Provided value (name: 'subject4') contains an element that is not null and is not equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Element value is '10'.  Specified 'comparisonValue' is '20'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeEqualToWhenNotNull(comparisonValue1));
            var actual2 = Record.Exception(() => new { subject2 }.Must().BeEqualToWhenNotNull(comparisonValue2));

            var actual3 = Record.Exception(() => new { subject3 }.Must().Each().BeEqualToWhenNotNull(comparisonValue3));
            var actual4 = Record.Exception(() => new { subject4 }.Must().Each().BeEqualToWhenNotNull(comparisonValue4));

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);

            actual3.Message.Should().Be(expected3);
            actual4.Message.Should().Be(expected4);
        }

        [Fact]
        public static void NotBeEqualToWhenNotNull___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange, Act, Assert
            VerificationHandler GetVerificationHandler<T>(T comparisonValue)
            {
                return (subject, because, applyBecause, data) => subject.NotBeEqualToWhenNotNull(comparisonValue, because, applyBecause, data);
            }

            var verificationName = nameof(Verifications.NotBeEqualToWhenNotNull);

            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<decimal>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "string",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(A.Dummy<int>()),
                VerificationName = verificationName,
                VerificationParameterInvalidTypeExpectedTypes = "decimal",
                VerificationParameterInvalidTypeName = "comparisonValue",
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

            var comparisonValue3 = A.Dummy<decimal>();
            var verificationTest3 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue3),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToWhenNotNullExceptionMessageSuffix,
            };

            var decimalTestValues3 = new TestValues<decimal>
            {
                MustPassingValues = new[]
                {
                    comparisonValue3 - .0000001m,
                    comparisonValue3 + .0000001m,
                },
                MustEachPassingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { },
                    new decimal[] { comparisonValue3 - .0000001m, comparisonValue3 + .0000001m },
                },
                MustFailingValues = new[]
                {
                    comparisonValue3,
                },
                MustEachFailingValues = new IEnumerable<decimal>[]
                {
                    new decimal[] { comparisonValue3 - .0000001m, comparisonValue3, comparisonValue3 + .0000001m },
                },
            };

            verificationTest3.Run(decimalTestValues3);

            // dictionary that contains a collection
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> comparisonValue4 =
                new Dictionary<string, IReadOnlyCollection<string>>
                {
                    {
                        "abc",
                        new List<string> { "1", "2", "3" }
                    },
                    {
                        "def",
                        new List<string> { "4", null, "5" }
                    },
                };

            var verificationTest4 = new VerificationTest
            {
                VerificationHandler = GetVerificationHandler(comparisonValue4),
                VerificationName = verificationName,
                ArgumentExceptionType = typeof(ArgumentOutOfRangeException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeEqualToWhenNotNullExceptionMessageSuffix,
            };

            var testValues4 = new TestValues<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
            {
                MustPassingValues = new[]
                {
                    null,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "abc",
                            new List<string> { "1", "2", "3" }
                        },
                        {
                            "def",
                            new List<string> { "4", null, "5", "6" }
                        },
                    },
                },
                MustEachPassingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[] { },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                    },
                    new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                    {
                        null,
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "ghi",
                                new List<string> { "4", null, "5", }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", "5" }
                            },
                        },
                    },
                },
                MustFailingValues = new IReadOnlyDictionary<string, IReadOnlyCollection<string>>[]
                {
                    comparisonValue4,
                    new Dictionary<string, IReadOnlyCollection<string>>
                    {
                        {
                            "def",
                            new List<string> { null, "5", "4" }
                        },
                        {
                            "abc",
                            new List<string> { "2", "3", "1" }
                        },
                    },
                },
                MustEachFailingValues = new IEnumerable<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>[]
                {
                    new[]
                    {
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "def",
                                new List<string> { null, "5", "4" }
                            },
                            {
                                "abc",
                                new List<string> { "2", "3", "1" }
                            },
                        },
                        new Dictionary<string, IReadOnlyCollection<string>>
                        {
                            {
                                "abc",
                                new List<string> { "1", "2", "3" }
                            },
                            {
                                "def",
                                new List<string> { "4", null, "5", "6" }
                            },
                        },
                    },
                },
            };

            verificationTest4.Run(testValues4);
        }

        [Fact]
        public static void NotBeEqualToWhenNotNull___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            int subject1 = 10;
            int comparisonValue1 = 10;
            var expected1 = "Provided value (name: 'subject1') is not null and is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            var subject2 = new int[] { 10 };
            int comparisonValue2 = 10;
            var expected2 = "Provided value (name: 'subject2') contains an element that is not null and is equal to the comparison value using EqualityExtensions.IsEqualTo<T>, where T: int.  Specified 'comparisonValue' is '10'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeEqualToWhenNotNull(comparisonValue1));

            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeEqualToWhenNotNull(comparisonValue2));

            // Assert
            actual1.Message.Should().Be(expected1);

            actual2.Message.Should().Be(expected2);
        }
    }
}