﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertionTracker.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Assertion.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Fully specifies a verification.
    /// </summary>
#if !OBeautifulCodeAssertionRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Assertion.Recipes", "See package version number")]
    internal
#else
    public
#endif
        class Verification
    {
        /// <summary>
        /// Gets or sets the name of the verification.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the rationale for the verification.
        /// </summary>
        public string Because { get; set; }

        /// <summary>
        /// Gets or sets a value that determines how to apply the <see cref="Because"/>.
        /// </summary>
        public ApplyBecause ApplyBecause { get; set; }

        /// <summary>
        /// Gets or sets the handler for the verification.
        /// </summary>
        public Delegates.VerificationHandler Handler { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="VerificationParameter"/>s.
        /// </summary>
        public VerificationParameter[] VerificationParameters { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TypeValidation"/>s to apply.
        /// </summary>
        public IReadOnlyCollection<TypeValidation> TypeValidations { get; set; }

        /// <summary>
        /// Gets or sets a collection of key/value pairs that provide additional user-defined information
        /// that is added to the exception's <see cref="Exception.Data"/> property, if thrown.
        /// </summary>
        public IDictionary Data { get; set; }

        /// <summary>
        /// Gets or sets - TO BE MOVED.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets - TO BE MOVED.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets - TO BE MOVED.
        /// </summary>
        public Type ValueType { get; set; }

        /// <summary>
        /// Gets or sets - TO BE MOVED.
        /// </summary>
        public bool IsElementInEnumerable { get; set; }
    }
}