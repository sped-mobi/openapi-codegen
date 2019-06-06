using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Repositories
{
    public class RepositoryGenerator : AbstractGenerator, IRepositoryGenerator
    {
        public RepositoryGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteClassCode(OpenApiSchema schema, string name, OpenApiOptions options)
        {
            string repositoryNamespace = Dependencies.Namespace.Repository(options.RootNamespace);
            string entityNamespace = Dependencies.Namespace.Entity(options.RootNamespace);
            string contextNamespace = Dependencies.Namespace.Context(options.RootNamespace);

            Clear();
            GenerateFileHeader();
            WriteLine($"using {entityNamespace};");
            WriteLine($"using {contextNamespace};");
            WriteLine();
            WriteLine($"namespace {repositoryNamespace}");
            using (OpenBlock())
            {

                string className = Dependencies.Namer.Repository(name);
                string entityName = Dependencies.Namer.Entity(name);

                WriteLine($"public partial class {className} : I{className}");
                using (OpenBlock())
                {
                    WriteLine($"private readonly {options.ContextClassName} _context;");
                    WriteLine();
                    WriteLine($"public {className}({options.ContextClassName} context)");
                    using (OpenBlock())
                    {
                        WriteLine("_context = context;");
                    }

                    WriteLine();
                    WriteLine($"private async Task<bool> {entityName}Exists({options.PrimaryKeyTypeName} id, CancellationToken ct = default) =>");
                    PushIndent();
                    WriteLine($"await _context.{entityName}.AnyAsync(a => a.{entityName}Id == id, ct);");
                    PopIndent();

                    WriteLine();
                    WriteLine($"public async Task<List<{entityName}>> GetAllAsync(CancellationToken ct = default) => await _context.{entityName}.ToListAsync(ct);");

                    WriteLine();
                    WriteLine($"public async Task<{entityName}> GetByIdAsync({options.PrimaryKeyTypeName} id, CancellationToken ct = default) => await _context.{entityName}.FindAsync(id);");

                    WriteLine();
                    WriteLine($"public async Task<{entityName}> AddAsync({entityName} entity, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine($"_context.{entityName}.Add(entity);");
                        WriteLine("await _context.SaveChangesAsync(ct);");
                        WriteLine("return entity;");
                    }

                    WriteLine();
                    WriteLine($"public async Task<bool> UpdateAsync({entityName} entity, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine($"if (!await {entityName}Exists(entity.{entityName}Id, ct))");
                        PushIndent();
                        WriteLine("return false;");
                        PopIndent();
                        WriteLine($"_context.{entityName}.Update(entity);");
                        WriteLine("await _context.SaveChangesAsync(ct);");
                        WriteLine("return true;");
                    }

                    WriteLine();
                    WriteLine($"public async Task<bool> DeleteAsync({options.PrimaryKeyTypeName} id, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine($"if (!await {entityName}Exists(entity.{entityName}Id, ct))");
                        PushIndent();
                        WriteLine("return false;");
                        PopIndent();
                        WriteLine($"var remove = _context.{entityName}.Find(id);");
                        WriteLine($"_context.{entityName}.Remove(remove);");
                        WriteLine("await _context.SaveChangesAsync(ct);");
                        WriteLine("return true;");
                    }

                    WriteLine();
                    WriteLine("public void Dispose() => _context.Dispose();");
                }
            }
            return GetText();
        }

    }
}
