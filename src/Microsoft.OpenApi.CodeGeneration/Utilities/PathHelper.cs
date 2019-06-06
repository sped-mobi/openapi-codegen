using System.IO;
using Microsoft.OpenApi.CodeGeneration.Utilities;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class PathHelper : IPathHelper
    {
        public PathHelper(INameHelper namer)
        {
            Namer = namer;
        }

        public INameHelper Namer { get; }

        public string Extension
        {
            get
            {
                return ".cs";
            }
        }


        public string Configuration(string projectDir, string name) => Path.Combine(projectDir, "Configurations", Namer.Configuration(name) + Extension);
        public string Controller(string projectDir, string name) => Path.Combine(projectDir, "Controllers", Namer.Controller(name) + Extension);
        public string Converter(string projectDir, string name) => Path.Combine(projectDir, "Converters", Namer.Converter(name) + Extension);
        public string Entity(string projectDir, string name) => Path.Combine(projectDir, "Entities", Namer.Entity(name) + Extension);
        public string Repository(string projectDir, string name) => Path.Combine(projectDir, "Repositories", Namer.Repository(name) + Extension);
        public string RepositoryInterface(string projectDir, string name) => Path.Combine(projectDir, "Repositories", Namer.RepositoryInterface(name) + Extension);
        public string ViewModel(string projectDir, string name) => Path.Combine(projectDir, "ViewModels", Namer.ViewModel(name) + Extension);
        public string Supervisor(string projectDir, string name) => Path.Combine(projectDir, "Supervisor", name + Extension);
        public string SupervisorInterface(string projectDir, string name) => Path.Combine(projectDir, "Supervisor", "I" + name + Extension);
        public string Context(string projectDir, string name) => Path.Combine(projectDir, name + Extension);


    }
}
