using System.IO;
using Microsoft.OpenApi.CodeGeneration;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi
{
    public class OpenApiOptions
    {
        public OpenApiOptions(OpenApiDocument document)
        {
            Document = new OpenApiDocumentV3(document);
        }

        public IOpenApiDocument Document { get; }


        public string DefaultRoute =>
            Document?.GetStringExtensionValue("defaultRoute");

        public string OutputDir => Document?.GetStringExtensionValue("outputDir");

        public string RootNamespace => Document?.GetStringExtensionValue("rootNamespace");

        public string SupervisorClassName => Document?.GetStringExtensionValue("supervisorName");

        public string SupervisorInterfaceName => "I" + SupervisorClassName;

        public string SupervisorParameterName => StringUtilities.MakeCamel(SupervisorClassName);

        public string SupervisorFieldName => "_" + SupervisorParameterName;

        public string ContextClassName => Document?.GetStringExtensionValue("contextName");

        public string ContextInterfaceName => "I" + ContextClassName;

        public string PrimaryKeyTypeName => Document?.GetStringExtensionValue("primaryKeyType");

        public bool AddPrimaryKeyProperties =>
            Document?.GetBoolExtensionValue("addPrimaryKeyProperties") ?? false;

        public bool AddForeignKeyProperties =>
            Document?.GetBoolExtensionValue("addForeignKeyProperties") ?? false;

        public bool AddReverseNavigationProperties =>
            Document?.GetBoolExtensionValue("addReverseNavigationProperties") ?? false;

        public string DatabaseName =>
            Document?.GetStringExtensionValue("databaseName");

        public string SolutionName
            => Document?.GetStringExtensionValue("solutionName");


        public string RootProjectName
            => Document?.GetStringExtensionValue("rootProjectName");

        public string ApiProjectName =>
            RootProjectName + ".Api";

        public string CoreProjectName =>
            RootProjectName + ".Core";

        public string DataProjectName =>
            RootProjectName + ".Data";

        public string SolutionDir =>
            Path.Combine(OutputDir, SolutionName);

        public string SolutionFile =>
            Path.Combine(SolutionDir, $"{SolutionName}.sln");

        public string ApiProjectDir =>
            Path.Combine(SolutionDir, "src", ApiProjectName);

        public string ApiProjectFile =>
            Path.Combine(ApiProjectDir, ApiProjectName + ".csproj");

        public string CoreProjectDir =>
            Path.Combine(SolutionDir, "src", CoreProjectName);

        public string CoreProjectFile =>
            Path.Combine(CoreProjectDir, CoreProjectName + ".csproj");

        public string DataProjectDir =>
            Path.Combine(SolutionDir, "src", DataProjectName);

        public string DataProjectFile =>
            Path.Combine(DataProjectDir, DataProjectName + ".csproj");




    }


}
