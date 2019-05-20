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
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public class EntityScaffolder : AbstractScaffolder, IEntityScaffolder
    {
        public EntityScaffolder(ScaffolderDependencies dependencies, IEntityGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IEntityGenerator Generator { get; } 

        public void Save(EntityModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public EntityModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new EntityModel();
            foreach(var kvp in options.Document.Components.Schemas)
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Entity(options.RootNamespace));
                var path = Dependencies.PathHelper.Entity(options.OutputDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
