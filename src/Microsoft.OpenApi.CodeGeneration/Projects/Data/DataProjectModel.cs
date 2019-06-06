using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class DataProjectModel
    {
        public ScaffoldedFile ProjectFile { get; set; }
        public ScaffoldedFile DataContext { get; set; }
        public IReadOnlyList<ScaffoldedFile> Configurations { get; set; }
        public IReadOnlyList<ScaffoldedFile> RepositoryClasses { get; set; }
    }
}