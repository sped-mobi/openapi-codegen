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
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public class RepositoryScaffolder : AbstractScaffolder, IRepositoryScaffolder
    {
        public RepositoryScaffolder(ScaffolderDependencies dependencies, IRepositoryGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IRepositoryGenerator Generator { get; } 

        public void Save(RepositoryModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public RepositoryModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new RepositoryModel();
            foreach(var kvp in options.Document.Components.Schemas)
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Repository(options.RootNamespace));
                var path = Dependencies.PathHelper.Repository(options.OutputDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
