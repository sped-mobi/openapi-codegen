// -----------------------------------------------------------------------
// <copyright file="StringUtilities.cs" company="sped.mobi">
//     Copyright © 2019 sped.mobi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    internal static class StringUtilities
    {
        public static string MakePascal(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            if (char.IsUpper(source[0]))
            {
                return source;
            }

            char first = char.ToUpper(source[0]);

            return first + source.Substring(1);
        }

        public static string MakeCamel(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            if (char.IsLower(source[0]))
            {
                return source;
            }
            char first = char.ToLower(source[0]);
            return first + source.Substring(1);
        }
    }
}
