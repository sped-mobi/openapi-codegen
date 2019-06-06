using Microsoft.OpenApi.CodeGeneration.Utilities;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class GeneratorDependencies
    {
        public GeneratorDependencies(
            ITextProvider textProvider,
            INamespaceHelper namespaceHelper,
            INameHelper namer,
            ISchemaConverter schema,
            IPluralizer pluralizer,
            IOpenApiDocument document)
        {
            Namer = namer;
            Provider = textProvider;
            Namespace = namespaceHelper;
            Schema = schema;
            Pluralizer = pluralizer;
            Document = document;
        }

        public ITextProvider Provider { get; }
        public INameHelper Namer { get; }
        public INamespaceHelper Namespace { get; }
        public ISchemaConverter Schema { get; }
        public IPluralizer Pluralizer { get; }
        public IOpenApiDocument Document { get; }
    }
}
