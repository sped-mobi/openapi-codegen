// -----------------------------------------------------------------------
// <copyright file="SolutionGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class SolutionGenerator : ProjectGeneratorBase, ISolutionGenerator
    {
        public SolutionGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public override string WriteProjectFile(OpenApiOptions options)
        {
            Clear();
            WriteLine("# xxx");
            return GetText();
        }
    }
}

