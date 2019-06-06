using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public class ConfigurationScaffolder : AbstractScaffolder, IConfigurationScaffolder
    {
        public ConfigurationScaffolder(ScaffolderDependencies dependencies, IConfigurationGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IConfigurationGenerator Generator { get; }

        public void Save(ConfigurationModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ConfigurationModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ConfigurationModel();
            List<ScaffoldedFile> list = new List<ScaffoldedFile>();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.Configuration(options.RootNamespace));
                var path = Dependencies.PathHelper.Configuration(options.DataProjectDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                list.Add(file);
            }

            model.Files = list;
            return model;
        }
    }
}
