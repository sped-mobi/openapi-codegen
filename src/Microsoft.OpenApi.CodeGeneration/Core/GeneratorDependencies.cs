using Microsoft.OpenApi.CodeGeneration.Utilities;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class GeneratorDependencies
    {
        public GeneratorDependencies(
            ITextProvider textProvider,
            INamespaceHelper namespaceHelper)
        {
            Provider = textProvider;
            Namespace = namespaceHelper;
        }

        public ITextProvider Provider { get; }
        public INamespaceHelper Namespace { get; }
    }
}
