using System;
using Microsoft.OpenApi.CodeGeneration;
using Microsoft.OpenApi.CodeGeneration.Projects;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;

namespace CodeGenerator
{
    public static class Program
    {


        public static void Main()
        {
            Console.WriteLine("Starting...");

            ISolutionFactory factory = OpenApiServices.GetService<ISolutionFactory>();
            factory.CreateSolution(PostAction.OpenInExplorer);

            Console.WriteLine("Complete!");

        }

        //private static void Process2()
        //{
        //    OpenApiOptions options = OpenApiUtilities.GetOptions("Guid", OutDir, RootNamespace, ContextName, SupervisorName, FilePath);
        //    IEntityModelBuilder builder = OpenApiServices.GetService<IEntityModelBuilder>();

        //    var model = builder.BuildModel(options);

        //    var entities = model.GetEntities();
        //}

        private static void Process()
        {
            IScaffoldingManager manager = OpenApiServices.GetService<IScaffoldingManager>();
            IOpenApiDocument document = OpenApiServices.GetService<IOpenApiDocument>();
            ScaffoldedModel model = manager.ScaffoldModel(document.Options);
            manager.SaveModel(model);
            System.Diagnostics.Process.Start("explorer.exe", document.Options.OutputDir);
        }



        //private static void Generate()
        //{
        //    CodeGenerationOptions options = new CodeGenerationOptions
        //    {
        //        OutputDir = @"C:\stage\github\openapi-codegen\src\Microsoft.OpenApi.CodeGeneration",
        //        RootNamespace = "Microsoft.OpenApi.CodeGeneration",
        //        Kinds = new List<Kind>
        //        {
        //            Kind.Configuration,
        //            Kind.Controller,
        //            Kind.Converter,
        //            Kind.Entity,
        //            Kind.Repository,
        //            Kind.ViewModel,
        //            Kind.Context,
        //            Kind.Supervisor,
        //        }
        //    };

        //    GeneratedModel model = ModelFactory.Create(options);

        //    ModelSaver.SaveModel(model);
        //}
    }

}
