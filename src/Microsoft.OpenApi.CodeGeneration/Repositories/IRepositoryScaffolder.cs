namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public interface IRepositoryScaffolder
    {
        void Save(RepositoryModel model);

        RepositoryModel ScaffoldModel(OpenApiOptions options);
    }
}
