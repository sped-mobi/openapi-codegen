using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.OpenApi.CodeGeneration;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Microsoft.OpenApi
{
    public static class OpenApiUtilities
    {
        private static readonly OpenApiReaderSettings _readerSettings =
            new OpenApiReaderSettings
            {
                ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences
            };

        //public static OpenApiOptions GetOptions(
        //    string primaryKeyTypeName,
        //    string outputDir,
        //    string rootNamespace,
        //    string contextName,
        //    string supervisorName,
        //    string filePath)
        //{
        //    return new OpenApiOptions
        //    {
        //        PrimaryKeyTypeName = primaryKeyTypeName,
        //        OutputDir = outputDir,
        //        RootNamespace = rootNamespace,
        //        ContextClassName = contextName,
        //        ContextInterfaceName = $"I{contextName}",
        //        SupervisorClassName = supervisorName,
        //        SupervisorInterfaceName = $"I{supervisorName}",
        //        Document = ReadDocument(filePath)
        //    };
        //}


        public static IDictionary<string, OpenApiSchema> GetSchemas(this IOpenApiDocument document)
        {
            return document.Components.Schemas.Where(x => !x.Value.GetBoolExtensionValue("skip"))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static OpenApiDocument ReadDocument(string filePath)
        {
            using (var fs = File.OpenRead(filePath))
            {
                var sr = new OpenApiStreamReader(_readerSettings);

                OpenApiDocument document = sr.Read(fs, out var diagnostic);
                SetSchemaTitles(document);
                return document;
            }
        }

        private static void SetSchemaTitles(OpenApiDocument document)
        {
            foreach ((string key, OpenApiSchema value) in document.Components.Schemas)
            {
                if (string.IsNullOrEmpty(value.Title))
                {
                    value.Title = key;
                }
            }
        }

        public static IDictionary<string, OpenApiSchema> GetAllPropertiesRecursive(this OpenApiSchema schema)
        {
            Dictionary<string, OpenApiSchema> dictionary = new Dictionary<string, OpenApiSchema>();

            foreach (var kvp in schema.Properties)
            {
                string name = StringUtilities.MakePascal(kvp.Key);
                OpenApiSchema value = kvp.Value;
                dictionary.Add(name, value);
            }

            if (schema.AllOf != null)
            {
                ProcessSchemaList(schema.AllOf, dictionary);
            }
            if (schema.OneOf != null)
            {
                ProcessSchemaList(schema.OneOf, dictionary);
            }
            if (schema.AnyOf != null)
            {
                ProcessSchemaList(schema.AnyOf, dictionary);
            }

            return dictionary;
        }

        private static void ProcessSchemaList(IEnumerable<OpenApiSchema> list, Dictionary<string, OpenApiSchema> dictionary)
        {
            foreach (var schema in list)
            {
                foreach (var kvp in schema.Properties)
                {
                    string name = StringUtilities.MakePascal(kvp.Key);

                    if (!dictionary.ContainsKey(name))
                    {
                        dictionary.Add(name, kvp.Value);
                    }
                }
            }
        }
    }
}
