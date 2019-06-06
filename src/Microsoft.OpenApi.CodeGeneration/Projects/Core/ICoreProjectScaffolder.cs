namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ICoreProjectScaffolder
    {
        void Save(CoreProjectModel model);

        CoreProjectModel ScaffoldModel(OpenApiOptions options);
    }
}

