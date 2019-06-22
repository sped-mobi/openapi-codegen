// -----------------------------------------------------------------------
// <copyright file="IRepositoryGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Repository
{
    public interface IRepositoryGenerator
    {
        string WriteClassCode(OpenApiSchema schema, string name, OpenApiOptions options);
    }
}
