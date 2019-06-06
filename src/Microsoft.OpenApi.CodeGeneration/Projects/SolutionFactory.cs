// -----------------------------------------------------------------------
// <copyright file="SolutionFactory.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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

        public void CreateSolution(bool openInExplorer = false)
        {

            Directory.CreateDirectory(Options.SolutionDir);
            Directory.CreateDirectory(Options.ApiProjectDir);
            Directory.CreateDirectory(Options.CoreProjectDir);
            Directory.CreateDirectory(Options.DataProjectDir);

            var model = Solution.ScaffoldModel(Document.Options);
            Solution.Save(model);
            if (openInExplorer)
            {
                System.Diagnostics.Process.Start("explorer.exe", Document.Options.SolutionDir);
            }
        }
    }
}
