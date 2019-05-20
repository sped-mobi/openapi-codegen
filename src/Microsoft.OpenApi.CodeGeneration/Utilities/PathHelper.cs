using System.IO;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class PathHelper : IPathHelper
    {
        public string Configuration(string outputDir, string name) => Path.Combine(outputDir, "Configurations", name + ".cs");
        public string Controller(string outputDir, string name) => Path.Combine(outputDir, "Controllers", name + ".cs");
        public string Converter(string outputDir, string name) => Path.Combine(outputDir, "Converters", name + ".cs");
        public string Entity(string outputDir, string name) => Path.Combine(outputDir, "Entities", name + ".cs");
        public string Repository(string outputDir, string name) => Path.Combine(outputDir, "Repositories", name + ".cs");
        public string ViewModel(string outputDir, string name) => Path.Combine(outputDir, "ViewModels", name + ".cs");
        public string Supervisor(string outputDir, string name) => Path.Combine(outputDir, "Supervisor", name + ".cs");
        public string Context(string outputDir, string name) => Path.Combine(outputDir, "Context", name + ".cs");
    }
}
