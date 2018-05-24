// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObcValidationScenariosRunner.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Test.Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using OBeautifulCode.Validation.Recipes;

    public class ObcValidationScenariosRunner : IRunValidationScenarios
    {
        public void PassingNotBeNullTest(
            object value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
            value.Named(nameof(value)).Must().NotBeNull();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testObject = value }.Must().NotBeNull();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingNotBeNullTest(
            object value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().NotBeNull();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testObject = value }.Must().NotBeNull();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
            }
        }

        public void PassingBeTrueTest(
            bool value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
            value.Named(nameof(value)).Must().BeTrue();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testBool = value }.Must().BeTrue();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingBeTrueTest(
            bool value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().BeTrue();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testBool = value }.Must().BeTrue();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
            }
        }

        public void PassingNotBeNullNorWhiteSpaceTest(
            string value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
            value.Named(nameof(value)).Must().NotBeNullNorWhiteSpace();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testString = value }.Must().NotBeNullNorWhiteSpace();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingNotBeNullNorWhiteSpaceTest(
            string value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().NotBeNullNorWhiteSpace();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testString = value }.Must().NotBeNullNorWhiteSpace();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
            }
        }

        public void PassingNotNullNorEmptyNorContainAnyNullsTest(
            IEnumerable<object> value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
            value.Named(nameof(value)).Must().NotBeNullNorEmptyNorContainAnyNulls();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testObjects = value }.Must().NotBeNullNorEmptyNorContainAnyNulls();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingNotNullNorEmptyNorContainAnyNullsTest(
            IEnumerable<object> value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().NotBeNullNorEmptyNorContainAnyNulls();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testObjects = value }.Must().NotBeNullNorEmptyNorContainAnyNulls();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
            }
        }
    }
}
