// -----------------------------------------------------------------------
// <copyright file="OpenApiExtensibleExtensions.cs" company="sped.mobi">
//     Copyright © 2019 sped.mobi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public static class OpenApiExtensibleExtensions
    {
        public const string ExtensionName = "x-codegen";


        public static string GetStringExtensionValue(this IOpenApiExtensible source, string property)
        {
            if (source.Extensions.TryGetValue(ExtensionName, out IOpenApiExtension value))
            {
                if (value is OpenApiObject extensionObject)
                {
                    var anyValue = (OpenApiString)extensionObject[property];

                    return anyValue.Value;
                }
            }

            return null;
        }

        public static bool GetBoolExtensionValue(this IOpenApiExtensible source, string property)
        {
            if (source.Extensions.TryGetValue(ExtensionName, out IOpenApiExtension value))
            {
                if (value is OpenApiObject extensionObject)
                {
                    var anyValue = (OpenApiBoolean)extensionObject[property];

                    return anyValue.Value;
                }
            }

            return false;
        }
    }
}
