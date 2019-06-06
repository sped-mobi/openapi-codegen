namespace Microsoft.OpenApi.CodeGeneration.ViewModels
{
    public class ViewModelScaffolder : AbstractScaffolder, IViewModelScaffolder
    {
        public ViewModelScaffolder(ScaffolderDependencies dependencies, IViewModelGenerator generator) : base(dependencies)
        {
            Generator = generator;
        }

        protected IViewModelGenerator Generator { get; }

        public void Save(ViewModelModel model)
        {
            Dependencies.FileWriter.WriteFiles(model.Files);
        }

        public ViewModelModel ScaffoldModel(OpenApiOptions options)
        {
            var model = new ViewModelModel();
            foreach (var kvp in options.Document.GetSchemas())
            {
                var name = kvp.Key;
                var schema = kvp.Value;
                var code = Generator.WriteCode(schema, name, Dependencies.Namespace.ViewModel(options.RootNamespace));
                var path = Dependencies.PathHelper.ViewModel(options.CoreProjectDir, name);
                var file = new ScaffoldedFile { Code = code, Path = path };
                model.Files.Add(file);
            }
            return model;
        }
    }
}
