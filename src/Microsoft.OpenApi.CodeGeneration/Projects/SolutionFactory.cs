// -----------------------------------------------------------------------
// <copyright file="SolutionFactory.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class SolutionFactory : ISolutionFactory
    {
        public SolutionFactory(IOpenApiDocument document, ISolutionScaffolder solution)
        {
            Document = document;
            Solution = solution;
        }

        public IOpenApiDocument Document { get; }

        public OpenApiOptions Options =>
            Document.Options;

        public ISolutionScaffolder Solution { get; }

        public void CreateSolution(bool open = false)
        {
            Directory.CreateDirectory(Options.SolutionDir);
            Directory.CreateDirectory(Options.ApiProjectDir);
            Directory.CreateDirectory(Options.CoreProjectDir);
            Directory.CreateDirectory(Options.DataProjectDir);
            RepositoryCreator.CreateRepository(Options);
            var model = Solution.ScaffoldModel(Options);
            Solution.Save(model);
            if (open)
            {
                Process.Start("explorer.exe", Document.Options.SolutionDir);
            }
        }
    }
}
