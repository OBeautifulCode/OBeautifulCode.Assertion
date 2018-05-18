// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using System;

    using Xunit;

    public static class ValidationsTest
    {
        [Fact(Skip = "true")]
        public static void Test4()
        {
            // Be, NotBe (func<bool>)
            // BeNull, NotBeNull
            // BeFalse, NotBeFalse, BeTrue, NotBeTrue
            // NotBeEmpty, BeEmpty (string, guid, enumerable)
            // NotBeWhiteSpace, BeWhiteSpace
            // NotBeNullOrWhiteSpace, BeNullOrWhiteSpace
            // BeDefault, NotBeDefault
            // ContainSomeNulls, NotContainAnyNulls
            // Contain, NotContain  (string, IEnumerable)
            // BeInRange(min,max),  NotBeInRange(min,max)
            // BeLessThan, NotBeLessThan
            // BeLessThanOrEqualTo, NotBeLessThanOrEqualTo
            // BeGreaterThan, NotBeGreaterThan
            // BeGreaterThanOrEqualTo, NotBeGreaterThanOrEqualTo
            // NotBeEqualTo, BeEqualTo
        }

        private static void Test(Guid guidValue)
        {
            guidValue.Must();
            guidValue.Named("namedGuidValue").Must();
            new { guidValue }.Must();
        }

        private static void Test(Guid? nullableGuidValue)
        {
        }

        private static void Test(DateTime dateTimeValue)
        {
        }

        private static void Test(DateTime? nullableDateTimeValue)
        {
        }

        private static void Test(decimal decimalValue)
        {
        }

        private static void Test(decimal? nullableDecimalValue)
        {
        }

        private static void Test(string stringValue)
        {
        }

        private static void Test(TestObject testObjectValue)
        {
        }

        private class TestObject
        {
        }
    }
}