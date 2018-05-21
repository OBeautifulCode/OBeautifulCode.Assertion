// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCommon.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes.Test
{
    public static class TestCommon
    {
        public static Parameter Clone(
            this Parameter parameter)
        {
            var result = new Parameter
            {
                Value = parameter.Value,
                Name = parameter.Name,
                HasBeenNamed = parameter.HasBeenNamed,
                HasBeenMusted = parameter.HasBeenMusted,
                HasBeenEached = parameter.HasBeenEached,
                HasBeenValidated = parameter.HasBeenValidated,
            };

            return result;
        }

        public static Parameter CloneWithHasBeenValidated(
            this Parameter parameter)
        {
            var result = parameter.Clone();
            result.HasBeenValidated = true;
            return result;
        }
    }
}
