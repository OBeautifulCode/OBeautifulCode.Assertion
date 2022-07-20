﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Verifications.TypeValidation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Assertion.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Linq;

    using OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

#if !OBeautifulCodeAssertionSolution
    internal
#else
    public
#endif
    static partial class Verifications
    {
#pragma warning disable SA1201
        private static readonly Type EnumerableType = typeof(IEnumerable);

        private static readonly Type BoolType = typeof(bool);

        private static readonly Type NullableBoolType = typeof(bool?);

        private static readonly Type StringType = typeof(string);

        private static readonly Type GuidType = typeof(Guid);

        private static readonly Type NullableGuidType = typeof(Guid?);

        private static readonly Type DateTimeType = typeof(DateTime);

        private static readonly Type NullableDateTimeType = typeof(DateTime?);

        private static readonly Type DictionaryType = typeof(IDictionary);

        private static readonly Type UnboundGenericDictionaryType = typeof(IDictionary<,>);

        private static readonly Type UnboundGenericReadOnlyDictionaryType = typeof(IReadOnlyDictionary<,>);

        private static readonly IReadOnlyCollection<TypeValidation> AlwaysThrowTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = AlwaysThrow,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeAssignableToNullTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeCannotBeAssignedToNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeBooleanOrNullableBooleanTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { BoolType, NullableBoolType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeNullableBooleanTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { NullableBoolType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeStringTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { StringType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeGuidOrNullableGuidTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { GuidType, NullableGuidType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeDateTimeOrNullableDateTimeTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { DateTimeType, NullableDateTimeType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeNullableDateTimeTypeValidations = new[]
        {
            new TypeValidation
            {
                // DateTime is assignable to DateTime?, so we call ThrowIfVerifiableItemTypeNotEqualToSpecifiedTypes instead of ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes
                Handler = ThrowIfVerifiableItemTypeNotEqualToSpecifiedTypes,
                ReferenceTypes = new[] { NullableDateTimeType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeEnumerableTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { EnumerableType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemEnumerableElementTypeMustBeAssignableToNullValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { EnumerableType },
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemEnumerableElementTypeCannotBeAssignedToNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeDictionaryTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { DictionaryType, UnboundGenericDictionaryType, UnboundGenericReadOnlyDictionaryType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemMustBeDictionaryWhoseValueTypeCanBeAssignedToNullValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { DictionaryType, UnboundGenericDictionaryType, UnboundGenericReadOnlyDictionaryType },
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemDictionaryValueTypeCannotBeAssignedToNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemTypeMustBeInequalityComparableToAllVerificationParameterTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeDoesNotHaveWorkingDefaultComparer,
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemTypeMustBeAssignableToNullAndInequalityComparableToAllVerificationParameterTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeCannotBeAssignedToNull,
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeDoesNotHaveWorkingDefaultComparer,
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemTypeMustBeSameReferenceAsAllVerificationParameterTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeIsValueType,
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemTypeMustBeEqualToAllVerificationParameterTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemTypeMustBeEqualToAllVerificationParameterEnumerableElementTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterEnumerableElementTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemEnumerableElementTypeMustBeEqualToAllVerificationParameterTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { EnumerableType },
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemEnumerableElementTypeDoesNotEqualAllVerificationParameterTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> VerifiableItemDictionaryKeyTypeMustBeEqualToAllVerificationParameterTypesValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes,
                ReferenceTypes = new[] { DictionaryType, UnboundGenericDictionaryType, UnboundGenericReadOnlyDictionaryType },
            },
            new TypeValidation
            {
                Handler = ThrowIfVerifiableItemDictionaryKeyTypeDoesNotEqualAllVerificationParameterTypes,
            },
        };

        private static void AlwaysThrow(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemTypeReadableString = verifiableItem.ItemType.ToStringReadable();

            ThrowImproperUseOfFramework(Invariant($"verificationName: {verification.Name}, isElementInEnumerable: {verifiableItem.ItemIsElementInEnumerable}, verifiableItemTypeName: {verifiableItemTypeReadableString}"));
        }

        private static void ThrowIfVerifiableItemTypeIsValueType(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            if (verifiableItem.ItemType.IsValueType)
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, AnyReferenceTypeName);
            }
        }

        private static void ThrowIfVerifiableItemTypeCannotBeAssignedToNull(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            if (!verifiableItemType.IsClosedTypeAssignableToNull())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, AnyReferenceTypeName, NullableGenericTypeName);
            }
        }

        private static void ThrowIfVerifiableItemEnumerableElementTypeCannotBeAssignedToNull(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var elementType = verifiableItemType.GetClosedEnumerableElementType();

            if (!elementType.IsClosedTypeAssignableToNull())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, EnumerableOfAnyReferenceTypeName, EnumerableOfNullableGenericTypeName, EnumerableWhenNotEnumerableOfAnyValueTypeName);
            }
        }

        private static void ThrowIfVerifiableItemDictionaryValueTypeCannotBeAssignedToNull(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var dictionaryValueType = verifiableItemType.GetClosedDictionaryValueType();

            if (!dictionaryValueType.IsClosedTypeAssignableToNull())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, DictionaryTypeName, DictionaryWithValueOfAnyReferenceTypeName, DictionaryWithValueOfNullableGenericTypeName, ReadOnlyDictionaryWithValueOfAnyReferenceTypeName, ReadOnlyDictionaryWithValueOfNullableGenericTypeName);
            }
        }

        private static void ThrowIfVerifiableItemTypeNotAssignableToSpecifiedTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var validTypes = typeValidation.ReferenceTypes;

            if (!validTypes.Any(_ => verifiableItemType.IsAssignableTo(_, treatGenericTypeDefinitionAsAssignableTo: true)))
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, validTypes);
            }
        }

        private static void ThrowIfVerifiableItemTypeNotEqualToSpecifiedTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var validTypes = typeValidation.ReferenceTypes;

            if (validTypes.All(_ => verifiableItemType != _))
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, validTypes);
            }
        }

        private static void ThrowIfVerifiableItemTypeDoesNotHaveWorkingDefaultComparer(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            if (!verifiableItem.ItemType.HasWorkingDefaultComparer())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, AnyTypeWithWorkingDefaultComparerName);
            }
        }

        private static void ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                if (verificationParameter.ParameterType != verifiableItemType)
                {
                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, verifiableItemType);
                }
            }
        }

        private static void ThrowIfVerifiableItemEnumerableElementTypeDoesNotEqualAllVerificationParameterTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var elementType = verifiableItem.ItemType.GetClosedEnumerableElementType();

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                if (verificationParameter.ParameterType != elementType)
                {
                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, elementType);
                }
            }
        }

        private static void ThrowIfVerifiableItemTypeIsNotEqualToAllVerificationParameterEnumerableElementTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                var elementType = verificationParameter.ParameterType.GetClosedEnumerableElementType();

                if (verifiableItemType != elementType)
                {
                    var expectedType = verificationParameter.ParameterType.GetGenericTypeDefinition().MakeGenericType(verifiableItemType);

                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, expectedType);
                }
            }
        }

        private static void ThrowIfVerifiableItemDictionaryKeyTypeDoesNotEqualAllVerificationParameterTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var keyType = verifiableItem.ItemType.GetClosedDictionaryKeyType();

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                if (verificationParameter.ParameterType != keyType)
                {
                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, keyType);
                }
            }
        }

#pragma warning restore SA1201
    }
}
