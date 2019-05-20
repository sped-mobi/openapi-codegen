namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IPathHelper
    {
        string Configuration(string outputDir, string name);
        string Controller(string outputDir, string name);
        string Converter(string outputDir, string name);
        string Entity(string outputDir, string name);
        string Repository(string outputDir, string name);
        string ViewModel(string outputDir, string name);
        string Supervisor(string outputDir, string name);
        string Context(string outputDir, string name);
    }
}
