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
using Microsoft.OpenApi.CodeGeneration.Context;

namespace Microsoft.OpenApi.CodeGeneration.Supervisor
{
    public class SupervisorScaffolder : AbstractScaffolder, ISupervisorScaffolder
    {
        public SupervisorScaffolder(ScaffolderDependencies dependencies, ISupervisorGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected ISupervisorGenerator Generator { get; } 

        public void Save(SupervisorModel model)
        {
            Dependencies.FileWriter.WriteFile(model.Class);
            Dependencies.FileWriter.WriteFile(model.Interface);
        }

        public SupervisorModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new SupervisorModel();
            var classFile = new ScaffoldedFile();
            var interfaceFile = new ScaffoldedFile();
            classFile.Code = Generator.WriteClassCode(options.Document, options.RootNamespace);
            classFile.Path = Dependencies.PathHelper.Supervisor(options.OutputDir, options.SupervisorClassName);
            interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options.RootNamespace);
            interfaceFile.Path = Dependencies.PathHelper.Supervisor(options.OutputDir, options.SupervisorInterfaceName);
            model.Class = classFile;
            model.Interface = interfaceFile;
            return model;
        }
    }
}
