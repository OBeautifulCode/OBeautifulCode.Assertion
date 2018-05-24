// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Test.Benchmarking
{
    public enum ValidationKind
    {
        /// <summary>
        /// Calls NotBeNull()
        /// </summary>
        NotBeNull,

        /// <summary>
        /// Calls BeTrue()
        /// </summary>
        BeTrue,

        /// <summary>
        /// Calls NotBeNullNorWhitespace()
        /// </summary>
        NotBeNullOrWhiteSpace,

        /// <summary>
        /// Calls NotBeNullNorEmptyNorContainAnyNulls()
        /// </summary>
        NotBeNullNorEmptyNorContainAnyNulls,
    }
}
