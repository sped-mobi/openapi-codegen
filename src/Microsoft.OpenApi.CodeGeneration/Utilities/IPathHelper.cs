namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IPathHelper
    {
        string Configuration(string projectDir, string name);
        string Controller(string projectDir, string name);
        string Converter(string projectDir, string name);
        string Entity(string projectDir, string name);
        string Repository(string projectDir, string name);
        string RepositoryInterface(string projectDir, string name);
        string ViewModel(string projectDir, string name);
        string Supervisor(string projectDir, string name);
        string SupervisorInterface(string projectDir, string name);
        string Context(string projectDir, string name);



    }
}
