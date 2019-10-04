﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeValidation.cs" company="OBeautifulCode">
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
    /// A validation that should be performed on the subject type and
    /// verification parameter types to ensure that the validation can executed.
    /// </summary>
#if !OBeautifulCodeAssertionRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Assertion.Recipes", "See package version number")]
    internal
#else
    public
#endif
        class TypeValidation
    {
        /// <summary>
        /// Gets or sets the handler for the type validation.
        /// </summary>
        public Delegates.TypeValidationHandler Handler { get; set; }

        /// <summary>
        /// Gets or sets some reference types required by the validation.
        /// </summary>
        public Type[] ReferenceTypes { get; set; }
    }
}