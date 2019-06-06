using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public class ConfigurationModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
