namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public interface IContextGenerator
    {
        string WriteClassCode(IOpenApiDocument document, OpenApiOptions options);

        string WriteInterfaceCode(IOpenApiDocument document, OpenApiOptions options);
    }
}
