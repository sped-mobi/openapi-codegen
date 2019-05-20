using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;
using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.ViewModels
{
    public class ViewModelScaffolder : AbstractScaffolder, IViewModelScaffolder
    {
        public ViewModelScaffolder(ScaffolderDependencies dependencies, IViewModelGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IViewModelGenerator Generator { get; } 

        public void Save(ViewModelModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ViewModelModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ViewModelModel();
            foreach(var kvp in options.Document.Components.Schemas)
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.ViewModel(options.RootNamespace));
                var path = Dependencies.PathHelper.ViewModel(options.OutputDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
