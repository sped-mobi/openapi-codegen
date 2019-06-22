// -----------------------------------------------------------------------
// <copyright file="RepositoryInterfaceGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.RepositoryInterface
{
    public class RepositoryInterfaceGenerator : AbstractGenerator, IRepositoryInterfaceGenerator
    {
        public RepositoryInterfaceGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteInterfaceCode(OpenApiSchema schema, string name, OpenApiOptions options)
        {
            string repositoryNamespace = Dependencies.Namespace.Repository(options.RootNamespace);
            string entityNamespace = Dependencies.Namespace.Entity(options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine($"using {entityNamespace};");
            WriteLine();
            WriteLine($"namespace {repositoryNamespace}");
            using (OpenBlock())
            {
                string className = Dependencies.Namer.Repository(name);
                string entityName = Dependencies.Namer.Entity(name);
                WriteLine($"public interface I{className} : IDisposable");
                using (OpenBlock())
                {
                    WriteLine();
                    WriteLine($"Task<List<{entityName}>> GetAllAsync(CancellationToken ct = default);");
                    WriteLine();
                    WriteLine($"Task<{entityName}> GetByIdAsync({options.PrimaryKeyTypeName} id, CancellationToken ct = default);");
                    WriteLine();
                    WriteLine($"Task<{entityName}> AddAsync({entityName} entity, CancellationToken ct = default);");
                    WriteLine();
                    WriteLine($"Task<bool> UpdateAsync({entityName} entity, CancellationToken ct = default);");
                    WriteLine();
                    WriteLine($"Task<bool> DeleteAsync({options.PrimaryKeyTypeName} id, CancellationToken ct = default);");
                }
            }

            return GetText();
        }
    }
}
