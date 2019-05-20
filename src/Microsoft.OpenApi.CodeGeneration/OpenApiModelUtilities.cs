using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Microsoft.OpenApi
{
    public static class OpenApiUtilities
    {
        private static OpenApiReaderSettings _readerSettings =
            new OpenApiReaderSettings
            {
                ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences
            };

        public static OpenApiOptions GetOptions(
            string outputDir,
            string rootNamespace,
            string contextName,
            string supervisorName,
            string filePath)
        {
            return new OpenApiOptions
            {
                OutputDir = outputDir,
                RootNamespace = rootNamespace,
                ContextClassName = contextName,
                ContextInterfaceName = $"I{contextName}",
                SupervisorClassName = supervisorName,
                SupervisorInterfaceName = $"I{supervisorName}",
                Document = ReadDocument(filePath)
            };
        }

        public static OpenApiDocument ReadDocument(string filePath)
        {
            using (var fs = File.OpenRead(filePath))
            {
                var sr = new OpenApiStreamReader(_readerSettings);

                return sr.Read(fs, out var diagnostic);
            }
        }
    }
}
