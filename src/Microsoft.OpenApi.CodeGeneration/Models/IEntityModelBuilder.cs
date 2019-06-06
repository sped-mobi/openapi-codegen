namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public interface IEntityModelBuilder
    {
        EntityModel BuildModel(OpenApiOptions options);
    }
}
