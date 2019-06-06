using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public interface IRepositoryInterfaceGenerator
    {
        string WriteInterfaceCode(OpenApiSchema schema, string name, OpenApiOptions options);
    }
}