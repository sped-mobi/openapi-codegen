using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class ApiProjectModel
    {
        public ScaffoldedFile ProjectFile { get; set; }
        public ScaffoldedFile ProgramCSFile { get; set; }
        public ScaffoldedFile StartupCSFile { get; set; }
        public ScaffoldedFile AppSettingsJSONFile { get; set; }
        public ScaffoldedFile AppSettingsDevelopmentJSONFile { get; set; }
        public ScaffoldedFile ServicesConfigurationCSFile { get; set; }

        public IReadOnlyList<ScaffoldedFile> Controllers { get; set; }
    }
}