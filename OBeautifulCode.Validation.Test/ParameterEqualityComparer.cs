// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterEqualityComparer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    using System.Collections.Generic;

    using OBeautifulCode.Math.Recipes;

    public class ParameterEqualityComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(
            Parameter x,
            Parameter y)
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
                (x.Value == y.Value) &&
                (x.Name == y.Name) &&
                (x.HasBeenNamed == y.HasBeenNamed) &&
                (x.HasBeenMusted == y.HasBeenMusted) &&
                (x.HasBeenEached == y.HasBeenEached) &&
                (x.HasBeenValidated == y.HasBeenValidated);
            return result;
        }

        public int GetHashCode(
            Parameter obj) =>
            HashCodeHelper.Initialize()
                .Hash(obj.Value)
                .Hash(obj.Name)
                .Hash(obj.HasBeenNamed)
                .Hash(obj.HasBeenMusted)
                .Hash(obj.HasBeenEached)
                .Hash(obj.HasBeenValidated)
                .Value;
    }
}
