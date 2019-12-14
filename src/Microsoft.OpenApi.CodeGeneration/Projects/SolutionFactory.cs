// -----------------------------------------------------------------------
// <copyright file="SolutionFactory.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class SolutionFactory : ISolutionFactory
    {
        private static readonly string _devEnv =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                , @"\Microsoft Visual Studio\2019\Preview\Common7\IDE");

        public SolutionFactory(IOpenApiDocument document, ISolutionScaffolder solution)
        {
            Document = document;
            Solution = solution;
        }

        public IOpenApiDocument Document { get; }

        public OpenApiOptions Options =>
            Document.Options;

        public ISolutionScaffolder Solution { get; }

        public void CreateSolution(PostAction postAction = PostAction.None)
        {
            Directory.CreateDirectory(Options.SolutionDir);
            Directory.CreateDirectory(Options.ApiProjectDir);
            Directory.CreateDirectory(Options.CoreProjectDir);
            Directory.CreateDirectory(Options.DataProjectDir);
            RepositoryCreator.CreateRepository(Options);
            var model = Solution.ScaffoldModel(Options);
            Solution.Save(model);

            switch (postAction)
            {
                case PostAction.OpenInExplorer:
                    Process.Start("explorer.exe", Document.Options.SolutionDir);
                    break;
                case PostAction.OpenInVisualStudio:
                    if (!File.Exists(_devEnv))
                    {
                        throw new Exception(string.Format("File '{0}' does not exist.", _devEnv));
                    }
                    Process.Start(_devEnv, '"' + Document.Options.SolutionFile + '"');
                    break;
            }
        }
    }
}
