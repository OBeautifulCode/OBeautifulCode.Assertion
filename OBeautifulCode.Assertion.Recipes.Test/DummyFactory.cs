// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes.Test
{
    using System;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;

    /// <inheritdoc />
    public class DummyFactory : IDummyFactory
    {
        public DummyFactory()
        {
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(AssertionKind.Unknown);
        }

        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}
