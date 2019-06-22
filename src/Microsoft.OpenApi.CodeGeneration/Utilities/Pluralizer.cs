using System;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
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