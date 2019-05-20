namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class ModelFactory
    {
        public static GeneratedModel Create(CodeGenerationOptions options)
        {
            GeneratedModel model = new GeneratedModel();

            foreach (var kind in options.Kinds)
            {
                //if (kind == Kind.Context || kind == Kind.Supervisor)
                //    continue;

                GeneratedFile modelClassFile = FileFactory.Create(options, FileType.ModelClass, kind);
                GeneratedFile generatorInterfaceFile = FileFactory.Create(options, FileType.GeneratorInterface, kind);
                GeneratedFile generatorClassFile = FileFactory.Create(options, FileType.GeneratorClass, kind);
                GeneratedFile scaffolderInterfaceFile = FileFactory.Create(options, FileType.ScaffolderInterface, kind);
                GeneratedFile scaffolderClassFile = FileFactory.Create(options, FileType.ScaffolderClass, kind);

                model.Models.Add(modelClassFile);
                model.GeneratorInterfaces.Add(generatorInterfaceFile);
                model.GeneratorClasses.Add(generatorClassFile);
                model.ScaffolderInterfaces.Add(scaffolderInterfaceFile);
                model.ScaffolderClasses.Add(scaffolderClassFile);
            }

            model.ScaffoldedModelClass = FileFactory.Create(options, FileType.ScaffoldedModelClass);
            model.ServicesClassFile = FileFactory.Create(options, FileType.ServicesClass);
            //model.SupervisorClass = FileFactory.Create(options, FileType.SupervisorClass);
            //model.SupervisorModelClass = FileFactory.Create(options, FileType.SupervisorModelClass);
            //model.SupervisorInterface = FileFactory.Create(options, FileType.SupervisorInterface);
            //model.ContextClass = FileFactory.Create(options, FileType.ContextClass);
            //model.ContextInterface = FileFactory.Create(options, FileType.ContextInterface);
            //model.ContextModelClass = FileFactory.Create(options, FileType.ContextModelClass);
            return model;
        }
    }
}
