﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidation.TypeValidation.cs" company="OBeautifulCode">
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
    using System.Collections.Generic;
    using System.Linq;

    using static System.FormattableString;

    /// <summary>
    /// Contains all validations that can be applied to a <see cref="Parameter"/>.
    /// </summary>
#if !OBeautifulCodeValidationRecipesProject
    internal
#else
    public
#endif
        static partial class ParameterValidation
    {
#pragma warning disable SA1201
        private delegate void TypeValidationHandler(
            string validationName, 
            bool isElementInEnumerable, 
            Type valueType, 
            Type[] referenceTypes, 
            ValidationParameter[] validationParameters);

        private static readonly Type EnumerableType = typeof(IEnumerable);

        private static readonly Type UnboundGenericEnumerableType = typeof(IEnumerable<>);

        private static readonly Type ComparableType = typeof(IComparable);

        private static readonly Type UnboundGenericComparableType = typeof(IComparable<>);

        private static readonly Type ObjectType = typeof(object);

        private static readonly IReadOnlyCollection<TypeValidation> MustBeNullableTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfTypeCannotBeNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeBooleanTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(bool), typeof(bool?) },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeStringTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(string) },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeGuidTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(Guid), typeof(Guid?) },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeEnumerableTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(IEnumerable) },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeEnumerableOfNullableTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(IEnumerable) },
            },
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfEnumerableTypeCannotBeNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> InequalityTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotComparable,
            },
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfAnyValidationParameterTypeDoesNotEqualValueType,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> EqualsTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfAnyValidationParameterTypeDoesNotEqualValueType,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> AlwaysThrowTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = Throw,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> ContainmentTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(IEnumerable) },
            },
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfAnyValidationParameterTypeDoesNotEqualEnumerableValueType,
            },
        };

        // ReSharper disable once UnusedParameter.Local
        private static void Throw(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] referenceTypes,
            ValidationParameter[] validationParameters)
        {
            var parameterValueTypeName = valueType.GetFriendlyTypeName();
            throw new InvalidCastException(Invariant($"validationName: {validationName}, isElementInEnumerable: {isElementInEnumerable}, parameterValueTypeName: {parameterValueTypeName}"));
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ThrowIfTypeCannotBeNull(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] referenceTypes,
            ValidationParameter[] validationParameters)
        {
            if (valueType.IsValueType && (Nullable.GetUnderlyingType(valueType) == null))
            {
                ThrowParameterUnexpectedType(validationName, valueType, isElementInEnumerable, AnyReferenceTypeName, NullableGenericTypeName);
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ThrowIfEnumerableTypeCannotBeNull(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] referenceTypes,
            ValidationParameter[] validationParameters)
        {
            var enumerableType = GetEnumerableGenericType(valueType);

            if (enumerableType.IsValueType && (Nullable.GetUnderlyingType(enumerableType) == null))
            {
                ThrowParameterUnexpectedType(validationName, valueType, isElementInEnumerable, nameof(IEnumerable), EnumerableOfAnyReferenceTypeName, EnumerableOfNullableGenericTypeName);
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ThrowIfNotOfType(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] validTypes,
            ValidationParameter[] validationParameters)
        {
            if ((!validTypes.Contains(valueType)) && (!validTypes.Any(_ => _.IsAssignableFrom(valueType))))
            {
                ThrowParameterUnexpectedType(validationName, valueType, isElementInEnumerable, validTypes);
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ThrowIfNotComparable(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] referenceTypes,
            ValidationParameter[] validationParameters)
        {
            // type is nullable
            if (Nullable.GetUnderlyingType(valueType) == null)
            {
                // type is IComparable or can be assigned to IComparable
                if ((valueType != ComparableType) && (!ComparableType.IsAssignableFrom(valueType)))
                {
                    // type is IComparable<T>
                    if ((!valueType.IsGenericType) || (valueType.GetGenericTypeDefinition() != UnboundGenericComparableType))
                    {
                        // type implements IComparable<T>
                        var comparableType = valueType.GetInterfaces().FirstOrDefault(_ => _.IsGenericType && (_.GetGenericTypeDefinition() == UnboundGenericEnumerableType));
                        if (comparableType == null)
                        {
                            // note that, for completeness, we should recurse through all interface implementations
                            // and check whether any of those are IComparable<>
                            // see: https://stackoverflow.com/questions/5461295/using-isassignablefrom-with-open-generic-types
                            ThrowParameterUnexpectedType(validationName, valueType, isElementInEnumerable, nameof(IComparable), ComparableGenericTypeName, NullableGenericTypeName);
                        }
                    }
                }
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ThrowIfAnyValidationParameterTypeDoesNotEqualValueType(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] validTypes,
            ValidationParameter[] validationParameters)
        {
            foreach (var validationParameter in validationParameters)
            {
                if (validationParameter.ValueType != valueType)
                {
                    ThrowValidationParameterUnexpectedType(validationName, validationParameter.ValueType, validationParameter.Name, valueType);
                }
            }
        }

        private static void ThrowIfAnyValidationParameterTypeDoesNotEqualEnumerableValueType(
            string validationName,
            bool isElementInEnumerable,
            Type valueType,
            Type[] validTypes,
            ValidationParameter[] validationParameters)
        {
            var enumerableType = GetEnumerableGenericType(valueType);

            foreach (var validationParameter in validationParameters)
            {
                if (validationParameter.ValueType != enumerableType)
                {
                    ThrowValidationParameterUnexpectedType(validationName, validationParameter.ValueType, validationParameter.Name, enumerableType);
                }
            }
        }

        private static void ThrowParameterUnexpectedType(
            string validationName,
            Type valueType,
            bool isElementInEnumerable,
            params Type[] expectedTypes)
        {
            var expectedTypeStrings = expectedTypes.Select(_ => _.GetFriendlyTypeName()).ToArray();
            ThrowParameterUnexpectedType(validationName, valueType, isElementInEnumerable, expectedTypeStrings);
        }

        private static void ThrowParameterUnexpectedType(
            string validationName,
            Type valueType,
            bool isElementInEnumerable,
            params string[] expectedTypes)
        {
            var expectedTypesMessage = expectedTypes.Select(_ => isElementInEnumerable ? Invariant($"IEnumerable<{_}>") : _).Aggregate((running, item) => running + ", " + item);
            var valueTypeMessage = isElementInEnumerable ? Invariant($"IEnumerable<{valueType.GetFriendlyTypeName()}>") : valueType.GetFriendlyTypeName();
            var exceptionMessage = Invariant($"Called {validationName}() on a parameter of type {valueTypeMessage}, which is not one of the following expected type(s): {expectedTypesMessage}.");
            throw new InvalidCastException(exceptionMessage);
        }

        private static void ThrowValidationParameterUnexpectedType(
            string validationName,
            Type validationParameterType,
            string validationParameterName,
            params Type[] expectedTypes)
        {
            var expectedTypesStrings = expectedTypes.Select(_ => _.GetFriendlyTypeName()).ToArray();
            var expectedTypesMessage = expectedTypesStrings.Aggregate((running, item) => running + ", " + item);
            var exceptionMessage = Invariant($"Called {validationName}({validationParameterName}:) where '{validationParameterName}' is of type {validationParameterType.GetFriendlyTypeName()}, which is not one of the following expected type(s): {expectedTypesMessage}.");
            throw new InvalidCastException(exceptionMessage);
        }

        private class TypeValidation
        {
            public TypeValidationHandler TypeValidationHandler { get; set; }

            public Type[] ReferenceTypes { get; set; }
        }

#pragma warning restore SA1201
    }
}
