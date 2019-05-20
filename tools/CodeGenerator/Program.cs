using System.Collections.Generic;
using Microsoft.OpenApi.CodeGeneration.Internal;

namespace CodeGenerator
{
    public static class Program
    {
        public static void Main()
        {
            CodeGenerationOptions options = new CodeGenerationOptions
            {
                OutputDir = @"C:\stage\github\openapi-codegen\src\Microsoft.OpenApi.CodeGeneration",
                RootNamespace = "Microsoft.OpenApi.CodeGeneration",
                Kinds = new List<Kind>
                {
                    Kind.Configuration,
                    Kind.Controller,
                    Kind.Converter,
                    Kind.Entity,
                    Kind.Repository,
                    Kind.ViewModel,
                    Kind.Context,
                    Kind.Supervisor,
                }
            };

            GeneratedModel model = ModelFactory.Create(options);

            ModelSaver.SaveModel(model);
        }
    }
}
