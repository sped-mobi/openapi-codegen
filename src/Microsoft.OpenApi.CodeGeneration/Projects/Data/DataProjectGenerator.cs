// -----------------------------------------------------------------------
// <copyright file="DataProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
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
                    WriteLine("<PackageReference Include=\"Microsoft.EntityFrameworkCore.Design\" Version=\"2.2.4\" />");
                    WriteLine("<PackageReference Include=\"Microsoft.EntityFrameworkCore.Abstractions\" Version=\"2.2.4\" />");
                    WriteLine("<PackageReference Include=\"Microsoft.EntityFrameworkCore.SqlServer\" Version=\"2.2.3\" />");
                    WriteLine("<PackageReference Include=\"Microsoft.Extensions.Logging\" Version=\"2.2.0\" />");
                }

                using (OpenItemGroupBlock())
                {
                    WriteLine($"<ProjectReference Include=\"..\\{options.CoreProjectName}\\{options.CoreProjectName}.csproj\"/>");
                }
            }

            return GetText();
        }
    }
}
