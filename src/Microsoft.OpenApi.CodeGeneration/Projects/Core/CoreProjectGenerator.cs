// -----------------------------------------------------------------------
// <copyright file="CoreProjectGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class CoreProjectGenerator : ProjectGeneratorBase, ICoreProjectGenerator
    {
        public CoreProjectGenerator(GeneratorDependencies dependencies) : base(dependencies)
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

            }
            return GetText();
        }
    }
}

