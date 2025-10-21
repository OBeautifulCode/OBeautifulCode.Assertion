// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCommon.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    public static class TestCommon
    {
        public static AssertionTracker Clone(
            this AssertionTracker assertionTracker)
        {
            var result = new AssertionTracker
            {
                SubjectValue = assertionTracker.SubjectValue,
                SubjectType = assertionTracker.SubjectType,
                SubjectName = assertionTracker.SubjectName,
                Actions  = assertionTracker.Actions,
                AssertionKind = assertionTracker.AssertionKind,
            };

            return result;
        }

        public static AssertionTracker CloneWithActionVerifiedAtLeastOnce(
            this AssertionTracker assertionTracker,
            bool eachValueVerifiedForIteration)
        {
            var result = assertionTracker.Clone();

            result.Actions |= Actions.VerifiedAtLeastOnce;

            if (eachValueVerifiedForIteration)
            {
                result.Actions |= Actions.EachedValueVerifiedForIteration;
            }

            return result;
        }
    }
}
