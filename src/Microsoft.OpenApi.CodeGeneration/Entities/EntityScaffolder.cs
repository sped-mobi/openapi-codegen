// -----------------------------------------------------------------------
// <copyright file="EntityScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public class EntityScaffolder : AbstractScaffolder, IEntityScaffolder
    {
        public EntityScaffolder(ScaffolderDependencies dependencies, IEntityGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IEntityGenerator Generator { get; }

        public void Save(EntityModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public EntityModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new EntityModel();
            var list = new List<ScaffoldedFile>();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Entity(options.RootNamespace));
                var path = Dependencies.PathHelper.Entity(options.CoreProjectDir, name);
                var file = new ScaffoldedFile {Code = code, Path = path};
                list.Add(file);
            }

            model.Files = list;
            return model;
        }
    }
}
