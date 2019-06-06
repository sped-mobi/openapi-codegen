namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IApiProjectScaffolder
    {
        void Save(ApiProjectModel model);

        ApiProjectModel ScaffoldModel(OpenApiOptions options);
    }
}
