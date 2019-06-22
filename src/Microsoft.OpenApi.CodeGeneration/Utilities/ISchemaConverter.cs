// -----------------------------------------------------------------------
// <copyright file="ISchemaConverter.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface ISchemaConverter
    {
        string ConvertToType(OpenApiSchema schema);
    }
}
