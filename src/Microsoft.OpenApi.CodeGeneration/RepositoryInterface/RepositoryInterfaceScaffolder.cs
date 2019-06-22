// -----------------------------------------------------------------------
// <copyright file="RepositoryInterfaceScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.RepositoryInterface
{
    public class RepositoryInterfaceScaffolder : AbstractScaffolder, IRepositoryInterfaceScaffolder
    {
        public RepositoryInterfaceScaffolder(ScaffolderDependencies dependencies, IRepositoryInterfaceGenerator generator) :
            base(dependencies)
        {
            Generator = generator;
        }

        protected IRepositoryInterfaceGenerator Generator { get; }

        public void Save(RepositoryInterfaceModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public RepositoryInterfaceModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new RepositoryInterfaceModel();
            List<ScaffoldedFile> list = new List<ScaffoldedFile>();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                ProcessInterfaceFile(options, list, name, schema);
            }

            model.Files = list;
            return model;
        }

        private void ProcessInterfaceFile(OpenApiOptions options, List<ScaffoldedFile> list, string name, OpenApiSchema schema)
        {
            var interfaceCode = Generator.WriteInterfaceCode(schema, name, options);
            var interfacePath = Dependencies.PathHelper.RepositoryInterface(options.CoreProjectDir, name);
            var interfaceFile = new ScaffoldedFile {Code = interfaceCode, Path = interfacePath};
            list.Add(interfaceFile);
        }
    }
}
