using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public class CodeGenerationOptions
    {
        public string OutputDir { get; set; }

        public string RootNamespace { get; set; }

        public List<Kind> Kinds { get; set; } = new List<Kind>();
    }
}
