// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRunValidationScenarios.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.ParameterValidator.Test.Benchmarking
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public interface IRunValidationScenarios
    {
        void PassingNotBeNullTest(
            object value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void FailingNotBeNullTest(
            object value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void PassingBeTrueTest(
            bool value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void FailingBeTrueTest(
            bool value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void PassingNotBeNullNorWhiteSpaceTest(
            string value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void FailingNotBeNullNorWhiteSpaceTest(
            string value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void PassingNotNullNorEmptyNorContainAnyNullsTest(
            IEnumerable<object> value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);

        void FailingNotNullNorEmptyNorContainAnyNullsTest(
            IEnumerable<object> value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap);
    }
}