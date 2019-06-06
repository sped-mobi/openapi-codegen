using Microsoft.OpenApi.CodeGeneration.Utilities;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class ScaffolderDependencies
    {
        public ScaffolderDependencies(
            IPathHelper pathHelper,
            IFileWriter fileWriter,
            INamespaceHelper @namespace,
            INameHelper namer)
        {
            PathHelper = pathHelper;
            FileWriter = fileWriter;
            Namespace = @namespace;
            Namer = namer;
        }

        public INameHelper Namer { get; }
        public IPathHelper PathHelper { get; }
        public IFileWriter FileWriter { get; }
        public INamespaceHelper Namespace { get; }
    }
}
