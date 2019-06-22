// -----------------------------------------------------------------------
// <copyright file="ConfigurationGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public class ConfigurationGenerator : AbstractGenerator, IConfigurationGenerator
    {
        public ConfigurationGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            string entityNamespace = Dependencies.Namespace.Entity(Dependencies.Document.Options.RootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine($"using {entityNamespace};");
            WriteLine("using Microsoft.EntityFrameworkCore;");
            WriteLine("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string className = Dependencies.Namer.Configuration(name);
                WriteLine($"public partial class {className} : IEntityTypeConfiguration<{name}>");
                using (OpenBlock())
                {
                    WriteLine($"public void Configure(EntityTypeBuilder<{name}> builder)");
                    using (OpenBlock())
                    {
                        var properties = schema.GetAllPropertiesRecursive();
                        foreach (var kvp in properties)
                        {
                            if (!kvp.Value.IsArrayOrObject())
                            {
                                Write($"builder.Property(x => x.{kvp.Key})");
                                WriteLine(";");
                            }
                        }
                    }
                }
            }

            return GetText();
        }
    }
}
