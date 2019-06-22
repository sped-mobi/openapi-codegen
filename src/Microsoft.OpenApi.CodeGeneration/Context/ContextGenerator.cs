// -----------------------------------------------------------------------
// <copyright file="ContextGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public class ContextGenerator : AbstractGenerator, IContextGenerator
    {
        public ContextGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteClassCode(IOpenApiDocument document, OpenApiOptions options)
        {
            string @namespace = Dependencies.Namespace.Context(options.RootNamespace);
            string configurationNamespace = Dependencies.Namespace.Configuration(options.RootNamespace);
            string entityNamespace = Dependencies.Namespace.Entity(options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Linq;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using Microsoft.EntityFrameworkCore;");
            WriteLine("using Microsoft.EntityFrameworkCore.Query;");
            WriteLine($"using {entityNamespace};");
            WriteLine($"using {configurationNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                WriteLine($"public partial class {options.ContextClassName} : DbContext");
                using (OpenBlock())
                {
                    WriteLine();
                    WriteLine("public static long InstanceCount;");
                    IDictionary<string, OpenApiSchema> schemas = document.GetSchemas();
                    foreach (var kvp in schemas)
                    {
                        string name = kvp.Key;
                        OpenApiSchema schema = kvp.Value;
                        string pluralName = Dependencies.Pluralizer.Pluralize(name);
                        WriteLine();
                        WriteLine(
                            $@"private static readonly Func<{options.ContextClassName}, AsyncEnumerable<{name}>> _queryGetAll{pluralName} =");
                        PushIndent();
                        WriteLine($"EF.CompileAsyncQuery(({options.ContextClassName} db) => db.{name});");
                        PopIndent();
                        WriteLine();
                        WriteLine(
                            $"private static readonly Func<{options.ContextClassName}, {options.PrimaryKeyTypeName}, AsyncEnumerable<{name}>> _queryGet{name} =");
                        PushIndent();
                        WriteLine($"EF.CompileAsyncQuery(({options.ContextClassName} db, {options.PrimaryKeyTypeName} id) =>");
                        PushIndent();
                        WriteLine($"db.{name}.Where( x => x.{name}Id == id));");
                        PopIndent();
                        PopIndent();
                    }

                    WriteLine();
                    foreach (var kvp in schemas)
                    {
                        string name = kvp.Key;
                        OpenApiSchema schema = kvp.Value;
                        WriteLine($@"public virtual DbSet<{name}> {name} {{ get; set; }}");
                    }

                    WriteLine();
                    WriteLine($"public {options.ContextClassName}(DbContextOptions options) : base(options) ");
                    PushIndent();
                    WriteLine("=> Interlocked.Increment(ref InstanceCount);");
                    PopIndent();
                    WriteLine();
                    WriteLine("protected override void OnModelCreating(ModelBuilder modelBuilder)");
                    using (OpenBlock())
                    {
                        foreach (var kvp in schemas)
                        {
                            string name = kvp.Key;
                            OpenApiSchema schema = kvp.Value;
                            string configurationName = Dependencies.Namer.Configuration(name);
                            WriteLine($@"new {configurationName}().Configure(modelBuilder.Entity<{name}>());");
                        }
                    }

                    foreach (var kvp in schemas)
                    {
                        string name = kvp.Key;
                        OpenApiSchema schema = kvp.Value;
                        string pluralName = Dependencies.Pluralizer.Pluralize(name);
                        WriteLine();
                        WriteLine(
                            $@"public async Task<List<{name}>> GetAll{pluralName}Async() => await _queryGetAll{pluralName}(this).ToListAsync();");
                        WriteLine();
                        WriteLine(
                            $"public async Task<List<{name}>> Get{name}Async({options.PrimaryKeyTypeName} id) => await _queryGet{name}(this, id).ToListAsync();");
                    }
                }
            }

            return GetText();
        }

        public string WriteInterfaceCode(IOpenApiDocument document, OpenApiOptions options)
        {
            string @namespace = Dependencies.Namespace.Context(options.RootNamespace);
            string entityNamespace = Dependencies.Namespace.Entity(options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Threading;");
            WriteLine("using Microsoft.EntityFrameworkCore;");
            WriteLine($"using {entityNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                WriteLine($"public interface {options.ContextInterfaceName}");
                using (OpenBlock())
                {
                    foreach (var kvp in document.Components.Schemas)
                    {
                        string name = kvp.Key;
                        OpenApiSchema schema = kvp.Value;
                        WriteLine();
                        WriteLine($@"DbSet<{name}> {name} {{ get; set; }}");
                    }
                }
            }

            return GetText();
        }
    }
}
