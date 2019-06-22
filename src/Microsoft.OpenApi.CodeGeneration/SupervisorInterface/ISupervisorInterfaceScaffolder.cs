namespace Microsoft.OpenApi.CodeGeneration.SupervisorInterface
{
    public interface ISupervisorInterfaceScaffolder
    {
        void Save(SupervisorInterfaceModel model);

        SupervisorInterfaceModel ScaffoldModel(OpenApiOptions options);
    }
}