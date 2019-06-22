// -----------------------------------------------------------------------
// <copyright file="OpenApiDocumentV3.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.CodeGeneration.Resources;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class OpenApiDocumentV3 : IOpenApiDocument
    {
        private readonly OpenApiDocument _document;
        private OpenApiOptions _options;

        public OpenApiDocumentV3() : this(EmbeddedResourcesManager.Build())
        {
        }

        public OpenApiDocumentV3(OpenApiDocument document)
        {
            _document = document;
        }

        public OpenApiOptions Options
        {
            get
            {
                return _options ?? (_options = new OpenApiOptions(_document));
            }
        }

        public IOpenApiReferenceable ResolveReference(OpenApiReference reference)
        {
            return _document.ResolveReference(reference);
        }

        public OpenApiInfo Info
        {
            get
            {
                return _document.Info;
            }
            set
            {
                _document.Info = value;
            }
        }

        public IList<OpenApiServer> Servers
        {
            get
            {
                return _document.Servers;
            }
            set
            {
                _document.Servers = value;
            }
        }

        public OpenApiPaths Paths
        {
            get
            {
                return _document.Paths;
            }
            set
            {
                _document.Paths = value;
            }
        }

        public OpenApiComponents Components
        {
            get
            {
                return _document.Components;
            }
            set
            {
                _document.Components = value;
            }
        }

        public IList<OpenApiSecurityRequirement> SecurityRequirements
        {
            get
            {
                return _document.SecurityRequirements;
            }
            set
            {
                _document.SecurityRequirements = value;
            }
        }

        public IList<OpenApiTag> Tags
        {
            get
            {
                return _document.Tags;
            }
            set
            {
                _document.Tags = value;
            }
        }

        public OpenApiExternalDocs ExternalDocs
        {
            get
            {
                return _document.ExternalDocs;
            }
            set
            {
                _document.ExternalDocs = value;
            }
        }

        /// <summary>Specification extensions.</summary>
        public IDictionary<string, IOpenApiExtension> Extensions
        {
            get
            {
                return _document.Extensions;
            }
            set
            {
                _document.Extensions = value;
            }
        }
    }
}
