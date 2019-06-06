// -----------------------------------------------------------------------
// <copyright file="IOpenApiDocument.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IOpenApiDocument : IOpenApiExtensible
    {
        OpenApiOptions Options { get; }

        IOpenApiReferenceable ResolveReference(OpenApiReference reference);

        OpenApiInfo Info { get; set; }

        IList<OpenApiServer> Servers { get; set; }

        OpenApiPaths Paths { get; set; }

        OpenApiComponents Components { get; set; }

        IList<OpenApiSecurityRequirement> SecurityRequirements { get; set; }

        IList<OpenApiTag> Tags { get; set; }

        OpenApiExternalDocs ExternalDocs { get; set; }
    }
}
