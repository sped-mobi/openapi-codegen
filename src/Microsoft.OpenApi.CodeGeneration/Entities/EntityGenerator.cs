using System;
using System.Collections.Generic;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public class EntityGenerator : AbstractGenerator, IEntityGenerator
    {
        public EntityGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }
        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            Clear();
            GenerateFileHeader();
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string className = Dependencies.Namer.Entity(name);
                WriteLine($"public partial class {className}");
                using (OpenBlock())
                {
                    WriteLine($"public {className}()");
                    IDictionary<string, OpenApiSchema> properties = schema.GetAllPropertiesRecursive();

                    using (OpenBlock())
                    {
                        foreach (var kvp in properties)
                        {
                            string n = StringUtilities.MakePascal(kvp.Key);
                            string t = Dependencies.Schema.ConvertToType(kvp.Value);

                            if (t.StartsWith("ICollection", StringComparison.CurrentCultureIgnoreCase))
                            {
                                t = t.Replace("ICollection", string.Empty);
                                t = t.TrimStart('<');
                                t = t.TrimEnd('>');
                                WriteLine($"{n} = new HashSet<{t}>();");
                            }
                        }
                    }

                    foreach (var kvp in properties)
                    {
                        string n = StringUtilities.MakePascal(kvp.Key);
                        string t = Dependencies.Schema.ConvertToType(kvp.Value);

                        WriteLine();
                        WriteLine($"public {t} {n} {{ get; set; }}");
                    }
                }
            }
            return GetText();
        }
    }
}
