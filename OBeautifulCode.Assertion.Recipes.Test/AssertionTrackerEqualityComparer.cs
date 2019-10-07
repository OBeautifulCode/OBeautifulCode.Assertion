// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertionTrackerEqualityComparer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System.Collections.Generic;

    using OBeautifulCode.Equality.Recipes;

    public class AssertionTrackerEqualityComparer : IEqualityComparer<AssertionTracker>
    {
        public bool Equals(
            AssertionTracker x,
            AssertionTracker y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            var result =
                ((x.SubjectValue == y.SubjectValue) || x.SubjectValue.Equals(y.SubjectValue)) && // .Equals will throw with null values, but == doesn't always return true when values are equal (e.g. two Guids will not be equal)
                (x.SubjectType == y.SubjectType) &&
                (x.SubjectName == y.SubjectName) &&
                (x.Actions == y.Actions) &&
                (x.AssertionKind == y.AssertionKind);
            return result;
        }

        public int GetHashCode(
            AssertionTracker obj) =>
            HashCodeHelper.Initialize()
                .Hash(obj.SubjectValue)
                .Hash(obj.SubjectType)
                .Hash(obj.SubjectName)
                .Hash(obj.Actions)
                .Hash(obj.AssertionKind)
                .Value;
    }
}
