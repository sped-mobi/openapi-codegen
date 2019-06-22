// -----------------------------------------------------------------------
// <copyright file="SupervisorGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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
            string entityNamespace = Dependencies.Namespace.Entity(document.Options.RootNamespace);
            string converterNamespace = Dependencies.Namespace.Converter(document.Options.RootNamespace);
            string viewModelNamespace = Dependencies.Namespace.ViewModel(document.Options.RootNamespace);
            string repositoryNamespace = Dependencies.Namespace.Repository(document.Options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine($"using {entityNamespace};");
            WriteLine($"using {converterNamespace};");
            WriteLine($"using {viewModelNamespace};");
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


                    foreach (var kvp in schemas)
                    {
                        Process(kvp.Key, kvp.Value);
                    }
                }
            }

            return GetText();
        }

        private void Process(string name, OpenApiSchema schema)
        {
            var primaryKeyTypeName = Dependencies.Document.Options.PrimaryKeyTypeName;
            string viewModelName = Dependencies.Namer.ViewModel(name);
            string converterName = Dependencies.Namer.Converter(name);
            string repositoryFieldName = Dependencies.Namer.RepositoryFieldName(name);

            WriteLine();
            WriteLine($"public async Task<List<{viewModelName}>> GetAll{name}Async(CancellationToken ct = default(CancellationToken))");
            using (OpenBlock())
            {
                WriteLine($"var entities = {converterName}.ConvertList(await {repositoryFieldName}.GetAllAsync(ct));");
                WriteLine($"return entities;");
            }

            WriteLine();
            WriteLine($"public async Task<{viewModelName}> Get{name}ByIdAsync({primaryKeyTypeName} id, CancellationToken ct = default(CancellationToken))");
            using (OpenBlock())
            {
                WriteLine($"var viewModel = {converterName}.Convert(await {repositoryFieldName}.GetByIdAsync(id, ct));");
                WriteLine($"return viewModel;");
            }


            WriteLine();
            WriteLine($"public async Task<{viewModelName}> Add{name}Async({viewModelName} input, CancellationToken ct = default(CancellationToken))");
            using (OpenBlock())
            {
                WriteLine($"var entity = new {name}();");
                foreach (var kvp in schema.GetSimpleProperties())
                {
                    WriteLine($"entity.{kvp.Key} = input.{kvp.Key};");
                }

                WriteLine($"entity = await {repositoryFieldName}.AddAsync(entity, ct);");
                WriteLine($"input.{name}Id = entity.{name}Id;");
                WriteLine($"return input;");

            }

            WriteLine();
            WriteLine($"public async Task<bool> Update{name}Async({viewModelName} input, CancellationToken ct = default(CancellationToken))");
            using (OpenBlock())
            {
                WriteLine($"var entity = await {repositoryFieldName}.GetByIdAsync(input.{name}Id, ct);");
                WriteLine($"if (entity == null) return false;");
                foreach (var kvp in schema.GetSimpleProperties())
                {
                    WriteLine($"entity.{kvp.Key} = input.{kvp.Key};");
                }
                WriteLine($"return await {repositoryFieldName}.UpdateAsync(entity, ct);");
            }

            WriteLine();
            WriteLine($"public async Task<bool> Delete{name}Async({primaryKeyTypeName} id, CancellationToken ct = default(CancellationToken))");
            using (OpenBlock())
            {
                WriteLine($"return await {repositoryFieldName}.DeleteAsync(id, ct);");
            }
        }


        public string WriteInterfaceCode(IOpenApiDocument document, string supervisorInterfaceName, string @namespace)
        {
            string viewModelNamespace = Dependencies.Namespace.ViewModel(document.Options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine($"using {viewModelNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                WriteLine($"public interface {supervisorInterfaceName}");
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
