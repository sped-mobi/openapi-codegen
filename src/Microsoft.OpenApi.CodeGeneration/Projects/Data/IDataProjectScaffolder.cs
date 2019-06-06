namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IDataProjectScaffolder
    {
        void Save(DataProjectModel model);

        DataProjectModel ScaffoldModel(OpenApiOptions options);
    }
}

