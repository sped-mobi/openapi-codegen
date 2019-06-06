// -----------------------------------------------------------------------
// <copyright file="SolutionScaffolder.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class SolutionScaffolder : AbstractScaffolder, ISolutionScaffolder
    {
        public SolutionScaffolder(
            ISolutionGenerator generator,
            IApiProjectScaffolder apiProjectScaffolder,
            ICoreProjectScaffolder coreProjectScaffolder,
            IDataProjectScaffolder dataProjectScaffolder,

            ScaffolderDependencies dependencies) : base(dependencies)
        {
            Generator = generator;
            ApiProject = apiProjectScaffolder;
            CoreProject = coreProjectScaffolder;
            DataProject = dataProjectScaffolder;
        }

        protected ISolutionGenerator Generator { get; }

        public IApiProjectScaffolder ApiProject { get; }

        public ICoreProjectScaffolder CoreProject { get; }

        public IDataProjectScaffolder DataProject { get; }

        public void Save(SolutionModel model)
        {
            Dependencies.FileWriter.WriteFile(model.SolutionFile);
            ApiProject.Save(model.ApiProject);
            CoreProject.Save(model.CoreProject);
            DataProject.Save(model.DataProject);
        }

        public SolutionModel ScaffoldModel(OpenApiOptions options)
        {
            SolutionModel model = new SolutionModel
            {
                SolutionFile = new ScaffoldedFile
                {
                    Code = Generator.WriteProjectFile(options),
                    Path = Path.Combine(options.SolutionDir, $"{options.SolutionName}.sln")
                },
                ApiProject = ApiProject.ScaffoldModel(options),
                CoreProject = CoreProject.ScaffoldModel(options),
                DataProject = DataProject.ScaffoldModel(options)
            };
            return model;
        }
    }
}

