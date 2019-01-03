// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class DictionaryExtensionsTest
    {
        [Fact]
        public static void ToNonGenericDictionary___Should_throw_ArgumentNullException___When_parameter_value_is_null()
        {
            // Arrange
            IDictionary<string, string> value = null;

            // Act
            var actual = Record.Exception(() => DictionaryExtensions.ToNonGenericDictionary(value));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("value");
        }

        [Fact]
        public static void ToNonGenericDictionary___Should_throw_ArgumentException___When_parameter_value_is_contains_duplicate_keys()
        {
            // Arrange
            var value = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("duplicate", A.Dummy<string>()),
                new KeyValuePair<string, string>(A.Dummy<string>(), A.Dummy<string>()),
                new KeyValuePair<string, string>("duplicate", A.Dummy<string>()),
            };

            // Act
            var actual = Record.Exception(() => DictionaryExtensions.ToNonGenericDictionary(value));

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("value contains duplicate keys");
        }

        [Fact]
        public static void ToNonGenericDictionary___Should_return_empty_IDictionary___When_parameter_value_is_empty()
        {
            // Arrange
            var value1 = new Dictionary<string, string>();
            var value2 = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
            IDictionary<string, string> value3 = value1;
            IReadOnlyDictionary<string, string> value4 = value2;

            // Act
            var actual1 = DictionaryExtensions.ToNonGenericDictionary(value1);
            var actual2 = DictionaryExtensions.ToNonGenericDictionary(value2);
            var actual3 = DictionaryExtensions.ToNonGenericDictionary(value3);
            var actual4 = DictionaryExtensions.ToNonGenericDictionary(value4);

            // Assert
            actual1.Should().BeEmpty();
            actual2.Should().BeEmpty();
            actual3.Should().BeEmpty();
            actual4.Should().BeEmpty();
        }

        [Fact]
        public static void ToNonGenericDictionary___Should_return_IDictionary_with_key_value_pairs_equivalent_to_parameter_value___When_parameter_value_is_not_empty()
        {
            // Arrange
            var value1 = A.Dummy<Dictionary<string, string>>();
            var value2 = new ReadOnlyDictionary<int, string>(A.Dummy<Dictionary<int, string>>());
            IDictionary<string, string> value3 = value1;
            IReadOnlyDictionary<int, string> value4 = value2;

            // Act
            var actual1 = DictionaryExtensions.ToNonGenericDictionary(value1);
            var actual2 = DictionaryExtensions.ToNonGenericDictionary(value2);
            var actual3 = DictionaryExtensions.ToNonGenericDictionary(value3);
            var actual4 = DictionaryExtensions.ToNonGenericDictionary(value4);

            // Assert
            actual1.Keys.Should().BeEquivalentTo(value1.Keys);
            foreach (var key in actual1.Keys)
            {
                actual1[key].Should().Be(value1[(string)key]);
            }

            actual2.Keys.Should().BeEquivalentTo(value2.Keys);
            foreach (var key in actual2.Keys)
            {
                actual2[key].Should().Be(value2[(int)key]);
            }

            actual3.Keys.Should().BeEquivalentTo(value3.Keys);
            foreach (var key in actual3.Keys)
            {
                actual3[key].Should().Be(value3[(string)key]);
            }

            actual4.Keys.Should().BeEquivalentTo(value4.Keys);
            foreach (var key in actual4.Keys)
            {
                actual4[key].Should().Be(value4[(int)key]);
            }
        }
    }
}
