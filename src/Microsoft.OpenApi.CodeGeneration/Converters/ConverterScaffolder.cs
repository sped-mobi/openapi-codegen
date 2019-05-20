using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;
using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Converters
{
    public class ConverterScaffolder : AbstractScaffolder, IConverterScaffolder
    {
        public ConverterScaffolder(ScaffolderDependencies dependencies, IConverterGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IConverterGenerator Generator { get; } 

        public void Save(ConverterModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ConverterModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ConverterModel();
            foreach(var kvp in options.Document.Components.Schemas)
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Converter(options.RootNamespace));
                var path = Dependencies.PathHelper.Converter(options.OutputDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
