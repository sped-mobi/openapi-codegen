// -----------------------------------------------------------------------
// <copyright file="RepositoryScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Repository
{
    public class RepositoryScaffolder : AbstractScaffolder, IRepositoryScaffolder
    {
        public RepositoryScaffolder(ScaffolderDependencies dependencies, IRepositoryGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IRepositoryGenerator Generator { get; }

        public void Save(RepositoryModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public RepositoryModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new RepositoryModel();
            var list = new List<ScaffoldedFile>();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                ProcessClassFile(options, list, name, schema);
            }

            model.Files = list;
            return model;
        }

        private void ProcessClassFile(OpenApiOptions options, List<ScaffoldedFile> list, string name, OpenApiSchema schema)
        {
            var classCode = Generator.WriteClassCode(schema, name, options);
            var classPath = Dependencies.PathHelper.Repository(options.DataProjectDir, name);
            var classFile = new ScaffoldedFile {Code = classCode, Path = classPath};
            list.Add(classFile);
        }
    }
}
