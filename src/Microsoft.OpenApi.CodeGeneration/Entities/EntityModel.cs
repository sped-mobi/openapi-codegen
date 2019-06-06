using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public class EntityModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
