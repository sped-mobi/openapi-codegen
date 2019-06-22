// -----------------------------------------------------------------------
// <copyright file="IRepositoryInterfaceGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.RepositoryInterface
{
    public interface IRepositoryInterfaceGenerator
    {
        string WriteInterfaceCode(OpenApiSchema schema, string name, OpenApiOptions options);
    }
}
