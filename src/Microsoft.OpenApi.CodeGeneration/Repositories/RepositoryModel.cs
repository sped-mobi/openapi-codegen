using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public class RepositoryModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
