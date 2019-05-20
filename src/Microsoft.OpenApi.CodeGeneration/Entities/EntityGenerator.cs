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
    public class EntityGenerator : AbstractGenerator, IEntityGenerator
    {
        public EntityGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }
        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
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
