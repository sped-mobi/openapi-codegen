using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Controllers
{
    public class ControllerModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
