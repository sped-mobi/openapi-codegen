﻿// -----------------------------------------------------------------------
// <copyright file="OpenApiSchemaExtensions.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public static class OpenApiSchemaExtensions
    {
        public static bool IsArrayOrObject(this OpenApiSchema source)
        {
            return IsArray(source) || IsObject(source);
        }

        public static bool IsObject(this OpenApiSchema source)
        {
            return source?.Type == "object";
        }

        public static bool IsArray(this OpenApiSchema source)
        {
            return source?.Type == "array";
        }
    }
}
