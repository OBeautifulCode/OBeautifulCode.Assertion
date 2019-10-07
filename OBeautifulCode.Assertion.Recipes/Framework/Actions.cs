﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplyBecause.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Assertion.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes
{
    using System;

    /// <summary>
    /// The actions that have been performed in the lifecycle of an assertion.
    /// </summary>
    [Flags]
#if !OBeautifulCodeAssertionRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Assertion.Recipes", "See package version number")]
    internal
#else
    public
#endif
    enum Actions
    {
        /// <summary>
        /// None (default).
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        None = 0,

        /// <summary>
        /// The subject should have been name with a call to
        /// <see cref="WorkflowExtensions.Named{TSubject}(TSubject, string)"/>.
        /// </summary>
        Named = 1,

        /// <summary>
        /// The subject should have been Must'ed with a call to
        /// <see cref="WorkflowExtensions.Must{TSubject}(TSubject)"/>.
        /// </summary>
        Musted = 2,

        /// <summary>
        /// The subject should have been Each'ed with a call to
        /// <see cref="WorkflowExtensions.Each(AssertionTracker)"/>.
        /// </summary>
        Eached = 4,

        /// <summary>
        /// The subject should have been verified by at least one verification.
        /// </summary>
        VerifiedAtLeastOnce = 8,
    }
}
