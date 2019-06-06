using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Controllers
{
    public class ControllerScaffolder : AbstractScaffolder, IControllerScaffolder
    {
        public ControllerScaffolder(ScaffolderDependencies dependencies, IControllerGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IControllerGenerator Generator { get; }

        public void Save(ControllerModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ControllerModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ControllerModel();
            var list = new List<ScaffoldedFile>();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Controller(options.RootNamespace));
                var path = Dependencies.PathHelper.Controller(options.ApiProjectDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                list.Add(file);
            }

            model.Files = list;
            return model;
        }
    }
}
