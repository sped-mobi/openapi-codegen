// -----------------------------------------------------------------------
// <copyright file="DataProjectGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class DataProjectGenerator : ProjectGeneratorBase, IDataProjectGenerator
    {
        public DataProjectGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public override string WriteProjectFile(OpenApiOptions options)
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

