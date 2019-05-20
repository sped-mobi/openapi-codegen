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
    public interface IViewModelGenerator
    {
        string WriteCode(OpenApiSchema schema, string name, string @namespace);
    }
}
