namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public class NamespaceHelper : INamespaceHelper
    {
        public string Configuration(string rootNamespace) => $"{rootNamespace}.Configurations";
        public string Controller(string rootNamespace) => $"{rootNamespace}.Controllers";
        public string Converter(string rootNamespace) => $"{rootNamespace}.Converters";
        public string Entity(string rootNamespace) => $"{rootNamespace}.Entities";
        public string Repository(string rootNamespace) => $"{rootNamespace}.Repositories";
        public string ViewModel(string rootNamespace) => $"{rootNamespace}.ViewModels";
        public string Supervisor(string rootNamespace) => $"{rootNamespace}.Supervisor";
        public string Context(string rootNamespace) => $"{rootNamespace}.Context";
    }
}
