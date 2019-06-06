namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public interface IRepositoryInterfaceScaffolder
    {
        void Save(RepositoryInterfaceModel model);

        RepositoryInterfaceModel ScaffoldModel(OpenApiOptions options);
    }
}