// -----------------------------------------------------------------------
// <copyright file="IContextGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public interface IContextGenerator
    {
        string WriteClassCode(IOpenApiDocument document, OpenApiOptions options);

        string WriteInterfaceCode(IOpenApiDocument document, OpenApiOptions options);
    }
}
