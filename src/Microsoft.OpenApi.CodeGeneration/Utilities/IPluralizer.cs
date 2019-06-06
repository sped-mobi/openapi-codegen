// -----------------------------------------------------------------------
// <copyright file="IPluralizer.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface IPluralizer
    {
        string Pluralize(string name);
    }

    public class Pluralizer : IPluralizer
    {
        public string Pluralize(string name)
        {
            if (name.EndsWith("y", StringComparison.CurrentCulture))
            {
                int index = name.LastIndexOf("y", StringComparison.CurrentCulture) - 1;

                return name.Substring(index) + "ies";
            }


            return name + "s";
        }
    }
}
