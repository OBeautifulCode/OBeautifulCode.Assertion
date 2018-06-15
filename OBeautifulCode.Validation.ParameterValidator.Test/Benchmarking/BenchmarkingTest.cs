// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BenchmarkingTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.ParameterValidator.Test.Benchmarking
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Enum.Recipes;

    using Xunit;

    public static class BenchmarkingTest
    {
        [Fact(Skip = "For local testing only.")]
        public static void Benchmark_OBC_versus_Spritely()
        {
            const int iterations = 1000;
            const int iterationsToDiscard = 100;
            const string outputFilePath = "c:\\users\\suraj\\desktop\\obc-spritely-benchmark4.csv";

            var obcScenariosRunner = new ObcValidationScenariosRunner();
            var obcBenchmarkKindToStopwatchMap = EnumExtensions.GetEnumValues<BenchmarkSignatureKind>().ToDictionary(_ => _, _ => new Stopwatch());

            var spritelyScenariosRunner = new SpritelyValidationScenariosRunner();
            var spritelyBenchmarkKindToStopwatchMap = EnumExtensions.GetEnumValues<BenchmarkSignatureKind>().ToDictionary(_ => _, _ => new Stopwatch());

            var testString = A.Dummy<string>();
            var testObjects = Some.ReadOnlyDummies<object>(25);
            var testObjectsWithNulls = Some.ReadOnlyDummies<object>(10).Concat(new object[] { null }).Concat(Some.ReadOnlyDummies<object>()).ToList();

            // each iteration generates 26 results = 13 tests * 2 signature kinds
            var obcResults = new List<BenchmarkResult>(iterations * 26);
            var spritelyResults = new List<BenchmarkResult>(iterations * 26);

            for (int x = 0; x < iterations / 2; x++)
            {
                var obcIterationResults = obcScenariosRunner.RunTests(obcBenchmarkKindToStopwatchMap, testString, testObjects, testObjectsWithNulls);
                var spritelyIterationResults = spritelyScenariosRunner.RunTests(spritelyBenchmarkKindToStopwatchMap, testString, testObjects, testObjectsWithNulls);

                if (x >= iterationsToDiscard)
                {
                    obcResults.AddRange(obcIterationResults);
                    spritelyResults.AddRange(spritelyIterationResults);
                }
            }

            for (int x = 0; x < iterations / 2; x++)
            {
                var spritelyIterationResults = spritelyScenariosRunner.RunTests(spritelyBenchmarkKindToStopwatchMap, testString, testObjects, testObjectsWithNulls);
                var obcIterationResults = obcScenariosRunner.RunTests(obcBenchmarkKindToStopwatchMap, testString, testObjects, testObjectsWithNulls);

                if (x >= iterationsToDiscard)
                {
                    spritelyResults.AddRange(spritelyIterationResults);
                    obcResults.AddRange(obcIterationResults);
                }
            }

            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("ValidationKind,BenchmarkSignature,OutcomeKind,TotalMilliseconds,Provider");
                foreach (var obcResult in obcResults)
                {
                    writer.WriteLine($"{obcResult.ValidationKind},{obcResult.BenchmarkSignatureKind},{obcResult.OutcomeKind},{obcResult.Elapsed.TotalMilliseconds},OBC");
                }

                foreach (var spritelyResult in spritelyResults)
                {
                    writer.WriteLine($"{spritelyResult.ValidationKind},{spritelyResult.BenchmarkSignatureKind},{spritelyResult.OutcomeKind},{spritelyResult.Elapsed.TotalMilliseconds},Spritely");
                }
            }
        }

        private static IReadOnlyCollection<BenchmarkResult> RunTests(
            this IRunValidationScenarios validationScenariosRunner,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> benchmarkKindToStopwatchMap,
            string testString,
            IReadOnlyCollection<object> testObjects,
            IReadOnlyCollection<object> testObjectsWithNulls)
        {
            var result = new List<BenchmarkResult>(50);

            validationScenariosRunner.PassingNotBeNullTest(new object(), benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNull, OutcomeKind.Passing);

            validationScenariosRunner.FailingNotBeNullTest((object)null, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNull, OutcomeKind.Failing);

            validationScenariosRunner.PassingBeTrueTest(true, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.BeTrue, OutcomeKind.Passing);

            validationScenariosRunner.FailingBeTrueTest(false, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.BeTrue, OutcomeKind.Failing);

            validationScenariosRunner.PassingNotBeNullNorWhiteSpaceTest(testString, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullOrWhiteSpace, OutcomeKind.Passing);

            validationScenariosRunner.FailingNotBeNullNorWhiteSpaceTest(null, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullOrWhiteSpace, OutcomeKind.Failing);

            validationScenariosRunner.FailingNotBeNullNorWhiteSpaceTest(string.Empty, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullOrWhiteSpace, OutcomeKind.Failing);

            validationScenariosRunner.FailingNotBeNullNorWhiteSpaceTest("  \r\n  ", benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullOrWhiteSpace, OutcomeKind.Failing);

            validationScenariosRunner.PassingNotNullNorEmptyNorContainAnyNullsTest(testObjects, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullNorEmptyEnumerableNorContainAnyNulls, OutcomeKind.Passing);

            validationScenariosRunner.FailingNotNullNorEmptyNorContainAnyNullsTest(null, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullNorEmptyEnumerableNorContainAnyNulls, OutcomeKind.Failing);

            validationScenariosRunner.FailingNotNullNorEmptyNorContainAnyNullsTest(new object[] { }, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullNorEmptyEnumerableNorContainAnyNulls, OutcomeKind.Failing);

            validationScenariosRunner.FailingNotNullNorEmptyNorContainAnyNullsTest(new List<object>(), benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullNorEmptyEnumerableNorContainAnyNulls, OutcomeKind.Failing);

            validationScenariosRunner.FailingNotNullNorEmptyNorContainAnyNullsTest(testObjectsWithNulls, benchmarkKindToStopwatchMap);
            result.RecordResultAndResetStopwatches(benchmarkKindToStopwatchMap, ValidationKind.NotBeNullNorEmptyEnumerableNorContainAnyNulls, OutcomeKind.Failing);

            return result;
        }

        private static void RecordResultAndResetStopwatches(
            this List<BenchmarkResult> results,
            IReadOnlyDictionary<BenchmarkSignatureKind, Stopwatch> benchmarkKindToStopwatchMap,
            ValidationKind validationKind,
            OutcomeKind outcomeKind)
        {
            foreach (var benchmarkSignature in benchmarkKindToStopwatchMap.Keys)
            {
                var stopwatch = benchmarkKindToStopwatchMap[benchmarkSignature];

                var benchmarkResult = new BenchmarkResult
                {
                    BenchmarkSignatureKind = benchmarkSignature,
                    ValidationKind = validationKind,
                    OutcomeKind = outcomeKind,
                    Elapsed = stopwatch.Elapsed,
                };

                results.Add(benchmarkResult);

                stopwatch.Reset();
            }
        }
    }
}
