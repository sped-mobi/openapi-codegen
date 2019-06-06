// -----------------------------------------------------------------------
// <copyright file="CoreProjectScaffolder.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class CoreProjectScaffolder : AbstractScaffolder, ICoreProjectScaffolder
    {
        public CoreProjectScaffolder(ICoreProjectGenerator generator,
            ISupervisorScaffolder supervisorScaffolder,
            IConverterScaffolder converterScaffolder,
            IEntityScaffolder entities,
            IRepositoryInterfaceScaffolder repositories,
            ScaffolderDependencies dependencies) : base(dependencies)
        {
            Generator = generator;
            Supervisor = supervisorScaffolder;
            Converter = converterScaffolder;
            Entities = entities;
            Repositories = repositories;
        }

        protected ICoreProjectGenerator Generator { get; }
        protected ISupervisorScaffolder Supervisor { get; }
        protected IConverterScaffolder Converter { get; }
        protected IEntityScaffolder Entities { get; }
        protected IRepositoryInterfaceScaffolder Repositories { get; }


        public void Save(CoreProjectModel model)
        {
            Dependencies.FileWriter.WriteFile(model.ProjectFile);
            Dependencies.FileWriter.WriteFile(model.Supervisor);
            Dependencies.FileWriter.WriteFile(model.SupervisorInterface);
            Dependencies.FileWriter.WriteFiles(model.Converters);
            Dependencies.FileWriter.WriteFiles(model.Entities);
            Dependencies.FileWriter.WriteFiles(model.RepositoryInterfaces);
        }

        public CoreProjectModel ScaffoldModel(OpenApiOptions options)
        {

            SupervisorModel supervisorModel = Supervisor.ScaffoldModel(options);

            return new CoreProjectModel
            {
                ProjectFile = CreateProjectFile(options),
                Supervisor = supervisorModel.Class,
                SupervisorInterface = supervisorModel.Interface,
                Converters = Converter.ScaffoldModel(options).Files,
                Entities = Entities.ScaffoldModel(options).Files,
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

