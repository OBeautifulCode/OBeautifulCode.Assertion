// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationsTest.Email.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static partial class VerificationsTest
    {
        [Fact]
        public static void BeValidEmailAddress___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.BeValidEmailAddress,
                VerificationName = nameof(Verifications.BeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "test@domain.com", null, "test@domain.com" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    "test@domain.com",
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { "test@domain.com" } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    "test@domain.com",
                    A.Dummy<object>(),
                    new List<string>() { "test@domain.com" },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), "test@domain.com", A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.BeValidEmailAddress,
                VerificationName = nameof(Verifications.BeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.BeValidEmailAddressExceptionMessageSuffix,
            };

            // from: https://gist.github.com/cjaoude/fd9910626629b53c4d25
            // and: https://stackoverflow.com/questions/2049502/what-characters-are-allowed-in-an-email-address
            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    // should be valid but our code doesn't allow:
                    // "email@123.123.123.123",
                    // "email@[123.123.123.123]",
                    // "much.\"more\\ unusual\"@example.com",
                    // "very.unusual.\"@\".unusual.com@example.com",
                    // "very.\"(),:;<>[]\".VERY.\"very@\\\\ \"very\".unusual@strange.example.com",
                    // "admin@mailserver1",
                    "email@example.com",
                    "firstname.lastname@example.com",
                    "email@subdomain.example.com",
                    "firstname+lastname@example.com",
                    "\"email\"@example.com",
                    "1234567890@example.com",
                    "email@example-one.com",
                    "_______@example.com",
                    "email@example.name",
                    "email@example.museum",
                    "email@example.co.jp",
                    "firstname-lastname@example.com",
                    "simple@example.com",
                    "very.common@example.com",
                    "disposable.style.email.with+symbol@example.com",
                    "other.email-with-hyphen@example.com",
                    "fully-qualified-domain@example.com",
                    "user.name+tag+sorting@example.com",
                    "x@example.com",
                    "example-indeed@strange-example.com",
                    "example@s.example",
                    "\" \"@example.org",
                    "\"john..doe\"@example.org",
                    "mailhost!username@example.org",
                    "user%example.com@example.org",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { "email@example.com", "firstname.lastname@example.com", "email@subdomain.example.com", "firstname+lastname@example.com", "\"email\"@example.com", "1234567890@example.com", "email@example-one.com", "_______@example.com", "email@example.name", "email@example.museum", "email@example.co.jp", "firstname-lastname@example.com", "simple@example.com", "very.common@example.com", "disposable.style.email.with+symbol@example.com", "other.email-with-hyphen@example.com", "fully-qualified-domain@example.com", "user.name+tag+sorting@example.com", "x@example.com", "example-indeed@strange-example.com", "example@s.example", "\" \"@example.org", "\"john..doe\"@example.org", "mailhost!username@example.org", "user%example.com@example.org" },
                },
                MustFailingValues = new[]
                {
                    // should fail but our code allows:
                    // "email@example.web",
                    // "1234567890123456789012345678901234567890123456789012345678901234+x@example.com",
                    // "i_like_underscore@but_its_not_allow_in_this_part.example.com",
                    string.Empty,
                    " ",
                    "\r\n",
                    "plainaddress",
                    "#@%^%#$@#$@#.com",
                    "@example.com",
                    "Joe Smith <email@example.com>",
                    "email.example.com",
                    "email@example@example.com",
                    ".email@example.com",
                    "email.@example.com",
                    "email..email@example.com",
                    "email@example.com (Joe Smith)",
                    "email@example",
                    "email@-example.com",
                    "email@111.222.333.44444",
                    "email@example..com",
                    "Abc..123@example.com",
                    "\"(),:;<>[\\]@example.com",
                    "just\"not\"right@example.com",
                    "this\\ is\"really\"not\\allowed@example.com",
                    "Abc.example.com",
                    "A@b@c@example.com",
                    "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com",
                    "just\"not\"right@example.com",
                    "this is\"not\\allowed@example.com",
                    "this\\ still\\\"not\\\\allowed@example.com",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "email@example.com", string.Empty, "email@example.com" },
                    new string[] { "email@example.com", " ", "email@example.com" },
                    new string[] { "email@example.com", "\r\n", "email@example.com" },
                    new string[] { "email@example.com", "plainaddress", "email@example.com" },
                    new string[] { "email@example.com", "#@%^%#$@#$@#.com", "email@example.com" },
                    new string[] { "email@example.com", "@example.com", "email@example.com" },
                    new string[] { "email@example.com", "Joe Smith <email@example.com>", "email@example.com" },
                    new string[] { "email@example.com", "email.example.com", "email@example.com" },
                    new string[] { "email@example.com", "email@example@example.com", "email@example.com" },
                    new string[] { "email@example.com", ".email@example.com", "email@example.com" },
                    new string[] { "email@example.com", "email.@example.com", "email@example.com" },
                    new string[] { "email@example.com", "email..email@example.com", "email@example.com" },
                    new string[] { "email@example.com", "email@example.com (Joe Smith)", "email@example.com" },
                    new string[] { "email@example.com", "email@example", "email@example.com" },
                    new string[] { "email@example.com", "email@-example.com", "email@example.com" },
                    new string[] { "email@example.com", "email@111.222.333.44444", "email@example.com" },
                    new string[] { "email@example.com", "email@example..com", "email@example.com" },
                    new string[] { "email@example.com", "Abc..123@example.com", "email@example.com" },
                    new string[] { "email@example.com", "\"(),:;<>[\\]@example.com", "email@example.com" },
                    new string[] { "email@example.com", "just\"not\"right@example.com", "email@example.com" },
                    new string[] { "email@example.com", "this\\ is\"really\"not\\allowed@example.com", "email@example.com" },
                    new string[] { "email@example.com", "Abc.example.com", "email@example.com" },
                    new string[] { "email@example.com", "A@b@c@example.com", "email@example.com" },
                    new string[] { "email@example.com", "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com", "email@example.com" },
                    new string[] { "email@example.com", "just\"not\"right@example.com", "email@example.com" },
                    new string[] { "email@example.com", "this is\"not\\allowed@example.com", "email@example.com" },
                    new string[] { "email@example.com", "this\\ still\\\"not\\\\allowed@example.com", "email@example.com" },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void BeValidEmailAddress___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "abc-def";
            var expected1 = "Provided value (name: 'subject1') is not a valid email address.  Provided value is 'abc-def'.";

            var subject2 = new[] { "test@example.com", "d f", "test@example.com" };
            var expected2 = "Provided value (name: 'subject2') contains an element that is not a valid email address.  Element value is 'd f'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().BeValidEmailAddress());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().BeValidEmailAddress());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }

        [Fact]
        public static void NotBeValidEmailAddress___Should_throw_or_not_throw_as_expected___When_called()
        {
            // Arrange
            var verificationTest1 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeValidEmailAddress,
                VerificationName = nameof(Verifications.NotBeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentNullException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeNullExceptionMessageSuffix,
                SubjectInvalidTypeExpectedTypes = "string",
                SubjectInvalidTypeExpectedEnumerableTypes = "IEnumerable<string>",
            };

            var guidTestValues = new TestValues<Guid>
            {
                MustSubjectInvalidTypeValues = new[]
                {
                    Guid.Empty,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new Guid[] { },
                    new Guid[] { Guid.Empty, Guid.Empty },
                    new Guid[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var nullableGuidTestValues = new TestValues<Guid?>
            {
                MustSubjectInvalidTypeValues = new Guid?[]
                {
                    Guid.Empty,
                    null,
                    Guid.NewGuid(),
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<Guid?>[]
                {
                    new Guid?[] { },
                    new Guid?[] { }, new Guid?[] { Guid.Empty, Guid.Empty },
                    new Guid?[] { Guid.Empty, null, Guid.Empty },
                    new Guid?[] { Guid.Empty, Guid.NewGuid(), Guid.Empty },
                },
            };

            var stringTestValues1 = new TestValues<string>
            {
                MustFailingValues = new string[]
                {
                    null,
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abc-def", null, "abc-def" },
                },
            };

            var enumerableTestValues = new TestValues<IEnumerable>
            {
                MustSubjectInvalidTypeValues = new IEnumerable[]
                {
                    "abc-def",
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new IEnumerable[] { new List<string> { "abc-def" } },
                },
            };

            var objectTestValues = new TestValues<object>
            {
                MustSubjectInvalidTypeValues = new object[]
                {
                    "abc-def",
                    A.Dummy<object>(),
                    new List<string>() { "abc-def" },
                },
                MustEachSubjectInvalidTypeValues = new IEnumerable<object>[]
                {
                    new object[] { },
                    new object[] { A.Dummy<object>(), A.Dummy<object>() },
                    new object[] { A.Dummy<object>(), "abc-def", A.Dummy<object>() },
                },
            };

            var boolTestValues = new TestValues<bool>
            {
                MustSubjectInvalidTypeValues = new bool[]
                {
                    true,
                    false,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool[] { },
                    new bool[] { true },
                },
            };

            var nullableBoolTestValues = new TestValues<bool?>
            {
                MustSubjectInvalidTypeValues = new bool?[]
                {
                    true,
                    false,
                    null,
                },
                MustEachSubjectInvalidTypeValues = new[]
                {
                    new bool?[] { },
                    new bool?[] { true },
                    new bool?[] { null },
                },
            };

            var verificationTest2 = new VerificationTest
            {
                VerificationHandler = Verifications.NotBeValidEmailAddress,
                VerificationName = nameof(Verifications.NotBeValidEmailAddress),
                ArgumentExceptionType = typeof(ArgumentException),
                EachArgumentExceptionType = typeof(ArgumentException),
                ExceptionMessageSuffix = Verifications.NotBeValidEmailAddressExceptionMessageSuffix,
            };

            // from: https://gist.github.com/cjaoude/fd9910626629b53c4d25
            // and: https://stackoverflow.com/questions/2049502/what-characters-are-allowed-in-an-email-address
            var stringTestValues2 = new TestValues<string>
            {
                MustPassingValues = new string[]
                {
                    // should fail but our code allows:
                    // "email@example.web",
                    // "1234567890123456789012345678901234567890123456789012345678901234+x@example.com",
                    // "i_like_underscore@but_its_not_allow_in_this_part.example.com",
                    string.Empty,
                    " ",
                    "\r\n",
                    "plainaddress",
                    "#@%^%#$@#$@#.com",
                    "@example.com",
                    "Joe Smith <email@example.com>",
                    "email.example.com",
                    "email@example@example.com",
                    ".email@example.com",
                    "email.@example.com",
                    "email..email@example.com",
                    "email@example.com (Joe Smith)",
                    "email@example",
                    "email@-example.com",
                    "email@111.222.333.44444",
                    "email@example..com",
                    "Abc..123@example.com",
                    "\"(),:;<>[\\]@example.com",
                    "just\"not\"right@example.com",
                    "this\\ is\"really\"not\\allowed@example.com",
                    "Abc.example.com",
                    "A@b@c@example.com",
                    "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com",
                    "just\"not\"right@example.com",
                    "this is\"not\\allowed@example.com",
                    "this\\ still\\\"not\\\\allowed@example.com",
                },
                MustEachPassingValues = new[]
                {
                    new string[] { },
                    new string[] { string.Empty, " ", "\r\n", "plainaddress", "#@%^%#$@#$@#.com", "@example.com", "Joe Smith <email@example.com>", "email.example.com", "email@example@example.com", ".email@example.com", "email.@example.com", "email..email@example.com", "email@example.com (Joe Smith)", "email@example", "email@-example.com", "email@111.222.333.44444", "email@example..com", "Abc..123@example.com", "\"(),:;<>[\\]@example.com", "just\"not\"right@example.com", "this\\ is\"really\"not\\allowed@example.com", "Abc.example.com", "A@b@c@example.com", "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com", "just\"not\"right@example.com", "this is\"not\\allowed@example.com", "this\\ still\\\"not\\\\allowed@example.com", },
                },
                MustFailingValues = new[]
                {
                    // should be valid but our code doesn't allow:
                    // "email@123.123.123.123",
                    // "email@[123.123.123.123]",
                    // "much.\"more\\ unusual\"@example.com",
                    // "very.unusual.\"@\".unusual.com@example.com",
                    // "very.\"(),:;<>[]\".VERY.\"very@\\\\ \"very\".unusual@strange.example.com",
                    // "admin@mailserver1",
                    "email@example.com",
                    "firstname.lastname@example.com",
                    "email@subdomain.example.com",
                    "firstname+lastname@example.com",
                    "\"email\"@example.com",
                    "1234567890@example.com",
                    "email@example-one.com",
                    "_______@example.com",
                    "email@example.name",
                    "email@example.museum",
                    "email@example.co.jp",
                    "firstname-lastname@example.com",
                    "simple@example.com",
                    "very.common@example.com",
                    "disposable.style.email.with+symbol@example.com",
                    "other.email-with-hyphen@example.com",
                    "fully-qualified-domain@example.com",
                    "user.name+tag+sorting@example.com",
                    "x@example.com",
                    "example-indeed@strange-example.com",
                    "example@s.example",
                    "\" \"@example.org",
                    "\"john..doe\"@example.org",
                    "mailhost!username@example.org",
                    "user%example.com@example.org",
                },
                MustEachFailingValues = new[]
                {
                    new string[] { "abc-def", "email@example.com", "abc-def" },
                    new string[] { "abc-def", "firstname.lastname@example.com", "abc-def" },
                    new string[] { "abc-def", "email@subdomain.example.com", "abc-def" },
                    new string[] { "abc-def", "firstname+lastname@example.com", "abc-def" },
                    new string[] { "abc-def", "\"email\"@example.com", "abc-def" },
                    new string[] { "abc-def", "1234567890@example.com", "abc-def" },
                    new string[] { "abc-def", "email@example-one.com", "abc-def" },
                    new string[] { "abc-def", "_______@example.com", "abc-def" },
                    new string[] { "abc-def", "email@example.name", "abc-def" },
                    new string[] { "abc-def", "email@example.museum", "abc-def" },
                    new string[] { "abc-def", "email@example.co.jp", "abc-def" },
                    new string[] { "abc-def", "firstname-lastname@example.com", "abc-def" },
                    new string[] { "abc-def", "simple@example.com", "abc-def" },
                    new string[] { "abc-def", "very.common@example.com", "abc-def" },
                    new string[] { "abc-def", "disposable.style.email.with+symbol@example.com", "abc-def" },
                    new string[] { "abc-def", "other.email-with-hyphen@example.com", "abc-def" },
                    new string[] { "abc-def", "fully-qualified-domain@example.com", "abc-def" },
                    new string[] { "abc-def", "user.name+tag+sorting@example.com", "abc-def" },
                    new string[] { "abc-def", "x@example.com", "abc-def" },
                    new string[] { "abc-def", "example-indeed@strange-example.com", "abc-def" },
                    new string[] { "abc-def", "example@s.example", "abc-def" },
                    new string[] { "abc-def", "\" \"@example.org", "abc-def" },
                    new string[] { "abc-def", "\"john..doe\"@example.org", "abc-def" },
                    new string[] { "abc-def", "mailhost!username@example.org", "abc-def" },
                    new string[] { "abc-def", "user%example.com@example.org", "abc-def" },
                },
            };

            // Act, Assert
            verificationTest1.Run(stringTestValues1);
            verificationTest1.Run(guidTestValues);
            verificationTest1.Run(nullableGuidTestValues);
            verificationTest1.Run(objectTestValues);
            verificationTest1.Run(boolTestValues);
            verificationTest1.Run(nullableBoolTestValues);
            verificationTest1.Run(enumerableTestValues);

            verificationTest2.Run(stringTestValues2);
        }

        [Fact]
        public static void NotBeValidEmailAddress___Should_throw_with_expected_Exception_message___When_called()
        {
            // Arrange
            var subject1 = "test@example.com";
            var expected1 = "Provided value (name: 'subject1') is a valid email address.  Provided value is 'test@example.com'.";

            var subject2 = new[] { "d f", "test@example.com", "d f" };
            var expected2 = "Provided value (name: 'subject2') contains an element that is a valid email address.  Element value is 'test@example.com'.";

            // Act
            var actual1 = Record.Exception(() => new { subject1 }.Must().NotBeValidEmailAddress());
            var actual2 = Record.Exception(() => new { subject2 }.Must().Each().NotBeValidEmailAddress());

            // Assert
            actual1.Message.Should().Be(expected1);
            actual2.Message.Should().Be(expected2);
        }
    }
}