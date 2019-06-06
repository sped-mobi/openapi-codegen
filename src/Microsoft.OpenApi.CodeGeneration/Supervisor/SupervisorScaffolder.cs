namespace Microsoft.OpenApi.CodeGeneration.Supervisor
{
    public class SupervisorScaffolder : AbstractScaffolder, ISupervisorScaffolder
    {
        public SupervisorScaffolder(ScaffolderDependencies dependencies, ISupervisorGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected ISupervisorGenerator Generator { get; }

        public void Save(SupervisorModel model)
        {
            Dependencies.FileWriter.WriteFile(model.Class);
            Dependencies.FileWriter.WriteFile(model.Interface);
        }

        public SupervisorModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new SupervisorModel();
            var classFile = new ScaffoldedFile();
            var interfaceFile = new ScaffoldedFile();
            classFile.Code = Generator.WriteClassCode(options.Document, options.SupervisorClassName, options.SupervisorInterfaceName, options.RootNamespace);
            classFile.Path = Dependencies.PathHelper.Supervisor(options.CoreProjectDir, options.SupervisorClassName);
            interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options.SupervisorInterfaceName, options.RootNamespace);
            interfaceFile.Path = Dependencies.PathHelper.Supervisor(options.CoreProjectDir, options.SupervisorInterfaceName);
            model.Class = classFile;
            model.Interface = interfaceFile;
            return model;
        }
    }
}
