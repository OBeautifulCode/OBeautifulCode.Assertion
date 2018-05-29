// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.ParameterValidator.Test.Benchmarking
{
    public enum ValidationKind
    {
        NotBeNull,

        BeTrue,

        NotBeNullOrWhiteSpace,

        NotBeNullNorEmptyNorContainAnyNulls,
    }
}
