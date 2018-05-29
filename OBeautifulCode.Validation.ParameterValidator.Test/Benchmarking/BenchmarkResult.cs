// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BenchmarkResult.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.ParameterValidator.Test.Benchmarking
{
    using System;

    public class BenchmarkResult
    {
        public ValidationKind ValidationKind { get; set; }

        public BenchmarkSignatureKind BenchmarkSignatureKind { get; set; }

        public OutcomeKind OutcomeKind { get; set; }

        public TimeSpan Elapsed { get; set; }
    }
}
