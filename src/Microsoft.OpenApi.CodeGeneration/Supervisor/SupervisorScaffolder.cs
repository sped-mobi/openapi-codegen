// -----------------------------------------------------------------------
// <copyright file="SupervisorScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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
            Dependencies.FileWriter.WriteFile(model.File);
            //Dependencies.FileWriter.WriteFile(model.Interface);
        }

        public SupervisorModel ScaffoldModel(OpenApiOptions options)
        {
            string supervisorNamespace = Dependencies.Namespace.Supervisor(options.RootNamespace);
            var model = new SupervisorModel();
            var classFile = new ScaffoldedFile
            {
                //var interfaceFile = new ScaffoldedFile();
                Code = Generator.WriteClassCode(options.Document,
                options.SupervisorClassName,
                options.SupervisorInterfaceName,
                supervisorNamespace),
                Path = Dependencies.PathHelper.Supervisor(options.CoreProjectDir, options.SupervisorClassName)
            };
            //interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options.SupervisorInterfaceName, supervisorNamespace);
            //interfaceFile.Path = Dependencies.PathHelper.Supervisor(options.CoreProjectDir, options.SupervisorInterfaceName);
            model.File = classFile;
            //model.Interface = interfaceFile;
            return model;
        }
    }
}
