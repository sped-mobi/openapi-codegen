using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class CoreProjectModel
    {
        public ScaffoldedFile ProjectFile { get; set; }
        public IReadOnlyList<ScaffoldedFile> Converters { get; set; }
        public IReadOnlyList<ScaffoldedFile> Entities { get; set; }
        public IReadOnlyList<ScaffoldedFile> RepositoryInterfaces { get; set; }
        public ScaffoldedFile Supervisor { get; set; }
        public ScaffoldedFile SupervisorInterface { get; set; }
    }
}