// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class ValidatorTest
    {
        [Fact]
        public static void And___Should_return_null___When_parameter_is_null()
        {
            // Arrange, Act
            var actual = ParameterValidator.And(null);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public static void And___Should_return_parameter___When_called()
        {
            // Arrange
            var expected = A.Dummy<Parameter>();

            // Act
            var actual = expected.And();

            // Assert
            actual.Should().BeSameAs(expected);
        }
    }
}
