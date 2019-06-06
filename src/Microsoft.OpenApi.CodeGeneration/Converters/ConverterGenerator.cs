using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Converters
{
    public class ConverterGenerator : AbstractGenerator, IConverterGenerator
    {
        public ConverterGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }
        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            string rootNamespace = Dependencies.Document.Options.RootNamespace;
            string entityNamespace = Dependencies.Namespace.Entity(rootNamespace);
            string viewModelNamespace = Dependencies.Namespace.ViewModel(rootNamespace);

            Clear();
            GenerateFileHeader();
            WriteLine($"using {entityNamespace};");
            WriteLine($"using {viewModelNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string converterName = Dependencies.Namer.Converter(name);
                string entityName = Dependencies.Namer.Entity(name);
                string viewModelName = Dependencies.Namer.ViewModel(name);

                WriteLine($"public static class {converterName}");
                using (OpenBlock())
                {
                    WriteLine($"public static {viewModelName} Convert({entityName} entity)");
                    using (OpenBlock())
                    {
                        WriteLine($"return new {viewModelName}");
                        using (OpenBlockSemicolon())
                        {
                            var properties = schema.GetAllPropertiesRecursive();
                            foreach (var property in properties)
                            {
                                WriteLine($"{property.Key} = entity.{property.Key},");
                            }
                        }
                    }

                    WriteLine();
                    WriteLine($"public static List<{viewModelName}> ConvertList(IEnumerable<{entityName}> entities)");
                    using (OpenBlock())
                    {
                        WriteLine($"return entities.Select(entity =>");
                        using (OpenBlockString(").ToList();"))
                        {
                            var properties = schema.GetAllPropertiesRecursive();
                            foreach (var property in properties)
                            {
                                WriteLine($"{property.Key} = entity.{property.Key},");
                            }
                        }
                    }
                }
            }
            return GetText();
        }
    }
}
