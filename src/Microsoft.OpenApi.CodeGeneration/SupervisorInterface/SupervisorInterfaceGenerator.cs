// -----------------------------------------------------------------------
// <copyright file="SupervisorInterfaceGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.SupervisorInterface
{
    public class SupervisorInterfaceGenerator : AbstractGenerator, ISupervisorInterfaceGenerator
    {
        public SupervisorInterfaceGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteCode(OpenApiOptions options)
        {
            var document = options.Document;

            string supervisorNamespace = Dependencies.Namespace.Supervisor(options.RootNamespace);
            string viewModelNamespace = Dependencies.Namespace.ViewModel(options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine($"using {viewModelNamespace};");
            WriteLine();
            WriteLine($"namespace {supervisorNamespace}");
            using (OpenBlock())
            {
                WriteLine($"public interface {options.SupervisorInterfaceName}");
                using (OpenBlock())
                {
                    foreach (var kvp in document.GetSchemas())
                    {
                        string name = kvp.Key;
                        string entityName = Dependencies.Namer.Entity(kvp.Key);
                        string viewModelName = Dependencies.Namer.ViewModel(kvp.Key);
                        string primaryKeyTypeName = document.Options.PrimaryKeyTypeName;

                        WriteLine();
                        WriteLine($"// {name}");
                        WriteLine();
                        WriteLine($"Task<List<{viewModelName}>> GetAll{name}Async(CancellationToken ct = default(CancellationToken));");
                        WriteLine($"Task<{viewModelName}> Get{name}ByIdAsync({primaryKeyTypeName} id, CancellationToken ct = default(CancellationToken));");
                        WriteLine($"Task<{viewModelName}> Add{name}Async({viewModelName} input, CancellationToken ct = default(CancellationToken));");
                        WriteLine($"Task<bool> Update{name}Async({viewModelName} input, CancellationToken ct = default(CancellationToken));");
                        WriteLine($"Task<bool> Delete{name}Async({primaryKeyTypeName} id, CancellationToken ct = default(CancellationToken));");

                    }
                }
            }

            return GetText();
        }
    }
}
