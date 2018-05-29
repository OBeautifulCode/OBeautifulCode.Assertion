// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpritelyValidationScenariosRunner.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.ParameterValidator.Test.Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Spritely.Recipes;

    public class SpritelyValidationScenariosRunner : IRunValidationScenarios
    {
        public void PassingNotBeNullTest(
            object value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
            value.Named(nameof(value)).Must().NotBeNull().OrThrow();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testObject = value }.Must().NotBeNull().OrThrow();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingNotBeNullTest(
            object value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().NotBeNull().OrThrow();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testObject = value }.Must().NotBeNull().OrThrow();
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
            value.Named(nameof(value)).Must().BeTrue().OrThrow();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testBool = value }.Must().BeTrue().OrThrow();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingBeTrueTest(
            bool value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().BeTrue().OrThrow();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testBool = value }.Must().BeTrue().OrThrow();
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
            value.Named(nameof(value)).Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testString = value }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingNotBeNullNorWhiteSpaceTest(
            string value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testString = value }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
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
            value.Named(nameof(value)).Must().NotBeNull().And().NotBeEmptyEnumerable<object>().And().NotContainAnyNulls<object>().OrThrowFirstFailure();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();

            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
            new { testObjects = value }.Must().NotBeNull().And().NotBeEmptyEnumerable<object>().And().NotContainAnyNulls<object>().OrThrowFirstFailure();
            signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
        }

        public void FailingNotNullNorEmptyNorContainAnyNullsTest(
            IEnumerable<object> value,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> signatureKindToStopwatchMap)
        {
            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Start();
                value.Named(nameof(value)).Must().NotBeNull().And().NotBeEmptyEnumerable<object>().And().NotContainAnyNulls<object>().OrThrowFirstFailure();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Named].Stop();
            }

            try
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Start();
                new { testObjects = value }.Must().NotBeNull().And().NotBeEmptyEnumerable<object>().And().NotContainAnyNulls<object>().OrThrowFirstFailure();
                throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                signatureKindToStopwatchMap[BenchmarkSignatureKind.Anonymous].Stop();
            }
        }
    }
}
