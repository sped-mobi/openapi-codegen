using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public interface IRepositoryGenerator
    {
        string WriteClassCode(OpenApiSchema schema, string name, OpenApiOptions options);
    }
}
