namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ISolutionScaffolder
    {
        void Save(SolutionModel model);

        SolutionModel ScaffoldModel(OpenApiOptions options);
    }
}

