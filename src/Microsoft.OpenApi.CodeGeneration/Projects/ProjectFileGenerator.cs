// -----------------------------------------------------------------------
// <copyright file="IProjectFileGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{


    public abstract class ProjectFileGenerator : AbstractGenerator
    {
        public ProjectFileGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public abstract string WriteProjectFile(OpenApiOptions options);

        protected void WriteSdkElement(string name)
        {
            WriteLine($"<Sdk Name=\"{name}\" />");
        }

        protected void WriteProperty(string name, string value)
        {
            WriteLine($"<{name}>{value}</{name}>");
        }

        protected void WriteItem(string name, string include)
        {
            WriteLine($"<{name} Include=\"{include}\" />");
        }

        public string WriteSolutionFile(OpenApiOptions options)
        {
            Clear();
            return GetText();
        }

        public string WriteApiProjectFile(OpenApiOptions options)
        {
            Clear();
            using (OpenProjectBlock())
            {
                using (OpenPropertyGroupBlock())
                {
                    WriteProperty("RootNamespace", options.RootNamespace);
                    WriteProperty("TargetFramework", "netcoreapp2.2");
                }
                WriteLine();
                WriteSdkElement("Microsoft.NET.Sdk");
                WriteLine();
                using (OpenItemGroupBlock())
                {
                    WriteLine($"<ProjectReference Include=\"..\\{options.DataProjectName}\\{options.DataProjectName}.csproj\"/>");
                    WriteLine($"<ProjectReference Include=\"..\\{options.CoreProjectName}\\{options.CoreProjectName}.csproj\"/>");
                }
            }
            return GetText();
        }

        public string WriteCoreProjectFile(OpenApiOptions options)
        {
            Clear();
            using (OpenProjectBlock())
            {
                using (OpenPropertyGroupBlock())
                {
                    WriteProperty("RootNamespace", options.RootNamespace);
                    WriteProperty("TargetFramework", "netcoreapp2.2");
                }
                WriteLine();
                WriteSdkElement("Microsoft.NET.Sdk");
                WriteLine();

            }

            return GetText();
        }

        public string WriteDataProjectFile(OpenApiOptions options)
        {
            Clear();
            using (OpenProjectBlock())
            {
                using (OpenPropertyGroupBlock())
                {
                    WriteProperty("RootNamespace", options.RootNamespace);
                    WriteProperty("TargetFramework", "netcoreapp2.2");
                }
                WriteLine();
                WriteSdkElement("Microsoft.NET.Sdk");
                WriteLine();
                using (OpenItemGroupBlock())
                {
                    WriteLine($"<ProjectReference Include=\"..\\{options.CoreProjectName}\\{options.CoreProjectName}.csproj\"/>");
                }
            }
            return GetText();
        }
    }
}
