// -----------------------------------------------------------------------
// <copyright file="EmbeddedResourcesManager.cs" company="sped.mobi">
//     Copyright © 2019 sped.mobi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Microsoft.OpenApi.CodeGeneration.Resources
{
    public static class EmbeddedResourcesManager
    {
        private static Stream _openApi;


        public static Stream OpenApi => _openApi ?? (_openApi = GetResourceStream("openapi.json"));


        private static Stream GetResourceStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Microsoft.OpenApi.CodeGeneration." + resourceName);
        }


        public static OpenApiDocument Build()
        {
            return FromStream(OpenApi);
        }

        private static readonly OpenApiReaderSettings _settings =
            new OpenApiReaderSettings
            {
                ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences
            };

        private static OpenApiDocument FromStream(Stream stream)
        {
            using (stream)
            {
                var sr = new OpenApiStreamReader(_settings);

                return sr.Read(stream, out var diagnostic);
            }
        }
    }
}
