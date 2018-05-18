// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.ParameterValidation.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public static class ValidationsTest
    {
        [Fact]
        public static void Test()
        {
            string value = " \r\n ";
            value.Named("myName").Must().NotBeNull().And().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public static void Test2()
        {
            string value = " \r\n ";
            value.Must().NotBeNull().And().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public static void Test3()
        {
            string something = " \r\n ";
            new { something }.Must().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public static void Test4()
        {
            var something = new[] { "asdf", null };

            new { something }.Must().NotBeNull().And().NotBeEmpty().And().NotContainAnyNulls();
            new { something }.Must().NotBeEmpty().And().NotContainAnyNulls();

            something.Named("asdf").Must().NotBeNull();

            var x = new Dictionary<object, object>();
            x.TryGetValue("asf", out object output).Named("somename").Must().BeTrue();

            // test the following:
            // Guid, Guid? DateTime, DateTime?, string, decimal, decimal?, custom object

            // all value types (including struct) should throw the InvalidCastException
            Guid.NewGuid().Must().BeNull();

            // do a reflection unit test to the static methods and Validation methods

            // support this with strong types:
            // don't support multiple validations
            Must.NotBeNull(new { something }, "something", "because");

            // test with Struct
            // test nullables
            // BeDefault()
            // NotBeEmptyEnumerable => NotBeEmpty
            // NotBeEmptyString
            // NotBeNullOrWhiteSpace
            // NotContainAnyNullElements => NotContainAnyNulls

            (x.IsValidMetricTree).Named("").new {somethingElse = something}).Named("somethingElse").Must();

            // write test case on this - for string not null or whitespace, put null check after we validate that it's a string or before?
        }
    }
