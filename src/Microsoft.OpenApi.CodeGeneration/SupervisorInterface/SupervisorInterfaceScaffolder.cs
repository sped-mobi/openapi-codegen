// -----------------------------------------------------------------------
// <copyright file="SupervisorInterfaceScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.SupervisorInterface
{
    public class SupervisorInterfaceScaffolder : AbstractScaffolder, ISupervisorInterfaceScaffolder
    {
        protected ISupervisorInterfaceGenerator Generator { get; }

        public SupervisorInterfaceScaffolder(ScaffolderDependencies dependencies, ISupervisorInterfaceGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        public void Save(SupervisorInterfaceModel model)
        {
            Dependencies.FileWriter.WriteFile(model.File);
        }

        public SupervisorInterfaceModel ScaffoldModel(OpenApiOptions options)
        {
            SupervisorInterfaceModel model = new SupervisorInterfaceModel
            {
                File = new ScaffoldedFile
                {
                    Code = Generator.WriteCode(options),
                    Path = Dependencies.PathHelper.SupervisorInterface(options.CoreProjectDir, options.SupervisorInterfaceName)
                }
            };
            return model;
        }
    }
}
