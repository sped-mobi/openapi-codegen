// -----------------------------------------------------------------------
// <copyright file="IPluralizer.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface IPluralizer
    {
        string Pluralize(string name);
    }
}
