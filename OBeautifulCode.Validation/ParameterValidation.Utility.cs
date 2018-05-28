﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidation.Utility.cs" company="OBeautifulCode">
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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
        private delegate void TypeValidationHandler(string validationName, bool isElementInEnumerable, Type valueType, Type[] referenceTypes, ValidationParameter[] validationParameters);

        private delegate void ValueValidationHandler(string validationName, object value, Type valueType, string parameterName, string because, bool isElementInEnumerable, params ValidationParameter[] validationParameters);

        private static readonly MethodInfo GetDefaultValueOpenGenericMethodInfo = ((Func<object>)GetDefaultValue<object>).Method.GetGenericMethodDefinition();

        private static readonly ConcurrentDictionary<Type, MethodInfo> GetDefaultValueTypeToMethodInfoMap = new ConcurrentDictionary<Type, MethodInfo>();

        private static readonly MethodInfo EqualsUsingDefaultEqualityComparerOpenGenericMethodInfo = ((Func<object, object, bool>)EqualsUsingDefaultEqualityComparer).Method.GetGenericMethodDefinition();

        private static readonly ConcurrentDictionary<Type, MethodInfo> EqualsUsingDefaultEqualityComparerTypeToMethodInfoMap = new ConcurrentDictionary<Type, MethodInfo>();

        private static readonly MethodInfo CompareUsingDefaultComparerOpenGenericMethodInfo = ((Func<object, object, CompareOutcome>)CompareUsingDefaultComparer).Method.GetGenericMethodDefinition();

        private static readonly ConcurrentDictionary<Type, MethodInfo> CompareUsingDefaultComparerTypeToMethodInfoMap = new ConcurrentDictionary<Type, MethodInfo>();

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
            }
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeBooleanTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(bool), typeof(bool?) },
            }
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeStringTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(string) },
            }
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeGuidTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(Guid), typeof(Guid?) },
            }
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeEnumerableTypeValidations = new[]
        {
            new TypeValidation
            {
                TypeValidationHandler = ThrowIfNotOfType,
                ReferenceTypes = new[] { typeof(IEnumerable) },
            }
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
            }
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

        private static void Validate(
            this Parameter parameter,
            IReadOnlyCollection<TypeValidation> typeValidations,
            ValueValidation valueValidation)
        {
            ParameterValidator.ThrowOnImproperUseOfFrameworkIfDetected(parameter, ParameterShould.BeMusted);

            if (parameter.HasBeenEached)
            {
                // check that the parameter is an IEnumerable and not null
                ThrowIfNotOfType(nameof(ParameterValidator.Each), false, parameter.ValueType, new[] { EnumerableType }, null);
                NotBeNullInternal(nameof(ParameterValidator.Each), parameter.Value, parameter.ValueType, parameter.Name, because: null, isElementInEnumerable: false);

                var valueAsEnumerable = (IEnumerable)parameter.Value;
                var enumerableType = GetEnumerableGenericType(parameter.ValueType);
                foreach (var typeValidation in typeValidations ?? new TypeValidation[] { })
                {
                    typeValidation.TypeValidationHandler(valueValidation.ValidationName, true, enumerableType, typeValidation.ReferenceTypes, valueValidation.ValidationParameters);
                }

                foreach (var element in valueAsEnumerable)
                {
                    valueValidation.ValueValidationHandler(valueValidation.ValidationName, element, enumerableType, parameter.Name, valueValidation.Because, isElementInEnumerable: true, validationParameters: valueValidation.ValidationParameters);
                }
            }
            else
            {
                foreach (var typeValidation in typeValidations ?? new TypeValidation[] { })
                {
                    typeValidation.TypeValidationHandler(valueValidation.ValidationName, false, parameter.ValueType, typeValidation.ReferenceTypes, valueValidation.ValidationParameters);
                }

                valueValidation.ValueValidationHandler(valueValidation.ValidationName, parameter.Value, parameter.ValueType, parameter.Name, valueValidation.Because, isElementInEnumerable: false, validationParameters: valueValidation.ValidationParameters);
            }

            parameter.HasBeenValidated = true;
        }

        private static Type GetEnumerableGenericType(
            Type enumerableType)
        {
            // adapted from: https://stackoverflow.com/a/17713382/356790
            Type result;
            if (enumerableType.IsArray)
            {
                // type is array, shortcut
                result = enumerableType.GetElementType();
            }
            else if (enumerableType.IsGenericType && (enumerableType.GetGenericTypeDefinition() == UnboundGenericEnumerableType))
            {
                // type is IEnumerable<T>
                result = enumerableType.GetGenericArguments()[0];
            }
            else
            {
                // type implements IEnumerable<T> or is a subclass (sub-sub-class, ...)
                // of a type that implements IEnumerable<T>
                // note that we are grabing the first implementation.  it is possible, but
                // highly unlikely, for a type to have multiple implementations of IEnumerable<T>
                result = enumerableType
                    .GetInterfaces()
                    .Where(_ => _.IsGenericType && (_.GetGenericTypeDefinition() == UnboundGenericEnumerableType))
                    .Select(_ => _.GenericTypeArguments[0])
                    .FirstOrDefault();

                if (result == null)
                {
                    // here we just assume it's an IEnumerable and return typeof(object),
                    // however, for completeness, we should recurse through all interface implementations
                    // and check whether those are IEnumerable<T>.
                    // see: https://stackoverflow.com/questions/5461295/using-isassignablefrom-with-open-generic-types
                    result = ObjectType;
                }
            }

            return result;
        }

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
                ThrowOnParameterUnexpectedTypes(validationName, isElementInEnumerable, "Any Reference Type", "Nullable<T>");
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
                ThrowOnParameterUnexpectedTypes(validationName, isElementInEnumerable, "IEnumerable", "IEnumerable<Any Reference Type>", "IEnumerable<Nullable<T>>");
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
                ThrowOnParameterUnexpectedTypes(validationName, isElementInEnumerable, validTypes);
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
                        ThrowOnParameterUnexpectedTypes(validationName, isElementInEnumerable, "IComparable", "IComparable<T>");
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
                    ThrowOnValidationParameterUnexpectedTypes(validationName, validationParameter.Name, valueType);
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
                    ThrowOnValidationParameterUnexpectedTypes(validationName, validationParameter.Name, enumerableType);
                }
            }
        }

        private static void ThrowIfMalformedRange(
            ValidationParameter[] validationParameters)
        {
            // the public BeInRange/NotBeInRange is generic and guarantees that minimum and maximum are of the same type
            var rangeIsMalformed =
                CompareUsingDefaultComparer(validationParameters[0].ValueType, validationParameters[0].Value,
                    validationParameters[1].Value) == CompareOutcome.Value1GreaterThanValue2;
            if (rangeIsMalformed)
            {
                var malformedRangeExceptionMessage = string.Format(MalformedRangeExceptionMessage, validationParameters[0].Name,
                    validationParameters[1].Name);
                ParameterValidator.ThrowOnImproperUseOfFramework(malformedRangeExceptionMessage);
            }
        }

        private static string BuildExceptionMessage(
            string parameterName,
            string because,
            bool isElementInEnumerable,
            string exceptionMessageSuffix)
        {
            if (because != null)
            {
                return because;
            }

            var parameterNameQualifier = parameterName == null ? string.Empty : Invariant($" '{parameterName}'");
            var enumerableQualifier = isElementInEnumerable ? " contains an element that" : string.Empty;
            var result = Invariant($"parameter{parameterNameQualifier}{enumerableQualifier} {exceptionMessageSuffix}");
            return result;
        }

        private static void ThrowOnParameterUnexpectedTypes(
            string validationName,
            bool isElementInEnumerable,
            params Type[] expectedTypes)
        {
            var expectedTypeStrings = expectedTypes.Select(_ => _.GetFriendlyTypeName()).ToArray();
            ThrowOnParameterUnexpectedTypes(validationName, isElementInEnumerable, expectedTypeStrings);
        }

        private static void ThrowOnParameterUnexpectedTypes(
            string validationName,
            bool isElementInEnumerable,
            params string[] expectedTypes)
        {
            var expectedTypesMessage = expectedTypes.Select(_ => isElementInEnumerable ? Invariant($"IEnumerable<{_}>") : _).Aggregate((running, item) => running + ", " + item);
            var exceptionMessage = Invariant($"called {validationName}() on an object that is not one of the following types: {expectedTypesMessage}");
            throw new InvalidCastException(exceptionMessage);
        }

        private static void ThrowOnValidationParameterUnexpectedTypes(
            string validationName,
            string validationParameterName,
            params Type[] expectedTypes)
        {
            var expectedTypeStrings = expectedTypes.Select(_ => _.GetFriendlyTypeName()).ToArray();
            ThrowOnValidationParameterUnexpectedTypes(validationName, validationParameterName, expectedTypeStrings);
        }

        private static void ThrowOnValidationParameterUnexpectedTypes(
            string validationName,
            string validationParameterName,
            params string[] expectedTypes)
        {
            var expectedTypesMessage = expectedTypes.Aggregate((running, item) => running + ", " + item);
            var exceptionMessage = Invariant($"called {validationName}({validationParameterName}:) where '{validationParameterName}' is not one of the following types: {expectedTypesMessage}");
            throw new InvalidCastException(exceptionMessage);
        }

        private static T GetDefaultValue<T>()
        {
            var result = default(T);
            return result;
        }

        private static object GetDefaultValue(
            Type type)
        {
            if (!GetDefaultValueTypeToMethodInfoMap.ContainsKey(type))
            {
                GetDefaultValueTypeToMethodInfoMap.TryAdd(type, GetDefaultValueOpenGenericMethodInfo.MakeGenericMethod(type));
            }

            var result = GetDefaultValueTypeToMethodInfoMap[type].Invoke(null, null);
            return result;
        }

        private static bool EqualsUsingDefaultEqualityComparer<T>(
            T value1,
            T value2)
        {
            var result = EqualityComparer<T>.Default.Equals(value1, value2);
            return result;
        }

        private static bool EqualUsingDefaultEqualityComparer(
            Type type,
            object value1,
            object value2)
        {
            if (!EqualsUsingDefaultEqualityComparerTypeToMethodInfoMap.ContainsKey(type))
            {
                EqualsUsingDefaultEqualityComparerTypeToMethodInfoMap.TryAdd(type, EqualsUsingDefaultEqualityComparerOpenGenericMethodInfo.MakeGenericMethod(type));
            }

            var result = (bool)EqualsUsingDefaultEqualityComparerTypeToMethodInfoMap[type].Invoke(null, new[] { value1, value2 });
            return result;
        }

        private static CompareOutcome CompareUsingDefaultComparer<T>(
            T x,
            T y)
        {
            var comparison = Comparer<T>.Default.Compare(x, y);
            CompareOutcome result;
            if (comparison < 0)
            {
                result = CompareOutcome.Value1LessThanValue2;
            }
            else if (comparison == 0)
            {
                result = CompareOutcome.Value1EqualsValue2;
            }
            else
            {
                result = CompareOutcome.Value1GreaterThanValue2;
            }

            return result;
        }

        private static CompareOutcome CompareUsingDefaultComparer(
            Type type,
            object value1,
            object value2)
        {
            if (!CompareUsingDefaultComparerTypeToMethodInfoMap.ContainsKey(type))
            {
                CompareUsingDefaultComparerTypeToMethodInfoMap.TryAdd(type, CompareUsingDefaultComparerOpenGenericMethodInfo.MakeGenericMethod(type));
            }

            // note that the call is ultimately, via reflection, to Compare(T, T)
            // as such, reflection will throw an ArgumentException if the types of value1 and value2 are
            // not "convertible" to the specified type.  It's a pretty complicated heuristic:
            // https://stackoverflow.com/questions/34433043/check-whether-propertyinfo-setvalue-will-throw-an-argumentexception
            // Instead of relying on this heuristic, we just check upfront that value2's type == the specified type
            // (value1's type will always be the specified type).  This constrains our capabilities - for example, we
            // can't compare an integer to a decimal.  That said, we feel like this is a good constraint in a parameter
            // validation framework.  We'd rather be forced to make the types align than get a false negative
            // (a validation passes when it should fail).

            // otherwise, if reflection is able to call Compare(T, T), then ArgumentException can be thrown if
            // Type T does not implement either the System.IComparable<T> generic interface or the System.IComparable interface
            // However we already check for this upfront in ThrowIfNotComparable
            var result = (CompareOutcome)CompareUsingDefaultComparerTypeToMethodInfoMap[type].Invoke(null, new[] { value1, value2 });
            return result;
        }

        private class TypeValidation
        {
            public TypeValidationHandler TypeValidationHandler { get; set; }

            public Type[] ReferenceTypes { get; set; }
        }

        private class ValueValidation
        {
            public string ValidationName { get; set; }

            public string Because { get; set; }

            public ValueValidationHandler ValueValidationHandler { get; set; }

            public ValidationParameter[] ValidationParameters { get; set; }
        }

        private class ValidationParameter
        {
            public string Name { get; set; }

            public object Value { get; set; }

            public Type ValueType { get; set; }
        }
        
        private enum CompareOutcome
        {
            Value1LessThanValue2,

            Value1EqualsValue2,

            Value1GreaterThanValue2
        }
    }
}
