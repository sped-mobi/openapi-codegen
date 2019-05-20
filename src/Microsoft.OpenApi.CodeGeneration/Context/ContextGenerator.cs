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
    public class ContextGenerator : AbstractGenerator, IContextGenerator
    {
        public ContextGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }
        public string WriteClassCode(OpenApiDocument document, string @namespace)
        {
            Clear();
            GenerateFileHeader();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
            }
            return GetText();
        }

        public string WriteInterfaceCode(OpenApiDocument document, string @namespace)
        {
            Clear();
            GenerateFileHeader();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
            }
            return GetText();
        }
    }
}
