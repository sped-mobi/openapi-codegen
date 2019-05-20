using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public class ConfigurationScaffolder : AbstractScaffolder, IConfigurationScaffolder
    {
        public ConfigurationScaffolder(ScaffolderDependencies dependencies, IConfigurationGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IConfigurationGenerator Generator { get; } 

        public void Save(ConfigurationModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ConfigurationModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ConfigurationModel();
            foreach(var kvp in options.Document.Components.Schemas)
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Configuration(options.RootNamespace));
                var path = Dependencies.PathHelper.Configuration(options.OutputDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
