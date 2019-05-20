using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi
{
    public class OpenApiOptions
    {
        public string OutputDir { get; set; }

        public string RootNamespace { get; set; }

        public string SupervisorClassName { get; set; }

        public string SupervisorInterfaceName { get; set; }

        public string ContextClassName { get; set; }

        public string ContextInterfaceName { get; set; }

        public OpenApiDocument Document { get; set; }
    }
}
