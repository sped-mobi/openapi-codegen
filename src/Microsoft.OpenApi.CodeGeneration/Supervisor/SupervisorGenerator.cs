using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Supervisor
{
    public class SupervisorGenerator : AbstractGenerator, ISupervisorGenerator
    {
        public SupervisorGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }
        public string WriteClassCode(IOpenApiDocument document, string supervisorName, string supervisorInterfaceName, string @namespace)
        {
            string repositoryNamespace = Dependencies.Namespace.Repository(document.Options.RootNamespace);

            Clear();
            GenerateFileHeader();
            WriteLine($"using {repositoryNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                WriteLine($"public partial class {supervisorName} : {supervisorInterfaceName}");
                using (OpenBlock())
                {
                    IDictionary<string, OpenApiSchema> schemas = document.GetSchemas();

                    foreach (var kvp in schemas)
                    {
                        string name = kvp.Key;
                        OpenApiSchema schema = kvp.Value;
                        string repositoryInterface = Dependencies.Namer.RepositoryInterface(name);
                        string fieldName = Dependencies.Namer.RepositoryFieldName(name);

                        WriteLine($"private readonly {repositoryInterface} {fieldName};");
                    }

                    WriteLine();
                    Write($"public {supervisorName}(");
                    PushIndent();

                    var list = schemas.ToList();

                    for (int i = 0; i < list.Count; i++)
                    {
                        var kvp = list[i];
                        string name = kvp.Key;
                        string repositoryInterface = Dependencies.Namer.RepositoryInterface(name);
                        string parameterName = Dependencies.Namer.RepositoryParameterName(name);

                        Write($"{repositoryInterface} {parameterName}");
                        WriteLine(i != list.Count - 1 ? "," : ")");
                    }
                    PopIndent();
                    using (OpenBlock())
                    {
                        foreach (var kvp in schemas)
                        {
                            string name = kvp.Key;
                            string parameterName = Dependencies.Namer.RepositoryParameterName(name);
                            string fieldName = Dependencies.Namer.RepositoryFieldName(name);
                            WriteLine($"{fieldName} = {parameterName};");
                        }
                    }

                }
            }
            return GetText();
        }

        public string WriteInterfaceCode(IOpenApiDocument document, string supervisorInterfaceName, string @namespace)
        {
            Clear();
            GenerateFileHeader();
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                WriteLine($"public partial interface {supervisorInterfaceName}");
                using (OpenBlock())
                {

                }
            }
            return GetText();
        }
    }
}
