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
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public class ContextScaffolder : AbstractScaffolder, IContextScaffolder
    {
        public ContextScaffolder(ScaffolderDependencies dependencies, IContextGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IContextGenerator Generator { get; } 

        public void Save(ContextModel model)
        {
            Dependencies.FileWriter.WriteFile(model.Class);
            Dependencies.FileWriter.WriteFile(model.Interface);
        }

        public ContextModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ContextModel();
            var classFile = new ScaffoldedFile();
            var interfaceFile = new ScaffoldedFile();
            classFile.Code = Generator.WriteClassCode(options.Document, options.RootNamespace);
            classFile.Path = Dependencies.PathHelper.Context(options.OutputDir, options.ContextClassName);
            interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options.RootNamespace);
            interfaceFile.Path = Dependencies.PathHelper.Context(options.OutputDir, options.ContextInterfaceName);
            model.Class = classFile;
            model.Interface = interfaceFile;
            return model;
        }
    }
}
