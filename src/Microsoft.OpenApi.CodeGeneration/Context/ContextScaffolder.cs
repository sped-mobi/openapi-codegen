// -----------------------------------------------------------------------
// <copyright file="ContextScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public class ContextScaffolder : AbstractScaffolder, IContextScaffolder
    {
        public ContextScaffolder(ScaffolderDependencies dependencies, IContextGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IContextGenerator Generator { get; }

        public void Save(ContextModel model)
        {
            Dependencies.FileWriter.WriteFile(model.Class);

            //Dependencies.FileWriter.WriteFile(model.Interface);
        }

        public ContextModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ContextModel();
            var classFile = new ScaffoldedFile();
            var interfaceFile = new ScaffoldedFile();
            classFile.Code = Generator.WriteClassCode(options.Document, options);
            classFile.Path = Dependencies.PathHelper.Context(options.DataProjectDir, options.ContextClassName);

            //interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options);
            //interfaceFile.Path = Dependencies.PathHelper.Context(options.DataProjectDir, options.ContextInterfaceName);
            model.Class = classFile;

            //model.Interface = interfaceFile;
            return model;
        }
    }
}
