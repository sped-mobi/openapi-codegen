// -----------------------------------------------------------------------
// <copyright file="CoreProjectScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.RepositoryInterface;
using Microsoft.OpenApi.CodeGeneration.Supervisor;
using Microsoft.OpenApi.CodeGeneration.SupervisorInterface;
using Microsoft.OpenApi.CodeGeneration.ViewModels;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class CoreProjectScaffolder : AbstractScaffolder, ICoreProjectScaffolder
    {
        public CoreProjectScaffolder(ICoreProjectGenerator generator,
            ISupervisorScaffolder supervisor,
            ISupervisorInterfaceScaffolder supervisorInterface,
            IConverterScaffolder converterScaffolder,
            IEntityScaffolder entities,
            IViewModelScaffolder viewModels,
            IRepositoryInterfaceScaffolder repositories,
            ScaffolderDependencies dependencies) : base(dependencies)
        {
            Generator = generator;
            Supervisor = supervisor;
            SupervisorInterface = supervisorInterface;
            Converter = converterScaffolder;
            Entities = entities;
            Repositories = repositories;
            ViewModels = viewModels;
        }

        protected ICoreProjectGenerator Generator { get; }

        protected ISupervisorScaffolder Supervisor { get; }

        protected ISupervisorInterfaceScaffolder SupervisorInterface { get; }

        protected IConverterScaffolder Converter { get; }

        protected IEntityScaffolder Entities { get; }

        protected IViewModelScaffolder ViewModels { get; }

        protected IRepositoryInterfaceScaffolder Repositories { get; }

        public void Save(CoreProjectModel model)
        {
            Dependencies.FileWriter.WriteFile(model.ProjectFile);
            Dependencies.FileWriter.WriteFile(model.Supervisor);
            Dependencies.FileWriter.WriteFile(model.SupervisorInterface);
            Dependencies.FileWriter.WriteFiles(model.Converters);
            Dependencies.FileWriter.WriteFiles(model.Entities);
            Dependencies.FileWriter.WriteFiles(model.RepositoryInterfaces);
            Dependencies.FileWriter.WriteFiles(model.ViewModels);
        }

        public CoreProjectModel ScaffoldModel(OpenApiOptions options)
        {
            SupervisorModel supervisorModel = Supervisor.ScaffoldModel(options);
            SupervisorInterfaceModel supervisorInterfaceModel = SupervisorInterface.ScaffoldModel(options);
            return new CoreProjectModel
            {
                ProjectFile = CreateProjectFile(options),
                Supervisor = supervisorModel.File,
                SupervisorInterface = supervisorInterfaceModel.File,
                Converters = Converter.ScaffoldModel(options).Files,
                Entities = Entities.ScaffoldModel(options).Files,
                ViewModels = ViewModels.ScaffoldModel(options).Files,
                RepositoryInterfaces = Repositories.ScaffoldModel(options).Files
            };
        }

        private ScaffoldedFile CreateProjectFile(OpenApiOptions options)
        {
            return new ScaffoldedFile
            {
                Code = Generator.WriteProjectFile(options),
                Path = Path.Combine(options.CoreProjectDir, options.CoreProjectName + ".csproj")
            };
        }
    }
}
