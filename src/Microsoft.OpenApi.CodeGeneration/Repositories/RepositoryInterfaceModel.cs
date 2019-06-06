using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public class RepositoryInterfaceModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
