﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidation.Private.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Validation source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes
{
    using System;
    using System.Collections;

    using static System.FormattableString;

    /// <summary>
    /// Contains all validations that can be applied to a <see cref="Parameter"/>.
    /// </summary>
#if !OBeautifulCodeValidationRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Validation", "See package version number")]
    internal
#else
    public
#endif
        static partial class ParameterValidation
    {
        private static void BeNullInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            if (!ReferenceEquals(value, null))
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeNullExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotBeNullInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            if (ReferenceEquals(value, null))
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeNullExceptionMessageSuffix);
                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentNullException(null, exceptionMessage);
                }
            }
        }

        private static void BeTrueInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = ReferenceEquals(value, null) || ((bool)value != true);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeTrueExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotBeTrueInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldNotThrow = ReferenceEquals(value, null) || ((bool)value == false);
            if (!shouldNotThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeTrueExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void BeFalseInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = ReferenceEquals(value, null) || (bool)value;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeFalseExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotBeFalseInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldNotThrow = ReferenceEquals(value, null) || (bool)value;
            if (!shouldNotThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeFalseExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotBeNullNorWhiteSpaceInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var shouldThrow = string.IsNullOrWhiteSpace((string)value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeNullNorWhiteSpaceExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void BeEmptyGuidInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = ReferenceEquals(value, null) || ((Guid)value != Guid.Empty);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeEmptyGuidExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength", Justification = "string.IsNullOrEmpty does not work here")]
        private static void BeEmptyStringInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = (string)value != string.Empty;

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeEmptyStringExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "unused", Justification = "Cannot iterate without a local")]
        private static void BeEmptyEnumerableInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var valueAsEnumerable = value as IEnumerable;
            var shouldThrow = false;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                shouldThrow = true;
                break;
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeEmptyEnumerableExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotBeEmptyGuidInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = (!ReferenceEquals(value, null)) && ((Guid)value == Guid.Empty);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeEmptyGuidExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength", Justification = "string.IsNullOrEmpty does not work here")]
        private static void NotBeEmptyStringInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = (string)value == string.Empty;

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeEmptyStringExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "unused", Justification = "Cannot iterate without a local")]
        private static void NotBeEmptyEnumerableInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var valueAsEnumerable = value as IEnumerable;
            var shouldThrow = true;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                shouldThrow = false;
                break;
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeEmptyEnumerableExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void ContainSomeNullsInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var valueAsEnumerable = value as IEnumerable;
            var shouldThrow = true;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                if (ReferenceEquals(unused, null))
                {
                    shouldThrow = false;
                    break;
                }
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, ContainSomeNullsExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotContainAnyNullsInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var valueAsEnumerable = value as IEnumerable;
            var shouldThrow = false;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                if (ReferenceEquals(unused, null))
                {
                    shouldThrow = true;
                    break;
                }
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotContainAnyNullsExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void BeDefaultInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var defaultValue = GetDefaultValue(valueType);
            var shouldThrow = !EqualUsingDefaultEqualityComparer(valueType, value, defaultValue);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeDefaultExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void NotBeDefaultInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var defaultValue = GetDefaultValue(valueType);
            var shouldThrow = EqualUsingDefaultEqualityComparer(valueType, value, defaultValue);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeDefaultExceptionMessageSuffix);
                throw new ArgumentException(exceptionMessage);
            }
        }

        private static void BeLessThanInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) != CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeLessThanExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void NotBeLessThanInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) == CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeLessThanExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void BeGreaterThanInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) != CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeGreaterThanExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void NotBeGreaterThanInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) == CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeGreaterThanExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void BeLessThanOrEqualToInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) == CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeLessThanOrEqualToExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void NotBeLessThanOrEqualToInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) != CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeLessThanOrEqualToExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void BeGreaterThanOrEqualToInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) == CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeGreaterThanOrEqualToExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void NotBeGreaterThanOrEqualToInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) != CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeGreaterThanOrEqualToExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void BeEqualToInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = !EqualUsingDefaultEqualityComparer(valueType, value, validationParameters[0].Value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeEqualToExceptionMessageSuffix);
                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void NotBeEqualToInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            var shouldThrow = EqualUsingDefaultEqualityComparer(valueType, value, validationParameters[0].Value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeEqualToExceptionMessageSuffix);
                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void BeInRangeInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            ThrowIfMalformedRange(validationParameters);

            var shouldThrow = (CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) == CompareOutcome.Value1LessThanValue2) ||
                              (CompareUsingDefaultComparer(valueType, value, validationParameters[1].Value) == CompareOutcome.Value1GreaterThanValue2);
            
            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, BeInRangeExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void NotBeInRangeInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            ThrowIfMalformedRange(validationParameters);

            var shouldThrow = (CompareUsingDefaultComparer(valueType, value, validationParameters[0].Value) != CompareOutcome.Value1LessThanValue2) &&
                              (CompareUsingDefaultComparer(valueType, value, validationParameters[1].Value) != CompareOutcome.Value1GreaterThanValue2);

            if (shouldThrow)
            {
                var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotBeInRangeExceptionMessageSuffix);

                if (isElementInEnumerable)
                {
                    throw new ArgumentException(exceptionMessage);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(exceptionMessage, (Exception)null);
                }
            }
        }

        private static void ContainInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var valueAsEnumerable = (IEnumerable)value;
            var searchForItem = validationParameters[0].Value;
            var elementType = validationParameters[0].ValueType;
            foreach (var element in valueAsEnumerable)
            {
                if (EqualUsingDefaultEqualityComparer(elementType, element, searchForItem))
                {
                    return;
                }
            }

            var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, ContainExceptionMessageSuffix);
            throw new ArgumentException(exceptionMessage);
        }

        private static void NotContainInternal(
            string validationName,
            object value,
            Type valueType,
            string parameterName,
            string because,
            bool isElementInEnumerable,
            params ValidationParameter[] validationParameters)
        {
            NotBeNullInternal(validationName, value, valueType, parameterName, because, isElementInEnumerable);

            var valueAsEnumerable = (IEnumerable)value;
            var searchForItem = validationParameters[0].Value;
            var elementType = validationParameters[0].ValueType;
            foreach (var element in valueAsEnumerable)
            {
                if (EqualUsingDefaultEqualityComparer(elementType, element, searchForItem))
                {
                    var exceptionMessage = BuildExceptionMessage(parameterName, because, isElementInEnumerable, NotContainExceptionMessageSuffix);
                    throw new ArgumentException(exceptionMessage);
                }
            }            
        }
    }
}
