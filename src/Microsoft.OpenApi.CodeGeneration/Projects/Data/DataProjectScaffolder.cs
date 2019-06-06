// -----------------------------------------------------------------------
// <copyright file="DataProjectScaffolder.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Repositories;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class DataProjectScaffolder : AbstractScaffolder, IDataProjectScaffolder
    {
        public DataProjectScaffolder(IDataProjectGenerator generator,
            IConfigurationScaffolder configurationScaffolder,
            IContextScaffolder contextScaffolder,
            IRepositoryScaffolder repositoryScaffolder,
            ScaffolderDependencies dependencies) : base(dependencies)
        {
            Generator = generator;
            Configuration = configurationScaffolder;
            Context = contextScaffolder;
            Repository = repositoryScaffolder;
        }

        protected IDataProjectGenerator Generator { get; }
        protected IConfigurationScaffolder Configuration { get; }
        protected IContextScaffolder Context { get; }
        protected IRepositoryScaffolder Repository { get; }

        public void Save(DataProjectModel model)
        {
            Dependencies.FileWriter.WriteFile(model.ProjectFile);
            Dependencies.FileWriter.WriteFile(model.DataContext);
            Dependencies.FileWriter.WriteFiles(model.Configurations);
            Dependencies.FileWriter.WriteFiles(model.RepositoryClasses);

        }

        public DataProjectModel ScaffoldModel(OpenApiOptions options)
        {
            DataProjectModel model = new DataProjectModel
            {
                ProjectFile = CreateProjectFile(options),
                DataContext = Context.ScaffoldModel(options).Class,
                Configurations = Configuration.ScaffoldModel(options).Files,
                RepositoryClasses = Repository.ScaffoldModel(options).Files,
            };

            return model;
        }

        private ScaffoldedFile CreateProjectFile(OpenApiOptions options)
        {
            return new ScaffoldedFile
            {
                Code = Generator.WriteProjectFile(options),
                Path = Path.Combine(options.DataProjectDir, options.DataProjectName + ".csproj")
            };
        }
    }
}

