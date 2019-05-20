using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;
using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Controllers
{
    public class ControllerScaffolder : AbstractScaffolder, IControllerScaffolder
    {
        public ControllerScaffolder(ScaffolderDependencies dependencies, IControllerGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IControllerGenerator Generator { get; } 

        public void Save(ControllerModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ControllerModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ControllerModel();
            foreach(var kvp in options.Document.Components.Schemas)
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Controller(options.RootNamespace));
                var path = Dependencies.PathHelper.Controller(options.OutputDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
