﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parameter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Validation source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a parameter that is being validated.
    /// </summary>
#if !OBeautifulCodeValidationRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Validation", "See package version number")]
    internal
#else
    public
#endif
        class Parameter
    {
        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="ParameterValidator.Named(object, string)"/> has been called.
        /// </summary>
        public bool HasBeenNamed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="ParameterValidator.Must(object)"/> has been called.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Musted", Justification = "This is the best wording for this identifier.")]
        public bool HasBeenMusted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="ParameterValidator.Each(Parameter)"/> has been called.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eached", Justification = "This is the best wording for this identifier.")]
        public bool HasBeenEached { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether at least one validation has been performed on the paramter.
        /// </summary>
        public bool HasBeenValidated { get; set; }
    }
}
