// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BenchmarkSignatureKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.ParameterValidator.Test.Benchmarking
{
    public enum BenchmarkSignatureKind
    {
        /// <summary>
        /// x.Named().Must()
        /// </summary>
        Named,

        /// <summary>
        /// new { x }.Must()
        /// </summary>
        Anonymous,
    }
}
