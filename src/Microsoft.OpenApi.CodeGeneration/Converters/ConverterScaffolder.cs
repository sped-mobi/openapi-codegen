// -----------------------------------------------------------------------
// <copyright file="ConverterScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Converters
{
    public class ConverterScaffolder : AbstractScaffolder, IConverterScaffolder
    {
        public ConverterScaffolder(ScaffolderDependencies dependencies, IConverterGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IConverterGenerator Generator { get; }

        public void Save(ConverterModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ConverterModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ConverterModel();
            List<ScaffoldedFile> list = new List<ScaffoldedFile>();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Converter(options.RootNamespace));
                var path = Dependencies.PathHelper.Converter(options.CoreProjectDir, name);
                var file = new ScaffoldedFile {Code = code, Path = path};
                list.Add(file);
            }

            model.Files = list;
            return model;
        }
    }
}
