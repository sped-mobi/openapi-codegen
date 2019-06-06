// -----------------------------------------------------------------------
// <copyright file="ApiProjectScaffolder.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Microsoft.OpenApi.CodeGeneration.Controllers;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class ApiProjectScaffolder : AbstractScaffolder, IApiProjectScaffolder
    {
        public ApiProjectScaffolder(IApiProjectGenerator generator, IControllerScaffolder controllerScaffolder, ScaffolderDependencies dependencies) : base(dependencies)
        {
            Generator = generator;
            ControllerScaffolder = controllerScaffolder;
        }

        protected IApiProjectGenerator Generator { get; }

        protected IControllerScaffolder ControllerScaffolder { get; }

        public void Save(ApiProjectModel model)
        {
            Dependencies.FileWriter.WriteFile(model.ProjectFile);
            Dependencies.FileWriter.WriteFile(model.ProgramCSFile);
            Dependencies.FileWriter.WriteFile(model.StartupCSFile);
            Dependencies.FileWriter.WriteFile(model.AppSettingsJSONFile);
            Dependencies.FileWriter.WriteFile(model.AppSettingsDevelopmentJSONFile);
            Dependencies.FileWriter.WriteFile(model.ServicesConfigurationCSFile);
            Dependencies.FileWriter.WriteFiles(model.Controllers);
        }

        public ApiProjectModel ScaffoldModel(OpenApiOptions options)
        {
            ApiProjectModel model = new ApiProjectModel
            {
                ProjectFile =
                    CreateFile(Generator.WriteProjectFile(options),
                        Path.Combine(options.ApiProjectDir, options.ApiProjectName + ".csproj")),

                StartupCSFile =
                    CreateFile(Generator.WriteStartupCSFile(options),
                        Path.Combine(options.ApiProjectDir, "Startup.cs")),

                ProgramCSFile =
                    CreateFile(Generator.WriteProgramCSFile(options),
                        Path.Combine(options.ApiProjectDir, "Program.cs")),

                ServicesConfigurationCSFile =
                    CreateFile(Generator.WriteServicesConfigurationCSFile(options),
                        Path.Combine(options.ApiProjectDir, "ServicesConfiguration.cs")),

                AppSettingsDevelopmentJSONFile =
                    CreateFile(Generator.WriteAppSettingsDevelopmentJSONFile(options),
                        Path.Combine(options.ApiProjectDir, "appsettings.Development.json")),

                AppSettingsJSONFile =
                    CreateFile(Generator.WriteAppSettingsJSONFile(options),
                        Path.Combine(options.ApiProjectDir, "appsettings.json"))
            };

            model.Controllers = ControllerScaffolder.ScaffoldModel(options).Files;
            return model;
        }

        private ScaffoldedFile CreateFile(string code, string path)
        {
            return new ScaffoldedFile
            {
                Code = code,
                Path = path
            };
        }
    }
}
