using Microsoft.OpenApi.CodeGeneration.Utilities;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class ScaffolderDependencies
    {
        public ScaffolderDependencies(
            IPathHelper pathHelper,
            IFileWriter fileWriter,
            INamespaceHelper @namespace)
        {
            PathHelper = pathHelper;
            FileWriter = fileWriter;
            Namespace = @namespace;
        }

        public IPathHelper PathHelper { get; }
        public IFileWriter FileWriter { get; }
        public INamespaceHelper Namespace { get; }
    }
}
