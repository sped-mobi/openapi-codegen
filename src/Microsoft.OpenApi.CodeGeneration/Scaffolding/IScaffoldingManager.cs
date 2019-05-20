namespace Microsoft.OpenApi.CodeGeneration.Scaffolding
{
    public interface IScaffoldingManager
    {
        void SaveModel(ScaffoldedModel model);

        ScaffoldedModel ScaffoldModel(OpenApiOptions options);
    }
}
