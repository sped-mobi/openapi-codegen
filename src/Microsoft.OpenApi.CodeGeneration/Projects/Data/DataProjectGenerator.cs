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
                    WriteProperty("TargetFramework", "netcoreapp3.0");
                }

                WriteLine();
                WriteSdkElement("Microsoft.NET.Sdk");
                WriteLine();
                using (OpenItemGroupBlock())
                {
                    WriteLine("<PackageReference Include=\"Microsoft.EntityFrameworkCore.Design\" Version=\"3.1.0\">");
                    WriteLine("  <PrivateAssets>all</PrivateAssets>");
                    WriteLine("  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>");
                    WriteLine("</PackageReference>");
                    WriteLine("<PackageReference Include=\"Microsoft.EntityFrameworkCore.Abstractions\" Version=\"3.1.0\" />");
                    WriteLine("<PackageReference Include=\"Microsoft.EntityFrameworkCore.SqlServer\" Version=\"3.1.0\" />");
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
